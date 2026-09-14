/* logicAstroKPCharts
   Astrology Software
   Author: Nachiappan Narayanan
   *********************************************************/
/* 
   License conditions
   ------------------
   This file is part of logicAstroKPCharts.

   logicAstroKPCharts is distributed with NO WARRANTY OF ANY KIND. No author
   or distributor accepts any responsibility for the consequences of using it,
   or for whether it serves any particular purpose or works at all, unless he
   or she says so in writing.
   
   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 2 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 ***********************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace srlWebCom.Astro.AstroObjects
{
    /// <summary>
    /// Central, unit-testable implementation of the Vimshottari/KP Star-Sub-Lord
    /// mathematics used by the KP-Star-Sub-Lords lookup table. All angular
    /// arithmetic is performed in integer "sixtieths of an arc second" so
    /// boundaries below arc-second precision (1/3 and 2/3 second) are handled
    /// exactly, matching the four-field positions stored in
    /// KP-Star-Sub-Lords.txt (format DD:MM:SS:F, where F is in units of 1/60 arc sec).
    ///
    /// Model (verified against KP-Star-Sub-Lords.txt and Degree-Positions.txt):
    ///  - One Nakshatra  = 13d20m = 800 minutes = 48000 arc seconds.
    ///  - The Nakshatra of a longitude is floor(zodiacArcSeconds / 48000).
    ///  - The Star Lord of Nakshatra index n is SubLordOrder[n % 9].
    ///  - Within a Nakshatra the 9 Sub Lords rotate from the Star Lord;
    ///    a Sub Lord's length = Vimshottari Dasa Years x 48000 / 120 arc seconds.
    ///  - Within a Sub Lord the 9 Sub-Sub Lords rotate from the Sub Lord;
    ///    a Sub-Sub Lord's length = (Sub Lord length / 120) x Sub-Sub Years.
    ///  - Vimshottari Dasa Years: KE=7, VE=20, SU=6, MO=10, MA=7, RA=18, JU=16, SA=19, ME=17.
    ///
    /// The Nadi Coordinates table is computed here: each lord's final Nadi
    /// coordinates are resolved from the underlying house data the chart already
    /// produces (planet's deposited house + houses it owns; for the nodes the
    /// sign lord's deposited house + houses it owns).
    /// </summary>
    public static class NadiCalculationService
    {
        #region Constants

        public const int ARCSECONDS_PER_NAKSHATRA = 48000;      // 13*3600 + 20*60
        public const int ARCSECONDS_PER_SIGN = 108000;          // 30 * 3600
        public const int ARCSECONDS_PER_ZODIAC = 1296000;       // 360 * 3600

        public const long SIXTIETHS_PER_NAKSHATRA = 2880000;    // 48000 * 60
        public const long SIXTIETHS_PER_SIGN = 6480000;         // 108000 * 60
        public const long SIXTIETHS_PER_ZODIAC = 77760000;      // 1296000 * 60
        public const long SIXTIETHS_PER_ARC_SECOND = 60;

        /// <summary>Vimshottari order of the nine lords. Index % 9 selectors rely on this order.</summary>
        public static readonly string[] SubLordOrder = new string[] { "KE", "VE", "SU", "MO", "MA", "RA", "JU", "SA", "ME" };

        /// <summary>Vimshottari Dasa Years aligned with SubLordOrder.</summary>
        public static readonly int[] SubLordDasaYears = new int[] { 7, 20, 6, 10, 7, 18, 16, 19, 17 };

        /// <summary>Nakshatra names as used by Degree-Positions.txt, indexed 0..26 (Aswini..Revathi).</summary>
        public static readonly string[] NakshatraNames = new string[]
        {
            "ASWINI", "BHARANI", "KARTHIKAI", "ROHINI", "MRIGASHIRA", "THIRUVATHIRAI",
            "PUNARPOOSAM", "POOSAM", "AAYILYAM", "MAGAM", "POORAM", "UTHTHIRAM",
            "HASTHAM", "CHITHIRAI", "SWATHI", "VISAKAM", "ANUSHAM", "KETTAI",
            "MOOLAM", "POORADAM", "UTHIRADAM", "THIRUVONAM", "AVITTAM", "SATHAYAM",
            "POORATTATHI", "UTHIRATTATHI", "REVATHI"
        };

        #endregion

        #region Position Parsing

        /// <summary>
        /// Parses a four-field KP position of the form "DD:MM:SS:F" into a count of
        /// sixtieths of an arc second. The F field is in units of 1/60 arc second
        /// (0, 20 = 1/3", 40 = 2/3") so a value such as 00:02:43:20 is exactly
        /// 0d 2m 43.333s of arc.
        /// </summary>
        public static long ParsePositionToSixtieths(string strPosition)
        {
            if (string.IsNullOrEmpty(strPosition))
            {
                throw new ArgumentException("Empty KP position.", "strPosition");
            }

            char[] separators = new char[] { ':', '.' };
            string[] parts = strPosition.Split(separators);

            long value = 0;
            for (int i = 0; i < 4; i++)
            {
                if (i >= parts.Length)
                {
                    throw new FormatException("Invalid KP position format -> " + strPosition);
                }

                int part = 0;
                if (!int.TryParse(parts[i].Trim(), out part))
                {
                    throw new FormatException("Invalid KP position format -> " + strPosition);
                }

                value = (value * 60) + part;
            }

            return value;
        }

        /// <summary>Formats a count of sixtieths of an arc second back to "DD:MM:SS:F".</summary>
        public static string FormatSixtieths(long sixtieths)
        {
            long sec = sixtieths / 60;
            long frac = sixtieths % 60;
            long min = sec / 60;
            sec = sec % 60;
            long deg = min / 60;
            min = min % 60;

            return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", deg, min, sec, frac);
        }

        #endregion

        #region Sign Helpers

        /// <summary>
        /// True when the KP table sign group (e.g. "Ari/Leo/Sag") contains the given
        /// short sign (e.g. "Ari") as a whole token. This replaces the previous
        /// fragile string.Contains matching which could match part of a token.
        /// </summary>
        public static bool SignGroupContains(string strSignGroup, string strShortSign)
        {
            if (string.IsNullOrEmpty(strSignGroup) || string.IsNullOrEmpty(strShortSign))
            {
                return false;
            }

            foreach (string token in strSignGroup.Split('/'))
            {
                if (string.Equals(token, strShortSign, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>Zodiac sign index 0..11 for a three-letter sign token (Ari=0, Tau=1 ... Pis=11).</summary>
        public static int GetSignZodiacIndex(string strShortSign)
        {
            switch (strShortSign.Trim().ToUpper())
            {
                case "ARI": return 0;
                case "TAU": return 1;
                case "GEM": return 2;
                case "CAN": return 3;
                case "LEO": return 4;
                case "VIR": return 5;
                case "LIB": return 6;
                case "SCO": return 7;
                case "SAG": return 8;
                case "CAP": return 9;
                case "AQU": return 10;
                case "PIS": return 11;
                default: throw new ArgumentException("Unknown short sign -> " + strShortSign, "strShortSign");
            }
        }

        #endregion

        #region Vimshottari Math

        public static int GetDasaYearsOfLord(string strLord)
        {
            for (int i = 0; i < SubLordOrder.Length; i++)
            {
                if (string.Equals(SubLordOrder[i], strLord, StringComparison.OrdinalIgnoreCase))
                {
                    return SubLordDasaYears[i];
                }
            }

            return 0;
        }

        /// <summary>Index (0..8) of a lord inside SubLordOrder, or -1 when unknown.</summary>
        public static int GetSubLordOrderIndex(string strLord)
        {
            for (int i = 0; i < SubLordOrder.Length; i++)
            {
                if (string.Equals(SubLordOrder[i], strLord, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>Sub Lord length in sixtieths of an arc second = Dasa Years x 48000 / 120.</summary>
        public static long GetSubLordLengthSixtieths(string strSubLord)
        {
            return ((long)GetDasaYearsOfLord(strSubLord) * ARCSECONDS_PER_NAKSHATRA * SIXTIETHS_PER_ARC_SECOND) / 120;
        }

        /// <summary>Sub-Sub Lord length in sixtieths of an arc second = Sub Lord length / 120 x Sub-Sub Years.</summary>
        public static long GetSubSubLordLengthSixtieths(string strSubLord, string strSubSubLord)
        {
            long subLordLength = GetSubLordLengthSixtieths(strSubLord);
            return (subLordLength * GetDasaYearsOfLord(strSubSubLord)) / 120;
        }

        /// <summary>Normalizes a zodiac count of sixtieths into the range [0, 77760000).</summary>
        public static long NormalizeZodiacSixtieths(long sixtieths)
        {
            long value = sixtieths % SIXTIETHS_PER_ZODIAC;
            if (value < 0)
            {
                value += SIXTIETHS_PER_ZODIAC;
            }
            return value;
        }

        /// <summary>Nakshatra index 0..26 for a zodiac count of sixtieths.</summary>
        public static int GetNakshatraIndex(long zodiacSixtieths)
        {
            long value = NormalizeZodiacSixtieths(zodiacSixtieths);
            return (int)(value / SIXTIETHS_PER_NAKSHATRA);
        }

        /// <summary>Star Lord of a Nakshatra by index.</summary>
        public static string GetStarLord(int nakshatraIndex)
        {
            return SubLordOrder[((nakshatraIndex % SubLordOrder.Length) + SubLordOrder.Length) % SubLordOrder.Length];
        }

        public static string GetNakshatraName(int nakshatraIndex)
        {
            return NakshatraNames[((nakshatraIndex % NakshatraNames.Length) + NakshatraNames.Length) % NakshatraNames.Length];
        }

        /// <summary>Pada (1..4) within a Nakshatra: Q1 starts at the Nakshatra start.</summary>
        public static int GetNakshatraPada(long positionInNakshatraSixtieths)
        {
            return (int)((positionInNakshatraSixtieths / (SIXTIETHS_PER_NAKSHATRA / 4)) + 1);
        }

        #endregion

        #region Sub Lord / Sub Sub Lord Resolution

        /// <summary>
        /// Resolves the Sub Lord covering a position within a Nakshatra.
        /// <param name="positionInNakshatraSixtieths">0 .. 2879999</param>
        /// </summary>
        public static string GetSubLordAtPosInNakshatra(long positionInNakshatraSixtieths, int nakshatraIndex)
        {
            if (positionInNakshatraSixtieths < 0 || positionInNakshatraSixtieths >= SIXTIETHS_PER_NAKSHATRA)
            {
                throw new ArgumentOutOfRangeException("positionInNakshatraSixtieths", "position must be within 0 and one Nakshatra span");
            }

            int startIndex = ((nakshatraIndex % SubLordOrder.Length) + SubLordOrder.Length) % SubLordOrder.Length;
            long cumulative = 0;

            for (int i = 0; i < SubLordOrder.Length; i++)
            {
                int lordIndex = (startIndex + i) % SubLordOrder.Length;
                long length = GetSubLordLengthSixtieths(SubLordOrder[lordIndex]);

                if (positionInNakshatraSixtieths < cumulative + length)
                {
                    return SubLordOrder[lordIndex];
                }

                cumulative += length;
            }

            throw new ArgumentOutOfRangeException("positionInNakshatraSixtieths", "position fell outside the Nakshatra span");
        }

        /// <summary>
        /// Resolves the Sub-Sub Lord covering a position within a Nakshatra together with
        /// the absolute segment boundaries (Nakshatra-relative sixtieths) of that Sub-Sub Lord.
        /// </summary>
        public static string GetSubSubLordSegment(long positionInNakshatraSixtieths, int nakshatraIndex,
            out long segmentFromSixtieths, out long segmentToSixtieths)
        {
            int nakshatraLordIndex = ((nakshatraIndex % SubLordOrder.Length) + SubLordOrder.Length) % SubLordOrder.Length;
            long subLordStartInNakshatra = 0;
            string subLord = "";

            for (int i = 0; i < SubLordOrder.Length; i++)
            {
                int subLordIndex = (nakshatraLordIndex + i) % SubLordOrder.Length;
                long subLordLength = GetSubLordLengthSixtieths(SubLordOrder[subLordIndex]);

                if (positionInNakshatraSixtieths < subLordStartInNakshatra + subLordLength || i == SubLordOrder.Length - 1)
                {
                    subLord = SubLordOrder[subLordIndex];

                    int subLordOrderIndex = GetSubLordOrderIndex(subLord);
                    long subSubStart = subLordStartInNakshatra;

                    for (int j = 0; j < SubLordOrder.Length; j++)
                    {
                        int subSubIndex = (subLordOrderIndex + j) % SubLordOrder.Length;
                        long subSubLength = GetSubSubLordLengthSixtieths(subLord, SubLordOrder[subSubIndex]);

                        if (positionInNakshatraSixtieths < subSubStart + subSubLength || j == SubLordOrder.Length - 1)
                        {
                            segmentFromSixtieths = subSubStart;
                            segmentToSixtieths = subSubStart + subSubLength;
                            return SubLordOrder[subSubIndex];
                        }

                        subSubStart += subSubLength;
                    }

                    break;
                }

                subLordStartInNakshatra += subLordLength;
            }

            throw new ArgumentOutOfRangeException("positionInNakshatraSixtieths", "position fell outside the Nakshatra span");
        }

        #endregion

        #region Node House Significations

        /// <summary>
        /// Builds the node (RA/KE) D6 significations as "houses occupied, houses owned"
        /// of the node's Sign Lord, joining only non-empty parts. The previous inline
        /// logic unconditionally inserted a comma which could produce a stray leading
        /// or trailing comma.
        /// </summary>
        public static string BuildNodeHouseSignifications(string strSignLord,
            Func<string, string> getHouseOccupiedBy, Func<string, string> getHousesOwnedBy)
        {
            string housesOccupied = getHouseOccupiedBy(strSignLord);
            string housesOwned = getHousesOwnedBy(strSignLord);

            StringBuilder result = new StringBuilder();

            if (!string.IsNullOrEmpty(housesOccupied))
            {
                result.Append(housesOccupied);
            }

            if (!string.IsNullOrEmpty(housesOwned))
            {
                if (result.Length > 0)
                {
                    result.Append(",");
                }
                result.Append(housesOwned);
            }

            return result.ToString();
        }

        #endregion

        #region Nadi Coordinate Resolution

        /// <summary>
        /// Resolves the final Nadi coordinates for a house-signification lord.
        ///
        /// Inputs are the intermediate house relationships the chart computes per
        /// lord (see AstroChart.PrepareKPAstroTables):
        ///   D3 - the house in which the lord is deposited (its own occupied house).
        ///   D4 - the houses (signs) owned by that lord.
        ///   D7 - the house in which the lord's sign lord is deposited.
        ///   D8 - the houses (signs) owned by the lord's sign lord.
        ///
        /// A regular planet is a Nadi significator of the houses it occupies and
        /// the houses it owns, so its coordinates are D3 + D4. A node (RA/KE)
        /// owns no signs itself; it functions through its sign lord, whose own
        /// coordinates are D7 + D8, so the node's coordinates are D3 + D7 + D8
        /// (the house the node occupies together with its sign lord's houses).
        ///
        /// The values are collected as integers, de-duplicated and sorted
        /// ascending. D./O. markers and parentheses never appear in the output;
        /// the underlying house relationships are used directly rather than
        /// stripping those prefixes out of a formatted signification string.
        /// </summary>
        public static string ResolveNadiCoordinates(string d3, string d4, string d7, string d8, bool isNode)
        {
            SortedSet<int> houses = new SortedSet<int>();

            CollectHouses(d3, houses);
            if (isNode)
            {
                CollectHouses(d7, houses);
                CollectHouses(d8, houses);
            }
            else
            {
                CollectHouses(d4, houses);
            }

            return string.Join(" ", houses);
        }

        private static void CollectHouses(string strValue, SortedSet<int> houses)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return;
            }

            string[] strEntries = strValue.Split(',');
            for (int i = 0; i < strEntries.Length; i++)
            {
                string strEntry = strEntries[i].Trim();
                if (strEntry.Length == 0)
                {
                    continue;
                }

                int nHouse;
                if (int.TryParse(strEntry, out nHouse) && nHouse >= 1 && nHouse <= 12)
                {
                    houses.Add(nHouse);
                }
            }
        }

        #endregion

        #region KP Table Validation

        /// <summary>
        /// Validates every entry of the loaded KP-Star-Sub-Lords table against the
        /// mathematical model above. Returns true when all entries match, otherwise
        /// false with a description of the first mismatch in <paramref name="strError"/>.
        /// </summary>
        public static bool ValidateKPStarSubLordsTable(ArrayList kpStarTable, out string strError)
        {
            strError = "";

            if (kpStarTable == null)
            {
                strError = "KP star table is null.";
                return false;
            }

            for (int i = 0; i < kpStarTable.Count; i++)
            {
                kpStarObject entry = (kpStarObject)kpStarTable[i];

                if (entry.FromPosSixtieths < 0 || entry.FromPosSixtieths >= entry.ToPosSixtieths ||
                    entry.ToPosSixtieths > SIXTIETHS_PER_SIGN)
                {
                    strError = string.Format("Entry {0} ({1}): bad position range {2} -> {3}.",
                        i + 1, entry.Lords, FormatSixtieths(entry.FromPosSixtieths), FormatSixtieths(entry.ToPosSixtieths));
                    return false;
                }

                string[] signTokens = entry.Sign.Split('/');
                string[] lordTokens = entry.Lords.Split('/');

                if (signTokens.Length != 3 || lordTokens.Length != 3)
                {
                    strError = string.Format("Entry {0}: expected 3 signs and 3 lords, got '{1}' and '{2}'.",
                        i + 1, entry.Sign, entry.Lords);
                    return false;
                }

                string expectedStar = "";
                string expectedSub = "";
                string expectedSubSub = "";

                for (int s = 0; s < signTokens.Length; s++)
                {
                    int signIndex = 0;
                    try
                    {
                        signIndex = GetSignZodiacIndex(signTokens[s]);
                    }
                    catch (ArgumentException)
                    {
                        strError = string.Format("Entry {0}: unknown sign token '{1}'.", i + 1, signTokens[s]);
                        return false;
                    }

                    long globalStart = ((long)signIndex * SIXTIETHS_PER_SIGN) + entry.FromPosSixtieths;
                    long globalEnd = ((long)signIndex * SIXTIETHS_PER_SIGN) + entry.ToPosSixtieths;

                    int nakshatraIndex = (int)(globalStart / SIXTIETHS_PER_NAKSHATRA);
                    if (nakshatraIndex < 0 || nakshatraIndex > 26)
                    {
                        strError = string.Format("Entry {0}: out of range nakshatra index {1} for sign {2}.",
                            i + 1, nakshatraIndex, signTokens[s]);
                        return false;
                    }

                    if ((globalStart % SIXTIETHS_PER_NAKSHATRA) > (globalEnd % SIXTIETHS_PER_NAKSHATRA) &&
                        (globalStart / SIXTIETHS_PER_NAKSHATRA) != (globalEnd - 1) / SIXTIETHS_PER_NAKSHATRA)
                    {
                        strError = string.Format("Entry {0}: crosses a Nakshatra boundary.", i + 1);
                        return false;
                    }

                    long positionInNakshatra = globalStart % SIXTIETHS_PER_NAKSHATRA;
                    long segmentFrom = 0;
                    long segmentTo = 0;
                    string subSub = GetSubSubLordSegment(positionInNakshatra, nakshatraIndex, out segmentFrom, out segmentTo);

                    string star = GetStarLord(nakshatraIndex);
                    string sub = GetSubLordAtPosInNakshatra(positionInNakshatra, nakshatraIndex);

                    long nakshatraStart = ((long)nakshatraIndex * SIXTIETHS_PER_NAKSHATRA);
                    long signWindowStart = (long)signIndex * SIXTIETHS_PER_SIGN;
                    long signWindowEnd = signWindowStart + SIXTIETHS_PER_SIGN;

                    long coveredFrom = Math.Max(nakshatraStart + segmentFrom, signWindowStart);
                    long coveredTo = Math.Min(nakshatraStart + segmentTo, signWindowEnd);

                    if (coveredFrom != globalStart || coveredTo != globalEnd)
                    {
                        strError = string.Format("Entry {0} ({1} {2}->{3}): boundary mismatch for sign {4}: stored {2}->{3}, model {5}->{6}.",
                            i + 1, entry.Sign, FormatSixtieths(entry.FromPosSixtieths), FormatSixtieths(entry.ToPosSixtieths),
                            signTokens[s], FormatSixtieths(coveredFrom - signWindowStart), FormatSixtieths(coveredTo - signWindowStart));
                        return false;
                    }

                    if (!string.Equals(star, lordTokens[0], StringComparison.OrdinalIgnoreCase) ||
                        !string.Equals(sub, lordTokens[1], StringComparison.OrdinalIgnoreCase) ||
                        !string.Equals(subSub, lordTokens[2], StringComparison.OrdinalIgnoreCase))
                    {
                        strError = string.Format("Entry {0} ({1}): lord mismatch for sign {2}: stored {3}, model {4}/{5}/{6}.",
                            i + 1, entry.Sign, signTokens[s], entry.Lords, star, sub, subSub);
                        return false;
                    }

                    if (s == 0)
                    {
                        expectedStar = star;
                        expectedSub = sub;
                        expectedSubSub = subSub;
                    }
                    else if (!string.Equals(star, expectedStar, StringComparison.OrdinalIgnoreCase) ||
                             !string.Equals(sub, expectedSub, StringComparison.OrdinalIgnoreCase) ||
                             !string.Equals(subSub, expectedSubSub, StringComparison.OrdinalIgnoreCase))
                    {
                        strError = string.Format("Entry {0}: signs '{1}' do not share the same model lords ({2}/{3}/{4}).",
                            i + 1, entry.Sign, expectedStar, expectedSub, expectedSubSub);
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}
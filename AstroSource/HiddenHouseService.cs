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
using System.Collections.Generic;
using System.Text;

namespace srlWebCom.Astro.AstroObjects
{
    /// <summary>
    /// Per-planet result of the hidden house computation.
    ///
    /// For each planet four groups of houses are collected from the existing chart data:
    ///   SGN     - houses associated through the planet's Sign-Lord (D4); for nodes (RA/KE)
    ///             the sign-lord's own houses (D7 + D8).
    ///   STL     - houses whose Nakshatra (Star) Lord is the planet.
    ///   SUB     - houses whose Cuspal Sub Lord is the planet.
    ///   Posited - the house in which the planet is physically positioned (D3).
    ///
    /// Candidates = unique(SGN ∪ STL ∪ SUB ∪ Posited), sorted numerically.
    /// Existing Nadi = the current Nadi coordinate numbers for this planet
    ///   (regular planets: D3 + D4; nodes: D3 + D7 + D8).
    /// Final = candidates MINUS existing Nadi, sorted numerically.
    /// </summary>
    public class HiddenHouseResult
    {
        /// <summary>The planet this result belongs to (e.g. "MA", "RA").</summary>
        public string Planet = "";

        /// <summary>Houses associated through the Sign-Lord relationship (D4, or D7+D8 for nodes).</summary>
        public List<int> Sgn = new List<int>();

        /// <summary>Houses whose Nakshatra (Star) Lord is the planet.</summary>
        public List<int> Stl = new List<int>();

        /// <summary>Houses whose Cuspal Sub Lord is the planet.</summary>
        public List<int> Sub = new List<int>();

        /// <summary>The house in which the planet is physically positioned (D3).</summary>
        public List<int> Posited = new List<int>();

        /// <summary>Unique candidates from all four groups, numerically sorted.</summary>
        public List<int> Candidates = new List<int>();

        /// <summary>Candidates minus the existing Nadi coordinate houses, numerically sorted.</summary>
        public List<int> Final = new List<int>();
    }

    /// <summary>
    /// Computes the "Hidden House Activation" values for a KP chart.
    ///
    /// Formula:
    ///   candidates = UNIQUE(SGN ∪ STL ∪ SUB ∪ POSITED), sorted numerically.
    ///   hidden     = candidates MINUS existingNadi,      sorted numerically.
    ///
    /// Where:
    ///   SGN(P)     = houses P owns as sign lord (D4); for nodes, the sign-lord's houses (D7 ∪ D8).
    ///   STL(P)     = cusp houses whose Star Lord is P.
    ///   SUB(P)     = cusp houses whose Sub Lord is P.
    ///   POSITED(P) = D3 (the house P occupies).
    ///   existingNadi(P) = NadiCalculationService.ResolveNadiCoordinates output
    ///                    (D3+D4 for regular planets, D3+D7+D8 for nodes).
    ///
    /// The computation is performed ONCE per chart via ComputeForChart and reused
    /// by both the House Signification table and the Nadi Coordinates table.
    /// </summary>
    public static class HiddenHouseService
    {
        /// <summary>
        /// Pure computation: given the four groups and the existing Nadi house set,
        /// returns the numerically sorted hidden house list.
        /// </summary>
        public static List<int> ComputeHiddenHouses(
            ICollection<int> sgn,
            ICollection<int> stl,
            ICollection<int> sub,
            ICollection<int> posited,
            ICollection<int> existingNadi)
        {
            SortedSet<int> candidates = new SortedSet<int>();
            AddAll(candidates, sgn);
            AddAll(candidates, stl);
            AddAll(candidates, sub);
            AddAll(candidates, posited);

            List<int> final = new List<int>();
            foreach (int n in candidates)
            {
                if (existingNadi == null || !existingNadi.Contains(n))
                    final.Add(n);
            }
            return final;
        }

        /// <summary>
        /// Builds the four groups and the hidden house result for a single planet
        /// from the chart data fields (D3/D4/D7/D8 + cusp lords).
        /// </summary>
        public static HiddenHouseResult ComputeForPlanet(
            string strPlanet,
            string strD3, string strD4, string strD7, string strD8,
            bool isNode,
            IList<CuspData> cusps)
        {
            HiddenHouseResult result = new HiddenHouseResult();
            result.Planet = strPlanet ?? "";

            if (string.IsNullOrEmpty(strPlanet))
                return result;

            string planetUpper = strPlanet.Trim().ToUpperInvariant();

            result.Sgn = ParseHouseNumbers(strD4);
            if (isNode)
            {
                List<int> nodeSign = ParseHouseNumbers(strD7);
                AppendUnique(result.Sgn, ParseHouseNumbers(strD8));
                AppendUnique(result.Sgn, nodeSign);
            }

            result.Stl = GetNakshatraLordHouses(cusps, planetUpper);
            result.Sub = GetCuspalSubLordHouses(cusps, planetUpper);

            List<int> posited = ParseHouseNumbers(strD3);
            result.Posited = (posited.Count > 0)
                ? new List<int>(new int[] { posited[0] })
                : new List<int>();

            HashSet<int> existingNadi = GetExistingNadiNumbers(strD3, strD4, strD7, strD8, isNode);
            result.Candidates = GetCandidatesSorted(result.Sgn, result.Stl, result.Sub, result.Posited);
            result.Final = ComputeHiddenHouses(result.Sgn, result.Stl, result.Sub, result.Posited, existingNadi);
            return result;
        }

        /// <summary>
        /// Computes the hidden houses for every planet in a chart and returns them
        /// keyed by clean planet name, so the calling UI can apply identical hidden
        /// values in both the House Signification and the Nadi Coordinates table.
        /// </summary>
        public static Dictionary<string, HiddenHouseResult> ComputeForChart(AstroChartData chartData)
        {
            Dictionary<string, HiddenHouseResult> dict = new Dictionary<string, HiddenHouseResult>(StringComparer.OrdinalIgnoreCase);
            if (chartData == null)
                return dict;

            foreach (PlanetData pd in chartData.PlanetList)
            {
                string planetName = pd.Name;
                if (string.IsNullOrEmpty(planetName))
                    continue;

                string cleanName = planetName.Replace("#", "").Replace("*", "").Trim();
                if (cleanName.Length == 0)
                    continue;

                bool isNode = IsNode(cleanName);

                HouseSignificationData hsd = FindPlanetSignification(chartData.HouseSignificationList, cleanName);
                if (hsd == null)
                    continue;

                HiddenHouseResult result = ComputeForPlanet(
                    cleanName,
                    hsd.D3, hsd.D4, hsd.D7, hsd.D8, isNode,
                    chartData.CuspList);

                if (!dict.ContainsKey(cleanName))
                    dict[cleanName] = result;
            }

            return dict;
        }

        public static bool IsNode(string strPlanet)
        {
            if (string.IsNullOrEmpty(strPlanet))
                return false;

            string upper = strPlanet.Trim().ToUpperInvariant();
            return upper == "RA" || upper == "KE";
        }

        #region Nadi coordinate existing set

        /// <summary>
        /// The planet's current Nadi coordinate houses: D3 + D4 for a regular planet,
        /// D3 + D7 + D8 for a node.  This mirrors the output of
        /// NadiCalculationService.ResolveNadiCoordinates exactly.
        /// </summary>
        internal static HashSet<int> GetExistingNadiNumbers(string d3, string d4, string d7, string d8, bool isNode)
        {
            HashSet<int> houses = new HashSet<int>();
            AddParsedHouses(d3, houses);
            if (isNode)
            {
                AddParsedHouses(d7, houses);
                AddParsedHouses(d8, houses);
            }
            else
            {
                AddParsedHouses(d4, houses);
            }
            return houses;
        }

        #endregion

        #region Cusp lord scanning

        private static List<int> GetCuspalSubLordHouses(IList<CuspData> cusps, string planetUpper)
        {
            List<int> houses = new List<int>();
            if (cusps == null)
                return houses;

            for (int i = 0; i < cusps.Count; i++)
            {
                CuspData cusp = cusps[i];
                if (cusp == null)
                    continue;

                string subLord = cusp.SubLord;
                if (!string.IsNullOrEmpty(subLord) &&
                    string.Equals(subLord.Replace("#", "").Replace("*", "").Trim().ToUpperInvariant(), planetUpper, StringComparison.Ordinal))
                {
                    if (cusp.HouseNo >= 1 && cusp.HouseNo <= 12)
                        houses.Add(cusp.HouseNo);
                }
            }
            houses.Sort();
            return houses;
        }

        private static List<int> GetNakshatraLordHouses(IList<CuspData> cusps, string planetUpper)
        {
            List<int> houses = new List<int>();
            if (cusps == null)
                return houses;

            for (int i = 0; i < cusps.Count; i++)
            {
                CuspData cusp = cusps[i];
                if (cusp == null)
                    continue;

                string starLord = cusp.StarLord;
                if (!string.IsNullOrEmpty(starLord) &&
                    string.Equals(starLord.Replace("#", "").Replace("*", "").Trim().ToUpperInvariant(), planetUpper, StringComparison.Ordinal))
                {
                    if (cusp.HouseNo >= 1 && cusp.HouseNo <= 12)
                        houses.Add(cusp.HouseNo);
                }
            }
            houses.Sort();
            return houses;
        }

        #endregion

        #region Chart lookup

        private static HouseSignificationData FindPlanetSignification(List<HouseSignificationData> list, string planetName)
        {
            if (list == null || string.IsNullOrEmpty(planetName))
                return null;

            string target = planetName.Replace("#", "").Replace("*", "").Trim().ToUpperInvariant();
            for (int i = 0; i < list.Count; i++)
            {
                HouseSignificationData hsd = list[i];
                if (hsd == null || string.IsNullOrEmpty(hsd.Planet))
                    continue;

                string hsdPlanet = hsd.Planet.Replace("#", "").Replace("*", "").Trim().ToUpperInvariant();
                if (hsdPlanet == target)
                    return hsd;
            }
            return null;
        }

        #endregion

        #region Number parsing / formatting

        /// <summary>
        /// Parses house numbers from a comma- or space-separated string (e.g. "5, 12" or "1 9").
        /// Tolerates empty brackets such as "()". Numbers outside 1..12 are ignored.
        /// </summary>
        public static List<int> ParseHouseNumbers(string strValue)
        {
            List<int> houses = new List<int>();
            if (string.IsNullOrEmpty(strValue))
                return houses;

            string cleaned = strValue.Replace("()", "").Trim();
            if (cleaned.Length == 0)
                return houses;

            string[] parts = cleaned.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i].Trim();
                if (part.Length == 0)
                    continue;

                int n;
                if (int.TryParse(part, out n) && n >= 1 && n <= 12)
                    houses.Add(n);
            }
            return houses;
        }

        /// <summary>Formats a compact "1, 2, 3" style list of house numbers.</summary>
        public static string FormatHouseList(List<int> houses)
        {
            if (houses == null || houses.Count == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < houses.Count; i++)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(houses[i]);
            }
            return sb.ToString();
        }

        /// <summary>Formats "(1, 2, 3)" style brackets, or "" when the list is empty.</summary>
        public static string FormatBracketString(List<int> houses)
        {
            string list = FormatHouseList(houses);
            if (string.IsNullOrEmpty(list))
                return "";
            return "(" + list + ")";
        }

        #endregion

        #region Internal helpers

        private static void AddAll(SortedSet<int> target, ICollection<int> source)
        {
            if (source == null)
                return;
            foreach (int n in source)
                target.Add(n);
        }

        private static void AddAll(HashSet<int> target, ICollection<int> source)
        {
            if (source == null)
                return;
            foreach (int n in source)
                target.Add(n);
        }

        private static void AppendUnique(List<int> target, List<int> source)
        {
            if (target == null || source == null)
                return;
            HashSet<int> seen = new HashSet<int>(target);
            for (int i = 0; i < source.Count; i++)
            {
                if (!seen.Contains(source[i]))
                {
                    target.Add(source[i]);
                    seen.Add(source[i]);
                }
            }
        }

        private static void AddParsedHouses(string strValue, HashSet<int> set)
        {
            List<int> houses = ParseHouseNumbers(strValue);
            for (int i = 0; i < houses.Count; i++)
                set.Add(houses[i]);
        }

        private static List<int> GetCandidatesSorted(List<int> sgn, List<int> stl, List<int> sub, List<int> posited)
        {
            SortedSet<int> set = new SortedSet<int>();
            AddAll(set, sgn);
            AddAll(set, stl);
            AddAll(set, sub);
            AddAll(set, posited);
            return new List<int>(set);
        }

        #endregion
    }
}

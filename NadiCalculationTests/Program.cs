/* NadiCalculationTests
   Self-contained console test harness for the KP Star-Sub-Lord mathematics
   implemented in AstroSource\NadiCalculationService.cs and the KP-Star-Sub-Lords
   lookup table loaded via AstroObjects.cs.

   Build/run:
       MSBuild NadiCalculationTests.csproj
       NadiCalculationTests\bin\Release\NadiCalculationTests.exe
   Exits with code 0 when every test passes, otherwise 1. */

using System;
using System.Collections;
using System.Collections.Generic;
using srlWebCom.Astro.AstroObjects;

namespace NadiCalculationTests
{
    class Program
    {
        private const long SIX_PER_NAK = NadiCalculationService.SIXTIETHS_PER_NAKSHATRA;
        private const long SIX_PER_SIGN = NadiCalculationService.SIXTIETHS_PER_SIGN;
        private const long SIX_PER_ZODIAC = NadiCalculationService.SIXTIETHS_PER_ZODIAC;

        private static int m_nPassed = 0;
        private static int m_nFailed = 0;
        private static ArrayList m_failures = new ArrayList();
        private static kpAstroObject m_oKPTable = new kpAstroObject();

        private static int Main(string[] args)
        {
            astroGlobal.AstroSourcePath = "logicAstroKPCharts.AstroSource";

            Run("Loads KP-Star-Sub-Lords.txt (731 entries)", TestLoadKPTable);
            Run("Vimshottari Dasa years agree with kpAstroObject", TestVimshottariYears);
            Run("Sub Lord lengths are Vimshottari years x 400 arc sec", TestSubLordLengths);
            Run("Sub Lord lengths add up to one Nakshatra", TestSubLordLengthsSum);
            Run("ParsePositionToSixtieths handles four-field positions exactly", TestParsePosition);
            Run("Nakshatra index boundaries", TestNakshatraIndexBoundaries);
            Run("Star Lords and names match Degree-Positions.txt reference", TestNakshatraStarLords);
            Run("Nakshatra Pada boundaries", TestNakshatraPadaBoundaries);
            Run("Ashwini Sub Lord boundaries", TestAshwiniSubLordBoundaries);
            Run("Sub-Sub segments inside Ke Sub Lord of Ashwini", TestKeSubSubSegments);
            Run("Sub-Sub rotation starts at the Sub Lord (Ve of Ashwini)", TestVeSubSubRotation);
            Run("Sub-Sub lengths sum to their Sub Lord length", TestSubSubLengthsSum);
            Run("Zodiac longitude normalization (360/-ve/>360)", TestNormalization);
            Run("Sign group token matching and sign indexes", TestSignMatching);
            Run("KP table is contiguous per sign token", TestTableContiguity);
            Run("Whole KP table validated against the mathematical model", TestTableValidation);
            Run("GetKPStarSubLords agrees with the model across all 12 signs", TestKPLookupVersusModel);
            Run("BuildNodeHouseSignifications has no stray commas", TestNodeSignifications);
            Run("Nadi coordinates resolve occupied/owned houses for all 27 cells", TestNadiResolution);
            Run("Hidden houses: SPEC example SUN -> [4]", TestHiddenHouseSpecExamples);
            Run("Hidden houses: reference chart final hidden houses sorted", TestHiddenHouseBasicChart);
            Run("Hidden houses: node RA/KE use D3+D7+D8 as existing", TestHiddenHouseNodes);
            Run("Hidden houses: edge cases (duplicates, empty STL, all-consumed)", TestHiddenHouseEdgeCases);
            Run("Hidden houses: deduplication and existing-signification removal", TestHiddenHouseDedupExisting);
            Run("Hidden houses: formatting helpers", TestHiddenHouseFormatting);
            Run("Hidden houses: chart-level results shared by both tables", TestHiddenHouseChartShared);
            Run("Hidden houses: changing chart data changes results", TestHiddenHouseDynamic);

            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("PASSED: {0}   FAILED: {1}", m_nPassed, m_nFailed);
            Console.WriteLine("=========================================================");

            if (m_failures.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Failures:");
                for (int i = 0; i < m_failures.Count; i++)
                {
                    Console.WriteLine("  - {0}", m_failures[i]);
                }
            }

            return m_nFailed == 0 ? 0 : 1;
        }

        private static void Run(string strTestName, Func<bool> testMethod)
        {
            try
            {
                if (testMethod())
                {
                    m_nPassed++;
                    Console.WriteLine("PASS: {0}", strTestName);
                }
                else
                {
                    m_nFailed++;
                    Console.WriteLine("FAIL: {0}", strTestName);
                }
            }
            catch (Exception ex)
            {
                m_nFailed++;
                m_failures.Add(string.Format("{0}: unhandled exception -> {1}", strTestName, ex.Message));
                Console.WriteLine("FAIL: {0} (exception)", strTestName);
            }
        }

        private static void Fail(string strMessage)
        {
            m_failures.Add(strMessage);
        }

        private static bool TestLoadKPTable()
        {
            bool bLoaded = m_oKPTable.LoadKPStarSubLordsTable();
            if (!bLoaded || !kpAstroObject.StarTableLoaded)
            {
                Fail("LoadKPStarSubLordsTable returned false or StarTableLoaded is false");
                return false;
            }

            if (m_oKPTable.KPStarTable.Count != 731)
            {
                Fail(string.Format("KP table count is {0}, expected 731", m_oKPTable.KPStarTable.Count));
                return false;
            }

            return true;
        }

        private static bool TestVimshottariYears()
        {
            for (int i = 0; i < NadiCalculationService.SubLordOrder.Length; i++)
            {
                string lord = NadiCalculationService.SubLordOrder[i];
                if (m_oKPTable.GetDasaYears(lord) != NadiCalculationService.SubLordDasaYears[i])
                {
                    Fail(string.Format("GetDasaYears({0}) = {1}, expected service {2}",
                        lord, m_oKPTable.GetDasaYears(lord), NadiCalculationService.SubLordDasaYears[i]));
                    return false;
                }
            }
            return true;
        }

        private static bool TestSubLordLengths()
        {
            int[] expectedArcSeconds = new int[] { 2800, 8000, 2400, 4000, 2800, 7200, 6400, 7600, 6800 };
            for (int i = 0; i < NadiCalculationService.SubLordOrder.Length; i++)
            {
                string lord = NadiCalculationService.SubLordOrder[i];
                long actual = NadiCalculationService.GetSubLordLengthSixtieths(lord) / 60;
                if (actual != expectedArcSeconds[i])
                {
                    Fail(string.Format("Sub Lord {0} length = {1}\", expected {2}\"", lord, actual, expectedArcSeconds[i]));
                    return false;
                }
            }
            return true;
        }

        private static bool TestSubLordLengthsSum()
        {
            long sum = 0;
            for (int i = 0; i < NadiCalculationService.SubLordOrder.Length; i++)
            {
                sum += NadiCalculationService.GetSubLordLengthSixtieths(NadiCalculationService.SubLordOrder[i]);
            }
            return sum == SIX_PER_NAK;
        }

        private static bool TestParsePosition()
        {
            if (NadiCalculationService.ParsePositionToSixtieths("00:00:00:00") != 0) { Fail("\"00:00:00:00\" != 0"); return false; }
            if (NadiCalculationService.ParsePositionToSixtieths("00:02:43:20") != 9800) { Fail("\"00:02:43:20\" != 9800"); return false; }
            if (NadiCalculationService.ParsePositionToSixtieths("00:46:40:00") != 168000) { Fail("\"00:46:40:00\" != 168000"); return false; }
            if (NadiCalculationService.ParsePositionToSixtieths("13:20:00:00") != SIX_PER_NAK) { Fail("\"13:20:00:00\" != " + SIX_PER_NAK); return false; }
            if (NadiCalculationService.ParsePositionToSixtieths("30:00:00:00") != SIX_PER_SIGN) { Fail("\"30:00:00:00\" != " + SIX_PER_SIGN); return false; }
            if (NadiCalculationService.ParsePositionToSixtieths("01:08:53:20") != 248000) { Fail("\"01:08:53:20\" != 248000"); return false; }

            try
            {
                NadiCalculationService.ParsePositionToSixtieths("00:02:43");
                Fail("Three-field position did not throw");
                return false;
            }
            catch (FormatException) { }

            try
            {
                NadiCalculationService.ParsePositionToSixtieths("abc:def:1:2");
                Fail("Non-numeric position did not throw");
                return false;
            }
            catch (FormatException) { }

            return true;
        }

        private static bool TestNakshatraIndexBoundaries()
        {
            return CheckNakshatraIndex(0, 0) &&
                   CheckNakshatraIndex(SIX_PER_NAK - 1, 0) &&
                   CheckNakshatraIndex(SIX_PER_NAK, 1) &&
                   CheckNakshatraIndex(2 * SIX_PER_NAK, 2) &&
                   CheckNakshatraIndex(25 * SIX_PER_NAK, 25) &&
                   CheckNakshatraIndex(26 * SIX_PER_NAK, 26) &&
                   CheckNakshatraIndex(SIX_PER_ZODIAC - 1, 26) &&
                   CheckNakshatraIndex(SIX_PER_ZODIAC, 0);
        }

        private static bool CheckNakshatraIndex(long sixtieths, int expectedIndex)
        {
            int actual = NadiCalculationService.GetNakshatraIndex(sixtieths);
            if (actual != expectedIndex)
            {
                Fail(string.Format("NakshatraIndex({0}) = {1}, expected {2}", sixtieths, actual, expectedIndex));
                return false;
            }
            return true;
        }

        private static bool TestNakshatraStarLords()
        {
            string[] knownLords = new string[]
            {
                "KE","VE","SU","MO","MA","RA","JU","SA","ME",
                "KE","VE","SU","MO","MA","RA","JU","SA","ME",
                "KE","VE","SU","MO","MA","RA","JU","SA","ME"
            };
            string[] knownNames = NadiCalculationService.NakshatraNames;

            for (int i = 0; i < 27; i++)
            {
                if (!string.Equals(NadiCalculationService.GetStarLord(i), knownLords[i], StringComparison.OrdinalIgnoreCase))
                {
                    Fail(string.Format("Star Lord of nakshatra {0} = {1}, expected {2}", i,
                        NadiCalculationService.GetStarLord(i), knownLords[i]));
                    return false;
                }
                if (NadiCalculationService.GetNakshatraName(i) != knownNames[i])
                {
                    Fail(string.Format("Nakshatra name {0} = {1}, expected {2}", i,
                        NadiCalculationService.GetNakshatraName(i), knownNames[i]));
                    return false;
                }
            }
            return true;
        }

        private static bool TestNakshatraPadaBoundaries()
        {
            return CheckPada(0, 1) &&
                   CheckPada(SIX_PER_NAK / 4 - 1, 1) &&
                   CheckPada(SIX_PER_NAK / 4, 2) &&
                   CheckPada(2 * (SIX_PER_NAK / 4) - 1, 2) &&
                   CheckPada(2 * (SIX_PER_NAK / 4), 3) &&
                   CheckPada(SIX_PER_NAK - 1, 4);
        }

        private static bool CheckPada(long posInNak, int expectedPada)
        {
            int actual = NadiCalculationService.GetNakshatraPada(posInNak);
            if (actual != expectedPada)
            {
                Fail(string.Format("Pada({0}) = {1}, expected {2}", posInNak, actual, expectedPada));
                return false;
            }
            return true;
        }

        private static bool TestAshwiniSubLordBoundaries()
        {
            long[] boundaries = new long[]
            {
                168000,   // Ke   ends  0d46m40s
                648000,   // Ve
                792000,   // Su
                1032000,  // Mo
                1200000,  // Ma
                1632000,  // Ra
                2016000,  // Ju
                2472000,  // Sa
                2880000   // Me = full Nakshatra
            };
            string[] expectedLords = new string[] { "KE", "VE", "SU", "MO", "MA", "RA", "JU", "SA", "ME" };

            if (NadiCalculationService.GetSubLordAtPosInNakshatra(0, 0) != "KE") { Fail("Ashwini 0 should be KE"); return false; }

            for (int i = 0; i < boundaries.Length; i++)
            {
                if (NadiCalculationService.GetSubLordAtPosInNakshatra(boundaries[i] - 1, 0) != expectedLords[i])
                {
                    Fail(string.Format("Ashwini {0} should be {1}", boundaries[i] - 1, expectedLords[i]));
                    return false;
                }
                if (i + 1 < boundaries.Length &&
                    NadiCalculationService.GetSubLordAtPosInNakshatra(boundaries[i], 0) != expectedLords[i + 1])
                {
                    Fail(string.Format("Ashwini {0} should be {1}", boundaries[i], expectedLords[i + 1]));
                    return false;
                }
            }
            return true;
        }

private static bool TestKeSubSubSegments()
        {
            // Lines 1-9 of KP-Star-Sub-Lords.txt: Ke Sub Lord of Ke star (Ashwini),
            // i.e. Nakshatra position 0 .. 46m40s.
            long[] boundaries = new long[] { 9800, 37800, 46200, 60200, 70000, 95200, 117600, 144200, 168000 };
            string[] expected = new string[] { "VE", "SU", "MO", "MA", "RA", "JU", "SA", "ME", "NA" };

            long segFrom, segTo;
            string subSub = NadiCalculationService.GetSubSubLordSegment(0, 0, out segFrom, out segTo);
            if (subSub != "KE") { Fail("Ashwini 0 should be sub-sub KE"); return false; }
            if (segFrom != 0 || segTo != 9800) { Fail("Ke/Ke/Ke segment should be [0,9800)"); return false; }

            for (int i = 0; i < 8; i++)
            {
                string atEnd = NadiCalculationService.GetSubSubLordSegment(boundaries[i], 0, out segFrom, out segTo);
                if (atEnd != expected[i])
                {
                    Fail(string.Format("Ke Sub at {0} should be {1}, got {2}", boundaries[i], expected[i], atEnd));
                    return false;
                }
                if (segFrom != boundaries[i])
                {
                    Fail(string.Format("Segment at {0} should start at {0}, got {1}", boundaries[i], segFrom));
                    return false;
                }
            }

            if (NadiCalculationService.GetSubSubLordSegment(167999, 0, out segFrom, out segTo) != "ME")
            {
                Fail("Ke Sub last slice should be ME");
                return false;
            }
            if (segTo != 168000)
            {
                Fail("Ke Sub last slice should end at 168000");
                return false;
            }

            return true;
        }

        private static bool TestVeSubSubRotation()
        {
            long segFrom, segTo;
            if (NadiCalculationService.GetSubSubLordSegment(168000, 0, out segFrom, out segTo) != "VE")
            {
                Fail("Ve Sub of Ashwini should start with sub-sub VE");
                return false;
            }
            if (segFrom != 168000) { Fail("Ve Sub starts at 168000"); return false; }
            if (segTo != 248000) { Fail("Ve/Ve segment should end at 248000 (1d8m53s20)"); return false; }

            if (NadiCalculationService.GetSubSubLordSegment(247999, 0, out segFrom, out segTo) != "VE") { Fail("Ve/Ve should cover 247999"); return false; }
            if (NadiCalculationService.GetSubSubLordSegment(248000, 0, out segFrom, out segTo) != "SU") { Fail("Ve/Su should start at 248000"); return false; }
            if (NadiCalculationService.GetSubSubLordSegment(619999, 0, out segFrom, out segTo) != "ME") { Fail("Ve/Me should cover 619999"); return false; }
            if (NadiCalculationService.GetSubSubLordSegment(620000, 0, out segFrom, out segTo) != "KE") { Fail("Ve/Ke should start at 620000"); return false; }
            if (NadiCalculationService.GetSubSubLordSegment(647999, 0, out segFrom, out segTo) != "KE") { Fail("Ve/Ke should cover 647999"); return false; }

            return true;
        }

        private static bool TestSubSubLengthsSum()
        {
            for (int i = 0; i < NadiCalculationService.SubLordOrder.Length; i++)
            {
                string sub = NadiCalculationService.SubLordOrder[i];
                int subIndex = NadiCalculationService.GetSubLordOrderIndex(sub);

                long sum = 0;
                for (int j = 0; j < 9; j++)
                {
                    string subSub = NadiCalculationService.SubLordOrder[(subIndex + j) % 9];
                    sum += NadiCalculationService.GetSubSubLordLengthSixtieths(sub, subSub);
                }

                if (sum != NadiCalculationService.GetSubLordLengthSixtieths(sub))
                {
                    Fail(string.Format("Sub-Sub lengths of {0} sum to {1}, Sub Lord length {2}", sub, sum,
                        NadiCalculationService.GetSubLordLengthSixtieths(sub)));
                    return false;
                }
            }
            return true;
        }

        private static bool TestNormalization()
        {
            if (NadiCalculationService.NormalizeZodiacSixtieths(0) != 0) { Fail("normalize(0) != 0"); return false; }
            if (NadiCalculationService.NormalizeZodiacSixtieths(SIX_PER_ZODIAC) != 0) { Fail("normalize(360d) != 0"); return false; }
            if (NadiCalculationService.NormalizeZodiacSixtieths(SIX_PER_ZODIAC + 12345) != 12345) { Fail("normalize(360d+x) != x"); return false; }
            if (NadiCalculationService.NormalizeZodiacSixtieths(-1) != SIX_PER_ZODIAC - 1) { Fail("normalize(-1s) != 359d59m59s"); return false; }
            if (NadiCalculationService.NormalizeZodiacSixtieths(-1000) != SIX_PER_ZODIAC - 1000) { Fail("normalize(-1000) mismatch"); return false; }

            long large = 1234567890123L;
            long expected = large % SIX_PER_ZODIAC;
            if (NadiCalculationService.NormalizeZodiacSixtieths(large) != expected) { Fail("normalize(large) != large % zodiac"); return false; }

            return true;
        }

        private static bool TestSignMatching()
        {
            if (!NadiCalculationService.SignGroupContains("Ari/Leo/Sag", "Ari")) { Fail("Ari missed"); return false; }
            if (!NadiCalculationService.SignGroupContains("Ari/Leo/Sag", "Leo")) { Fail("Leo missed"); return false; }
            if (!NadiCalculationService.SignGroupContains("Ari/Leo/Sag", "Sag")) { Fail("Sag missed"); return false; }
            if (!NadiCalculationService.SignGroupContains("Ari/Leo/Sag", "ari")) { Fail("case insensitive missed"); return false; }
            if (NadiCalculationService.SignGroupContains("Tau/Vir/Cap", "Ari")) { Fail("Ari matched Tau group"); return false; }
            if (NadiCalculationService.SignGroupContains("Gem/Lib/Aqu", "Sag")) { Fail("Sag matched Gem group"); return false; }
            if (NadiCalculationService.SignGroupContains("Can/Sco/Pis", "Cap")) { Fail("Cap matched Can group"); return false; }
            if (NadiCalculationService.SignGroupContains("Ari/Leo/Sag", "Ta")) { Fail("partial token should not match"); return false; }
            if (NadiCalculationService.SignGroupContains("Ari/Leo/Sag", "")) { Fail("empty short sign matched"); return false; }
            if (NadiCalculationService.SignGroupContains("", "Ari")) { Fail("empty group matched"); return false; }
            if (NadiCalculationService.SignGroupContains(null, "Ari")) { Fail("null group matched"); return false; }

            string[] signs = new string[] { "ARI", "TAU", "GEM", "CAN", "LEO", "VIR", "LIB", "SCO", "SAG", "CAP", "AQU", "PIS" };
            for (int i = 0; i < signs.Length; i++)
            {
                if (NadiCalculationService.GetSignZodiacIndex(signs[i]) != m_oKPTable.GetSignID(signs[i]))
                {
                    Fail(string.Format("GetSignZodiacIndex({0}) differs from GetSignID", signs[i]));
                    return false;
                }
            }

            return true;
        }

        private static bool TestTableContiguity()
        {
            Hashtable lastTo = new Hashtable();
            Hashtable foundAny = new Hashtable();

            for (int i = 0; i < m_oKPTable.KPStarTable.Count; i++)
            {
                kpStarObject entry = (kpStarObject)m_oKPTable.KPStarTable[i];

                string[] signTokens = entry.Sign.Split('/');
                if (signTokens.Length != 3)
                {
                    Fail(string.Format("Entry {0}: sign group does not have 3 signs: {1}", i + 1, entry.Sign));
                    return false;
                }

                foreach (string token in signTokens)
                {
                    if (!foundAny.ContainsKey(token))
                    {
                        if (entry.FromPosSixtieths != 0)
                        {
                            Fail(string.Format("Entry {0}: first entry of sign {1} does not start at 00:00:00:00", i + 1, token));
                            return false;
                        }
                        foundAny[token] = true;
                    }
                    else if (Convert.ToInt64(lastTo[token]) != entry.FromPosSixtieths)
                    {
                        Fail(string.Format("Entry {0}: sign {1} gap between {2} and {3}", i + 1, token,
                            lastTo[token], entry.FromPosSixtieths));
                        return false;
                    }

                    lastTo[token] = entry.ToPosSixtieths;
                }
            }

            foreach (DictionaryEntry de in lastTo)
            {
                if (Convert.ToInt64(de.Value) != SIX_PER_SIGN)
                {
                    Fail(string.Format("Sign {0} does not end at 30:00:00:00", de.Key));
                    return false;
                }
            }

            return true;
        }

        private static bool TestTableValidation()
        {
            string strError = "";
            bool bValid = NadiCalculationService.ValidateKPStarSubLordsTable(m_oKPTable.KPStarTable, out strError);

            if (!bValid)
            {
                Fail("KP table validation failed: " + strError);
            }
            return bValid;
        }

        private static bool TestKPLookupVersusModel()
        {
            string[] signNames = new string[]
            {
                "ARIES", "TAURUS", "GEMINI", "CANCER", "LEO", "VIRGO",
                "LIBRA", "SCORPIO", "SAGITTARIUS", "CAPRICORN", "AQUARIUS", "PISCES"
            };

            long[] sampleArcSeconds = new long[]
            {
                0, 1, 163, 630, 2799, 2800, 48000 - 1, 48000, 72000, 108000 - 1
            };

            for (int sign = 0; sign < 12; sign++)
            {
                long signBase = (long)sign * SIX_PER_SIGN;

                for (int s = 0; s < sampleArcSeconds.Length; s++)
                {
                    long arcSec = sampleArcSeconds[s];
                    astroPosition pos = new astroPosition((int)(arcSec / 3600), (int)((arcSec % 3600) / 60), (int)(arcSec % 60));

                    long zodiacSix = signBase + arcSec * NadiCalculationService.SIXTIETHS_PER_ARC_SECOND;
                    int nakshatraIndex = (int)(zodiacSix / SIX_PER_NAK);
                    long posInNak = zodiacSix % SIX_PER_NAK;

                    string modelStar = NadiCalculationService.GetStarLord(nakshatraIndex);
                    string modelSub = NadiCalculationService.GetSubLordAtPosInNakshatra(posInNak, nakshatraIndex);
                    long segFrom = 0;
                    long segTo = 0;
                    string modelSubSub = NadiCalculationService.GetSubSubLordSegment(posInNak, nakshatraIndex, out segFrom, out segTo);

                    string expectedFull = string.Format("{0}/{1}/{2}", modelStar, modelSub, modelSubSub);
                    string actualFull = m_oKPTable.GetKPStarSubLords(signNames[sign], pos, 3);

                    if (!string.Equals(actualFull, expectedFull, StringComparison.OrdinalIgnoreCase))
                    {
                        Fail(string.Format("{0} at {1}: lookup '{2}', model '{3}'", signNames[sign], arcSec, actualFull, expectedFull));
                        return false;
                    }

                    if (!string.Equals(m_oKPTable.GetKPStarSubLords(signNames[sign], pos, 1), modelStar, StringComparison.OrdinalIgnoreCase))
                    {
                        Fail(string.Format("{0} at {1}: nType=1 lookup differs", signNames[sign], arcSec));
                        return false;
                    }

                    string expectedTwo = string.Format("{0}/{1}", modelStar, modelSub);
                    if (!string.Equals(m_oKPTable.GetKPStarSubLords(signNames[sign], pos, 2), expectedTwo, StringComparison.OrdinalIgnoreCase))
                    {
                        Fail(string.Format("{0} at {1}: nType=2 lookup differs", signNames[sign], arcSec));
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool TestNodeSignifications()
        {
            if (NadiCalculationService.BuildNodeHouseSignifications("MA",
                delegate(string lord) { return "1,4"; },
                delegate(string lord) { return "5,12"; }) != "1,4,5,12")
            {
                Fail("occupied + owned join failed");
                return false;
            }

            if (NadiCalculationService.BuildNodeHouseSignifications("MA",
                delegate(string lord) { return ""; },
                delegate(string lord) { return "5,12"; }) != "5,12")
            {
                Fail("stray leading comma present");
                return false;
            }

            if (NadiCalculationService.BuildNodeHouseSignifications("MA",
                delegate(string lord) { return "1,4"; },
                delegate(string lord) { return ""; }) != "1,4")
            {
                Fail("stray trailing comma present");
                return false;
            }

            if (NadiCalculationService.BuildNodeHouseSignifications("MA",
                delegate(string lord) { return ""; },
                delegate(string lord) { return ""; }) != "")
            {
                Fail("empty result should be empty");
                return false;
            }

            return true;
        }

        private class NadiLord
        {
            public string Name;
            public string D3;
            public string D4;
            public string D7;
            public string D8;
            public bool IsNode;
            public string Expected;
        }

        private static bool TestNadiResolution()
        {
            // Per-lord intermediate relationships for the reference chart.
            // D3 = house the lord occupies, D4 = houses the lord owns, D7 = house
            // the sign lord occupies, D8 = houses the sign lord owns (nodes use
            // the sign lord's D7/D8 because the node owns no signs itself).
            NadiLord[] lords = new NadiLord[]
            {
                new NadiLord { Name = "SU", D3 = "1", D4 = "9", Expected = "1 9" },
                new NadiLord { Name = "MO", D3 = "6", D4 = "8", Expected = "6 8" },
                new NadiLord { Name = "MA", D3 = "12", D4 = "5,12", Expected = "5 12" },
                new NadiLord { Name = "RA", D3 = "4", D7 = "12", D8 = "5,12", IsNode = true, Expected = "4 5 12" },
                new NadiLord { Name = "JU", D3 = "9", D4 = "1,4", Expected = "1 4 9" },
                new NadiLord { Name = "SA", D3 = "7", D4 = "2,3", Expected = "2 3 7" },
                new NadiLord { Name = "ME", D3 = "1", D4 = "7,10", Expected = "1 7 10" },
                new NadiLord { Name = "KE", D3 = "10", D7 = "1", D8 = "6,11", IsNode = true, Expected = "1 6 10 11" },
                new NadiLord { Name = "VE", D3 = "1", D4 = "6,11", Expected = "1 6 11" }
            };

            // Base planet, nakshatra lord and sub lord for each chart row.
            string[][] rows = new string[][]
            {
                new string[] { "KE", "MA", "VE" },
                new string[] { "VE", "VE", "SA" },
                new string[] { "SU", "SU", "VE" },
                new string[] { "MO", "RA", "ME" },
                new string[] { "MA", "ME", "RA" },
                new string[] { "RA", "KE", "SU" },
                new string[] { "JU", "MA", "RA" },
                new string[] { "SA", "JU", "SU" },
                new string[] { "ME", "VE", "ME" }
            };

            for (int r = 0; r < rows.Length; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    string lord = rows[r][c];
                    NadiLord nl = null;
                    for (int i = 0; i < lords.Length; i++)
                    {
                        if (lords[i].Name == lord)
                        {
                            nl = lords[i];
                            break;
                        }
                    }

                    string actual = NadiCalculationService.ResolveNadiCoordinates(nl.D3, nl.D4, nl.D7, nl.D8, nl.IsNode);
                    if (!string.Equals(actual, nl.Expected, StringComparison.Ordinal))
                    {
                        Fail(string.Format("{0} ({1}): resolved '{2}', expected '{3}'", lord, rows[r][0], actual, nl.Expected));
                        return false;
                    }
                }
            }

            return true;
        }

        #region Hidden House Tests

        private class HiddenLord
        {
            public string Name;
            public string D3;
            public string D4;
            public string D7;
            public string D8;
            public bool IsNode;
        }

        // Reference chart used by TestNadiResolution above. D3 = house occupied,
        // D4 = houses owned, D7/D8 = sign lord occupied/owned (nodes only).
        private static readonly HiddenLord[] ReferenceLords = new HiddenLord[]
        {
            new HiddenLord { Name = "SU", D3 = "1",  D4 = "9",       D7 = "",  D8 = "" },
            new HiddenLord { Name = "MO", D3 = "6",  D4 = "8",       D7 = "",  D8 = "" },
            new HiddenLord { Name = "MA", D3 = "12", D4 = "5,12",    D7 = "",  D8 = "" },
            new HiddenLord { Name = "RA", D3 = "4",  D4 = "",        D7 = "12", D8 = "5,12", IsNode = true },
            new HiddenLord { Name = "JU", D3 = "9",  D4 = "1,4",     D7 = "",  D8 = "" },
            new HiddenLord { Name = "SA", D3 = "7",  D4 = "2,3",     D7 = "",  D8 = "" },
            new HiddenLord { Name = "ME", D3 = "1",  D4 = "7,10",    D7 = "",  D8 = "" },
            new HiddenLord { Name = "KE", D3 = "10", D4 = "",        D7 = "1", D8 = "6,11", IsNode = true },
            new HiddenLord { Name = "VE", D3 = "1",  D4 = "6,11",    D7 = "",  D8 = "" }
        };

        // Owned houses per planet, byte-identical to each lord's D4. A cusp's
        // in the chart owns the same signs; listed so the synthetic cusp rule set
        // below stays consistent with the reference chart.
        private static readonly string[] ReferenceSignLords = new string[]
        { "JU", "SA", "SA", "JU", "MA", "VE", "ME", "MO", "SU", "ME", "VE", "MA" };

        // Star Lord and Sub Lord chosen for each of the 12 house cusps of the
        // reference chart. They are fixed test inputs (the reference chart record
        // does not carry cusp lords) and are intentionally varied to exercise every
        // priority category of the hidden house algorithm.
        private static readonly string[] ReferenceCuspStarLords = new string[]
        { "SU", "MO", "JU", "MA", "ME", "VE", "SA", "KE", "SU", "RA", "SA", "JU" };

        private static readonly string[] ReferenceCuspSubLords = new string[]
        { "MA", "VE", "ME", "RA", "SU", "JU", "MO", "ME", "MA", "JU", "KE", "VE" };

        private static List<CuspData> BuildReferenceCusps()
        {
            List<CuspData> list = new List<CuspData>();
            for (int i = 0; i < 12; i++)
            {
                CuspData cd = new CuspData();
                cd.HouseNo = i + 1;
                cd.SignLord = ReferenceSignLords[i];
                cd.StarLord = ReferenceCuspStarLords[i];
                cd.SubLord = ReferenceCuspSubLords[i];
                list.Add(cd);
            }
            return list;
        }

        private static AstroChartData BuildReferenceChartData()
        {
            AstroChartData chart = new AstroChartData();
            chart.CuspList = BuildReferenceCusps();

            for (int i = 1; i <= 12; i++)
            {
                HouseSignificationData hsd = new HouseSignificationData();
                hsd.Planet = i.ToString();
                chart.HouseSignificationList.Add(hsd);
            }

            chart.PlanetList.Clear();
            for (int i = 0; i < ReferenceLords.Length; i++)
            {
                HiddenLord hl = ReferenceLords[i];
                chart.PlanetList.Add(new PlanetData { Name = hl.Name });

                HouseSignificationData hsd = new HouseSignificationData();
                hsd.Planet = hl.Name;
                hsd.D3 = hl.D3;
                hsd.D4 = hl.D4;
                hsd.D7 = hl.D7;
                hsd.D8 = hl.D8;
                chart.HouseSignificationList.Add(hsd);
            }

            return chart;
        }

        private static HiddenHouseResult ComputeHiddenFor(string strPlanet, AstroChartData chart)
        {
            HouseSignificationData hsd = null;
            for (int i = 0; i < chart.HouseSignificationList.Count; i++)
            {
                if (chart.HouseSignificationList[i].Planet == strPlanet)
                {
                    hsd = chart.HouseSignificationList[i];
                    break;
                }
            }

            bool isNode = HiddenHouseService.IsNode(strPlanet);
            return HiddenHouseService.ComputeForPlanet(
                strPlanet, hsd.D3, hsd.D4, hsd.D7, hsd.D8, isNode, chart.CuspList);
        }

        private static string Join(List<int> houses)
        {
            return HiddenHouseService.FormatHouseList(houses);
        }

        private static string JoinRange(ICollection<int> houses)
        {
            return Join(new List<int>(houses));
        }

        private static HashSet<int> HouseSet(params int[] houses)
        {
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < houses.Length; i++)
                set.Add(houses[i]);
            return set;
        }

        private static List<int> HouseList(params int[] houses)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < houses.Length; i++)
                list.Add(houses[i]);
            return list;
        }

        private static HashSet<int> ExistingNadiSet(string name)
        {
            AstroChartData chart = BuildReferenceChartData();
            HouseSignificationData hsd = null;
            for (int i = 0; i < chart.HouseSignificationList.Count; i++)
            {
                if (chart.HouseSignificationList[i].Planet == name)
                {
                    hsd = chart.HouseSignificationList[i];
                    break;
                }
            }

            bool isNode = HiddenHouseService.IsNode(name);
            string nadiText = NadiCalculationService.ResolveNadiCoordinates(hsd.D3, hsd.D4, hsd.D7, hsd.D8, isNode);
            HashSet<int> set = new HashSet<int>();
            List<int> nadis = HiddenHouseService.ParseHouseNumbers(nadiText);
            for (int i = 0; i < nadis.Count; i++)
                set.Add(nadis[i]);
            return set;
        }

        private static bool TestHiddenHouseSpecExamples()
        {
            // The exact regression cases from the feature specification.
            // SUN: SGN=[9], STL=[], SUB=[4], Posited=[1], Existing Nadi=[1,9] -> hidden [4].
            List<int> sunFinal = HiddenHouseService.ComputeHiddenHouses(
                HouseList(9), new List<int>(), HouseList(4), HouseList(1), HouseSet(1, 9));
            if (Join(sunFinal) != "4")
            {
                Fail(string.Format("SUN spec example: hidden '{0}', expected '4'", Join(sunFinal)));
                return false;
            }

            // MOON: SGN=[8], STL=[2,6,10], SUB=[3], Posited=[7], Existing Nadi=[6,8]
            //        -> hidden [2,3,7,10] (numerically sorted).
            List<int> moonFinal = HiddenHouseService.ComputeHiddenHouses(
                HouseList(8), HouseList(2, 6, 10), HouseList(3), HouseList(7), HouseSet(6, 8));
            if (Join(moonFinal) != "2, 3, 7, 10")
            {
                Fail(string.Format("MOON spec example: hidden '{0}', expected '2, 3, 7, 10'", Join(moonFinal)));
                return false;
            }

            // Duplicates within and across sources collapse.
            List<int> dupFinal = HiddenHouseService.ComputeHiddenHouses(
                HouseList(9, 9, 4), HouseList(4), new List<int>(), HouseList(1), HouseSet(1, 9));
            if (Join(dupFinal) != "4")
            {
                Fail(string.Format("duplicate sources: hidden '{0}', expected '4'", Join(dupFinal)));
                return false;
            }

            // STL and SUB both point at house 8; 8 is already significant -> nothing hidden.
            List<int> absorbedFinal = HiddenHouseService.ComputeHiddenHouses(
                HouseSet(8), HouseSet(8), new List<int>(), HouseSet(8), HouseSet(8));
            if (absorbedFinal.Count != 0)
            {
                Fail(string.Format("fully-absorbed spec: hidden '{0}', expected empty", Join(absorbedFinal)));
                return false;
            }

            return true;
        }

        private static bool TestHiddenHouseBasicChart()
        {
            AstroChartData chart = BuildReferenceChartData();

            // planet -> expected final hidden houses (numerically sorted, deduped, minus Nadi).
            string[][] expected = new string[][]
            {
                new string[] { "SU", "5" },
                new string[] { "MO", "2, 7" },
                new string[] { "MA", "1, 4, 9" },
                new string[] { "RA", "10" },
                new string[] { "JU", "3, 6, 10, 12" },
                new string[] { "SA", "11" },
                new string[] { "ME", "3, 5, 8" },
                new string[] { "KE", "8" },
                new string[] { "VE", "2, 12" }
            };

            for (int i = 0; i < expected.Length; i++)
            {
                string actual = Join(ComputeHiddenFor(expected[i][0], chart).Final);
                if (actual != expected[i][1])
                {
                    Fail(string.Format("{0}: final hidden houses '{1}', expected '{2}'", expected[i][0], actual, expected[i][1]));
                    return false;
                }
            }

            // MA exercises all four groups: SGN [5,12], STL [4], SUB [1,9], Posited [12].
            HiddenHouseResult ma = ComputeHiddenFor("MA", chart);
            if (Join(ma.Sgn) != "5, 12") { Fail("MA SGN != [5,12]"); return false; }
            if (Join(ma.Stl) != "4") { Fail("MA STL != [4]"); return false; }
            if (Join(ma.Sub) != "1, 9") { Fail("MA SUB != [1,9]"); return false; }
            if (Join(ma.Posited) != "12") { Fail("MA Posited != [12]"); return false; }
            if (Join(ma.Candidates) != "1, 4, 5, 9, 12") { Fail("MA candidates != sorted union [1,4,5,9,12]"); return false; }

            return true;
        }

        private static bool TestHiddenHouseNodes()
        {
            AstroChartData chart = BuildReferenceChartData();

            // RA is a node: its existing Nadi is D3(4) + D7(12) + D8(5,12). The sign-lord
            // houses (D7/D8) form its SGN; STL cusp10, SUB cusp4; only house 10 remains.
            HiddenHouseResult ra = ComputeHiddenFor("RA", chart);
            if (Join(ra.Sgn) != "5, 12")
            {
                Fail(string.Format("RA SGN (sign-lord houses) != [5,12], got '{0}'", Join(ra.Sgn)));
                return false;
            }
            if (Join(ra.Final) != "10")
            {
                Fail(string.Format("RA hidden houses '{0}', expected '10'", Join(ra.Final)));
                return false;
            }

            // KE is a node: existing D3(10) + D7(1) + D8(6,11). SUB cusp11, STL cusp8;
            // 11 already signified -> only 8 remains.
            HiddenHouseResult ke = ComputeHiddenFor("KE", chart);
            if (Join(ke.Final) != "8")
            {
                Fail(string.Format("KE hidden houses '{0}', expected '8'", Join(ke.Final)));
                return false;
            }

            return true;
        }

        private static bool TestHiddenHouseEdgeCases()
        {
            AstroChartData chart = BuildReferenceChartData();

            // Empty STL: an empty source contributes no candidates.
            List<int> noStl = HiddenHouseService.ComputeHiddenHouses(
                HouseSet(9), new List<int>(), HouseSet(4), HouseSet(1), HouseSet(1, 9));
            if (Join(noStl) != "4") { Fail("empty STL should not contribute houses"); return false; }

            // Every candidate already in Nadi -> no hidden houses, no empty brackets.
            List<int> all = HiddenHouseService.ComputeHiddenHouses(
                HouseSet(1, 4), HouseSet(6), new List<int>(), HouseSet(9), HouseSet(1, 4, 6, 9));
            if (all.Count != 0) { Fail("all-consumed chart should produce no hidden houses"); return false; }
            if (HiddenHouseService.FormatBracketString(all) != "") { Fail("empty hidden must render no brackets"); return false; }

            // Sun has no cusp where it is the Nakshatra Lord in the reference chart.
            HiddenHouseResult su = ComputeHiddenFor("SU", chart);
            if (su.Stl.Count == 0) { Fail("SU should have STL houses in the reference chart"); return false; }
            HiddenHouseResult sa = ComputeHiddenFor("SA", chart);
            if (sa.Sub.Count != 0) { Fail("SA is no Cuspal Sub Lord in the reference chart"); return false; }

            return true;
        }

        private static bool TestHiddenHouseDedupExisting()
        {
            AstroChartData chart = BuildReferenceChartData();

            for (int i = 0; i < ReferenceLords.Length; i++)
            {
                string name = ReferenceLords[i].Name;
                HiddenHouseResult result = ComputeHiddenFor(name, chart);

                // No duplicates and every final house comes from a candidate source.
                for (int j = 0; j < result.Final.Count; j++)
                {
                    int nHouse = result.Final[j];
                    if (!result.Candidates.Contains(nHouse))
                    {
                        Fail(string.Format("{0}: final house {1} is not among the candidates", name, nHouse));
                        return false;
                    }
                    if (result.Sgn.Contains(nHouse) == false &&
                        result.Stl.Contains(nHouse) == false &&
                        result.Sub.Contains(nHouse) == false &&
                        result.Posited.Contains(nHouse) == false)
                    {
                        Fail(string.Format("{0}: final house {1} belongs to no source group", name, nHouse));
                        return false;
                    }
                }

                for (int j = 0; j < result.Final.Count; j++)
                {
                    for (int k = j + 1; k < result.Final.Count; k++)
                    {
                        if (result.Final[j] == result.Final[k])
                        {
                            Fail(string.Format("{0}: duplicate house {1} in final list", name, result.Final[j]));
                            return false;
                        }
                    }
                }
            }

            // A planet must never hide a house that is already part of its Nadi coordinates.
            string[][] na = new string[][] { new string[] { "SU", "9" }, new string[] { "MA", "5" }, new string[] { "KE", "11" } };
            for (int i = 0; i < na.Length; i++)
            {
                string name = na[i][0];
                HiddenHouseResult result = ComputeHiddenFor(name, chart);
                HashSet<int> existing = ExistingNadiSet(name);
                for (int j = 0; j < result.Final.Count; j++)
                {
                    if (existing.Contains(result.Final[j]))
                    {
                        Fail(string.Format("{0}: house {1} belongs to existing Nadi but is still hidden", name, result.Final[j]));
                        return false;
                    }
                }
                if (existing.Contains(int.Parse(na[i][1])) == false)
                {
                    Fail(string.Format("{0}: control Nadi house {1} missing from existing set", name, na[i][1]));
                    return false;
                }
            }

            return true;
        }

        private static bool TestHiddenHouseFormatting()
        {
            List<int> houses = new List<int>();
            houses.Add(7);
            houses.Add(10);

            if (HiddenHouseService.FormatHouseList(houses) != "7, 10") { Fail("FormatHouseList(7,10) != '7, 10'"); return false; }
            if (HiddenHouseService.FormatBracketString(houses) != "(7, 10)") { Fail("FormatBracketString(7,10) != '(7, 10)'"); return false; }
            if (HiddenHouseService.FormatHouseList(null) != "") { Fail("FormatHouseList(null) should be empty"); return false; }
            if (HiddenHouseService.FormatBracketString(new List<int>()) != "") { Fail("empty bracket list should be empty string"); return false; }
            if (HiddenHouseService.IsNode("RA") != true) { Fail("IsNode(RA) != true"); return false; }
            if (HiddenHouseService.IsNode("KE") != true) { Fail("IsNode(KE) != true"); return false; }
            if (HiddenHouseService.IsNode("SU") != false) { Fail("IsNode(SU) != false"); return false; }
            if (HiddenHouseService.IsNode("") != false) { Fail("IsNode('') != false"); return false; }
            if (Join(HiddenHouseService.ParseHouseNumbers("1 9")) != "1, 9") { Fail("ParseHouseNumbers('1 9') != [1,9]"); return false; }
            if (Join(HiddenHouseService.ParseHouseNumbers("5, 12")) != "5, 12") { Fail("ParseHouseNumbers('5, 12') != [5,12]"); return false; }
            return true;
        }

        private static bool TestHiddenHouseChartShared()
        {
            AstroChartData chart = BuildReferenceChartData();
            Dictionary<string, HiddenHouseResult> dict = HiddenHouseService.ComputeForChart(chart);

            if (dict.Count != ReferenceLords.Length)
            {
                Fail(string.Format("ComputeForChart returned {0} planets, expected {1}", dict.Count, ReferenceLords.Length));
                return false;
            }

            // The signification and Nadi tables read from the same dictionary, so
            // the per-planet values must match the direct per-planet computation.
            for (int i = 0; i < ReferenceLords.Length; i++)
            {
                string name = ReferenceLords[i].Name;
                HiddenHouseResult chartResult;
                if (!dict.TryGetValue(name, out chartResult))
                {
                    Fail("ComputeForChart missing planet " + name);
                    return false;
                }

                HiddenHouseResult direct = ComputeHiddenFor(name, chart);
                if (Join(chartResult.Final) != Join(direct.Final))
                {
                    Fail(string.Format("{0}: chart-level final '{1}' differs from direct '{2}'", name, Join(chartResult.Final), Join(direct.Final)));
                    return false;
                }
            }

            return true;
        }

        private static bool TestHiddenHouseDynamic()
        {
            AstroChartData chart = BuildReferenceChartData();
            string originalSu = Join(ComputeHiddenFor("SU", chart).Final);
            string originalSa = Join(ComputeHiddenFor("SA", chart).Final);
            if (originalSu != "5") { Fail("control: SU hidden should start as '5'"); return false; }

            // Changing the Sun's owned house (D4) changes its SGN and its Nadi,
            // which changes the hidden houses even though the cusps stay fixed.
            for (int i = 0; i < chart.HouseSignificationList.Count; i++)
            {
                if (chart.HouseSignificationList[i].Planet == "SU")
                    chart.HouseSignificationList[i].D4 = "2";
            }

            string changedSu = Join(ComputeHiddenFor("SU", chart).Final);
            if (changedSu == originalSu || changedSu != "5, 9")
            {
                Fail(string.Format("SU D4 change produced '{0}', expected '5, 9'", changedSu));
                return false;
            }

            // Changing a cusp's Sub Lord changes the SUB group and the hidden list.
            for (int i = 0; i < chart.CuspList.Count; i++)
            {
                if (chart.CuspList[i].HouseNo == 11)
                    chart.CuspList[i].SubLord = "SU";
            }

            string cuspChanged = Join(ComputeHiddenFor("SU", chart).Final);
            if (cuspChanged == changedSu || cuspChanged != "5, 9, 11")
            {
                Fail(string.Format("cusp 11 SubLord change produced '{0}', expected '5, 9, 11'", cuspChanged));
                return false;
            }

            // SA is unaffected by the Sun's ownership change.
            if (Join(ComputeHiddenFor("SA", chart).Final) != originalSa)
            {
                Fail("SA hidden houses changed although its own data did not");
                return false;
            }

            return true;
        }

        #endregion
    }
}
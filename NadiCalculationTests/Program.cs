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
    }
}
/* HiddenHouseCalculator
   Console verification harness for the Hidden House Activation feature.
   Uses the SAME chart engine as the application (astroChart.CalculateChartData)
   so the hidden-house values printed here are exactly what the signification
   and Nadi tables would show for the stored charts in \Store.

   Build/run:
       MSBuild HiddenHouseCalculator.csproj
       HiddenHouseCalculator\bin\Release\HiddenHouseCalculator.exe

   It reads every 17-field chart record file in logicAstroKPCharts\bin\Release\Store,
   recomputes the charts through the real Swiss Ephemeris pipeline, and prints
   the dynamically computed hidden houses per planet together with a check that
   the charts' hidden houses differ from each other (proof that changing the
   birth data produces different hidden-house results). Exits with code 0 when
   every processed chart succeeded and the dynamism check passed, otherwise 2. */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using srlWebCom.Astro.AstroObjects;

namespace HiddenHouseCalculator
{
    static class Program
    {
        private static string m_strSWEParentFolder = "";
        private static string m_strSWEConFilePath = "";
        private static string m_strSWEFolderPath = "";

        static int Main(string[] args)
        {
            Console.WriteLine("Hidden House Calculator - real chart verification");
            Console.WriteLine("==================================================");

            int nResult = 0;

            astroGlobal.AstroSourcePath = "logicAstroKPCharts.AstroSource";

            if (!InitPaths())
                return 2;

            string strLoadError = "";
            if (astroStar.LoadStars(ref strLoadError) == false)
            {
                Console.WriteLine("Unable to load Degree-Positions.txt -> " + strLoadError);
                return 2;
            }

            m_listTimeZone = LoadTimeZoneList();

            string storeDir = Path.Combine(m_strSWEParentFolder, @"..\Store");
            if (!Directory.Exists(storeDir))
                storeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Store");

            string[] files = Directory.GetFiles(storeDir, "*.txt");
            if (files.Length == 0)
            {
                Console.WriteLine("No chart files found in " + storeDir);
                return 2;
            }

            Dictionary<string, string> chartFingerprints = new Dictionary<string, string>();

            foreach (string file in files)
            {
                string strStoreError = "";
                string strAstroData = BuildAstroDataFromStoreFile(file, out strStoreError);
                if (string.IsNullOrEmpty(strAstroData))
                {
                    Console.WriteLine("SKIP {0}: {1}", Path.GetFileName(file), strStoreError);
                    continue;
                }

                AstroChartData chartData = null;
                string strErrorMessage = "";
                if (CalculateChartDataFromInput(strAstroData, out chartData, ref strErrorMessage) == false)
                {
                    Console.WriteLine("FAIL {0}: {1}", Path.GetFileName(file), strErrorMessage);
                    nResult = 2;
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Chart: {0}  |  DOB: {1}  |  Place: {2}",
                    chartData.Name, chartData.DateTimeOfBirth, chartData.PlaceOfBirth);

                string fingerprint = PrintHiddenHouses(chartData);
                chartFingerprints[chartData.Name] = fingerprint;
            }

            Console.WriteLine();
            Console.WriteLine("==================================================");
            if (chartFingerprints.Count >= 2)
            {
                string[] names = new List<string>(chartFingerprints.Keys).ToArray();
                bool allDifferent = true;
                for (int i = 0; i < names.Length && allDifferent; i++)
                {
                    for (int j = i + 1; j < names.Length; j++)
                    {
                        if (chartFingerprints[names[i]] == chartFingerprints[names[j]])
                        {
                            allDifferent = false;
                            Console.WriteLine("Dynamism FAIL: charts '{0}' and '{1}' produced identical hidden houses.", names[i], names[j]);
                            nResult = 2;
                        }
                    }
                }
                if (allDifferent)
                    Console.WriteLine("Dynamism check PASSED: {0} charts all produce different hidden houses.", names.Length);
            }
            else
            {
                Console.WriteLine("Dynamism check SKIPPED (need at least 2 charts).");
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("RESULT: " + (nResult == 0 ? "ALL PASSED" : "FAILURES PRESENT"));
            return nResult;
        }

        private static ArrayList m_listTimeZone = new ArrayList();

        private class TZEntry
        {
            public int Minutes;
            public string Name;
        }

        private static bool InitPaths()
        {
            string appRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\logicAstroKPCharts\bin\Release"));
            if (!File.Exists(Path.Combine(appRoot, "swiss-eph", "swetest.exe")))
                appRoot = AppDomain.CurrentDomain.BaseDirectory;

            m_strSWEParentFolder = Path.Combine(appRoot, "swiss-eph");
            m_strSWEConFilePath = Path.Combine(appRoot, "swiss-eph", "swetest.exe");
            m_strSWEFolderPath = Path.Combine(appRoot, "swiss-eph", "Eph");

            if (!Directory.Exists(m_strSWEParentFolder))
            {
                Console.WriteLine("Swiss Ephemeris parent folder missing at " + m_strSWEParentFolder);
                return false;
            }
            if (!File.Exists(m_strSWEConFilePath))
            {
                Console.WriteLine("Swiss Ephemeris executable missing at " + m_strSWEConFilePath);
                return false;
            }
            if (!Directory.Exists(m_strSWEFolderPath))
            {
                Console.WriteLine("Swiss Ephemeris data folder missing at " + m_strSWEFolderPath);
                return false;
            }
            return true;
        }

        private static ArrayList LoadTimeZoneList()
        {
            ArrayList list = new ArrayList();
            try
            {
                Assembly _assembly = Assembly.GetExecutingAssembly();
                string strResourceFile = string.Format("{0}.TimeZoneList.txt", astroGlobal.AstroSourcePath);
                using (StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream(strResourceFile)))
                {
                    string strTimeZoneData = _textStreamReader.ReadToEnd();
                    strTimeZoneData = strTimeZoneData.Replace("\r", "\n").Replace("\n\n", "\n");
                    string[] TSElements = strTimeZoneData.Split('\n');
                    foreach (string TSElement in TSElements)
                    {
                        string[] TimeZoneElements = TSElement.Split('|');
                        if (TimeZoneElements.Length == 2)
                        {
                            TZEntry entry = new TZEntry();
                            entry.Minutes = Convert.ToInt32(TimeZoneElements[0]);
                            entry.Name = TimeZoneElements[1];
                            list.Add(entry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TimeZoneList load warning: " + ex.Message);
            }
            return list;
        }

        private static int GetTimeZoneMinutes(string strName)
        {
            foreach (object item in m_listTimeZone)
            {
                TZEntry entry = (TZEntry)item;
                if (string.Equals(entry.Name, strName, StringComparison.OrdinalIgnoreCase))
                    return entry.Minutes;
            }
            return 0;
        }

        /// <summary>Mirrors BirthDataForm.LoadChartFile + btnGenerate_Click field mapping.</summary>
        private static string BuildAstroDataFromStoreFile(string strFile, out string strError)
        {
            strError = "";
            try
            {
                string strFileData = File.ReadAllText(strFile);
                string[] parts = strFileData.Split('|');
                if (parts.Length < 16)
                {
                    strError = "file is not a compliance chart record (<16 fields)";
                    return "";
                }

                string strName = parts[0];
                string strGender = parts[1];
                int nDay = Convert.ToInt32(parts[2]);
                int nMonth = Convert.ToInt32(parts[3]);
                int nYear = Convert.ToInt32(parts[4]);
                int nHours = Convert.ToInt32(parts[5]);
                int nMinutes = Convert.ToInt32(parts[6]);
                int nSeconds = Convert.ToInt32(parts[7]);

                int nTimeZoneMinutes = GetTimeZoneMinutes(parts[8]);
                string strTimeZoneSign = nTimeZoneMinutes < 0 ? "+" : "-";
                int nAbsMinutes = nTimeZoneMinutes < 0 ? -nTimeZoneMinutes : nTimeZoneMinutes;

                string[] strLocationParts = parts[9].Split('~');
                string strPOB = strLocationParts.Length > 0 ? strLocationParts[0] : "";
                string strSOB = strLocationParts.Length > 1 ? strLocationParts[1] : "";
                string strCOB = strLocationParts.Length > 2 ? strLocationParts[2] : "";
                string strLocationOfBirth = string.Format("{0}, {1}, {2}", strPOB.Trim(), strSOB.Trim(), strCOB.Trim());

                string strLonDirection = parts[10];
                int nLonDegrees = Convert.ToInt32(parts[11]);
                int nLonMinutes = Convert.ToInt32(parts[12]);
                string strLatDirection = parts[13];
                int nLatDegrees = Convert.ToInt32(parts[14]);
                int nLatMinutes = Convert.ToInt32(parts[15]);

                string strAstroData = string.Format(
                    "{0}|{1}|{2}-{3}-{4} {5}.{6}.{7}|{8}{9:00}.{10:00}|{11}|{12} {13:000}.{14:00}|{15} {16:00}.{17:00}|Standard Time|0\n",
                    strName, strGender, nYear, nMonth, nDay, nHours, nMinutes, nSeconds,
                    strTimeZoneSign, nAbsMinutes / 60, nAbsMinutes % 60, strLocationOfBirth,
                    strLonDirection, nLonDegrees, nLonMinutes, strLatDirection, nLatDegrees, nLatMinutes);

                return strAstroData;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return "";
            }
        }

        /// <summary>Mirrors SoftwarePanelForm.CalculateChartDataFromInput.</summary>
        private static bool CalculateChartDataFromInput(string strAstroData, out AstroChartData chartData, ref string strErrorMessage)
        {
            chartData = null;
            string strData = "";
            astroPerson apObj = new astroPerson();

            try
            {
                strErrorMessage = "";

                string[] strFileLineElements = strAstroData.Split('|');
                if (strFileLineElements.Length != 9)
                {
                    strErrorMessage = string.Format("Invalid record found in data\r\nRecord:" + strAstroData);
                    return false;
                }

                apObj.Clear();
                apObj.Name = strFileLineElements[0].Trim();
                apObj.Sex = strFileLineElements[1].Trim();
                apObj.BirthDateTime = DateTime.Parse(strFileLineElements[2].Trim().Replace(".", ":"),
                    CultureInfo.CreateSpecificCulture("ta-IN"), DateTimeStyles.None);

                strData = strFileLineElements[3].Trim();
                apObj.TimeZoneValue = strData;
                int nTZHours = Convert.ToInt32(strData.Substring(1, 2));
                int nTZMinutes = Convert.ToInt32(strData.Substring(4, 2));
                if (strData[0] == '-')
                {
                    apObj.TimeZoneDifference -= TimeSpan.Parse(string.Format("0.{0}:{1}:00", nTZHours, nTZMinutes));
                }
                else
                {
                    apObj.TimeZoneDifference += TimeSpan.Parse(string.Format("0.{0}:{1}:00", nTZHours, nTZMinutes));
                }

                apObj.PlaceOfBirth = strFileLineElements[4].Trim();

                strData = strFileLineElements[5].Trim();
                apObj.Longitude = strData;
                apObj.LongitudeDegrees = Convert.ToInt32(strData.Substring(5, 3));
                apObj.LongitudeMinutes = Convert.ToInt32(strData.Substring(9, 2));
                if (strData.Substring(0, 4) == "WEST")
                {
                    apObj.LongitudeDegrees *= -1;
                    apObj.LongitudeMinutes *= -1;
                }

                strData = strFileLineElements[6].Trim();
                apObj.Latitude = strData;
                apObj.LatitudeDegrees = Convert.ToInt32(strData.Substring(6, 2));
                apObj.LatitudeMinutes = Convert.ToInt32(strData.Substring(9, 2));
                if (strData.Substring(0, 5) == "SOUTH")
                {
                    apObj.LatitudeDegrees *= -1;
                    apObj.LatitudeMinutes *= -1;
                }

                apObj.TimeZoneName = strFileLineElements[7].Trim();
                apObj.HourCorrection = Convert.ToInt32(strFileLineElements[8].Trim());

                apObj.BirthDateTimeUTC = apObj.BirthDateTime + apObj.TimeZoneDifference;
                if (apObj.HourCorrection > 0)
                {
                    apObj.BirthDateTimeUTC -= new TimeSpan(apObj.HourCorrection, 0, 0);
                }

                astroChart acObj = new astroChart();

                astroChart.SwissPath = m_strSWEParentFolder;
                astroChart.SWEConFilePath = m_strSWEConFilePath;
                astroChart.SWEFolderPath = m_strSWEFolderPath;
                astroChart.HousingSystem = "P";
                astroChart.PrintAyanamsaCalculation = 0;
                astroChart.PrintAstroTables = 0;
                astroChart.PrintDasaTables = 1;

                return acObj.CalculateChartData(ref apObj, 999, out chartData, ref strErrorMessage);
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
            }

            return false;
        }

        private static string PrintHiddenHouses(AstroChartData chartData)
        {
            Dictionary<string, HiddenHouseResult> hidden = HiddenHouseService.ComputeForChart(chartData);

            List<string> fingerprint = new List<string>();

            foreach (PlanetData pd in chartData.PlanetList)
            {
                string cleanName = pd.Name;
                HouseSignificationData hsd = FindPlanetSignification(chartData.HouseSignificationList, cleanName);
                if (hsd == null)
                    continue;

                bool isNode = HiddenHouseService.IsNode(cleanName);
                string existing = NadiCalculationService.ResolveNadiCoordinates(hsd.D3, hsd.D4, hsd.D7, hsd.D8, isNode);

                HiddenHouseResult result;
                if (!hidden.TryGetValue(cleanName, out result))
                    continue;

                string bracket = HiddenHouseService.FormatBracketString(result.Final);
                Console.WriteLine("  {0,4}  existing [{1}]  hidden {2}", cleanName, existing, bracket.Length > 0 ? bracket : "(none)");

                bool bOverlap = false;
                for (int i = 0; i < result.Final.Count; i++)
                {
                    if (IsHouseInList(result.Final[i], existing))
                    {
                        bOverlap = true;
                        Console.WriteLine("      >>> OVERLAP: house {0} already signified by {1}", result.Final[i], cleanName);
                    }
                }
                if (bOverlap)
                {
                    Console.WriteLine("      >>> FAIL: hidden houses overlap existing significations for " + cleanName);
                    fingerprint.Add(cleanName + "=FAIL");
                }
                else
                {
                    fingerprint.Add(cleanName + "=" + HiddenHouseService.FormatHouseList(result.Final));
                }
            }

            return string.Join(" | ", fingerprint.ToArray());
        }

        private static HouseSignificationData FindPlanetSignification(List<HouseSignificationData> list, string planetName)
        {
            if (list == null || planetName == null)
                return null;

            string target = planetName.Replace("#", "").Replace("*", "").Trim().ToUpperInvariant();
            for (int i = 0; i < list.Count; i++)
            {
                HouseSignificationData hsd = list[i];
                if (hsd == null || hsd.Planet == null)
                    continue;

                string hsdPlanet = hsd.Planet.Replace("#", "").Replace("*", "").Trim().ToUpperInvariant();
                if (hsdPlanet == target)
                    return hsd;
            }
            return null;
        }

        private static bool IsHouseInList(int nHouse, string strList)
        {
            if (string.IsNullOrEmpty(strList))
                return false;

            string[] parts = strList.Split(' ');
            for (int i = 0; i < parts.Length; i++)
            {
                int n;
                if (int.TryParse(parts[i].Trim(), out n) && n == nHouse)
                    return true;
            }
            return false;
        }
    }
}
/* NadiPaintPreview
   Diagnostic harness that renders the REAL ChartResultsUserControl (the same
   control hosted by SoftwarePanelForm) for the Krish store chart and saves
   screenshots to PNG so the hidden-house green rendering can be inspected
   visually. Also prints each Sgn Nadi cell value that carries the hidden
   segment.

   The chart is built through the application's own private
   SoftwarePanelForm.CalculateChartDataFromInput method (via reflection) so
   the preview exercises the exact same pipeline as the running app.

   Build/run (from repo root):
       MSBuild NadiPaintPreview.csproj /p:Configuration=Release
       Copy the files from logicAstroKPCharts\bin\Release next to the exe, then
       NadiPaintPreview\bin\Release\NadiPaintPreview.exe
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using logicAstroKPCharts;
using srlWebCom.Astro.AstroObjects;

namespace NadiPaintPreview
{
    static class Program
    {
        private static string m_strAppRoot = "";
        private static ArrayList m_listTimeZone = new ArrayList();

        [STAThread]
        static int Main(string[] args)
        {
            Console.WriteLine("NadiPaintPreview - renders the real ChartResultsUserControl to PNG");
            Console.WriteLine("==================================================================");

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

            string chrFile = Path.Combine(m_strAppRoot, "Store", "Krish.txt");
            if (!File.Exists(chrFile))
            {
                Console.WriteLine("Store chart not found: " + chrFile);
                return 2;
            }

            string strStoreError = "";
            string strAstroData = BuildAstroDataFromStoreFile(chrFile, out strStoreError);
            if (string.IsNullOrEmpty(strAstroData))
            {
                Console.WriteLine("Store read failed: " + strStoreError);
                return 2;
            }

            AstroChartData chartData = null;
            string strErrorMessage = "";
            using (SoftwarePanelForm form = new SoftwarePanelForm())
            {
                MethodInfo mi = typeof(SoftwarePanelForm).GetMethod("CalculateChartDataFromInput",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                object[] argsIn = new object[] { strAstroData, null, "" };
                try
                {
                    bool ok = (bool)mi.Invoke(form, argsIn);
                    chartData = (AstroChartData)argsIn[1];
                    strErrorMessage = (string)argsIn[2];
                    if (!ok)
                    {
                        Console.WriteLine("Chart calculation failed: " + strErrorMessage);
                        return 2;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Reflection chart call failed: " + ex);
                    return 2;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Chart: {0}  |  DOB: {1}  |  Place: {2}",
                chartData.Name, chartData.DateTimeOfBirth, chartData.PlaceOfBirth);

            Dictionary<string, HiddenHouseResult> hidden = HiddenHouseService.ComputeForChart(chartData);
            foreach (PlanetData pd in chartData.PlanetList)
            {
                HouseSignificationData hsd = FindPlanetSignification(chartData.HouseSignificationList, pd.Name);
                if (hsd == null) continue;
                bool isNode = HiddenHouseService.IsNode(pd.Name);
                string existing = NadiCalculationService.ResolveNadiCoordinates(hsd.D3, hsd.D4, hsd.D7, hsd.D8, isNode);
                string bracket = "";
                HiddenHouseResult result;
                if (hidden.TryGetValue(pd.Name, out result) && result.Final.Count > 0)
                    bracket = HiddenHouseService.FormatBracketString(result.Final);
                Console.WriteLine("  {0,2}  existing [{1}]  hidden {2}", pd.Name, existing,
                    bracket.Length > 0 ? bracket : "(none)");
            }

            string outDir = args.Length > 0 ? args[0] : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.CreateDirectory(outDir);

            ChartResultsUserControl ctrl = new ChartResultsUserControl();
            ctrl.Size = new Size(1280, 1150);
            ctrl.LoadChartData(chartData);
            ctrl.CreateControl();

            Console.WriteLine();
            DumpNadiValues(ctrl);

            Bitmap bmpFull = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmpFull, new Rectangle(0, 0, ctrl.Width, ctrl.Height));
            string fullPath = Path.Combine(outDir, "preview_full.png");
            bmpFull.Save(fullPath, ImageFormat.Png);
            Console.WriteLine("Saved " + fullPath);

            DataGridView dgvNadi = GetField(ctrl, "dgvNadi") as DataGridView;
            if (dgvNadi != null && dgvNadi.Rows.Count > 0)
            {
                Bitmap bmpNadi = new Bitmap(dgvNadi.Width, dgvNadi.Height);
                dgvNadi.DrawToBitmap(bmpNadi, new Rectangle(0, 0, dgvNadi.Width, dgvNadi.Height));
                string nadiPath = Path.Combine(outDir, "preview_nadi.png");
                bmpNadi.Save(nadiPath, ImageFormat.Png);
                Console.WriteLine("Saved " + nadiPath);
            }
            else
            {
                Console.WriteLine("WARNING: dgvNadi not found via reflection or has no rows.");
            }

            Console.WriteLine("==== RESULT: screenshots written to " + outDir);
            return 0;
        }

        private static void DumpNadiValues(ChartResultsUserControl ctrl)
        {
            DataGridView dgvNadi = GetField(ctrl, "dgvNadi") as DataGridView;
            if (dgvNadi == null)
            {
                Console.WriteLine("WARNING: dgvNadi not found.");
                return;
            }
            Console.WriteLine("dgvNadi rows (planet | SgnNadi cell value | contains separator):");
            for (int i = 0; i < dgvNadi.Rows.Count; i++)
            {
                object v = dgvNadi.Rows[i].Cells[2].Value;
                string s = v as string;
                bool hasSep = !string.IsNullOrEmpty(s) && s.IndexOf('\u0001') >= 0;
                string printable = (s ?? "").Replace("\u0001", "<SEP>");
                Console.WriteLine("  [{0,2}] {1,-6} value='{2}'  sep={3}", i,
                    dgvNadi.Rows[i].Cells[0].Value, printable, hasSep);
            }
        }

        private static object GetField(object target, string fieldName)
        {
            return target.GetType().GetField(fieldName,
                BindingFlags.Instance | BindingFlags.NonPublic).GetValue(target);
        }

        private static bool InitPaths()
        {
            string appRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\logicAstroKPCharts\bin\Release"));
            if (!File.Exists(Path.Combine(appRoot, "swiss-eph", "swetest.exe")))
                appRoot = AppDomain.CurrentDomain.BaseDirectory;

            m_strAppRoot = appRoot;
            Directory.SetCurrentDirectory(appRoot);

            if (!Directory.Exists(Path.Combine(appRoot, "swiss-eph")))
            {
                Console.WriteLine("Swiss Ephemeris parent folder missing at " + Path.Combine(appRoot, "swiss-eph"));
                return false;
            }
            return true;
        }

        private static ArrayList LoadTimeZoneList()
        {
            ArrayList list = new ArrayList();
            try
            {
                Assembly asm = typeof(SoftwarePanelForm).Assembly;
                using (StreamReader reader = new StreamReader(
                    asm.GetManifestResourceStream("logicAstroKPCharts.AstroSource.TimeZoneList.txt")))
                {
                    string strTimeZoneData = reader.ReadToEnd();
                    strTimeZoneData = strTimeZoneData.Replace("\r", "\n");
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

        private class TZEntry
        {
            public int Minutes;
            public string Name;
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
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using srlWebCom.Astro.AstroObjects;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Layout.Renderer;

namespace logicAstroKPCharts
{
    public partial class SoftwarePanelForm : Form
    {
        private ChartResultsUserControl m_chartResultsControl;
        private AstroChartData m_currentChartData;
        private string m_currentAstroData = "";
        private string m_originalAstroData = "";
        private int m_nAyanamsa = 999;
        private string m_strSWEParentFolder = "";
        private string m_strSWEConFilePath = "";
        private string m_strSWEFolderPath = "";
        private string m_strChartStorePath = "";
        private string m_strPDFStorePath = "";
        private string m_strHousingSystem = "P";
        private const string SETTINGS_FILE = "settings.dat";

        public SoftwarePanelForm()
        {
            InitializeComponent();
            this.Icon = AppIcon.Logo;
            InitPaths();
            LoadSettings();
        }

        private void InitPaths()
        {
            m_strSWEParentFolder = string.Format(@"{0}\swiss-eph", Directory.GetCurrentDirectory());
            m_strSWEConFilePath = string.Format(@"{0}\swiss-eph\swetest.exe", Directory.GetCurrentDirectory());
            m_strSWEFolderPath = string.Format(@"{0}\swiss-eph\Eph", Directory.GetCurrentDirectory());
            m_strChartStorePath = string.Format(@"{0}\Store", Directory.GetCurrentDirectory());
            m_strPDFStorePath = string.Format(@"{0}\PDF", Directory.GetCurrentDirectory());

            if (!Directory.Exists(m_strChartStorePath))
                Directory.CreateDirectory(m_strChartStorePath);
            if (!Directory.Exists(m_strPDFStorePath))
                Directory.CreateDirectory(m_strPDFStorePath);
        }

        private void LoadSettings()
        {
            try
            {
                string settingsPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), SETTINGS_FILE);
                if (File.Exists(settingsPath))
                {
                    string[] lines = File.ReadAllLines(settingsPath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();
                            if (key == "HousingSystem")
                                m_strHousingSystem = value;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void ShowChartInMainPanel(AstroChartData chartData, string astroData)
        {
            m_originalAstroData = astroData;
            m_currentAstroData = astroData;
            m_currentChartData = chartData;
            panelMain.Controls.Clear();

            m_chartResultsControl = new ChartResultsUserControl();
            m_chartResultsControl.Dock = DockStyle.Fill;
            m_chartResultsControl.LoadChartData(chartData);
            m_chartResultsControl.SetBtrEngine(RecomputeChartForBtr);
            panelMain.Controls.Add(m_chartResultsControl);

            menuExportPDF.Enabled = true;
            statusLabel.Text = string.Format("Chart loaded: {0}", chartData.Name);
        }

        private AstroChartData RecomputeChartForBtr(DateTime newBirthTime)
        {
            if (string.IsNullOrEmpty(m_originalAstroData)) return null;

            string strNewAstroData = BuildAstroDataWithBirthTime(m_originalAstroData, newBirthTime);
            if (string.IsNullOrEmpty(strNewAstroData)) return null;

            string strErrorMessage = "";
            AstroChartData newChartData = null;
            if (CalculateChartDataFromInput(strNewAstroData, out newChartData, ref strErrorMessage) == false)
            {
                MessageBox.Show(strErrorMessage, "BTR - Recalculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            m_currentAstroData = strNewAstroData;
            m_currentChartData = newChartData;
            statusLabel.Text = string.Format("BTR: {0}", newBirthTime.ToString("dd-MM-yyyy HH:mm:ss"));
            return newChartData;
        }

        private static string BuildAstroDataWithBirthTime(string strAstroData, DateTime birthTime)
        {
            if (string.IsNullOrEmpty(strAstroData)) return null;

            string strTrimmed = strAstroData.EndsWith("\n") ? strAstroData.Substring(0, strAstroData.Length - 1) : strAstroData;
            string[] elements = strTrimmed.Split('|');
            if (elements.Length != 9) return null;

            elements[2] = string.Format("{0}-{1}-{2} {3}.{4}.{5}",
                birthTime.Year, birthTime.Month, birthTime.Day,
                birthTime.Hour, birthTime.Minute, birthTime.Second);

            return string.Join("|", elements) + "\n";
        }

        private void ClearChart()
        {
            m_currentChartData = null;
            m_currentAstroData = "";
            m_originalAstroData = "";
            panelMain.Controls.Clear();
            menuExportPDF.Enabled = false;
            statusLabel.Text = "Ready";
        }

        private void menuEnterBirthData_Click(object sender, EventArgs e)
        {
            try
            {
                BirthDataForm birthDataForm = new BirthDataForm();
                if (birthDataForm.ShowDialog(this) == DialogResult.OK)
                {
                    m_nAyanamsa = birthDataForm.GeneratedAyanamsa;
                    ShowChartInMainPanel(birthDataForm.GeneratedChartData, birthDataForm.GeneratedAstroData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Enter Birth Data - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuEditBirthData_Click(object sender, EventArgs e)
        {
            try
            {
                BirthDataForm birthDataForm = new BirthDataForm(loadChartOnShow: true);
                if (birthDataForm.ShowDialog(this) == DialogResult.OK)
                {
                    m_nAyanamsa = birthDataForm.GeneratedAyanamsa;
                    ShowChartInMainPanel(birthDataForm.GeneratedChartData, birthDataForm.GeneratedAstroData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open Saved Chart - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Software Settings will be available in a future update.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuExportPDF_Click(object sender, EventArgs e)
        {
            if (m_currentChartData == null || string.IsNullOrEmpty(m_currentAstroData))
            {
                MessageBox.Show("No chart data to export. Please generate a chart first.", "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string strErrorMessage = "";
                string strPDFFile = string.Format(@"{0}\{1}.pdf", m_strPDFStorePath, m_currentChartData.Name);

                if (BuildAstroCharts(m_currentAstroData, strPDFFile, ref strErrorMessage) == false)
                {
                    MessageBox.Show(strErrorMessage, "PDF Generation - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    System.Diagnostics.Process.Start(strPDFFile);
                    statusLabel.Text = string.Format("PDF exported: {0}", strPDFFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PDF Generation - Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuClearChart_Click(object sender, EventArgs e)
        {
            ClearChart();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region PDF Generation

        private bool CalculateChartDataFromInput(string strAstroData, out AstroChartData chartData, ref string strErrorMessage)
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
                apObj.BirthDateTime = DateTime.Parse(strFileLineElements[2].Trim().Replace(".", ":"), CultureInfo.CreateSpecificCulture("ta-IN"), DateTimeStyles.None);

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
                astroChart.HousingSystem = m_strHousingSystem;
                astroChart.PrintAyanamsaCalculation = 0;
                astroChart.PrintAstroTables = 0;
                astroChart.PrintDasaTables = 1;

                if (acObj.CalculateChartData(ref apObj, m_nAyanamsa, out chartData, ref strErrorMessage) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
            }

            return false;
        }

        private bool BuildAstroCharts(string strAstroData, string strPDFFile, ref string strErrorMessage)
        {
            bool bResult = false;
            string strData = "";
            astroPerson apObj = new astroPerson();

            PdfWriter pdfWriterObj = null;
            PdfDocument pdfDoc = null;
            iText.Layout.Document docObj = null;

            try
            {
                strErrorMessage = "";

                pdfWriterObj = new PdfWriter(strPDFFile);
                pdfDoc = new PdfDocument(pdfWriterObj);
                pdfDoc.SetDefaultPageSize(PageSize.A4);
                pdfDoc.GetCatalog().SetPageLayout(PdfName.SinglePage);
                docObj = new iText.Layout.Document(pdfDoc);

                string[] strFileLineElements = strAstroData.Split('|');

                if (strFileLineElements.Length != 9)
                {
                    strErrorMessage = string.Format("Invalid record found in data\r\nRecord:" + strAstroData);
                    return false;
                }

                apObj.Clear();

                apObj.Name = strFileLineElements[0].Trim();
                apObj.Sex = strFileLineElements[1].Trim();
                apObj.BirthDateTime = DateTime.Parse(strFileLineElements[2].Trim().Replace(".", ":"), CultureInfo.CreateSpecificCulture("ta-IN"), DateTimeStyles.None);

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
                astroChart.HousingSystem = m_strHousingSystem;
                astroChart.PrintAyanamsaCalculation = 0;
                astroChart.PrintAstroTables = 0;
                astroChart.PrintDasaTables = 1;

                if (acObj.AstroSwissEphemerisService(ref apObj, 999, ref docObj, ref strErrorMessage) == true)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }

                return bResult;
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message;
            }
            finally
            {
                if (docObj != null) { docObj.Close(); }
                docObj = null;
                pdfWriterObj = null;
                pdfDoc = null;
            }

            return false;
        }

        #endregion
    }
}

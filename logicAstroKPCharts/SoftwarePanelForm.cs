using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace logicAstroKPCharts
{
    public partial class SoftwarePanelForm : Form
    {
        public SoftwarePanelForm()
        {
            InitializeComponent();
        }

        private void menuEnterBirthData_Click(object sender, EventArgs e)
        {
            try
            {
                BirthDataForm birthDataForm = new BirthDataForm();
                birthDataForm.ShowDialog(this);
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
                birthDataForm.ShowDialog(this);
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

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

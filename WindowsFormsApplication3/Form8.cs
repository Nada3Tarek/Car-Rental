using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using CrystalDecisions.Shared;

namespace WindowsFormsApplication3
{
    public partial class Form8 : Form
    {
        CrystalReport2 cr;
        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the Crystal Report
                cr = new CrystalReport2();

                // Set parameter values
                cr.SetParameterValue("vehicle number", textBox1.Text); // Assuming the first parameter name is "ParameterName1"
                cr.SetParameterValue("cus_id", textBox2.Text); // Assuming the second parameter name is "ParameterName2"

                // Set the Crystal Report as the ReportSource for the CrystalReportViewer
                crystalReportViewer1.ReportSource = cr;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }
    }
}

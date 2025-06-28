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

namespace WindowsFormsApplication3
{
    public partial class Form5 : Form
    {

        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;
        string ordb = "Data Source=ORCL1;User Id=hr;Password=hr;";
        OracleConnection conn;
        public Form5()
        {
            conn = new OracleConnection(ordb);
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
{
    
}

        
        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string constr = "Data Source=ORCL1; User Id=hr; Password=hr;";
            string cmdstr = "SELECT customer_id, vehicle_id, rental_period_start, rental_period_end FROM Bookings WHERE customer_id = :cus_id";

            // Ensure adapter is initialized only once, ideally in the constructor or using a lazy initialization pattern.
            if (adapter == null)
            {
                adapter = new OracleDataAdapter(cmdstr, constr);
                adapter.SelectCommand.Parameters.Add("cus_id", OracleDbType.Varchar2);
            }

            adapter.SelectCommand.Parameters["cus_id"].Value = textBox2.Text;

            // If ds is not null, clear it, else initialize it
            if (ds == null)
            {
                ds = new DataSet();
            }
            else
            {
                ds.Clear();
            }

            // Fill the DataSet and bind it to the DataGridView
            adapter.Fill(ds);
            dataGridView2.DataSource = null; // Clear previous binding
            dataGridView2.DataSource = ds.Tables[0];
        }
        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                // Ensure the DataSet is not null and contains at least one DataTable
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable table = ds.Tables[0];

                    // Set the primary key for the DataTable
                    table.PrimaryKey = new DataColumn[] { table.Columns["booking_id"] };

                    // Initialize the OracleCommandBuilder
                    if (builder == null)
                    {
                        builder = new OracleCommandBuilder(adapter);
                    }

                    // Update the database using OracleCommandBuilder
                    adapter.Update(table);

                    MessageBox.Show("Changes saved successfully.");
                }
                else
                {
                    MessageBox.Show("DataSet is null or does not contain any tables.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save changes: " + ex.Message);
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }
    }
}

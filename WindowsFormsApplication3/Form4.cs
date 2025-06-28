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
    public partial class Form4 : Form
    {
        string ordb = "Data Source=ORCL1;User Id=hr;Password=hr;";
        OracleConnection conn;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            if (conn == null)
            {
                MessageBox.Show("Database connection is not initialized.");
                return;
            }
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetModelID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_Model", OracleDbType.Varchar2).Value = textBox1.Text;
                cmd.Parameters.Add("p_MID", OracleDbType.RefCursor, ParameterDirection.Output);

                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            comboBox1.Items.Add(dr[0].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime endDate = Convert.ToDateTime(textBox4.Text);
            DateTime startDate = Convert.ToDateTime(textBox3.Text);


            TimeSpan duration = endDate - startDate;
            int days = (int)duration.TotalDays;  


            int newcost = days * 5;


            textBox5.Text = newcost.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                int maxId, newId;

                // Retrieve the highest current rental ID and calculate the next one
                OracleCommand cmd = new OracleCommand("GetRentID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                maxId = Convert.ToInt32(cmd.Parameters["id"].Value.ToString());
                newId = maxId + 1;

                // Parse dates from text boxes
                DateTime startDate;
                DateTime endDate;
                if (!DateTime.TryParse(textBox3.Text, out startDate) || !DateTime.TryParse(textBox4.Text, out endDate))
                {
                    MessageBox.Show("Invalid date format. Please enter dates in the correct format.");
                    return; // Exit the method if date parsing fails
                }

                // Convert cost to decimal (assuming it's a decimal value)
                decimal cost;
                if (!decimal.TryParse(textBox5.Text, out cost))
                {
                    MessageBox.Show("Invalid cost format. Please enter a valid decimal number.");
                    return; // Exit the method if cost parsing fails
                }

                // Insert the new rental record using the new inputs
                OracleCommand c = new OracleCommand("Insert_Rental", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                c.Parameters.Add("RID", OracleDbType.Int32).Value = newId;
                c.Parameters.Add("cus_id", OracleDbType.Varchar2).Value = textBox2.Text; // Customer ID now from textBox2
                c.Parameters.Add("veh_id", OracleDbType.Varchar2).Value = comboBox1.SelectedItem.ToString();
                c.Parameters.Add("start_date", OracleDbType.Date).Value = startDate;
                c.Parameters.Add("end_date", OracleDbType.Date).Value = endDate;
                c.Parameters.Add("cost", OracleDbType.Decimal).Value = cost; // Assuming cost is a decimal value
                c.ExecuteNonQuery();
                MessageBox.Show("Rental Info stored successfully");

                // Update vehicle status
                OracleCommand c2 = new OracleCommand("COMMIT_SATUT", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                c2.Parameters.Add("Car_id", OracleDbType.Varchar2).Value = comboBox1.SelectedItem.ToString();
                c2.ExecuteNonQuery();
                MessageBox.Show("Vehicle status updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }
    }
}

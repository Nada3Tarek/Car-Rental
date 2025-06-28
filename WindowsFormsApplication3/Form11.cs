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
    public partial class Form11 : Form
    {
        string ordb = "Data Source=ORCL1; User id=hr; Password=hr;";
        OracleConnection conn;
        public Form11()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                // Validate if the input in textBox2 is a valid number

                //int temp;
                //if (!int.TryParse(textBox2.Text, out temp))
                //{
                   // MessageBox.Show("Invalid Admin_Password. Please enter a valid number.");
                   // return;
                //}

                conn = new OracleConnection(ordb);
                conn.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select USERNAME from ADMINS where PASSWORD = :password";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("password", textBox2.Text);

                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string cus_name = dr["USERNAME"].ToString();
                    if (textBox1.Text == cus_name)
                    {
                        MessageBox.Show("Welcome");
                    }
                }
                else
                {
                    MessageBox.Show("Password not found");
                }
                dr.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Oracle Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            Form12 form12 = new Form12();
            form12.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

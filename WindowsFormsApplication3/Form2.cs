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
    public partial class Form2 : Form
    {
        string ordb = "Data Source=ORCL1; User id=hr; Password=hr;";
        OracleConnection conn;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void form2_closing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Customers (customer_id, name, license_number, cellphone) VALUES (:id,:user_name,:lic_number,:user_phone)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = textBox5.Text;
            cmd.Parameters.Add("user_name", OracleDbType.Varchar2).Value = textBox6.Text;
            cmd.Parameters.Add("lic_number", OracleDbType.Varchar2).Value = textBox7.Text;
            cmd.Parameters.Add("user_phone", OracleDbType.Varchar2).Value = textBox8.Text;
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Register succeeded");
            }

            Form10 form10 = new Form10();
            form10.Show();
            this.Hide();

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
    }
}

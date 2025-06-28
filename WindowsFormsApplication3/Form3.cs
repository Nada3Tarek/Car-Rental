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
    public partial class Form3 : Form
    {
        string ordb = "Data Source=ORCL1;User Id=hr;Password=hr;";
        OracleConnection conn;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Admins (admin_id, username, password) VALUES (:id,:user_name,:user_password)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = textBox1.Text;
            cmd.Parameters.Add("user_name", OracleDbType.Varchar2).Value = textBox2.Text;
            cmd.Parameters.Add("user_password", OracleDbType.Varchar2).Value = textBox3.Text;
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Rigester succeeded");
            }
            Form11 form11 = new Form11();
            form11.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

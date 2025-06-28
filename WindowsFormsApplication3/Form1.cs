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
    public partial class Form1 : Form
    {
        string ordb = "Data Source=ORCL1; User Id=hr; Password=hr;";
        OracleConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void form_load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select VEHICLE_ID from VEHICLES";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0]);
            }
            dr.Close();

        }

       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        private void form_closing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into VEHICLES (VEHICLE_ID, VEHICLE_NUMBER, STATUS, VEHICLEMODEL) values (:VEHICLE_ID, :VEHICLE_NUMBER, :STATUS, :VEHICLEMODEL)";
            cmd.Parameters.Add("VEHICLE_ID", comboBox3.Text);
            cmd.Parameters.Add("VEHICLE_NUMBER", textBox7.Text);
            cmd.Parameters.Add("STATUS", textBox9.Text);
            cmd.Parameters.Add("VEHICLEMODEL", textBox8.Text);
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                comboBox3.Items.Add(comboBox3.Text);
                MessageBox.Show("New vehicle is added");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please select a vehicle to update.");
                return;
            }

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (OracleCommand c = new OracleCommand())
                {
                    c.Connection = conn;
                    c.CommandText = "update VEHICLES set VEHICLE_NUMBER = :VEHICLE_NUMBER, STATUS = :STATUS, VEHICLEMODEL = :VEHICLEMODEL where VEHICLE_ID = :VEHICLE_ID";
                    c.Parameters.Add("VEHICLE_NUMBER", OracleDbType.Varchar2).Value = textBox7.Text;
                    c.Parameters.Add("STATUS", OracleDbType.Varchar2).Value = textBox9.Text;
                    c.Parameters.Add("VEHICLEMODEL", OracleDbType.Varchar2).Value = textBox8.Text;
                    c.Parameters.Add("VEHICLE_ID", OracleDbType.Varchar2).Value = comboBox3.SelectedItem.ToString();

                    int r = c.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("Vehicle modified");
                    }
                    else
                    {
                        MessageBox.Show("No records updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please select a vehicle to delete.");
                return;
            }

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (OracleCommand c = new OracleCommand())
                {
                    c.Connection = conn;
                    c.CommandText = "Delete from VEHICLES where VEHICLE_ID = :VEHICLE_ID";
                    c.Parameters.Add("VEHICLE_ID", OracleDbType.Varchar2).Value = comboBox3.SelectedItem.ToString();
                    int r = c.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("Vehicle deleted");
                        comboBox3.Items.Remove(comboBox3.SelectedItem);
                        textBox7.Text = "";
                        textBox9.Text = "";
                        textBox8.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("No records deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select VEHICLE_NUMBER, STATUS, VEHICLEMODEL from Vehicles where vehicle_id = :id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", comboBox3.SelectedItem.ToString());
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox7.Text = dr[0].ToString();
                textBox9.Text = dr[1].ToString();
                textBox8.Text = dr[2].ToString();
            }
            dr.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();

        }

    }
}

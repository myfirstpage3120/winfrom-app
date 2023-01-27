using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewWindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;

        public async void Show()
        {
            using (SqlCommand cmd = new SqlCommand("select * from tblEmployee", conn))
            { //select the table from the database
                conn.Open();   //we have opened the connection
               reader = await cmd.ExecuteReaderAsync();
                
                    DataTable dt = new DataTable();
                    dt.Load(reader);                         //the data will get stored in this 
                    dataGridView1.DataSource = dt;
                    reader.Close();
                Thread.Sleep(10000);
                conn.Close();
                
            }
        }
        private void Form1_Load(object sender, EventArgs e)  // to display the data
        {
            conn = new SqlConnection("Data Source=POOLW42SLPC7838;Initial Catalog=\"Assignment 3\";Integrated Security=True");

            Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private  void btnInsertWithSp_Click(object sender, EventArgs e)
        {

            conn = new SqlConnection("Data Source=POOLW42SLPC7838;Initial Catalog=\"Assignment 3\";Integrated Security=True");
            using (cmd = new SqlCommand("sp_InsertEmployee", conn)) ; //we have to pass the stored  processor name
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.Add("@empName", SqlDbType.VarChar, 20).Value = txtEmpName.Text;   //the txt value name should be same as text box value name
                cmd.Parameters.Add("@empSalary", SqlDbType.Float).Value = txtempSalary.Text;
                   

                Task<int> x = cmd.ExecuteNonQueryAsync();
                try
                {
                    cmd.ExecuteNonQueryAsync().Wait();
                    
                                       
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Data inserted successfully");
                }
                finally
                {
                    conn.Close();
                }
                //int x = cmd.ExecuteNonQuery();

                //if (x > 0)
                //{
                //    MessageBox.Show("Row added succesfully");
                //}
                //else
                //{
                //    MessageBox.Show("Error");
                //}
                //conn.Close();
                Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=POOLW42SLPC7838;Initial Catalog=\"Assignment 3\";Integrated Security=True");
            using (cmd = new SqlCommand("sp_DeleteEmployee", conn)) ; //we have to pass the stored  processor name
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.Add("@empNo", SqlDbType.Int).Value = Convert.ToInt32(txtEmpNo.Text);  //the value name should be same as text box value name
                //cmd.Parameters.Add("@empSalary", SqlDbType.Float).Value = txtempSalary.Text;
                //int x = cmd.ExecuteNonQuery();

                Task<int> x = cmd.ExecuteNonQueryAsync();
                try
                {
                    cmd.ExecuteNonQueryAsync().Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }


                //if (x > 0)
                //{
                //    MessageBox.Show("Row deleted succesfully");
                //}
                //else
                //{
                //    MessageBox.Show("Error");
                //}
                //conn.Close();
                Show();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=POOLW42SLPC7838;Initial Catalog=\"Assignment 3\";Integrated Security=True");
            using (cmd = new SqlCommand("sp_UpdateEmployee", conn)) ; //we have to pass the stored  processor name
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.Add("@empNo", SqlDbType.Int).Value = Convert.ToInt32(txtEmpNo.Text);  //the value name should be same as text box value name
                cmd.Parameters.Add("@empSalary", SqlDbType.Float).Value = txtempSalary.Text;
                cmd.Parameters.Add("@empName", SqlDbType.VarChar, 20).Value = txtEmpName.Text;
                Task <int> x = cmd.ExecuteNonQueryAsync();



                try
                {
                    cmd.ExecuteNonQueryAsync().Wait(500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }

                //if (x > 0)
                //{
                //    MessageBox.Show("Row Updated succesfully");
                //}
                //else
                //{
                //    MessageBox.Show("Error");
                //}

                Show();
            }
        }

        private void txtEmpName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

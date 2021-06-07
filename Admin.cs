using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tutorial_Purchasing
{
    public partial class Admin : Form
    {

        String ConnectionString = "server=127.0.0.1; port=3306; username=root; password=; database=tutorial_purchasing;";

        public static void OpenMainForm()
        {
            Application.Run(new EmployeeLogin()); //run your new form
        }

        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            GetEmployeeDetails();
            GetEmployeeAccountDetails();
        }

        private void GetEmployeeAccountDetails()
        {
            string query = "SELECT * FROM employee_accounts;";
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    dataGridView1.DataSource = datatable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        private void GetEmployeeDetails()
        {
            string query = "SELECT * FROM employees;";
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    dataGridView2.DataSource = datatable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            ChangePassword change = new ChangePassword();
            change.Show();
        }

        private void buttonRefreshEmployeeAccounts_Click(object sender, EventArgs e)
        {
            GetEmployeeAccountDetails();
        }

        private void linkLabelLogOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(OpenMainForm)); //you create a new thread
            this.Close(); //you close your current form 
            t.Start();  //you start the thread
        }

        private void linkLabelChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {           
            ChangePassword changePassword = new ChangePassword();
            changePassword.Show();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            GetEmployeeAccountDetails();
            GetEmployeeDetails();
        }
    }
}

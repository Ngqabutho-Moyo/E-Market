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
    public partial class AdminLogin : Form
    {
        String ConnectionString = "server=127.0.0.1; port=3306; username=root; password=; database=tutorial_purchasing;";
        public AdminLogin()
        {
            InitializeComponent();
        }

        public bool BlankFields()
        {
            if (textBoxEmployeeNumber.Text.Trim() == "" || textBoxPassword.Text.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                if (!BlankFields())
                {
                    try
                    {
                        connection.Open();
                        string query = "SELECT * FROM `admin` WHERE employee_num=@enum AND Password=@pass";
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        DataTable datatable = new DataTable();
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.Add("@enum", MySqlDbType.VarChar).Value = textBoxEmployeeNumber.Text;
                        command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBoxPassword.Text;
                        adapter.SelectCommand = command;
                        adapter.Fill(datatable);
                        MessageBox.Show($"Welcome {textBoxEmployeeNumber.Text}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Admin admin = new Admin();
                        admin.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Enter a username and password", "Blank Username or Password", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tutorial_Purchasing
{
    public partial class EmployeeLogin : Form
    {
        String ConnectionString = "server=127.0.0.1; port=3306; username=root; password=; database=tutorial_purchasing;";        
        public EmployeeLogin()
        {
            InitializeComponent();
        }

        private bool Login()
        {
            string select = "SELECT * FROM `employee_accounts` WHERE Employee_No=@num AND Password=@pass";
            using(MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(select, connection);
                    command.Parameters.Add("@num", MySqlDbType.VarChar).Value = textBoxEmployeeNumber.Text;
                    command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBoxPassword.Text;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
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
            if (!BlankFields())
            {
                if (Login())
                {
                    MessageBox.Show($"Welcome {textBoxEmployeeNumber.Text}. Press OK to be redirected to Stock Manager", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    StockManager stock = new StockManager();
                    stock.Show();
                }
                else
                {
                    MessageBox.Show("Failed to log in", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
            else
            {
                MessageBox.Show("Fill in all fields", "Blank Fields Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();            
        }
    }
}

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
    public partial class EmployeeCreateAccount : Form
    {
        String ConnectionString = "server=127.0.0.1; port=3306; username=root; password=; database=tutorial_purchasing;";

        public EmployeeCreateAccount()
        {
            InitializeComponent();
        }

        private bool InsertIntoEmployees()
        {
            string insert_into_employees = "INSERT INTO `employees`(`Employee_No`, `First_Name`, `Last_Name`, `DOB`, `Designation`) VALUES (@no, @fn, @ln, @dob, @des)";
            using(MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insert_into_employees, connection);
                    command.Parameters.Add("@no", MySqlDbType.VarChar).Value = textBoxEmployeeNumber.Text;
                    command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = textBoxFirstName.Text;
                    command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = textBoxLastName.Text;
                    command.Parameters.Add("@dob", MySqlDbType.Date).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@des", MySqlDbType.VarChar).Value = comboBox1.SelectedItem.ToString();
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

        private bool InsertIntoEmployeeAccounts()
        {
            string insert_into_employee_accounts = "INSERT INTO `employee_accounts`(`Employee_No`, `Password`) VALUES (@no, @pass)";
            using(MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insert_into_employee_accounts, connection);
                    command.Parameters.Add("@no", MySqlDbType.VarChar).Value = textBoxEmployeeNumber.Text;
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

        public void ClearAll()
        {
            textBoxEmployeeNumber.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            comboBox1.Items.Clear();
            textBoxPassword.Clear();
            textBoxConfirmPassword.Clear();
        }

        public bool BlankFields()
        {
            if(textBoxEmployeeNumber.Text.Trim() == "" || textBoxFirstName.Text.Trim() == "" || textBoxLastName.Text.Trim() == "" || comboBox1.SelectedItem.ToString().Trim() == "" || textBoxPassword.Text.Trim() == "" || textBoxConfirmPassword.Text.Trim() == "")
            {
                MessageBox.Show("Fill in all fields", "Empty Fields Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClearPasswords()
        {            
            textBoxPassword.Clear();
            textBoxConfirmPassword.Clear();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (!BlankFields())
            {
                if (CheckPassword())
                {
                    if (InsertIntoEmployees())
                    {
                        if (InsertIntoEmployeeAccounts())
                        {
                            MessageBox.Show($"Account for {textBoxFirstName.Text} {textBoxLastName.Text} has been created successfully", "Account Creation Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            StockManager stock = new StockManager();
                            stock.Show();
                        }
                        else
                        {
                            MessageBox.Show($"Account for {textBoxFirstName.Text} {textBoxLastName.Text} could not be created", "Account Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Account for {textBoxFirstName.Text} {textBoxLastName.Text} could not be created", "Account Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Account for {textBoxFirstName.Text} {textBoxLastName.Text} could not be created", "Account Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Account for {textBoxFirstName.Text} {textBoxLastName.Text} could not be created", "Account Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Function to check if passwords match
        private bool CheckPassword()
        {
            string pass1 = textBoxPassword.Text;
            string pass2 = textBoxConfirmPassword.Text;
            if (pass1 == pass2)
            {
                MessageBox.Show("Passwords do not match", "Password Mismatch Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeLogin login = new EmployeeLogin();
            login.Show();
        }

        private void EmployeeCreateAccount_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            EmployeeLogin login = new EmployeeLogin();
            login.Show();
        }
    }
}

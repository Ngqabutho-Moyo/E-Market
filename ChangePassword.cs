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
    public partial class ChangePassword : Form
    {
        String ConnectionString = "server=127.0.0.1; port=3306; username=root; password=; database=tutorial_purchasing;";
        MySqlCommand select_command;
        MySqlCommand update_command;

        public ChangePassword()
        {
            InitializeComponent();
        }

        private bool CheckNewPassword()
        {
            string pass = textBoxNewPassword.Text;
            string confirm_pass = textBoxConfirmNewPassword.Text;

            if(pass == confirm_pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckEmptyFields()
        {
            if(textBoxEmployeeNumber.Text.Trim() == "" || textBoxOldPassword.Text.Trim() == "" || textBoxNewPassword.Text.Trim() == "" || textBoxConfirmNewPassword.Text.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            string select = "SELECT * FROM `employee_accounts` WHERE `Employee_No`=@enum AND `Password`=@p";
            string update = "UPDATE `employee_accounts` SET `Password`=@p WHERE `Employee_No`=@enum";
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                if (!CheckEmptyFields())
                {
                    if (CheckNewPassword())
                    {
                        try
                        {
                            connection.Open();
                            //Look for employee to update
                            select_command = new MySqlCommand(select, connection);
                            select_command.Parameters.Add("@enum", MySqlDbType.VarChar).Value = textBoxEmployeeNumber.Text;
                            select_command.Parameters.Add("@p", MySqlDbType.VarChar).Value = textBoxOldPassword.Text;

                            //Set new password for employee
                            update_command = new MySqlCommand(update, connection);
                            update_command.Parameters.Add("@p", MySqlDbType.VarChar).Value = textBoxNewPassword.Text;
                            update_command.Parameters.Add("@enum", MySqlDbType.VarChar).Value = textBoxEmployeeNumber.Text;

                            select_command.ExecuteNonQuery();
                            update_command.ExecuteNonQuery();

                            //Success
                            MessageBox.Show($"Account for {textBoxEmployeeNumber.Text} has been updated successfully.", "Password Change Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
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
                    else
                    {
                        MessageBox.Show("Passwords do not match", "Password Mismatch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Fill in all blank fields", "Blank Fields Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

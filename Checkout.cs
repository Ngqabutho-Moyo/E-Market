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
    public partial class Checkout : Form
    {
        String ConnectionString = "server=127.0.0.1; port=3306; username=root; password=; database=tutorial_purchasing;";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        public static void OpenMainForm()
        {
            Application.Run(new EmployeeLogin()); //run your new form
        }

        private void GetCustomerDetails()
        {
            string query = "SELECT `First_Name`, `Last_Name`, `Balance` FROM customers;";
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(query, connection);
                    DataTable datatable2 = new DataTable();
                    adapter2.Fill(datatable2);

                    dataGridViewBankDetails.DataSource = datatable2;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        //Function to check if customer's balance has exceeded the price
        private bool InsufficientBalance()
        {
            decimal balance = Convert.ToDecimal(textBoxBalance.Text);
            decimal price = Convert.ToDecimal(textBoxPrice.Text);

            if(price >= balance)
            {
                MessageBox.Show("Insufficient balance", "Insufficient balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                return false;
            }
        }

        
        //Clear textboxes
        private void ClearAll()
        {
            //Clear 'Cart' textboxes
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxBalance.Clear();
            textBoxProductNameInCart.Clear();            
            textBoxQuantityInCart.Clear();

            //Clear 'Groceries' textboxes
            textBoxProductName.Clear();
            textBoxPackagingSize.Clear();
            comboBoxUnits.Items.Clear();
            textBoxQuantityOnShelf.Clear();
            textBoxPrice.Clear();
        }

        //Function to check whether a customer has exceeded the number of grocery items on the shelf
        private bool InvalidQuantity()
        {
            int quantity_on_shelf = Convert.ToInt32(textBoxQuantityOnShelf.Text);
            int quantity_in_cart = Convert.ToInt32(textBoxQuantityInCart.Text);

            if(quantity_in_cart >= quantity_on_shelf)
            {
                MessageBox.Show("You are not allowed to buy that many items.", "Too many", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool BlankFields()
        {
            if(textBoxFirstName.Text.Trim() == ""|| textBoxLastName.Text.Trim() == "" || textBoxBalance.Text.Trim() == "" || textBoxProductNameInCart.Text.Trim() == "" || textBoxQuantityInCart.Text.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetGroceryDetails()
        {
            string query = "SELECT * FROM groceries;";
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    dataGridViewGroceryList.DataSource = datatable;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    connection.Close();
                }
            }
        }   
        public Checkout()
        {
            InitializeComponent();
        }

        //Function to update bank details
        private void UpdateBankDetails()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                decimal price = Convert.ToDecimal(textBoxPrice.Text);
                decimal balance = Convert.ToDecimal(textBoxBalance.Text);
                int quantity = Convert.ToInt32(textBoxQuantityInCart.Text);
                decimal new_balance = balance - (quantity * price);
                string update_balance = "UPDATE `customers` SET `Balance`=@bal WHERE `First_Name`=@fn AND `Last_Name`=@ln";
                if (!BlankFields())
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(update_balance, connection);
                        command.Parameters.Add("@bal", MySqlDbType.Decimal).Value = new_balance;
                        command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = textBoxFirstName.Text;
                        command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = textBoxLastName.Text;
                        command.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        //Function to update shelf after a purchase
        private void UpdateShelf()
        {
            int quantity = Convert.ToInt32(textBoxQuantityOnShelf.Text);
            int quantity_in_cart = Convert.ToInt32(textBoxQuantityInCart.Text);
            int new_quantity = quantity - quantity_in_cart;
            string update_shelf = "UPDATE `groceries` SET `Quantity_On_Shelf`=@q WHERE `Name`=@pn;";
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(update_shelf, connection);
                    command.Parameters.Add("@q", MySqlDbType.Int32).Value = new_quantity;
                    command.Parameters.Add("@pn", MySqlDbType.VarChar).Value = textBoxProductNameInCart.Text;
                    command.ExecuteNonQuery();                    
                    return;

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
       

        private void Purchase_Load(object sender, EventArgs e)
        {
            GetGroceryDetails();
            GetCustomerDetails();
        }        

        private void buttonCheckout_Click(object sender, EventArgs e)
        {
            if (!BlankFields())
            {
                if (!InvalidQuantity())
                {
                    if (!InsufficientBalance())
                    {
                        //Update customer's balance
                        UpdateBankDetails();

                        //Deduct the number of grocery items bought from the shelf
                        UpdateShelf();

                        //Successful purchase
                        MessageBox.Show("Thank you for your purchase", "Purchase Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Refresh and reflect the new information
                        ClearAll();
                        GetCustomerDetails();
                        GetGroceryDetails();
                    }
                    else
                    {
                        MessageBox.Show("We could not process your transaction", "Purchase Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("We could not process your transaction", "Purchase Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

       
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            GetGroceryDetails();
            GetCustomerDetails();
            ClearAll();
        }


        //Assign data table row values to corresponding textboxes in Groceries
        private void dataGridViewGroceryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                //Get collection for all rows
                DataGridViewRow row = this.dataGridViewGroceryList.Rows[e.RowIndex];

                //Populate textboxes
                textBoxProductName.Text = row.Cells[1].Value.ToString();
                textBoxPackagingSize.Text = row.Cells[2].Value.ToString();
                comboBoxUnits.Text = row.Cells[3].Value.ToString();
                textBoxQuantityOnShelf.Text = row.Cells[4].Value.ToString();
                textBoxPrice.Text = row.Cells[5].Value.ToString();
            }
        }

        private void dataGridViewBankDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Get collection for all rows
                DataGridViewRow row = this.dataGridViewBankDetails.Rows[e.RowIndex];

                //Populate textboxes
                textBoxFirstName.Text = row.Cells[0].Value.ToString();
                textBoxLastName.Text = row.Cells[1].Value.ToString();
                textBoxBalance.Text = row.Cells[2].Value.ToString();
            }
        }

        private void linkLabelBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            StockManager stock = new StockManager();
            stock.Show();
        }

        private void linkLabelLogOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(OpenMainForm)); //you create a new thread
            this.Close(); //you close your current form 
            t.Start();  //you start the thread
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

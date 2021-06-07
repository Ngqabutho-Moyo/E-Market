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
    public partial class NewQuantity : Form
    {
        String ConnectionString = "server=127.0.0.1; port=3306; username=root; password=; database=tutorial_purchasing;";
        public NewQuantity()
        {
            InitializeComponent();
        }

        private void NewQuantity_Load(object sender, EventArgs e)
        {
            GetCustomerDetails();
            GetGroceryDetails();
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

        private void UpdateShelf()
        {
            int quantity = Convert.ToInt32(textBoxQuantity.Text);
            int quantity_in_cart = Convert.ToInt32(textBoxQuantityInCart.Text);
            int new_quantity = quantity - quantity_in_cart;
            string update_shelf = "UPDATE `groceries` SET `Quantity_On_Shelf`=@q WHERE `Name`=@pn;";
            using(MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(update_shelf, connection);
                    command.Parameters.Add("@q", MySqlDbType.Int32).Value = new_quantity;
                    command.Parameters.Add("@pn", MySqlDbType.VarChar).Value = textBoxProductNameInCart.Text;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Done");
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void buttonCheckout_Click(object sender, EventArgs e)
        {
            UpdateShelf();
        }
    }
}

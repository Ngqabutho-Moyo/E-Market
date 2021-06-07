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
    public partial class StockManager : Form
    {
        String ConnectionString = "server=127.0.0.1; port=3306; username=root; password=; database=tutorial_purchasing;";        
        MySqlCommand command;        
        

        public StockManager()
        {
            InitializeComponent();
        }

        private void GroceryList_Load(object sender, EventArgs e)
        {
            GetGroceryDetails();            
        }

        private void ClearAll()
        {
            textBoxProductName.Clear();
            textBoxPackagingSize.Clear();
            textBoxQuantityOnShelf.Clear();
            textBoxPrice.Clear();
        }

        private bool AddToShelf()
        {
            string add = "INSERT INTO `groceries`(`Name`, `Packaging_Size`, `Units`, `Quantity_On_Shelf`, `Price(US$)`) VALUES (@pn, @packaging_size, @units, @quantity_on_shelf, @price)";
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    command = new MySqlCommand(add, connection);
                    command.Parameters.Add("@pn", MySqlDbType.VarChar).Value = textBoxProductName.Text;
                    command.Parameters.Add("@packaging_size", MySqlDbType.Decimal).Value = textBoxPackagingSize.Text;
                    command.Parameters.Add("@units", MySqlDbType.VarChar).Value = comboBoxUnits.SelectedItem;
                    command.Parameters.Add("@quantity_on_shelf", MySqlDbType.Int32).Value = textBoxQuantityOnShelf.Text;
                    command.Parameters.Add("@price", MySqlDbType.Decimal).Value = textBoxPrice.Text;
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

        private bool UpdateShelf()
        {            
            string update = "UPDATE `groceries` SET `Name`=@name,`Packaging_Size`=@packaging_size,`Units` = @units, `Quantity_On_Shelf`=@quantity_on_shelf,`Price(US$)`=@price WHERE `Product_ID`=@id";
            using(MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(update, connection);
                    command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBoxProductName.Text;
                    command.Parameters.Add("@packaging_size", MySqlDbType.Decimal).Value = Convert.ToDecimal(textBoxPackagingSize.Text);
                    command.Parameters.Add("@units", MySqlDbType.VarChar).Value = comboBoxUnits.SelectedItem.ToString();
                    command.Parameters.Add("@quantity_on_shelf", MySqlDbType.Int32).Value = Convert.ToInt32(textBoxQuantityOnShelf.Text);
                    command.Parameters.Add("@price", MySqlDbType.Decimal).Value = Convert.ToDecimal(textBoxPrice.Text);
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(textBoxID.Text);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private bool DeleteFromShelf()
        {
            string delete = "DELETE FROM `groceries` WHERE `Product_ID`= @id";
            using(MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(delete, connection);
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(textBoxID.Text);
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


        public static void OpenMainMenu()
        {
            Application.Run(new MainMenu()); //run your new form
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (AddToShelf())
            {
                MessageBox.Show($"{textBoxProductName.Text} has been added to the shelf", "Item Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAll();
                GetGroceryDetails();
            }
            else
            {
                MessageBox.Show($"{textBoxProductName.Text} could not be added", "Failed To Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateShelf())
            {
                MessageBox.Show($"{textBoxProductName.Text} has been updated successfully.", "Item Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetGroceryDetails();
            }
            else
            {
                MessageBox.Show($"{textBoxProductName.Text} could not be updated", "Failed To Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (DeleteFromShelf())
            {
                MessageBox.Show($"{textBoxProductName.Text} has been deleted successfully.", "Item Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetGroceryDetails();
            }
            else
            {
                MessageBox.Show($"{textBoxProductName.Text} could not be deleted", "Failed To Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewGroceryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Get collection for all rows
                DataGridViewRow row = this.dataGridViewGroceryList.Rows[e.RowIndex];

                //Populate textboxes
                textBoxID.Text = row.Cells[0].Value.ToString();
                textBoxProductName.Text = row.Cells[1].Value.ToString();
                textBoxPackagingSize.Text = row.Cells[2].Value.ToString();
                comboBoxUnits.SelectedItem = row.Cells[3].Value.ToString();
                textBoxQuantityOnShelf.Text = row.Cells[4].Value.ToString();
                textBoxPrice.Text = row.Cells[5].Value.ToString();
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

        

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void linkLabelLogOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(OpenMainMenu)); //you create a new thread
            this.Close(); //you close your current form 
            t.Start();  //you start the thread
        }       

        private void linkLabelTill_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Checkout purchase = new Checkout();
            purchase.Show();
        }
    }
}

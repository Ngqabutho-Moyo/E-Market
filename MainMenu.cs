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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        
        private void button3_Click(object sender, EventArgs e)
        {            
           
        }

        private void button4_Click(object sender, EventArgs e)
        {            
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabelEmpLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmployeeLogin login = new EmployeeLogin();
            login.Show();
        }

        private void linkLabelAdminLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminLogin admin = new AdminLogin();
            admin.Show();
        }

        private void linkLabelCreateEmpAcc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmployeeCreateAccount acc = new EmployeeCreateAccount();
            acc.Show();
        }
    }
}

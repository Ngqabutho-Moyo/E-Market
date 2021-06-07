
namespace Tutorial_Purchasing
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelEmpLogin = new System.Windows.Forms.LinkLabel();
            this.linkLabelAdminLogin = new System.Windows.Forms.LinkLabel();
            this.linkLabelCreateEmpAcc = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(67, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 96);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to e-shopper,\r\na prototype Windows App\r\nsupermarket system\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(451, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "X";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // linkLabelEmpLogin
            // 
            this.linkLabelEmpLogin.AutoSize = true;
            this.linkLabelEmpLogin.Location = new System.Drawing.Point(98, 138);
            this.linkLabelEmpLogin.Name = "linkLabelEmpLogin";
            this.linkLabelEmpLogin.Size = new System.Drawing.Size(92, 15);
            this.linkLabelEmpLogin.TabIndex = 7;
            this.linkLabelEmpLogin.TabStop = true;
            this.linkLabelEmpLogin.Text = "Employee Login";
            this.linkLabelEmpLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelEmpLogin_LinkClicked);
            // 
            // linkLabelAdminLogin
            // 
            this.linkLabelAdminLogin.AutoSize = true;
            this.linkLabelAdminLogin.Location = new System.Drawing.Point(98, 226);
            this.linkLabelAdminLogin.Name = "linkLabelAdminLogin";
            this.linkLabelAdminLogin.Size = new System.Drawing.Size(76, 15);
            this.linkLabelAdminLogin.TabIndex = 8;
            this.linkLabelAdminLogin.TabStop = true;
            this.linkLabelAdminLogin.Text = "Admin Login";
            this.linkLabelAdminLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAdminLogin_LinkClicked);
            // 
            // linkLabelCreateEmpAcc
            // 
            this.linkLabelCreateEmpAcc.AutoSize = true;
            this.linkLabelCreateEmpAcc.Location = new System.Drawing.Point(98, 320);
            this.linkLabelCreateEmpAcc.Name = "linkLabelCreateEmpAcc";
            this.linkLabelCreateEmpAcc.Size = new System.Drawing.Size(144, 15);
            this.linkLabelCreateEmpAcc.TabIndex = 9;
            this.linkLabelCreateEmpAcc.TabStop = true;
            this.linkLabelCreateEmpAcc.Text = "Create Employee Account";
            this.linkLabelCreateEmpAcc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCreateEmpAcc_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Tutorial_Purchasing.Properties.Resources.main_memu;
            this.pictureBox1.Location = new System.Drawing.Point(248, 138);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(233, 197);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 417);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkLabelCreateEmpAcc);
            this.Controls.Add(this.linkLabelAdminLogin);
            this.Controls.Add(this.linkLabelEmpLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelEmpLogin;
        private System.Windows.Forms.LinkLabel linkLabelAdminLogin;
        private System.Windows.Forms.LinkLabel linkLabelCreateEmpAcc;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
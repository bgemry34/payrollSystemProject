namespace PayRollSystem
{
    partial class adminLogin1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminLogin1));
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.adminPanel = new System.Windows.Forms.Panel();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.correctAdmin = new System.Windows.Forms.Label();
            this.incorrectAdmin = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.adminSelectlogin = new System.Windows.Forms.ComboBox();
            this.adminPassword = new System.Windows.Forms.TextBox();
            this.Panel9 = new System.Windows.Forms.Panel();
            this.Panel10 = new System.Windows.Forms.Panel();
            this.adminUsername = new System.Windows.Forms.TextBox();
            this.adminLogin = new System.Windows.Forms.Button();
            this.loginSelect = new System.Windows.Forms.ComboBox();
            this.userPassword = new System.Windows.Forms.TextBox();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.userUsername = new System.Windows.Forms.TextBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.goBtn = new System.Windows.Forms.Button();
            this.attendanceBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.adminPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.Color.Red;
            this.Label4.Location = new System.Drawing.Point(69, 253);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(0, 18);
            this.Label4.TabIndex = 29;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Red;
            this.Label3.Location = new System.Drawing.Point(69, 162);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(0, 18);
            this.Label3.TabIndex = 28;
            this.Label3.Visible = false;
            // 
            // adminPanel
            // 
            this.adminPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.adminPanel.Controls.Add(this.Label8);
            this.adminPanel.Controls.Add(this.Label9);
            this.adminPanel.Controls.Add(this.correctAdmin);
            this.adminPanel.Controls.Add(this.incorrectAdmin);
            this.adminPanel.Controls.Add(this.PictureBox1);
            this.adminPanel.Controls.Add(this.adminSelectlogin);
            this.adminPanel.Controls.Add(this.adminPassword);
            this.adminPanel.Controls.Add(this.Panel9);
            this.adminPanel.Controls.Add(this.Panel10);
            this.adminPanel.Controls.Add(this.adminUsername);
            this.adminPanel.Controls.Add(this.adminLogin);
            this.adminPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.adminPanel.Location = new System.Drawing.Point(0, 0);
            this.adminPanel.Name = "adminPanel";
            this.adminPanel.Size = new System.Drawing.Size(0, 562);
            this.adminPanel.TabIndex = 27;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.ForeColor = System.Drawing.Color.Red;
            this.Label8.Location = new System.Drawing.Point(61, 385);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(159, 18);
            this.Label8.TabIndex = 47;
            this.Label8.Text = "*Password is Required";
            this.Label8.Visible = false;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.ForeColor = System.Drawing.Color.Red;
            this.Label9.Location = new System.Drawing.Point(61, 280);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(161, 18);
            this.Label9.TabIndex = 46;
            this.Label9.Text = "*Username is Required";
            this.Label9.Visible = false;
            // 
            // correctAdmin
            // 
            this.correctAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.correctAdmin.AutoSize = true;
            this.correctAdmin.BackColor = System.Drawing.Color.Transparent;
            this.correctAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.correctAdmin.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.correctAdmin.ForeColor = System.Drawing.Color.LimeGreen;
            this.correctAdmin.Location = new System.Drawing.Point(124, 552);
            this.correctAdmin.Name = "correctAdmin";
            this.correctAdmin.Size = new System.Drawing.Size(176, 18);
            this.correctAdmin.TabIndex = 27;
            this.correctAdmin.Text = "Login Successfully !";
            this.correctAdmin.Visible = false;
            // 
            // incorrectAdmin
            // 
            this.incorrectAdmin.AutoSize = true;
            this.incorrectAdmin.BackColor = System.Drawing.Color.Transparent;
            this.incorrectAdmin.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incorrectAdmin.ForeColor = System.Drawing.Color.Red;
            this.incorrectAdmin.Location = new System.Drawing.Point(88, 554);
            this.incorrectAdmin.Name = "incorrectAdmin";
            this.incorrectAdmin.Size = new System.Drawing.Size(248, 16);
            this.incorrectAdmin.TabIndex = 26;
            this.incorrectAdmin.Text = "Incorrect Username Or Password";
            this.incorrectAdmin.Visible = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(127, 24);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(187, 189);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 25;
            this.PictureBox1.TabStop = false;
            // 
            // adminSelectlogin
            // 
            this.adminSelectlogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.adminSelectlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adminSelectlogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminSelectlogin.FormattingEnabled = true;
            this.adminSelectlogin.Items.AddRange(new object[] {
            "Employee Login",
            "Admin Login"});
            this.adminSelectlogin.Location = new System.Drawing.Point(127, 237);
            this.adminSelectlogin.Name = "adminSelectlogin";
            this.adminSelectlogin.Size = new System.Drawing.Size(173, 24);
            this.adminSelectlogin.TabIndex = 24;
            this.adminSelectlogin.SelectedIndexChanged += new System.EventHandler(this.adminSelectlogin_SelectedIndexChanged);
            // 
            // adminPassword
            // 
            this.adminPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.adminPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.adminPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminPassword.ForeColor = System.Drawing.Color.Gainsboro;
            this.adminPassword.Location = new System.Drawing.Point(64, 418);
            this.adminPassword.Name = "adminPassword";
            this.adminPassword.Size = new System.Drawing.Size(312, 28);
            this.adminPassword.TabIndex = 23;
            this.adminPassword.Text = "Password";
            this.adminPassword.Enter += new System.EventHandler(this.adminPassword_Enter);
            this.adminPassword.Leave += new System.EventHandler(this.adminPassword_Leave);
            // 
            // Panel9
            // 
            this.Panel9.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel9.Location = new System.Drawing.Point(64, 452);
            this.Panel9.Name = "Panel9";
            this.Panel9.Size = new System.Drawing.Size(312, 5);
            this.Panel9.TabIndex = 22;
            // 
            // Panel10
            // 
            this.Panel10.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel10.Location = new System.Drawing.Point(64, 357);
            this.Panel10.Name = "Panel10";
            this.Panel10.Size = new System.Drawing.Size(312, 5);
            this.Panel10.TabIndex = 21;
            // 
            // adminUsername
            // 
            this.adminUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.adminUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.adminUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminUsername.ForeColor = System.Drawing.Color.Gainsboro;
            this.adminUsername.Location = new System.Drawing.Point(64, 316);
            this.adminUsername.Name = "adminUsername";
            this.adminUsername.Size = new System.Drawing.Size(312, 28);
            this.adminUsername.TabIndex = 20;
            this.adminUsername.Text = "Employee ID";
            this.adminUsername.Enter += new System.EventHandler(this.adminUsername_Enter);
            this.adminUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.adminUsername_KeyPress);
            this.adminUsername.Leave += new System.EventHandler(this.adminUsername_Leave);
            // 
            // adminLogin
            // 
            this.adminLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(154)))), ((int)(((byte)(222)))));
            this.adminLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(92)))), ((int)(((byte)(191)))));
            this.adminLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.adminLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.adminLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.adminLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.adminLogin.Location = new System.Drawing.Point(64, 498);
            this.adminLogin.Name = "adminLogin";
            this.adminLogin.Size = new System.Drawing.Size(312, 39);
            this.adminLogin.TabIndex = 19;
            this.adminLogin.Text = "Login as Admin";
            this.adminLogin.UseVisualStyleBackColor = false;
            this.adminLogin.Click += new System.EventHandler(this.adminLogin_Click);
            // 
            // loginSelect
            // 
            this.loginSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loginSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginSelect.FormattingEnabled = true;
            this.loginSelect.Items.AddRange(new object[] {
            "Employee Login",
            "Admin Login"});
            this.loginSelect.Location = new System.Drawing.Point(127, 120);
            this.loginSelect.Name = "loginSelect";
            this.loginSelect.Size = new System.Drawing.Size(173, 24);
            this.loginSelect.TabIndex = 26;
            this.loginSelect.SelectedIndexChanged += new System.EventHandler(this.loginSelect_SelectedIndexChanged);
            // 
            // userPassword
            // 
            this.userPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(66)))), ((int)(((byte)(74)))));
            this.userPassword.Location = new System.Drawing.Point(72, 278);
            this.userPassword.Name = "userPassword";
            this.userPassword.Size = new System.Drawing.Size(312, 28);
            this.userPassword.TabIndex = 25;
            this.userPassword.Text = "Password";
            this.userPassword.Enter += new System.EventHandler(this.userPassword_Enter);
            this.userPassword.Leave += new System.EventHandler(this.userPassword_Leave);
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel3.Location = new System.Drawing.Point(72, 319);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(312, 5);
            this.Panel3.TabIndex = 24;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel2.Location = new System.Drawing.Point(72, 224);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(312, 5);
            this.Panel2.TabIndex = 23;
            // 
            // userUsername
            // 
            this.userUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(66)))), ((int)(((byte)(74)))));
            this.userUsername.Location = new System.Drawing.Point(72, 183);
            this.userUsername.Name = "userUsername";
            this.userUsername.Size = new System.Drawing.Size(312, 28);
            this.userUsername.TabIndex = 22;
            this.userUsername.Text = "Employee ID";
            this.userUsername.Enter += new System.EventHandler(this.userUsername_Enter);
            this.userUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userUsername_KeyPress);
            this.userUsername.Leave += new System.EventHandler(this.userUsername_Leave);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.DarkOrange;
            this.Panel1.Location = new System.Drawing.Point(0, 48);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(6, 60);
            this.Panel1.TabIndex = 21;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Orange;
            this.Label1.Location = new System.Drawing.Point(12, 60);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(129, 39);
            this.Label1.TabIndex = 20;
            this.Label1.Text = "LOGIN";
            // 
            // goBtn
            // 
            this.goBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(92)))), ((int)(((byte)(191)))));
            this.goBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.goBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.goBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(92)))), ((int)(((byte)(191)))));
            this.goBtn.Location = new System.Drawing.Point(149, 383);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(127, 39);
            this.goBtn.TabIndex = 19;
            this.goBtn.Text = "GO";
            this.goBtn.UseVisualStyleBackColor = true;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // attendanceBtn
            // 
            this.attendanceBtn.BackColor = System.Drawing.Color.Orange;
            this.attendanceBtn.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.attendanceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attendanceBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attendanceBtn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.attendanceBtn.Location = new System.Drawing.Point(311, 24);
            this.attendanceBtn.Name = "attendanceBtn";
            this.attendanceBtn.Size = new System.Drawing.Size(112, 31);
            this.attendanceBtn.TabIndex = 30;
            this.attendanceBtn.Text = "Attendance";
            this.attendanceBtn.UseVisualStyleBackColor = false;
            this.attendanceBtn.Click += new System.EventHandler(this.attendanceBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // adminLogin1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(423, 562);
            this.Controls.Add(this.attendanceBtn);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.adminPanel);
            this.Controls.Add(this.loginSelect);
            this.Controls.Add(this.userPassword);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.userUsername);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.goBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "adminLogin1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "adminLogin1";
            this.Load += new System.EventHandler(this.adminLogin1_Load);
            this.adminPanel.ResumeLayout(false);
            this.adminPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Panel adminPanel;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label correctAdmin;
        internal System.Windows.Forms.Label incorrectAdmin;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.ComboBox adminSelectlogin;
        internal System.Windows.Forms.TextBox adminPassword;
        internal System.Windows.Forms.Panel Panel9;
        internal System.Windows.Forms.Panel Panel10;
        internal System.Windows.Forms.TextBox adminUsername;
        internal System.Windows.Forms.Button adminLogin;
        internal System.Windows.Forms.ComboBox loginSelect;
        internal System.Windows.Forms.TextBox userPassword;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.TextBox userUsername;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button goBtn;
        internal System.Windows.Forms.Button attendanceBtn;
        private System.Windows.Forms.Timer timer1;
    }
}
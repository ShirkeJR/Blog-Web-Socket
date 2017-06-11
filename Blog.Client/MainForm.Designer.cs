namespace Blog.Client
{
    partial class MainForm
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
            this.listBlogs = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.labelConnection = new System.Windows.Forms.Label();
            this.panelLogged = new System.Windows.Forms.Panel();
            this.btnMyBlog = new System.Windows.Forms.Button();
            this.labelLoggedUser = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelGuest = new System.Windows.Forms.Panel();
            this.labelGuest = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelLogged.SuspendLayout();
            this.panelGuest.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBlogs
            // 
            this.listBlogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBlogs.FormattingEnabled = true;
            this.listBlogs.Location = new System.Drawing.Point(12, 60);
            this.listBlogs.Name = "listBlogs";
            this.listBlogs.Size = new System.Drawing.Size(347, 340);
            this.listBlogs.TabIndex = 0;
            this.listBlogs.DoubleClick += new System.EventHandler(this.listBlogs_DoubleClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 31);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(347, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // labelConnection
            // 
            this.labelConnection.BackColor = System.Drawing.SystemColors.Window;
            this.labelConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelConnection.Location = new System.Drawing.Point(12, 8);
            this.labelConnection.Name = "labelConnection";
            this.labelConnection.Size = new System.Drawing.Size(347, 20);
            this.labelConnection.TabIndex = 2;
            this.labelConnection.Text = "Connected to::";
            this.labelConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLogged
            // 
            this.panelLogged.BackColor = System.Drawing.SystemColors.Window;
            this.panelLogged.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLogged.Controls.Add(this.btnMyBlog);
            this.panelLogged.Controls.Add(this.labelLoggedUser);
            this.panelLogged.Controls.Add(this.btnLogout);
            this.panelLogged.Location = new System.Drawing.Point(12, 408);
            this.panelLogged.Name = "panelLogged";
            this.panelLogged.Size = new System.Drawing.Size(347, 30);
            this.panelLogged.TabIndex = 3;
            // 
            // btnMyBlog
            // 
            this.btnMyBlog.Location = new System.Drawing.Point(3, 2);
            this.btnMyBlog.Name = "btnMyBlog";
            this.btnMyBlog.Size = new System.Drawing.Size(75, 24);
            this.btnMyBlog.TabIndex = 2;
            this.btnMyBlog.Text = "My Blog";
            this.btnMyBlog.UseVisualStyleBackColor = true;
            this.btnMyBlog.Click += new System.EventHandler(this.btnMyBlog_Click);
            // 
            // labelLoggedUser
            // 
            this.labelLoggedUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLoggedUser.Location = new System.Drawing.Point(98, 2);
            this.labelLoggedUser.Name = "labelLoggedUser";
            this.labelLoggedUser.Size = new System.Drawing.Size(150, 23);
            this.labelLoggedUser.TabIndex = 1;
            this.labelLoggedUser.Text = "user";
            this.labelLoggedUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(267, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 24);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelGuest
            // 
            this.panelGuest.BackColor = System.Drawing.SystemColors.Window;
            this.panelGuest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGuest.Controls.Add(this.labelGuest);
            this.panelGuest.Controls.Add(this.btnRegister);
            this.panelGuest.Controls.Add(this.btnLogin);
            this.panelGuest.Location = new System.Drawing.Point(12, 408);
            this.panelGuest.Name = "panelGuest";
            this.panelGuest.Size = new System.Drawing.Size(347, 30);
            this.panelGuest.TabIndex = 4;
            // 
            // labelGuest
            // 
            this.labelGuest.Location = new System.Drawing.Point(98, 2);
            this.labelGuest.Name = "labelGuest";
            this.labelGuest.Size = new System.Drawing.Size(150, 23);
            this.labelGuest.TabIndex = 2;
            this.labelGuest.Text = "Guest";
            this.labelGuest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(3, 2);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 24);
            this.btnRegister.TabIndex = 1;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(267, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 24);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 441);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(347, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(371, 466);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panelGuest);
            this.Controls.Add(this.panelLogged);
            this.Controls.Add(this.labelConnection);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listBlogs);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainForm";
            this.panelLogged.ResumeLayout(false);
            this.panelGuest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBlogs;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label labelConnection;
        private System.Windows.Forms.Panel panelLogged;
        private System.Windows.Forms.Label labelLoggedUser;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnMyBlog;
        private System.Windows.Forms.Panel panelGuest;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label labelGuest;
        private System.Windows.Forms.Button btnExit;
    }
}
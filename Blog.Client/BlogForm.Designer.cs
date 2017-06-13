namespace Blog.Client
{
    partial class BlogForm
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
            this.txtBoxBlogTitle = new System.Windows.Forms.TextBox();
            this.listEntries = new System.Windows.Forms.ListBox();
            this.panelUserControl = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.panelGuest = new System.Windows.Forms.Panel();
            this.labelGuest = new System.Windows.Forms.Label();
            this.panelUserControl.SuspendLayout();
            this.panelGuest.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxBlogTitle
            // 
            this.txtBoxBlogTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxBlogTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxBlogTitle.Location = new System.Drawing.Point(12, 9);
            this.txtBoxBlogTitle.Name = "txtBoxBlogTitle";
            this.txtBoxBlogTitle.ReadOnly = true;
            this.txtBoxBlogTitle.Size = new System.Drawing.Size(410, 24);
            this.txtBoxBlogTitle.TabIndex = 0;
            this.txtBoxBlogTitle.Text = "nazwa bloga";
            this.txtBoxBlogTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listEntries
            // 
            this.listEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntries.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listEntries.FormattingEnabled = true;
            this.listEntries.ItemHeight = 31;
            this.listEntries.Location = new System.Drawing.Point(12, 36);
            this.listEntries.Name = "listEntries";
            this.listEntries.Size = new System.Drawing.Size(410, 469);
            this.listEntries.TabIndex = 1;
            // 
            // panelUserControl
            // 
            this.panelUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUserControl.BackColor = System.Drawing.SystemColors.Window;
            this.panelUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserControl.Controls.Add(this.panelGuest);
            this.panelUserControl.Controls.Add(this.btnReturn);
            this.panelUserControl.Controls.Add(this.btnRefresh);
            this.panelUserControl.Location = new System.Drawing.Point(12, 511);
            this.panelUserControl.Name = "panelUserControl";
            this.panelUserControl.Size = new System.Drawing.Size(410, 87);
            this.panelUserControl.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(2, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(404, 24);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Location = new System.Drawing.Point(2, 60);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(404, 24);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // panelGuest
            // 
            this.panelGuest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGuest.Controls.Add(this.labelGuest);
            this.panelGuest.Location = new System.Drawing.Point(0, 29);
            this.panelGuest.Name = "panelGuest";
            this.panelGuest.Size = new System.Drawing.Size(410, 30);
            this.panelGuest.TabIndex = 2;
            // 
            // labelGuest
            // 
            this.labelGuest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGuest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelGuest.Location = new System.Drawing.Point(129, 2);
            this.labelGuest.Name = "labelGuest";
            this.labelGuest.Size = new System.Drawing.Size(150, 24);
            this.labelGuest.TabIndex = 0;
            this.labelGuest.Text = "Guest";
            this.labelGuest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BlogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(434, 611);
            this.ControlBox = false;
            this.Controls.Add(this.panelUserControl);
            this.Controls.Add(this.listEntries);
            this.Controls.Add(this.txtBoxBlogTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BlogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Blog";
            this.panelUserControl.ResumeLayout(false);
            this.panelGuest.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxBlogTitle;
        private System.Windows.Forms.ListBox listEntries;
        private System.Windows.Forms.Panel panelUserControl;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelGuest;
        private System.Windows.Forms.Label labelGuest;
    }
}
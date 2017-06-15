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
            this.panelLogged = new System.Windows.Forms.Panel();
            this.labelLoggedUser = new System.Windows.Forms.Label();
            this.btnChangeTitle = new System.Windows.Forms.Button();
            this.btnAddEntry = new System.Windows.Forms.Button();
            this.panelGuest = new System.Windows.Forms.Panel();
            this.labelGuest = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtBoxEntryTitle = new System.Windows.Forms.TextBox();
            this.panelUserControl2 = new System.Windows.Forms.Panel();
            this.panelLogged2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.btnEditCancel = new System.Windows.Forms.Button();
            this.btnEditSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh2 = new System.Windows.Forms.Button();
            this.txtBoxEntryText = new System.Windows.Forms.RichTextBox();
            this.panelUserControl.SuspendLayout();
            this.panelLogged.SuspendLayout();
            this.panelGuest.SuspendLayout();
            this.panelUserControl2.SuspendLayout();
            this.panelLogged2.SuspendLayout();
            this.panelEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxBlogTitle
            // 
            this.txtBoxBlogTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBoxBlogTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxBlogTitle.HideSelection = false;
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
            this.listEntries.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listEntries.FormattingEnabled = true;
            this.listEntries.HorizontalExtent = 1000;
            this.listEntries.HorizontalScrollbar = true;
            this.listEntries.ItemHeight = 31;
            this.listEntries.Location = new System.Drawing.Point(12, 36);
            this.listEntries.MaximumSize = new System.Drawing.Size(410, 469);
            this.listEntries.MinimumSize = new System.Drawing.Size(410, 469);
            this.listEntries.Name = "listEntries";
            this.listEntries.Size = new System.Drawing.Size(410, 469);
            this.listEntries.TabIndex = 1;
            this.listEntries.DoubleClick += new System.EventHandler(this.listEntries_DoubleClick);
            // 
            // panelUserControl
            // 
            this.panelUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelUserControl.BackColor = System.Drawing.SystemColors.Window;
            this.panelUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserControl.Controls.Add(this.panelLogged);
            this.panelUserControl.Controls.Add(this.panelGuest);
            this.panelUserControl.Controls.Add(this.btnReturn);
            this.panelUserControl.Controls.Add(this.btnRefresh);
            this.panelUserControl.Location = new System.Drawing.Point(12, 511);
            this.panelUserControl.MaximumSize = new System.Drawing.Size(410, 87);
            this.panelUserControl.MinimumSize = new System.Drawing.Size(410, 87);
            this.panelUserControl.Name = "panelUserControl";
            this.panelUserControl.Size = new System.Drawing.Size(410, 87);
            this.panelUserControl.TabIndex = 2;
            // 
            // panelLogged
            // 
            this.panelLogged.Controls.Add(this.labelLoggedUser);
            this.panelLogged.Controls.Add(this.btnChangeTitle);
            this.panelLogged.Controls.Add(this.btnAddEntry);
            this.panelLogged.Location = new System.Drawing.Point(0, 29);
            this.panelLogged.Name = "panelLogged";
            this.panelLogged.Size = new System.Drawing.Size(410, 30);
            this.panelLogged.TabIndex = 3;
            // 
            // labelLoggedUser
            // 
            this.labelLoggedUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLoggedUser.Location = new System.Drawing.Point(129, 2);
            this.labelLoggedUser.Name = "labelLoggedUser";
            this.labelLoggedUser.Size = new System.Drawing.Size(150, 24);
            this.labelLoggedUser.TabIndex = 2;
            this.labelLoggedUser.Text = "logged as:";
            this.labelLoggedUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnChangeTitle
            // 
            this.btnChangeTitle.Location = new System.Drawing.Point(285, 2);
            this.btnChangeTitle.Name = "btnChangeTitle";
            this.btnChangeTitle.Size = new System.Drawing.Size(121, 24);
            this.btnChangeTitle.TabIndex = 1;
            this.btnChangeTitle.Text = "Change Title";
            this.btnChangeTitle.UseVisualStyleBackColor = true;
            this.btnChangeTitle.Click += new System.EventHandler(this.btnChangeTitle_Click);
            // 
            // btnAddEntry
            // 
            this.btnAddEntry.Location = new System.Drawing.Point(2, 2);
            this.btnAddEntry.Name = "btnAddEntry";
            this.btnAddEntry.Size = new System.Drawing.Size(121, 24);
            this.btnAddEntry.TabIndex = 0;
            this.btnAddEntry.Text = "New Entry";
            this.btnAddEntry.UseVisualStyleBackColor = true;
            this.btnAddEntry.Click += new System.EventHandler(this.btnAddEntry_Click);
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
            // txtBoxEntryTitle
            // 
            this.txtBoxEntryTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxEntryTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.txtBoxEntryTitle.Location = new System.Drawing.Point(462, 9);
            this.txtBoxEntryTitle.Name = "txtBoxEntryTitle";
            this.txtBoxEntryTitle.ReadOnly = true;
            this.txtBoxEntryTitle.Size = new System.Drawing.Size(410, 24);
            this.txtBoxEntryTitle.TabIndex = 3;
            this.txtBoxEntryTitle.Text = "nazwa wpisu";
            this.txtBoxEntryTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelUserControl2
            // 
            this.panelUserControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUserControl2.BackColor = System.Drawing.SystemColors.Window;
            this.panelUserControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserControl2.Controls.Add(this.panelLogged2);
            this.panelUserControl2.Controls.Add(this.panelEdit);
            this.panelUserControl2.Controls.Add(this.btnClose);
            this.panelUserControl2.Controls.Add(this.btnRefresh2);
            this.panelUserControl2.Location = new System.Drawing.Point(462, 511);
            this.panelUserControl2.Name = "panelUserControl2";
            this.panelUserControl2.Size = new System.Drawing.Size(410, 87);
            this.panelUserControl2.TabIndex = 5;
            // 
            // panelLogged2
            // 
            this.panelLogged2.Controls.Add(this.btnDelete);
            this.panelLogged2.Location = new System.Drawing.Point(0, 29);
            this.panelLogged2.Name = "panelLogged2";
            this.panelLogged2.Size = new System.Drawing.Size(410, 30);
            this.panelLogged2.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(2, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(404, 24);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.btnEditCancel);
            this.panelEdit.Controls.Add(this.btnEditSave);
            this.panelEdit.Location = new System.Drawing.Point(0, 29);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(410, 30);
            this.panelEdit.TabIndex = 2;
            // 
            // btnEditCancel
            // 
            this.btnEditCancel.Location = new System.Drawing.Point(285, 2);
            this.btnEditCancel.Name = "btnEditCancel";
            this.btnEditCancel.Size = new System.Drawing.Size(121, 24);
            this.btnEditCancel.TabIndex = 1;
            this.btnEditCancel.Text = "Cancel";
            this.btnEditCancel.UseVisualStyleBackColor = true;
            this.btnEditCancel.Click += new System.EventHandler(this.btnEditCancel_Click);
            // 
            // btnEditSave
            // 
            this.btnEditSave.Location = new System.Drawing.Point(2, 2);
            this.btnEditSave.Name = "btnEditSave";
            this.btnEditSave.Size = new System.Drawing.Size(121, 24);
            this.btnEditSave.TabIndex = 0;
            this.btnEditSave.Text = "Save";
            this.btnEditSave.UseVisualStyleBackColor = true;
            this.btnEditSave.Click += new System.EventHandler(this.btnEditSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(2, 60);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(404, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh2
            // 
            this.btnRefresh2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh2.Location = new System.Drawing.Point(2, 3);
            this.btnRefresh2.Name = "btnRefresh2";
            this.btnRefresh2.Size = new System.Drawing.Size(404, 24);
            this.btnRefresh2.TabIndex = 0;
            this.btnRefresh2.Text = "Refresh";
            this.btnRefresh2.UseVisualStyleBackColor = true;
            this.btnRefresh2.Click += new System.EventHandler(this.btnRefresh2_Click);
            // 
            // txtBoxEntryText
            // 
            this.txtBoxEntryText.Location = new System.Drawing.Point(462, 36);
            this.txtBoxEntryText.Name = "txtBoxEntryText";
            this.txtBoxEntryText.Size = new System.Drawing.Size(411, 469);
            this.txtBoxEntryText.TabIndex = 6;
            this.txtBoxEntryText.Text = "";
            // 
            // BlogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(884, 611);
            this.ControlBox = false;
            this.Controls.Add(this.txtBoxEntryText);
            this.Controls.Add(this.panelUserControl2);
            this.Controls.Add(this.txtBoxEntryTitle);
            this.Controls.Add(this.panelUserControl);
            this.Controls.Add(this.listEntries);
            this.Controls.Add(this.txtBoxBlogTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BlogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Blog";
            this.panelUserControl.ResumeLayout(false);
            this.panelLogged.ResumeLayout(false);
            this.panelGuest.ResumeLayout(false);
            this.panelUserControl2.ResumeLayout(false);
            this.panelLogged2.ResumeLayout(false);
            this.panelEdit.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtBoxEntryTitle;
        private System.Windows.Forms.Panel panelUserControl2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh2;
        private System.Windows.Forms.Panel panelLogged;
        private System.Windows.Forms.Button btnChangeTitle;
        private System.Windows.Forms.Button btnAddEntry;
        private System.Windows.Forms.Label labelLoggedUser;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.Button btnEditCancel;
        private System.Windows.Forms.Button btnEditSave;
        private System.Windows.Forms.Panel panelLogged2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.RichTextBox txtBoxEntryText;
    }
}
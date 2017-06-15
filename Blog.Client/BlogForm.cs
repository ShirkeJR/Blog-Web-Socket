using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Client
{
    public partial class BlogForm : Form
    {
        public string Title { set; get; }
        public int ID { set; get; }
        public int EID { set; get; }

        public BlogForm(int id, string title)
        {
            InitializeComponent();
            ID = id;
            Title = title.Remove(0, 3);
            DataService.Instance.TxtBoxBlogTitle = txtBoxBlogTitle;
            DataService.Instance.ListEntries = listEntries;
            DataService.Instance.LabelLoggedUser = labelLoggedUser;
            DataService.Instance.TxtBoxEntryTitle = txtBoxEntryTitle;
            DataService.Instance.TxtBoxEntryText = txtBoxEntryText;
            DataService.Instance.EntriesID = new List<int>();

            DataService.Instance.GetLoggedUser();
            txtBoxBlogTitle.Text = Title;
            DataService.Instance.GetEntries(ID);

            this.Width = 450;
            this.Height = 650;
            txtBoxEntryText.Visible = false;
            txtBoxEntryTitle.Visible = false;
            panelUserControl2.Visible = false;

            txtBoxBlogTitle.SelectionLength = 0;
            txtBoxBlogTitle.SelectionStart = 0;
            txtBoxBlogTitle.Enabled = false;

            txtBoxEntryTitle.ReadOnly = true;
            txtBoxEntryText.ReadOnly = true;

            if (AccountService.Instance.Logged)
            {
                panelLogged.Visible = true;
                panelLogged2.Visible = true;
                panelEdit.Visible = false;
                panelGuest.Visible = false;
                if (AccountService.Instance.User.ID != ID)
                {
                    btnAddEntry.Enabled = false;
                    btnChangeTitle.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnAddEntry.Enabled = true;
                    btnChangeTitle.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
            else
            {
                panelLogged.Visible = false;
                panelLogged2.Visible = false;
                panelEdit.Visible = false;
                panelGuest.Visible = true;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataService.Instance.GetLoggedUser();          
            DataService.Instance.GetEntries(ID);
            Title = DataService.Instance.GetBlogTitle(ID);
            txtBoxBlogTitle.Text = Title;
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            this.Width = 900;
            this.Height = 650;
            txtBoxEntryText.Visible = true;
            txtBoxEntryTitle.Visible = true;
            panelUserControl2.Visible = true;
            panelLogged2.Visible = false;
            panelEdit.Visible = true;

            txtBoxEntryTitle.Text = "<title>";
            txtBoxEntryTitle.ReadOnly = false;
            txtBoxEntryText.Text = "text";
            txtBoxEntryText.ReadOnly = false;

            panelUserControl.Enabled = false;
            btnRefresh2.Enabled = false;
            btnClose.Enabled = false;
        }
        private void btnChangeTitle_Click(object sender, EventArgs e)
        {
            if(btnChangeTitle.Text.Equals("Change Title"))
            {
                btnChangeTitle.Text = "Save";
                txtBoxBlogTitle.Enabled = true;
                txtBoxBlogTitle.ReadOnly = false;

                panelUserControl2.Enabled = false;
                btnRefresh.Enabled = false;
                btnReturn.Enabled = false;
                btnAddEntry.Enabled = false;
            }
            else if (btnChangeTitle.Text.Equals("Save"))
            {
                txtBoxBlogTitle.ReadOnly = true;
                btnChangeTitle.Text = "Change Title";
                string newTitle = txtBoxBlogTitle.Text;

                panelUserControl2.Enabled = true;
                btnRefresh.Enabled = true;
                btnReturn.Enabled = true;
                btnAddEntry.Enabled = true;
                if (DataService.Instance.ChangeBlogTitle(ID, newTitle)) Title = newTitle;
                else txtBoxBlogTitle.Text = Title;
                txtBoxBlogTitle.Enabled = false;
            }

        }
        //druga czesc
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Width = 450;
            this.Height = 650;
            txtBoxEntryText.Visible = false;
            txtBoxEntryTitle.Visible = false;
            panelUserControl2.Visible = false;
        }
        private void btnRefresh2_Click(object sender, EventArgs e)
        {
            DataService.Instance.GetEntries(ID);
            if(DataService.Instance.EntriesID.Contains(EID))
                DataService.Instance.DisplayEntry(EID);
            else
            {
                this.Width = 450;
                this.Height = 650;
                txtBoxEntryText.Visible = false;
                txtBoxEntryTitle.Visible = false;
                panelUserControl2.Visible = false;
            }
        }
        private void btnEditSave_Click(object sender, EventArgs e)
        {
            DataService.Instance.AddEntry(txtBoxEntryTitle.Text, txtBoxEntryText.Text);
            this.Width = 450;
            this.Height = 650;
            txtBoxEntryText.Visible = false;
            txtBoxEntryTitle.Visible = false;
            panelUserControl2.Visible = false;
            panelEdit.Visible = false;
            panelLogged2.Visible = true;

            txtBoxEntryTitle.ReadOnly = true;
            txtBoxEntryText.ReadOnly = true;

            panelUserControl.Enabled = true;
            btnRefresh2.Enabled = true;
            btnClose.Enabled = true;

            DataService.Instance.GetLoggedUser();
            txtBoxBlogTitle.Text = Title;
            DataService.Instance.GetEntries(ID);

        }
        private void btnEditCancel_Click(object sender, EventArgs e)
        {
            this.Width = 450;
            this.Height = 650;
            txtBoxEntryText.Visible = false;
            txtBoxEntryTitle.Visible = false;
            panelUserControl2.Visible = false;
            panelEdit.Visible = false;
            panelLogged2.Visible = true;

            txtBoxEntryTitle.ReadOnly = true;
            txtBoxEntryText.ReadOnly = true;

            panelUserControl.Enabled = true;
            btnRefresh2.Enabled = true;
            btnClose.Enabled = true;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DataService.Instance.DeleteEntry(DataService.Instance.EID))
            {
                this.Width = 450;
                txtBoxEntryText.Visible = false;
                txtBoxEntryTitle.Visible = false;
                panelUserControl2.Visible = false;
                //panelEdit.Visible = false;

                //txtBoxEntryTitle.ReadOnly = true;
                //txtBoxEntryText.ReadOnly = true;

                DataService.Instance.GetLoggedUser();
                txtBoxBlogTitle.Text = Title;
                DataService.Instance.GetEntries(ID);
            }   
        }
        private void listEntries_DoubleClick(object sender, EventArgs e)
        {
            if (listEntries.SelectedItem != null)
            {
                EID = DataService.Instance.EntriesID[listEntries.SelectedIndex];

                DataService.Instance.DisplayEntry(EID);

                this.Width = 900;
                this.Height = 650;
                txtBoxEntryText.Visible = true;
                txtBoxEntryTitle.Visible = true;
                panelUserControl2.Visible = true;

                txtBoxEntryTitle.ReadOnly = true;
                txtBoxEntryText.ReadOnly = true;
            }
        }
    }
}

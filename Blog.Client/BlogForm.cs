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
        public BlogForm()
        {
            InitializeComponent();
            DataService.Instance.TxtBoxBlogTitle = txtBoxBlogTitle;
            DataService.Instance.ListEntries = listEntries;
            DataService.Instance.LabelLoggedUser = labelLoggedUser;
            DataService.Instance.TxtBoxEntryTitle = txtBoxEntryTitle;
            DataService.Instance.TxtBoxEntryText = txtBoxEntryText;

            DataService.Instance.GetLoggedUser();
            txtBoxBlogTitle.Text = Title;
            DataService.Instance.GetEntries(ID);

            this.Width = 450;
            txtBoxEntryText.Visible = false;
            txtBoxEntryTitle.Visible = false;
            panelUserControl2.Visible = false;
            panelEdit.Visible = false;
            panelLogged2.Visible = false;

            panelUserControl.Visible = true;
            if (AccountService.Instance.Logged)
            {
                panelLogged.Visible = true;
                panelGuest.Visible = false;
            }
            else
            {
                panelLogged.Visible = true;
                panelGuest.Visible = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!ConnectionService.Instance.Connected()) ConnectionService.Instance.Reconnect();
            DataService.Instance.GetLoggedUser();
            txtBoxBlogTitle.Text = Title;
            DataService.Instance.GetEntries(ID);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            this.Width = 900;
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
                txtBoxBlogTitle.ReadOnly = false;

                panelUserControl2.Enabled = false;
                btnRefresh.Enabled = false;
                btnReturn.Enabled = false;
                btnAddEntry.Enabled = false;
            }
            if (btnChangeTitle.Text.Equals("Save"))
            {
                txtBoxBlogTitle.ReadOnly = true;
                btnChangeTitle.Text = "Change Title";
                string newTitle = txtBoxBlogTitle.Text;

                panelUserControl2.Enabled = true;
                btnRefresh.Enabled = true;
                btnReturn.Enabled = true;
                btnAddEntry.Enabled = true;
                if (DataService.Instance.ChangeBlogTitle(ID, newTitle))
                    Title = newTitle;
            }

        }
        //druga czesc
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Width = 450;
            txtBoxEntryText.Visible = false;
            txtBoxEntryTitle.Visible = false;
            panelUserControl2.Visible = false;
        }

        private void btnRefresh2_Click(object sender, EventArgs e)
        {
            if (!ConnectionService.Instance.Connected()) ConnectionService.Instance.Reconnect();
            DataService.Instance.DisplayEntry(DataService.Instance.EID);
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            DataService.Instance.AddEntry(txtBoxEntryTitle.Text, txtBoxEntryText.Text);
            this.Width = 450;
            txtBoxEntryText.Visible = false;
            txtBoxEntryTitle.Visible = false;
            panelUserControl2.Visible = false;
            panelEdit.Visible = false;

            txtBoxEntryTitle.ReadOnly = true;
            txtBoxEntryText.ReadOnly = true;

            panelUserControl.Enabled = true;
            btnRefresh2.Enabled = true;
            btnClose.Enabled = true;

            if (!ConnectionService.Instance.Connected()) ConnectionService.Instance.Reconnect();
            DataService.Instance.GetLoggedUser();
            txtBoxBlogTitle.Text = Title;
            DataService.Instance.GetEntries(ID);

        }

        private void btnEditCancel_Click(object sender, EventArgs e)
        {
            this.Width = 450;
            txtBoxEntryText.Visible = false;
            txtBoxEntryTitle.Visible = false;
            panelUserControl2.Visible = false;
            panelEdit.Visible = false;

            txtBoxEntryTitle.ReadOnly = true;
            txtBoxEntryText.ReadOnly = true;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DataService.Instance.DeleteEntry(DataService.Instance.EID))
            {
                this.Width = 450;
                txtBoxEntryText.Visible = false;
                txtBoxEntryTitle.Visible = false;
                panelUserControl2.Visible = false;
                panelEdit.Visible = false;

                txtBoxEntryTitle.ReadOnly = true;
                txtBoxEntryText.ReadOnly = true;

                if (!ConnectionService.Instance.Connected()) ConnectionService.Instance.Reconnect();
                DataService.Instance.GetLoggedUser();
                txtBoxBlogTitle.Text = Title;
                DataService.Instance.GetEntries(ID);
            }   
        }

        private void listEntries_DoubleClick(object sender, EventArgs e)
        {
            if (listEntries.SelectedItem != null)
            {
                int id = Convert.ToInt32(listEntries.SelectedItem.ToString().Split('|')[0]);

                DataService.Instance.DisplayEntry(id);

                this.Width = 900;
                txtBoxEntryText.Visible = true;
                txtBoxEntryTitle.Visible = true;
                panelUserControl2.Visible = true;
                
                txtBoxEntryTitle.ReadOnly = true;
                txtBoxEntryText.ReadOnly = true;

                if (AccountService.Instance.Logged)
                {
                    panelLogged2.Visible = true;
                    panelEdit.Visible = false;
                }
                else
                {
                    panelLogged2.Visible = false;
                    panelEdit.Visible = false;
                }

            }
        }
    }
}

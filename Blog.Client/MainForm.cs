using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            panelLogged.Visible = false;
            panelGuest.Visible = true;

            DataService.Instance.LabelConnection = labelConnection;
            DataService.Instance.LabelLoggedUser = labelLoggedUser;
            DataService.Instance.ListBlogs = listBlogs;
            DataService.Instance.BlogsID = new List<int>();

            DataService.Instance.GetBlogs();
            DataService.Instance.GetConnection();
        }

        private void listBlogs_DoubleClick(object sender, EventArgs e)
        {
            if (listBlogs.SelectedItem != null)
            {
                int id = DataService.Instance.BlogsID[listBlogs.SelectedIndex];
                string title = listBlogs.SelectedItem.ToString();
                BlogForm child = new BlogForm(id, title);
                this.Hide();
                child.ShowDialog();
                DataService.Instance.LabelLoggedUser = labelLoggedUser;
                DataService.Instance.GetLoggedUser();
                DataService.Instance.GetBlogs();
                DataService.Instance.GetConnection();
                this.Show();
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //if (!ConnectionService.Instance.Connected()) ConnectionService.Instance.Reconnect();
            DataService.Instance.GetBlogs();
            DataService.Instance.GetConnection();  
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            DialogResult result = login.ShowDialog();
            login.Dispose();
            if(result == DialogResult.OK)
            {
                DataService.Instance.GetLoggedUser();
                panelLogged.Visible = true;
                panelGuest.Visible = false;
            }   
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            AccountService.Instance.Logout();
            DataService.Instance.GetLoggedUser();
            panelLogged.Visible = false;
            panelGuest.Visible = true;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            DialogResult result = register.ShowDialog();
            register.Dispose();
            DataService.Instance.GetBlogs();
            DataService.Instance.GetConnection();
        }
        private void btnMyBlog_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(AccountService.Instance.User.ID);
            string title = DataService.Instance.GetBlogTitle(Convert.ToInt32(AccountService.Instance.User.ID));
            BlogForm child = new BlogForm(id, title);
            this.Hide();
            child.ShowDialog();
            DataService.Instance.LabelLoggedUser = labelLoggedUser;
            DataService.Instance.GetLoggedUser();
            DataService.Instance.GetBlogs();
            DataService.Instance.GetConnection();
            this.Show();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            ConnectionService.Instance.Disconnect();
            this.Close();
        }
    }
}

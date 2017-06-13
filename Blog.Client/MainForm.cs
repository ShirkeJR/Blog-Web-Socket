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

            DataService.Instance.GetBlogs();
            DataService.Instance.GetConnection();
        }

        private void listBlogs_DoubleClick(object sender, EventArgs e)
        {
            if (listBlogs.SelectedItem != null)
            {
                BlogForm child = new BlogForm();
                this.Hide();
                child.ID = Convert.ToInt32(listBlogs.SelectedItem.ToString().Split('|'));
                child.ShowDialog();
                this.Show();
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!ConnectionService.Instance.Connected()) ConnectionService.Instance.Reconnect();

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
        }
        private void btnMyBlog_Click(object sender, EventArgs e)
        {
            //blog frame + id;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            ConnectionService.Instance.Disconnect();
            this.Close();
        }
    }
}

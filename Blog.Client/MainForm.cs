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
            DataService.Instance.ListBlogs = listBlogs;
            DataService.Instance.GetBlogsList();
            DataService.Instance.GetConnection();
        }

        private void listBlogs_DoubleClick(object sender, EventArgs e)
        {
            listBlogs.SelectedItem = "lel"; // nie dziala xd
            BlogForm child = new BlogForm();
            this.Hide();
            child.ShowDialog();
            this.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(!ConnectionService.Instance.ConnectionSocket.Connected)
            {
                labelConnection.Text = "Reconnect.";
                ConnectionService.Instance.Connect(ConnectionService.Instance.Host, ConnectionService.Instance.Port);
                DataService.Instance.GetBlogsList();
            }
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

        }

        private void btnMyBlog_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ConnectionService.Instance.Disconnect();
            this.Close();
        }
    }
}

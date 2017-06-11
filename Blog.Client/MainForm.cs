﻿using System;
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
            labelConnection.Text = "Connected to:: " + IPAddress.Parse(((IPEndPoint)ServerConnection.Instance.ConnectionSocket.RemoteEndPoint).Address.ToString()) + ":" + ((IPEndPoint)ServerConnection.Instance.ConnectionSocket.RemoteEndPoint).Port.ToString();
            ServerConnection.Instance.listBlogs = listBlogs;
            ServerConnection.Instance.GetBlogsList();
        }

        private void listBlogs_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ServerConnection.Instance.GetBlogsList();
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
    }
}

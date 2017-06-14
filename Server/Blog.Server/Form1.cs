using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Server
{
    public partial class Form1 : Form
    {
        private bool running;
        public Form1()
        {
            InitializeComponent();
            TestServer.Instance.logBox = logBox;
            TestServer.Instance.clientBox = clientBox;
            running = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            logBox.Items.Add("*Server starting...");
            button1.Enabled = false;
            if (!running)
            {
                running = true;
                await Task.Run(() => TestServer.Instance.StartListening());
            }  
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void logBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

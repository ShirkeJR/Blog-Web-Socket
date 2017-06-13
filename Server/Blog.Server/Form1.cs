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
        public Form1()
        {
            InitializeComponent();
            TestServer.Instance.logBox = logBox;
            TestServer.Instance.clientBox = clientBox;
            button2.Enabled = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            logBox.Items.Add("Server starting...");
            button1.Enabled = false;
            button2.Enabled = true;
            await Task.Run(() => TestServer.Instance.StartListening());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }
}

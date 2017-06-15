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
            LoggingService.Instance.Initialize(logBox, clientBox);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            LoggingService.Instance.AddLog("*Server starting...");
            btnStart.Enabled = false;
            await Task.Run(() => TestServer.Instance.StartListening());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LoggingService.Instance.AddLog("*Server closing...");
            System.Environment.Exit(1);
        }

    }
}

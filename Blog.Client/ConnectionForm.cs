using Blog.Constants;
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
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
            txtBoxHost.Text = Convert.ToString(Dns.Resolve(Dns.GetHostName()).AddressList[0]);
            txtBoxPort.Text = Convert.ToString(Int16Constants.DefaultPort);
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            string host = txtBoxHost.Text.ToString();
            ushort port = Convert.ToUInt16(txtBoxPort.Text.ToString());
            if (ConnectionService.Instance.Connect(host, port)) this.DialogResult = DialogResult.OK;
            else MessageBox.Show("Can't connect to server.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

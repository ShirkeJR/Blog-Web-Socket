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
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (ConnectionService.Instance.Connect(txtBoxHost.Text.ToString(), Convert.ToUInt16(txtBoxPort.Text.ToString())))
                this.DialogResult = DialogResult.OK;
            else
                MessageBox.Show("Can't connect to server.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

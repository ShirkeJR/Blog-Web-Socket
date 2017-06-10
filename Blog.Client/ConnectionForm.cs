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
            ServerConnection.Instance.Host = txtBoxHost.Text.ToString();
            ServerConnection.Instance.Port = Convert.ToUInt16(txtBoxPort.Text.ToString());
            if(ServerConnection.Instance.Connect())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Can't connect to server.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.DialogResult = DialogResult.Cancel;
                //this.DialogResult = DialogResult.OK;
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

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
        AsynchronousSocketListener asynchronousSocketListener;
        public Form1()
        {
            InitializeComponent();
            asynchronousSocketListener = new AsynchronousSocketListener(logBox, clientBox);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() => AsynchronousSocketListener.StartListening());
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}

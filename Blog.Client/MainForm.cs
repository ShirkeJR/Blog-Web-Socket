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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ServerConnection.Instance.blogsList = listBox1;
            ServerConnection.Instance.GetBlogsList();
        }
    }
}

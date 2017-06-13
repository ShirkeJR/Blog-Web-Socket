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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtBoxLogin.Text;
            string pass = txtBoxPassword.Text;
            if (AccountService.Instance.Login(login, pass)) this.DialogResult = DialogResult.OK;
            else MessageBox.Show("Wrong login or password", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

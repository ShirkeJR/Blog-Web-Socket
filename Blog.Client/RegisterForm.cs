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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string login = txtBoxLogin.Text;
            string pass1 = txtBoxPassword1.Text;
            string pass2 = txtBoxPassword2.Text;
            if (pass1.Equals(pass2) && !pass1.Equals(""))
            {
                if(login.Equals("")) MessageBox.Show("Login can't be empty", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (AccountService.Instance.Register(login, pass1)) this.DialogResult = DialogResult.OK;
                    else MessageBox.Show("Register error", "Register error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }               
            }
            else MessageBox.Show("Both passwords must be equal", "Password error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}

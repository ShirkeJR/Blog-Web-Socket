﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConnectionForm connect = new ConnectionForm();
            DialogResult result = connect.ShowDialog();
            connect.Dispose();
            if(result == DialogResult.OK) Application.Run(new MainForm());
        }
    }
}

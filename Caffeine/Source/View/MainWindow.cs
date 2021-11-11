using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Caffeine.View
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();

            Program.Window = this;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            TopLevel = false;

            SystemTrayIcon TrayIcon = new SystemTrayIcon();
        }

    }
}

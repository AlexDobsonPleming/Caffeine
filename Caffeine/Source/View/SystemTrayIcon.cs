using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Caffeine.View
{
    class SystemTrayIcon
    {
        private SystemTrayIconVM ViewModel = new SystemTrayIconVM();

        private NotifyIcon TrayIcon = new NotifyIcon();
        private SystemTrayContextMenu ContextMenu = new SystemTrayContextMenu();
        public SystemTrayIcon()
        {
            TrayIcon.Visible = true;
            TrayIcon.ContextMenuStrip = ContextMenu;
            UpdateIcon();

            ViewModel.CurrentStateChanged += ViewModel_CurrentStateChanged;
            TrayIcon.MouseClick += TrayIconClicked;

        }

        private void ViewModel_CurrentStateChanged(object sender, EventArgs e)
        {
            UpdateIcon();
        }

        private void UpdateIcon()
        {
            TrayIcon.Text = ViewModel.CurrentState.Text;
            TrayIcon.Icon = ViewModel.CurrentState.Icon;
        }

        private void TrayIconClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ViewModel.LeftClickChangePowerState();
            }
            else if (e.Button == MouseButtons.Right) {
                ContextMenu.Show();
            }
        }
    }
}

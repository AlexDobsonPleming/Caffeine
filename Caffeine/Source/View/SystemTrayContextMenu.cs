using System;
using System.Drawing;
using System.Windows.Forms;
using Caffeine.View_Model;

namespace Caffeine.View
{
    class SystemTrayContextMenu : ContextMenuStrip
    {
        private SystemTrayContextMenuVM ViewModel = new SystemTrayContextMenuVM();

        private ScreensOffToolstripMenuItem ScreensOffMenuItem;
        private SleepToolstripMenuItem SleepMenuItem;
        public SystemTrayContextMenu()
        {
            ScreensOffMenuItem = new ScreensOffToolstripMenuItem(ViewModel.ScreensOut, ViewModel.TurnOffScreens);
            SleepMenuItem = new SleepToolstripMenuItem(ViewModel.SleepImage, ViewModel.Sleep);

            Items.AddRange(new ToolStripMenuItem[] { SleepMenuItem, ScreensOffMenuItem});
        }

        private class ScreensOffToolstripMenuItem : ToolStripMenuItem
        {
            private Action OnClickVM;

            public ScreensOffToolstripMenuItem(Image ScreensOffImage, Action OnClickDelegateToUse)
            {
                Text = "Screens Off";
                Image = ScreensOffImage;
                base.Click += ScreensOffMenuItem_Click;
                OnClickVM = OnClickDelegateToUse;
            }

            private void ScreensOffMenuItem_Click(object sender, EventArgs e)
            {
                OnClickVM();
            }
        }
        private class SleepToolstripMenuItem : ToolStripMenuItem
        {
            private Action OnClickVM;

            public SleepToolstripMenuItem(Image SleepImage, Action OnClickDelegateToUse)
            {
                Text = "Sleep";
                Image = SleepImage;
                base.Click += SleepToolstripMenuItem_Click;
                OnClickVM = OnClickDelegateToUse;
            }

            private void SleepToolstripMenuItem_Click(object sender, EventArgs e)
            {
                OnClickVM();
            }
        }
    }
}

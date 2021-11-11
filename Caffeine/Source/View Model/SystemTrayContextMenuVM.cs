using System;
using System.Collections.Generic;
using System.Drawing;
using Caffeine.Model;
namespace Caffeine.View_Model
{
    public class SystemTrayContextMenuVM
    {
        private Win32MonitorState Screens = new Win32MonitorState();
        private Win32Sleep SleepyTime = new Win32Sleep();
        public Image SleepImage { get { return Properties.Resources.Sleepy.ToBitmap(); } }
        public Image ScreensOut { get { return Properties.Resources.ScreensOut.ToBitmap(); } }

        public void TurnOffScreens()
        {
            Screens.TurnOffScreens();
        }
        public void Sleep()
        {
            SleepyTime.StandBy();
        }
    }
}

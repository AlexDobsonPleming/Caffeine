using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Caffeine.Model
{
    class Win32MonitorState
    {
        #region Turn off monitor Win32
        [DllImport("user32.dll")]
        /// <summary>
        /// Prevent screensaver, display dimming and power saving. This function wraps PInvokes on Win32 API. 
        /// </summary>
        /// <param name="hWnd">Handle to the window whose window procedure will receive the message.</param>
        /// <param name="Msg">Message to send</param>
        /// <param name="wParam">Additional message-specific information</param>
        /// <param name="lParam">Monitor state</param>
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const int SC_MONITORPOWER = 0xF170;
        private const int WM_SYSCOMMAND = 0x0112;
        
        private enum MONITOR_STATE
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }
        #endregion

        internal void TurnOffScreens()
        {
            IntPtr MainWindow_hWND = Program.Window.Handle;
            SendMessage(MainWindow_hWND, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)MONITOR_STATE.OFF);
        }


    }
}

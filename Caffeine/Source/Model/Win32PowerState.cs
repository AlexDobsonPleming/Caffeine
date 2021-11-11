using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Caffeine
{
    public class Win32PowerState
    {
        //Modifed from https://stackoverflow.com/questions/629240/prevent-windows-from-going-into-sleep-when-my-program-is-running


        #region "Win32 Imports"
        [DllImport("kernel32.dll")]
        private static extern IntPtr PowerCreateRequest(ref POWER_REQUEST_CONTEXT Context);

        [DllImport("kernel32.dll")]
        private static extern bool PowerSetRequest(IntPtr PowerRequestHandle, PowerRequestType RequestType);

        [DllImport("kernel32.dll")]
        private static extern bool PowerClearRequest(IntPtr PowerRequestHandle, PowerRequestType RequestType);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        internal static extern int CloseHandle(IntPtr hObject);

        enum PowerRequestType //see https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/wdm/ne-wdm-_power_request_type for more info
        {
            PowerRequestDisplayRequired,    //Screen will not turn off from inactivity
            PowerRequestSystemRequired,     //System will not sleep from inactivity
            PowerRequestAwayModeRequired,   //System pretends to sleep by turning off screen and sound if ordered to but continues execution
            PowerRequestExecutionRequired   //Process will not be terminated by power management. Only supported on NT 6.2+ (Win8+)
        }

        const int POWER_REQUEST_CONTEXT_VERSION = 0;
        const int POWER_REQUEST_CONTEXT_SIMPLE_STRING = 0x1;
        const int POWER_REQUEST_CONTEXT_DETAILED_STRING = 0x2;



        // Availablity Request Structures
        // Note:  Windows defines the POWER_REQUEST_CONTEXT structure with an
        // internal union of SimpleReasonString and Detailed information.
        // To avoid runtime interop issues, this version of 
        // POWER_REQUEST_CONTEXT only supports SimpleReasonString.  
        // To use the detailed information,
        // define the PowerCreateRequest function with the first 
        // parameter of type POWER_REQUEST_CONTEXT_DETAILED.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct POWER_REQUEST_CONTEXT
        {
            public UInt32 Version;
            public UInt32 Flags;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string SimpleReasonString;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PowerRequestContextDetailedInformation
        {
            public IntPtr LocalizedReasonModule;
            public UInt32 LocalizedReasonId;
            public UInt32 ReasonStringCount;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string[] ReasonStrings;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct POWER_REQUEST_CONTEXT_DETAILED
        {
            public UInt32 Version;
            public UInt32 Flags;
            public PowerRequestContextDetailedInformation DetailedInformation;
        }
        #endregion

        private bool _StayingAwake;

        public bool StayingAwake
        {
            get { return _StayingAwake; }
            set
            {
                _StayingAwake = value;
                EnableConstantDisplayAndPower(value);
                StayingAwakeStateChanged?.Invoke(this, new EventArgs()); ;
            }
        }

        public Win32PowerState()
        {
            StayingAwake = false;
        }

        public event EventHandler StayingAwakeStateChanged;

        /// <summary>
        /// Prevent screensaver, display dimming and power saving. This function wraps PInvokes on Win32 API. 
        /// </summary>
        /// <param name="KeepDisplayAndPowerAwake">True to get a constant display and power - False to clear the settings</param>
        private void EnableConstantDisplayAndPower(bool KeepDisplayAndPowerAwake)
        {
            POWER_REQUEST_CONTEXT RequestContext = new POWER_REQUEST_CONTEXT()
            {
                Version = POWER_REQUEST_CONTEXT_VERSION,
                Flags = POWER_REQUEST_CONTEXT_SIMPLE_STRING,
                SimpleReasonString = "Keep awake tray icon activated"
            };

            IntPtr _PowerRequest = PowerCreateRequest(ref RequestContext); //HANDLE
            if (KeepDisplayAndPowerAwake == true)
            {
                // Set the request
                PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestSystemRequired);
                PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestDisplayRequired);
            }
            else
            {
                // Clear the request
                PowerClearRequest(_PowerRequest, PowerRequestType.PowerRequestSystemRequired);
                PowerClearRequest(_PowerRequest, PowerRequestType.PowerRequestDisplayRequired);

                CloseHandle(_PowerRequest);
            }

        }
    }
}

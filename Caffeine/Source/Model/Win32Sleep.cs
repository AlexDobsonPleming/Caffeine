using System.Runtime.InteropServices;

namespace Caffeine.Model
{
    internal class Win32Sleep
    {
        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        internal void StandBy()
        {
            SetSuspendState(false, true, true);
        }
        internal void Hibernate()
        {
            SetSuspendState(true, true, true);
        }
        
    }
}

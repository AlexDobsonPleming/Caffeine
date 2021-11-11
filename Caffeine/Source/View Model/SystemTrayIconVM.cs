using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Caffeine
{
    class SystemTrayIconVM
    {
        private Win32PowerState PowerStateModel = new Win32PowerState();

        public Dictionary<bool, IconState> IconStates = new Dictionary<bool, IconState>() {
            {false, new IconState(Properties.Resources.Sleepy, "Sleepy") },
            {true,  new IconState(Properties.Resources.Caffeinated, "Staying awake") } };

        public SystemTrayIconVM()
        {
            PowerStateModel.StayingAwakeStateChanged += PowerStateModel_StayingAwakeStateChanged;
        }

        private void PowerStateModel_StayingAwakeStateChanged(object sender, EventArgs e)
        {
            CurrentStateChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CurrentStateChanged;
        public IconState CurrentState { get { return IconStates[PowerStateModel.StayingAwake]; } }


        public void LeftClickChangePowerState()
        {
            PowerStateModel.StayingAwake = !(PowerStateModel.StayingAwake);
        }

        public void RightClickContextMenu()
        {

        }
    }
}

using Microsoft.Win32.TaskScheduler;

//UNDER CONSTRUCTION
namespace Caffeine.Source.Model
{
    class Autostart
    {
        private bool _RunOnLogin;

        public bool RunOnLogin { 
            get => _RunOnLogin;
            set {
                _RunOnLogin = value;

                } 
        }

        private void AddLoginTask()
        {
            TaskService.Instance.AddTask("Test", QuickTriggerType.Daily, "myprogram.exe", "-a arg");
        }
    }
}

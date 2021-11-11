using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Caffeine.Source.View_Model
{
    public class MainWindowVM
    {
        public string WindowTitle
        {
            get { return "Caffeine";  }
        }

        public Icon WindowIcon
        {
            get { return Properties.Resources.Caffeinated; }
        }

    }
}

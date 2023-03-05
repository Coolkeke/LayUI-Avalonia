using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayNotificationControl:ContentControl
    {
        private LayNotificationHost Host;
        public LayNotificationControl() { 
        
        }
        public LayNotificationControl(LayNotificationHost host)
        {
            Host=host;
        }
        
    }
}

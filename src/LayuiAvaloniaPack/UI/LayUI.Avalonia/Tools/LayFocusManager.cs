using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Input;
using LayUI.Avalonia.Interfaces;

namespace LayUI.Avalonia.Tools
{
    public class LayFocusManager 
    {
        private IFocusManager? FocusManager;
        public void InitializeFocusManager(Visual? visual)
        {
            if (visual != null) FocusManager = TopLevel.GetTopLevel(visual)?.FocusManager;
        }
    }
}

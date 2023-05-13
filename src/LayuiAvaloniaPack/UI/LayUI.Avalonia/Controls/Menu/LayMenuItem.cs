using Avalonia.Controls;
using Avalonia.Controls.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayMenuItem: MenuItem
    {
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new LayMenuItemContainerGenerator(this);
        }
    }
}

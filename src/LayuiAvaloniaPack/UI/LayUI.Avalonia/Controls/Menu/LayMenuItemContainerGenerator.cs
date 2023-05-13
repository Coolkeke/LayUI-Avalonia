using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayMenuItemContainerGenerator : ItemContainerGenerator<LayMenuItem>
    {
        public LayMenuItemContainerGenerator(IControl owner)
            : base(owner, HeaderedSelectingItemsControl.HeaderProperty, null)
        {
        }
    }
}

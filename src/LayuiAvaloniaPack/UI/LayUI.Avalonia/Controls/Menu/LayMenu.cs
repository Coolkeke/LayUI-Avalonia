using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayMenu: Menu
    {
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<LayMenuItem>(this, HeaderedSelectingItemsControl.HeaderProperty, null);
        }
        protected override void OnSubmenuOpened(RoutedEventArgs e)
        {
            LayMenuItem menuItem = e.Source as LayMenuItem;
            if (menuItem != null && menuItem.Parent == this)
            {
                foreach (LayMenuItem item in this.GetLogicalChildren().OfType<LayMenuItem>())
                {
                    if (item != menuItem && item.IsSubMenuOpen)
                    {
                        item.IsSubMenuOpen = false;
                    }
                }
            }

            IsOpen = true;
        }
    }
}

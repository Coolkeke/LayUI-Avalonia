using Avalonia;
using Avalonia.Controls;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayTabControl : TabControl, ILayControl
    {

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<TabType> TypeProperty =
            AvaloniaProperty.Register<LayTabControl, TabType>(nameof(Type));

        /// <summary>
        /// 类型
        /// </summary>
        public TabType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            var tabItem = new LayTabItem();
            tabItem.SetValue(TabItem.TabStripPlacementProperty, TabStripPlacement);
            return tabItem;
        }

        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<LayTabItem>(item, out recycleKey);
        }
    }
}

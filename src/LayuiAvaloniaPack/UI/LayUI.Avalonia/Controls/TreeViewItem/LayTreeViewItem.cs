using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayTreeViewItem: TreeViewItem
    {

        /// <summary>
        /// Defines the <see cref="IsCheckBoxVisible"/> property.
        /// </summary>
        internal static readonly StyledProperty<bool> IsCheckBoxVisibleProperty =
            AvaloniaProperty.Register<LayTreeViewItem, bool>(nameof(IsCheckBoxVisible), false);

        /// <summary>
        /// 显示复选框
        /// </summary>
        internal bool IsCheckBoxVisible
        {
            get { return GetValue(IsCheckBoxVisibleProperty); }
            set { SetValue(IsCheckBoxVisibleProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="IsChecked"/> property.
        /// </summary>
        public static readonly StyledProperty<bool?> IsCheckedProperty =
            AvaloniaProperty.Register<LayTreeViewItem, bool?>(nameof(IsChecked),false);

        /// <summary>
        /// 复选
        /// </summary>
        public bool? IsChecked
        {
            get { return GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            return new LayTreeViewItem();
        }
        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<LayTreeViewItem>(item, out recycleKey);
        }
    }
}

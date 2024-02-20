using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayTreeView : TreeView, ILayControl
    {
        /// <summary>
        /// Defines the <see cref="IsCheckBoxVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsCheckBoxVisibleProperty =
            AvaloniaProperty.Register<LayTreeView, bool>(nameof(IsCheckBoxVisible), false);

        /// <summary>
        /// 显示复选框
        /// </summary>
        public bool IsCheckBoxVisible
        {
            get { return GetValue(IsCheckBoxVisibleProperty); }
            set { SetValue(IsCheckBoxVisibleProperty, value); }
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

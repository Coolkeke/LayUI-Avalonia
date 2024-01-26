using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayTreeViewItem : TreeViewItem, ILayControl
    {
        public LayTreeViewItem()
        {
            IsCheckedProperty.Changed.AddClassHandler<LayTreeViewItem>((o, e) => o.OnIsCheckedChanged(e));
        }

        private void OnIsCheckedChanged(AvaloniaPropertyChangedEventArgs e) => OnIsCheckedChanged((bool?)e.NewValue);

        private void OnIsCheckedChanged(bool? newValue)
        {
            if (LogicalChildren.Count > 0)
            {
                var items = LogicalChildren.Where(o => o is LayTreeViewItem).ToList();
                if (items.Count > 0 && newValue != null)
                {
                    foreach (var item in items)
                    {
                        ((LayTreeViewItem)item).IsChecked = newValue;
                    }
                }
            }
            if (this.Parent is LayTreeViewItem layTreeViewItem)
            {
                var parentItems = layTreeViewItem.LogicalChildren.Where(o => o is LayTreeViewItem).ToList();
                if (parentItems.Count > 0)
                {
                    if (parentItems.Cast<LayTreeViewItem>().Where(o => o.IsChecked == true).Count() == parentItems.Count)
                    {
                        layTreeViewItem.IsChecked = true;
                    }
                    else if (parentItems.Cast<LayTreeViewItem>().Where(o => o.IsChecked == false).Count() == parentItems.Count)
                    {
                        layTreeViewItem.IsChecked = false;
                    }
                    else
                    {
                        if (parentItems.Cast<LayTreeViewItem>().Where(o => o.IsChecked == true).Count() > 0)
                        {
                            layTreeViewItem.IsChecked = null;
                        }
                        else if (parentItems.Cast<LayTreeViewItem>().Where(o => o.IsChecked == null).Count() > 0)
                        {
                            layTreeViewItem.IsChecked = null;
                        }
                    }
                }
            }
        }

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
            AvaloniaProperty.Register<LayTreeViewItem, bool?>(nameof(IsChecked), false, false);

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

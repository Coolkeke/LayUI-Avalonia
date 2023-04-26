using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.LogicalTree;
using LayUI.Avalonia.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 渐变轮播图
    /// </summary>
    public class LayGradientCarousel : LayCarousel
    {
        /// <summary>
        /// 存储轮播图容器
        /// </summary>
        private Panel PART_ItemsGrid;
        static LayGradientCarousel()
        {
            ItemsProperty.Changed.AddClassHandler<LayGradientCarousel>((x, e) => x.ItemsChanged(e));
            ItemTemplateProperty.Changed.AddClassHandler<LayGradientCarousel>((x, e) => x.ItemTemplateChanged(e));
            SelectedIndexProperty.Changed.AddClassHandler<LayGradientCarousel>((x, e) => x.OnSelectedIndexChanged());
        }

        private void OnSelectedIndexChanged()
        {
            UpdateItems();
        }
        public LayGradientCarousel()
        {
            SubscribeToItems(Items);
        }
        private void ItemTemplateChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (PART_ItemsGrid == null) return;
            var items = PART_ItemsGrid.Children.Where(o => o is LayCarouselItem).Cast<LayCarouselItem>();
            foreach (LayCarouselItem item in items) item.ContentTemplate = (IDataTemplate)e.NewValue;
        }
        /// <summary>
        /// 修改数量
        /// </summary>
        private void UpdateItemCount()
        {
            if (Items == null)
            {
                ItemsCount = 0;
                return;
            }

            IList list = Items as IList;
            if (list != null)
            {
                ItemsCount = list.Count;
            }
            else
            {
                var items = Items?.Cast<object>();
                ItemsCount = items == null ? 0 : items.Count();
            }
        }
        private void ItemsChanged(AvaloniaPropertyChangedEventArgs e)
        {
            IEnumerable enumerable = e.OldValue as IEnumerable;
            IEnumerable items = e.NewValue as IEnumerable;
            INotifyCollectionChanged enumerableCollectionChanged = enumerable as INotifyCollectionChanged;
            if (enumerableCollectionChanged != null)
            {
                enumerableCollectionChanged.CollectionChanged -= ItemsCollectionChanged;
            }
            RemoveControlItemsFromLogicalChildren(enumerable);
            AddControlItemsToLogicalChildren(items);
            UpdateItemCount();
            SubscribeToItems(items);
            UpdateItems();
        }
        private void SubscribeToItems(IEnumerable items)
        {
            if (items is INotifyCollectionChanged incc)
            {
                incc.CollectionChanged -= ItemsCollectionChanged;
                incc.CollectionChanged += ItemsCollectionChanged;
            }
        }
        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItemCount();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddControlItemsToLogicalChildren(e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveControlItemsFromLogicalChildren(e.OldItems);
                    break;
            }
            UpdateItems();
        }
        /// <summary>
        /// 选中项
        /// </summary>
        /// <param name="index"></param>
        public void SelectedItem(int index)
        {
            SelectedIndex = index;
        }
        /// <summary>
        /// 删除历史Item
        /// </summary>
        /// <param name="items"></param>
        private void RemoveControlItemsFromLogicalChildren(IEnumerable items)
        {
            if (items != null)
            {
                foreach (var i in items)
                {
                    if (!IsItemItsOwnContainerOverride(i)) continue;
                    var control = i as IControl;
                    if (control != null)
                    {
                        LogicalChildren.Remove(control);
                    }
                }
            }
        }
        /// <summary>
        /// 新增Item
        /// </summary>
        /// <param name="items"></param>
        private void AddControlItemsToLogicalChildren(IEnumerable items)
        {
            if (items != null)
            {
                foreach (var i in items)
                {
                    if (IsItemItsOwnContainerOverride(i) && i is LayCarouselItem control && !LogicalChildren.Contains(control))
                    {
                        LogicalChildren.Add(control);
                    }
                    else
                    {
                        var item = GetContainerForItemOverride() as LayCarouselItem;
                        item.Content = i;
                        item.ContentTemplate = ItemTemplate;
                        LogicalChildren.Add(item);
                    }

                }
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_ItemsGrid = e.NameScope.Find<Panel>("PART_ItemsGrid");
            UpdateItems();
        }
        void UpdateItems()
        {
            if (PART_ItemsGrid == null) return;
            PART_ItemsGrid.Children.Clear();
            foreach (var item in LogicalChildren)
            {
                PART_ItemsGrid.Children.Add(item as IControl);
            }
            for (int i = 0; i < PART_ItemsGrid.Children.Count; i++)
            {
                if (!(PART_ItemsGrid.Children[i] is LayCarouselItem item)) continue;
                if (i == SelectedIndex)
                {
                    item.Opacity = 1;
                    item.ZIndex = 1;
                }
                else
                {
                    item.Opacity = 0;
                    item.ZIndex = 0;
                }
                item.ContentTemplate = ItemTemplate;
            }
        }
    }
}

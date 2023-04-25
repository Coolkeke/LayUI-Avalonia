using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using System.Collections;
using System.Collections.Generic;
using Avalonia.Metadata;
using LayUI.Avalonia.Interface;
using System.Linq;
using System.Collections.Specialized;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Controls.Generators;
using System;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 轮播图基类
    /// </summary>
    public class LayCarousel : TemplatedControl, ICarousel
    {
        /// <summary>
        /// 存储轮播图容器
        /// </summary>
        private Panel PART_ItemsGrid;
        static LayCarousel()
        {
            ItemsProperty.Changed.AddClassHandler(delegate (LayCarousel x, AvaloniaPropertyChangedEventArgs e)
            {
                x.ItemsChanged(e);
            });
            ItemTemplateProperty.Changed.AddClassHandler(delegate (LayCarousel x, AvaloniaPropertyChangedEventArgs e)
            {
                x.ItemTemplateChanged(e);
            });
        }
        /// <summary>
        /// 创建或标识用于显示给定项的元素
        /// </summary>
        /// <returns>用于显示给定项的元素</returns>
        protected virtual AvaloniaObject GetContainerForItemOverride()
        {
            return new LayCarouselItem() { ContentTemplate = ItemTemplate };
        }
        /// <summary>
        /// 确定指定项是否是（或者是否可以作为）其自己的容器
        /// </summary>
        /// <param name="item">要检查的项</param>
        /// <returns>如果该项是（或者可以作为）其自己的容器，则为 true；否则为 false</returns>
        protected virtual bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayCarouselItem;
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
                ItemCount = 0;
                return;
            }

            IList list = Items as IList;
            if (list != null)
            {
                ItemCount = list.Count;
            }
            else
            {
                var items = Items?.Cast<object>();
                ItemCount = items == null ? 0 : items.Count();
            }
        }
        protected virtual void ItemsChanged(AvaloniaPropertyChangedEventArgs e)
        {
            IEnumerable enumerable = e.OldValue as IEnumerable;
            IEnumerable items = e.NewValue as IEnumerable;
            INotifyCollectionChanged enumerableCollectionChanged = enumerable as INotifyCollectionChanged;
            if (enumerableCollectionChanged != null)
            {
                enumerableCollectionChanged.CollectionChanged -= ItemsCollectionChanged;
            }
            INotifyCollectionChanged itemsCollectionChanged = items as INotifyCollectionChanged;
            if (itemsCollectionChanged != null)
            {
                itemsCollectionChanged.CollectionChanged += ItemsCollectionChanged;
            }
            RemoveControlItemsFromLogicalChildren(enumerable);
            AddControlItemsToLogicalChildren(items);
            UpdateItemCount();
        }

        protected virtual void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
        }
        /// <summary>
        /// 删除历史Item
        /// </summary>
        /// <param name="items"></param>
        private void RemoveControlItemsFromLogicalChildren(IEnumerable items)
        {
            if (PART_ItemsGrid == null) return;
            if (items != null)
            {
                foreach (var i in items)
                {
                    if (!IsItemItsOwnContainerOverride(i)) continue;
                    var control = i as IControl;
                    if (control != null)
                    {
                        PART_ItemsGrid.Children.Remove(control);
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
            if (PART_ItemsGrid == null) return;
            if (items != null)
            {
                foreach (var i in items)
                {
                    if (IsItemItsOwnContainerOverride(i) && i is LayCarouselItem control && !PART_ItemsGrid.Children.Contains(control))
                    {
                        PART_ItemsGrid.Children.Add(control);
                    }
                    else
                    {
                        var item = GetContainerForItemOverride() as LayCarouselItem;
                        item.Content = i;
                        PART_ItemsGrid.Children.Add(item);
                    }

                }
            }
        }

        /// <summary>
        /// 当前项
        /// </summary>
        public int SelectedIndex
        {
            get { return GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly StyledProperty<int> SelectedIndexProperty =
       AvaloniaProperty.Register<LayCarousel, int>(nameof(SelectedIndex), -1);

        /// <summary>
        /// 集合
        /// </summary>
        [Content]
        public IEnumerable Items
        {
            get { return GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IEnumerable"/>属性
        /// </summary>
        public static readonly StyledProperty<IEnumerable> ItemsProperty =
       AvaloniaProperty.Register<LayCarousel, IEnumerable>(nameof(Items), new AvaloniaList<object>());


        /// <summary>
        /// 轮播图数量
        /// </summary>
        public int ItemCount
        {
            get { return GetValue(ItemCountProperty); }
            private set { SetValue(ItemCountProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly StyledProperty<int> ItemCountProperty =
       AvaloniaProperty.Register<LayCarousel, int>(nameof(ItemCount), 0);

        /// <summary>
        /// 轮播图模板
        /// </summary>
        public IDataTemplate ItemTemplate
        {
            get { return GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IDataTemplate"/>属性
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> ItemTemplateProperty =
       AvaloniaProperty.Register<LayCarousel, IDataTemplate>(nameof(ItemTemplate), null);

        public void Next()
        {
            if (SelectedIndex >= ItemCount) SelectedIndex = 0;
            SelectedIndex++;
        }

        public void Previous()
        {
            if (SelectedIndex <= 0) SelectedIndex = ItemCount - 1;
            SelectedIndex--;
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_ItemsGrid = e.NameScope.Find<Panel>("PART_ItemsGrid");
        }
    }
}

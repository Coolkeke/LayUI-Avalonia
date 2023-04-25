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
using Avalonia.Media;

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
            ItemsProperty.Changed.AddClassHandler<LayCarousel>((x, e) => x.ItemsChanged(e));
            ItemTemplateProperty.Changed.AddClassHandler<LayCarousel>((x, e) => x.ItemTemplateChanged(e));
            SelectedIndexProperty.Changed.AddClassHandler<LayCarousel>((x, e) => x.OnSelectedIndexChanged());
        }

        private void OnSelectedIndexChanged()
        {
            UpdateItems();
        }
        public LayCarousel()
        {
            SubscribeToItems(_items);
        }
        /// <summary>
        /// 创建或标识用于显示给定项的元素
        /// </summary>
        /// <returns>用于显示给定项的元素</returns>
        protected virtual AvaloniaObject GetContainerForItemOverride()
        {
            return new LayCarouselItem();
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
            UpdateItems();
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
        private IEnumerable _items = new AvaloniaList<object>();
        /// <summary>
        /// 集合
        /// </summary>
        [Content]
        public IEnumerable Items
        {
            get { return _items; }
            set { SetAndRaise(ItemsProperty, ref _items, value); }
        }
        /// <summary>
        /// 定义<see cref="IEnumerable"/>属性
        /// </summary>
        public static readonly DirectProperty<LayCarousel, IEnumerable> ItemsProperty =
       AvaloniaProperty.RegisterDirect<LayCarousel, IEnumerable>(nameof(Items), o => o.Items, (o, v) => o.Items = v);


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
            if (SelectedIndex >= (ItemCount - 1)) SelectedIndex = 0;
            else SelectedIndex++;
        }

        public void Previous()
        {
            if (SelectedIndex <= 0) SelectedIndex = ItemCount - 1;
            else SelectedIndex--;
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
                if (i == SelectedIndex) item.Opacity = 1;
                else item.Opacity = 0;
                item.ContentTemplate = ItemTemplate;
            }  
        }
    }
}

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
using LayUI.Avalonia.Enums;
using Avalonia.VisualTree;
using Avalonia.Interactivity;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 轮播图基类
    /// </summary>
    public class LayCarousel : TemplatedControl, ICarousel
    {
        /// <summary>
        /// 存储轮播图信息
        /// </summary>
        private Panel PART_ItemsGrid;
        public LayCarousel()
        {
            SubscribeToItems(_items);
        }
        static LayCarousel()
        {
            ItemTemplateProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnItemTemplateChanged(e));
            SelectedIndexProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnSelectedIndexChanged(e));
            SelectedItemProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnSelectedItemChanged(e));
            ItemsProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnItemsChanged(e));
        }

        public int SelectedIndex
        {
            get { return GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly StyledProperty<int> SelectedIndexProperty =
       AvaloniaProperty.Register<LayCarousel, int>(nameof(SelectedIndex));

        public bool IsIndicatorVisible
        {
            get { return GetValue(IsIndicatorVisibleProperty); }
            set { SetValue(IsIndicatorVisibleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsIndicatorVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsIndicatorVisibleProperty =
            AvaloniaProperty.Register<LayCarousel, bool>(nameof(IsIndicatorVisible), true);
        private IEnumerable _items = new AvaloniaList<object>();
        [Content]
        public IEnumerable Items
        {
            get { return _items; }
            set { SetAndRaise(ItemsProperty, ref _items, value); }
        }
        /// <summary>
        /// 定义<see cref="string"/>属性
        /// </summary>
        public static readonly DirectProperty<LayCarousel, IEnumerable> ItemsProperty =
        AvaloniaProperty.RegisterDirect<LayCarousel, IEnumerable>(nameof(Items), o => o.Items, (o, v) => o.Items = v);
        private int _itemCount = 0;
        public int ItemCount
        {
            get { return _itemCount; }
            set { SetAndRaise(ItemCountProperty, ref _itemCount, value); }
        }
        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly DirectProperty<LayCarousel, int> ItemCountProperty =
       AvaloniaProperty.RegisterDirect<LayCarousel, int>(nameof(ItemCount), o => o.ItemCount, (o, v) => o.ItemCount = v);

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<CarouselType> TypeProperty =
            AvaloniaProperty.Register<LayCarousel, CarouselType>(nameof(Type), CarouselType.Always);

        /// <summary>
        /// 箭头状态
        /// </summary>
        public CarouselType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public IDataTemplate ItemTemplate
        {
            get { return GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IDataTemplate"/>属性
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> ItemTemplateProperty =
       AvaloniaProperty.Register<LayCarousel, IDataTemplate>(nameof(ItemTemplate));
        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { SetAndRaise(SelectedItemProperty, ref _selectedItem, value); }
        } 
        /// <summary>
        /// Defines the <see cref="SelectedItem"/> property.
        /// </summary>
        public static readonly DirectProperty<LayCarousel, object> SelectedItemProperty =
            AvaloniaProperty.RegisterDirect<LayCarousel, object>(nameof(SelectedItem), o => o.SelectedItem, (o, v) => o.SelectedItem = v);

        public AvaloniaObject GetContainerForItemOverride()
        {
            return new LayCarouselItem();
        }
        public bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayCarouselItem;
        }
        /// <summary>
        /// 新增数据集通知效果
        /// </summary>
        /// <param name="items"></param>
        private void SubscribeToItems(IEnumerable items)
        {
            if (items is INotifyCollectionChanged notifyCollectionChanged)
            {
                if (notifyCollectionChanged != null) notifyCollectionChanged.CollectionChanged += (o, n) => OnItemsChanged(n);
            }
        }

        /// <summary>
        /// 监听数据源变化
        /// </summary>
        /// <param name="e"></param>
        private void OnItemsChanged(AvaloniaPropertyChangedEventArgs e)
        {
            IEnumerable enumerable = (IEnumerable)e.OldValue;
            IEnumerable items = (IEnumerable)e.NewValue;
            if (enumerable is INotifyCollectionChanged notifyCollectionChanged && notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged -= (o, n) => OnItemsChanged(n);
            UpdateItemCount();
            RemoveControlItems(enumerable);
            AddControlItems(items);
            SubscribeToItems(items);
        } 
        /// <summary>
        /// 数据集合项变化通知
        /// </summary>
        /// <param name="e"></param>
        private void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            UpdateItemCount();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddControlItems(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveControlItems(e.OldItems);
                    break;
            }
        }
        /// <summary>
        /// 监听当前第N项变化
        /// </summary>
        /// <param name="e"></param>
        private void OnSelectedIndexChanged(AvaloniaPropertyChangedEventArgs e)
        {
            LayCarousel carousel = (LayCarousel)e.Sender;
            carousel.SelectedItem = carousel.GetItem((int)e.NewValue);
            SetItemIsSelected();  
        }
        /// <summary>
        /// 监听当前第N项变化
        /// </summary>
        /// <param name="e"></param>
        private void OnSelectedItemChanged(AvaloniaPropertyChangedEventArgs e)
        {
            LayCarousel carousel = (LayCarousel)e.Sender;
            carousel.SelectedIndex = carousel.GetIndex(e.NewValue);
            SetItemIsSelected(); 
        }
        /// <summary>
        /// 监听模板变化
        /// </summary>
        /// <param name="e"></param>
        private void OnItemTemplateChanged(AvaloniaPropertyChangedEventArgs e)
        {
            UpdateItemTemplate();
        }
        /// <summary>
        /// 选中项
        /// </summary>
        /// <param name="index">选中当前索引</param>
        public void Selected(int index) => SelectedIndex = index;
        /// <summary>
        /// 获取索引抓取子项
        /// </summary>
        /// <returns></returns>
        private object GetItem(int index)
        {
            if (Items == null) return null;
            var items = Items as IList;
            for (int i = 0; i < items.Count; i++)
            {
                if (index == i) return items[i];
            }
            return null;
        }
        /// <summary>
        /// 根据子项值抓取当前索引
        /// </summary>
        /// <param name="obj">内容</param>
        /// <returns></returns>
        private int GetIndex(object obj)
        {
            int num = 0;
            foreach (var item in Items)
            {
                if (item == obj) return num;
                num++;
            }
            return -1;
        }
        /// <summary>
        ///  新增历史项
        /// </summary>
        /// <param name="newItems"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void AddControlItems(IEnumerable newItems)
        {
            foreach (var item in newItems)
            {
                if (IsItemItsOwnContainerOverride(item))
                {
                    var layCarouselItem = item as LayCarouselItem;
                    LogicalChildren.Add(layCarouselItem);
                }
                else
                {
                    var layCarouselItem = GetContainerForItemOverride() as LayCarouselItem;
                    layCarouselItem.Content = item;
                    LogicalChildren.Add(layCarouselItem);
                    UpdateItemTemplate();
                }
            }
        }
        /// <summary>
        /// 删除历史项
        /// </summary>
        /// <param name="oldItems"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void RemoveControlItems(IEnumerable oldItems)
        {
            foreach (var item in oldItems)
            {
                if (IsItemItsOwnContainerOverride(item))
                {
                    var layCarouselItem = item as LayCarouselItem;
                    LogicalChildren.Remove(layCarouselItem);
                }
                else
                {
                    foreach (var info in LogicalChildren)
                    {
                        if (info is LayCarouselItem carouselItem)
                        {
                            if (carouselItem.DataContext == item) LogicalChildren.Remove(info);
                        }
                    }
                }
            }
        }
        protected override void LogicalChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.LogicalChildrenCollectionChanged(sender, e);
            RefreshView();
        }
        /// <summary>
        /// 刷新视图
        /// </summary>
        private void RefreshView()
        {
            if (PART_ItemsGrid == null) return;
            PART_ItemsGrid.Children.Clear();
            foreach (var item in LogicalChildren)
            {
                if (item is IControl control) PART_ItemsGrid.Children.Add(control);
            }
            SetItemIsSelected();
        }
        /// <summary>
        /// 刷新数量
        /// </summary>
        private void UpdateItemCount()
        {
            if (Items == null)
            {
                ItemCount = 0;
                return;
            }
            IList list = Items as IList;
            if (list != null) ItemCount = list.Count;
            else ItemCount = Enumerable.Count(Items.Cast<object>());
        }
        private void UpdateItemTemplate()
        {
            if (LogicalChildren == null) return;
            foreach (var item in LogicalChildren)
            {
                if (item is LayCarouselItem control)
                {
                    control.ContentTemplate = ItemTemplate;
                }
            }
        }
        public void Next()
        {
            if (SelectedIndex >= ItemCount - 1) SelectedIndex = 0;
            else SelectedIndex++;
            SetItemIsSelected();
        }
        /// <summary>
        /// 刷新子项选中状态
        /// </summary>
        private void SetItemIsSelected()
        {
            if (PART_ItemsGrid == null) return;
            for (int i = 0; i < ItemCount; i++)
            {
                if (SelectedIndex == i)
                {
                    var item = PART_ItemsGrid.Children[i] as LayCarouselItem;
                    item.IsSelected = true;
                    item.ZIndex = 1;
                }
                else
                {
                    var item = PART_ItemsGrid.Children[i] as LayCarouselItem;
                    item.IsSelected = false;
                    item.ZIndex = 0;
                }
            }
        }
        public void Previous()
        {
            if (SelectedIndex <= 0) SelectedIndex = ItemCount - 1;
            else SelectedIndex--;
            SetItemIsSelected();
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_ItemsGrid = e.NameScope.Find<Panel>("PART_ItemsGrid");
            UpdateItemTemplate();
            RefreshView();
        }
    }
}

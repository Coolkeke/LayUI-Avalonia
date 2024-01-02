using Avalonia.Controls.Primitives;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayUI.Avalonia.Interfaces;
using System.Collections;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using LayUI.Avalonia.Enums;
using Avalonia.Collections;
using Avalonia.Controls.Metadata;
using Avalonia.LogicalTree;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Net.NetworkInformation;
using Avalonia.Threading;
using System.Windows.Input;
using LayUI.Avalonia.Mvvm;
using LayUI.Avalonia.Models;
using System.Collections.ObjectModel;
using System.Reflection;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 轮播图
    /// </summary> 
    [TemplatePart(Name = nameof(PART_ItemsGrid), Type = typeof(Grid))]
    public class LayCarousel : TemplatedControl, ICarousel, ILayControl, IDisposable
    {
        /// <summary>
        /// 计时器
        /// </summary>
        private DispatcherTimer _timer;
        /// <summary>
        /// 存储轮播图信息
        /// </summary>
        private Grid? PART_ItemsGrid;
        /// <summary>
        /// 手势位置
        /// </summary>
        private Point point;
        public LayCarousel()
        {
            CreateTimer();
            SubscribeToItemsSource(_items);
            ItemCountProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.ItemCountChanged());
            ItemTemplateProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnItemTemplateChanged(e));
            SelectedIndexProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnSelectedIndexChanged(e));
            SelectedItemProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnSelectedItemChanged(e));
            ItemsSourceProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnItemsSourceChanged(e));
            IsAutoSwitchProperty.Changed.AddClassHandler<LayCarousel>((o, e) => o.OnIsAutoSwitchChanged(e));
            SelectedCommand = new LayCommand<int>(Selected);
        }

        private void OnIsAutoSwitchChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (!IsLoaded) return;
            if (IsAutoSwitch) StartTimer();
            else StopTimer();
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
        public IEnumerable ItemsSource
        {
            get { return _items; }
            set { SetAndRaise(ItemsSourceProperty, ref _items, value); }
        }
        /// <summary>
        /// 定义<see cref="string"/>属性
        /// </summary>
        public static readonly DirectProperty<LayCarousel, IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.RegisterDirect<LayCarousel, IEnumerable>(nameof(ItemsSource), o => o.ItemsSource, (o, v) => o.ItemsSource = v);
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
       AvaloniaProperty.RegisterDirect<LayCarousel, int>(nameof(ItemCount), o => o.ItemCount);

        private void ItemCountChanged()
        {
            if (Items == null) Items = new ObservableCollection<CarouselItemData>();
            Items.Clear();
            for (int i = 0; i < ItemCount; i++)
            {
                Items.Add(new CarouselItemData() { Index = i, IsSelected = false });
            }
        }

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
        private bool disposedValue;

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

        /// <summary>
        /// Defines the <see cref="TouchSlidingInterval"/> property.
        /// </summary>
        public static readonly StyledProperty<int> TouchSlidingIntervalProperty =
            AvaloniaProperty.Register<LayCarousel, int>(nameof(TouchSlidingInterval), 100);

        /// <summary>
        /// Defines the <see cref="SelectedCommand"/> property.
        /// </summary>
        internal static readonly StyledProperty<ICommand> SelectedCommandProperty =
            AvaloniaProperty.Register<LayCarousel, ICommand>(nameof(SelectedCommand));

        /// <summary>
        /// Comment
        /// </summary>
        internal ICommand SelectedCommand
        {
            get { return GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }
        /// <summary>
        /// 触摸滑动间隔
        /// </summary>
        public int TouchSlidingInterval
        {
            get { return GetValue(TouchSlidingIntervalProperty); }
            set { SetValue(TouchSlidingIntervalProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsAutoSwitch"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsAutoSwitchProperty =
            AvaloniaProperty.Register<LayCarousel, bool>(nameof(IsAutoSwitch));

        /// <summary>
        /// 自动轮播
        /// </summary>
        public bool IsAutoSwitch
        {
            get { return GetValue(IsAutoSwitchProperty); }
            set { SetValue(IsAutoSwitchProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="SwitchInterval"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SwitchIntervalProperty =
            AvaloniaProperty.Register<LayCarousel, double>(nameof(SwitchInterval), 3);
        /// <summary>
        /// 切换间隔时间
        /// <para>间隔单位（秒）</para>
        /// </summary>
        public double SwitchInterval
        {
            get { return GetValue(SwitchIntervalProperty); }
            set { SetValue(SwitchIntervalProperty, value); }
        }

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
        private void SubscribeToItemsSource(IEnumerable items)
        {
            if (items == null) return;
            if (items is INotifyCollectionChanged notifyCollectionChanged)
            {
                if (notifyCollectionChanged != null) notifyCollectionChanged.CollectionChanged += (o, n) => OnItemsSourceChanged(n);
            }
        }


        /// <summary>
        /// Defines the <see cref="Items"/> property.
        /// </summary>
        internal static readonly StyledProperty<ObservableCollection<CarouselItemData>> ItemsProperty =
            AvaloniaProperty.Register<Control, ObservableCollection<CarouselItemData>>(nameof(Items), null);

        /// <summary>
        /// 按钮集合
        /// </summary>
        internal ObservableCollection<CarouselItemData> Items
        {
            get { return GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }


        /// <summary>
        /// 监听数据源变化
        /// </summary>
        /// <param name="e"></param>
        private void OnItemsSourceChanged(AvaloniaPropertyChangedEventArgs e)
        {
            IEnumerable enumerable = (IEnumerable)e.OldValue;
            IEnumerable items = (IEnumerable)e.NewValue;
            if (enumerable is INotifyCollectionChanged notifyCollectionChanged && notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged -= (o, n) => OnItemsSourceChanged(n);
            UpdateItemCount();
            RemoveControlItemsSource(enumerable);
            AddControlItemsSource(items);
            SubscribeToItemsSource(items);
        }
        /// <summary>
        /// 数据集合项变化通知
        /// </summary>
        /// <param name="e"></param>
        private void OnItemsSourceChanged(NotifyCollectionChangedEventArgs e)
        {
            UpdateItemCount();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddControlItemsSource(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveControlItemsSource(e.OldItems);
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
        public void Selected(int index)
        {
            if (IsAutoSwitch) StopTimer();
            SelectedIndex = index;
            if (IsAutoSwitch) StartTimer();
        }
        /// <summary>
        /// 获取索引抓取子项
        /// </summary>
        /// <returns></returns>
        private object GetItem(int index)
        {
            if (ItemsSource == null) return null;
            var items = ItemsSource as IList;
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
            foreach (var item in ItemsSource)
            {
                if (item == obj) return num;
                num++;
            }
            return -1;
        }
        /// <summary>
        ///  新增历史项
        /// </summary>
        /// <param name="newItemsSource"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void AddControlItemsSource(IEnumerable newItemsSource)
        {
            foreach (var item in newItemsSource)
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
        /// <param name="oldItemsSource"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void RemoveControlItemsSource(IEnumerable oldItemsSource)
        {
            foreach (var item in oldItemsSource)
            {
                if (IsItemItsOwnContainerOverride(item))
                {
                    var layCarouselItem = item as LayCarouselItem;
                    if (layCarouselItem != null) LogicalChildren.Remove(layCarouselItem);
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
        /// <summary>
        /// 刷新视图
        /// </summary>
        private void RefreshView()
        {
            if (PART_ItemsGrid == null) return;
            PART_ItemsGrid.Children.Clear();
            foreach (var item in LogicalChildren)
            {
                if (item is Control control) PART_ItemsGrid.Children.Add(control);
            }
            SetItemIsSelected();
        }
        /// <summary>
        /// 创建计时器
        /// </summary>
        void CreateTimer()
        {
            if (_timer == null) _timer = new DispatcherTimer();
            StopTimer();
            _timer.Tick += _timer_Tick;
        }
        /// <summary>
        /// 用于自动切换轮播
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _timer_Tick(object? sender, EventArgs e) => Next();

        /// <summary>
        /// 移除计时器
        /// </summary>
        void RemoveTimer()
        {
            if (_timer == null) return;
            StopTimer();
            _timer = null;
        }
        /// <summary>
        /// 停止
        /// </summary>
        void StopTimer()
        {
            if (_timer == null) return;
            _timer.Stop();
        }
        /// <summary>
        /// 启动
        /// </summary>
        void StartTimer()
        {
            if (_timer == null) return;
            _timer.Interval = TimeSpan.FromSeconds(SwitchInterval);
            _timer.Start();
        }
        /// <summary>
        /// 刷新数量
        /// </summary>
        private void UpdateItemCount()
        {
            if (ItemsSource == null)
            {
                ItemCount = 0;
                return;
            }
            IList list = ItemsSource as IList;
            if (list != null) ItemCount = list.Count;
            else ItemCount = Enumerable.Count(ItemsSource.Cast<object>());
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
            if (IsAutoSwitch) StopTimer();
            if (SelectedIndex >= ItemCount - 1) SelectedIndex = 0;
            else SelectedIndex++;
            SetItemIsSelected();
            if (IsAutoSwitch) StartTimer();
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
                    Items[i].IsSelected = item.IsSelected;
                    item.ZIndex = 1;
                }
                else
                {
                    var item = PART_ItemsGrid.Children[i] as LayCarouselItem;
                    item.IsSelected = false;
                    Items[i].IsSelected = item.IsSelected;
                    item.ZIndex = 0;
                }
            }
        }
        public void Previous()
        {
            if (IsAutoSwitch) StopTimer();
            if (SelectedIndex <= 0) SelectedIndex = ItemCount - 1;
            else SelectedIndex--;
            SetItemIsSelected();
            if (IsAutoSwitch) StartTimer();
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_ItemsGrid = e.NameScope.Find<Grid>(nameof(PART_ItemsGrid));
            if (PART_ItemsGrid != null)
            {
                UpdateItemTemplate();
                RefreshView();
                PART_ItemsGrid.PointerPressed -= PART_ItemsGrid_PointerPressed;
                PART_ItemsGrid.PointerReleased -= PART_ItemsGrid_PointerReleased;
                PART_ItemsGrid.PointerPressed += PART_ItemsGrid_PointerPressed;
                PART_ItemsGrid.PointerReleased += PART_ItemsGrid_PointerReleased;
                if (IsAutoSwitch) StartTimer();
            }
        }
        /// <summary>
        /// 按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_ItemsGrid_PointerPressed(object? sender, PointerPressedEventArgs e) => point = e.GetPosition(this);
        /// <summary>
        /// 抬起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_ItemsGrid_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            var posit = e.GetPosition(this);
            if (point.X - posit.X > TouchSlidingInterval) Next();
            if (point.X - posit.X < -TouchSlidingInterval) Previous();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    RemoveTimer();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        ~LayCarousel()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{

    public class PaginationRoutedEventArgs : RoutedEventArgs
    {

        /// <summary>
        /// 需要查找的目标页
        /// </summary>
        public int newPageIndex { get; set; }
        /// <summary>
        /// 本页查询条数
        /// </summary>
        public int newPageSize { get; set; }

    }
    /// <summary>
    /// 分页插件
    /// </summary>
    [TemplatePart(Name = nameof(PART_Next), Type = typeof(Button))]
    [TemplatePart(Name = nameof(PART_Previous), Type = typeof(Button))]
    [TemplatePart(Name = nameof(PART_Refresh), Type = typeof(Button))]
    public class LayPagination : TemplatedControl, ILayControl
    {
        public LayPagination()
        {
            PageIndexProperty.Changed.AddClassHandler<LayPagination>((o, e) => o.OnPageIndexChanged());
            TotalProperty.Changed.AddClassHandler<LayPagination>((o, e) => o.OnTotalChanged());
            PageSizeProperty.Changed.AddClassHandler<LayPagination>((o, e) => o.OnPageSizeChanged()); 
        }
        /// <summary>
        /// 下一页按钮
        /// </summary>
        private Button? PART_Next;
        /// <summary>
        /// 上一页按钮
        /// </summary>
        private Button? PART_Previous;
        /// <summary>
        /// 刷新按钮
        /// </summary>
        private Button? PART_Refresh;
        /// <summary>
        /// 存储按钮
        /// </summary>
        private StackPanel? PART_Items;
        /// <summary>
        /// Defines the <see cref="PageIndex"/> property.
        /// </summary>
        public static readonly StyledProperty<int> PageIndexProperty =
            AvaloniaProperty.Register<LayPagination, int>(nameof(PageIndex), 1);

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref=" "/> property.
        /// </summary>
        public static readonly StyledProperty<int> PageNumberProperty =
            AvaloniaProperty.Register<LayPagination, int>(nameof(PageNumber));

        /// <summary>
        /// 页数
        /// </summary>
        public int PageNumber
        {
            get { return GetValue(PageNumberProperty); }
            private set { SetValue(PageNumberProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref=" "/> property.
        /// </summary>
        public static readonly StyledProperty<int> PageCountProperty =
            AvaloniaProperty.Register<LayPagination, int>(nameof(PageCount));

        /// <summary>
        /// 总共页数
        /// </summary>
        public int PageCount
        {
            get { return GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Total"/> property.
        /// </summary>
        public static readonly StyledProperty<int> TotalProperty =
            AvaloniaProperty.Register<LayPagination, int>(nameof(Total), 0, false);

        /// <summary>
        /// 子集数量总数
        /// </summary>
        public int Total
        {
            get { return GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref=" "/> property.
        /// </summary>
        public static readonly StyledProperty<int> PageSizeProperty =
            AvaloniaProperty.Register<LayPagination, int>(nameof(PageSize), 20, false);

        /// <summary>
        /// 当页显示条数
        /// </summary>
        public int PageSize
        {
            get { return GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="PageSizes"/> property.
        /// </summary>
        public static readonly StyledProperty<int[]> PageSizesProperty =
            AvaloniaProperty.Register<LayPagination, int[]>(nameof(PageSizes));

        /// <summary>
        /// 每页显示个数选择器的选项设置
        /// </summary>
        public int[] PageSizes
        {
            get { return GetValue(PageSizesProperty); }
            set { SetValue(PageSizesProperty, value); }
        }
         
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_Next = e.NameScope.Find<Button>(nameof(PART_Previous));
            PART_Previous = e.NameScope.Find<Button>(nameof(PART_Previous));
            PART_Refresh = e.NameScope.Find<Button>(nameof(PART_Refresh));
            PART_Items = e.NameScope.Find<StackPanel>(nameof(PART_Items));
            if (PART_Next != null && PART_Previous != null && PART_Refresh != null && PART_Items != null)
            {
                PART_Next.Click -= PART_Next_Click;
                PART_Previous.Click -= PART_Previous_Click;
                PART_Refresh.Click -= PART_Refresh_Click;
                PART_Next.Click += PART_Next_Click;
                PART_Previous.Click += PART_Previous_Click;
                PART_Refresh.Click += PART_Refresh_Click; 
            }
        }

        private void PART_Refresh_Click(object? sender, RoutedEventArgs e) => Refresh();

        private void PART_Next_Click(object? sender, RoutedEventArgs e) => Next();

        private void PART_Previous_Click(object? sender, RoutedEventArgs e) => Previous();

        /// <summary>
        /// Defines the <see cref="NextContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> NextContentProperty =
            AvaloniaProperty.Register<LayPagination, object>(nameof(NextContent));

        /// <summary>
        /// 下一页按钮内容>
        public object NextContent
        {
            get { return GetValue(NextContentProperty); }
            set { SetValue(NextContentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="PreviousContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> PreviousContentProperty =
            AvaloniaProperty.Register<LayPagination, object>(nameof(PreviousContent));

        /// <summary>
        /// 上一页按钮内容>
        /// </summary>
        public object PreviousContent
        {
            get { return GetValue(PreviousContentProperty); }
            set { SetValue(PreviousContentProperty, value); }
        }
        /// <summary>
        /// 执行分页跳转
        /// </summary> 
        public event EventHandler<PaginationRoutedEventArgs> Completed
        {
            add
            {
                AddHandler(CompletedEvent, value);
            }
            remove
            {
                RemoveHandler(CompletedEvent, value);
            }
        }
        public static readonly RoutedEvent<PaginationRoutedEventArgs> CompletedEvent =
            RoutedEvent.Register<LayDrawer, PaginationRoutedEventArgs>("Completed", RoutingStrategies.Bubble);

        /// <summary>
        /// 下一页
        /// </summary>
        private void Next()
        {
            if (PageIndex + 1 >= PageCount) PageIndex = PageCount;
            else PageIndex++;
        }
        /// <summary>
        /// 上一页
        /// </summary>
        private void Previous()
        {
            if (PageIndex - 1 <= 1) PageIndex = 1;
            else PageIndex--;
        }
        /// <summary>
        /// 数据变化监听
        /// </summary> 
        private void OnPageIndexChanged()
        {
            if (!IsLoaded) return;
            UpdatePageCount();
            RaiseEvent(new PaginationRoutedEventArgs() { Source = this, RoutedEvent = CompletedEvent, newPageIndex = PageIndex, newPageSize = PageSize });
        } 

        private void OnTotalChanged()
        {
            UpdatePageCount();
        }
        private void OnPageSizeChanged()
        {
            UpdatePageCount();
        }
        /// <summary>
        /// 修改当前最新总页数
        /// </summary>
        void UpdatePageCount()
        {
            PageCount = (int)Math.Ceiling((double)Total / PageSize);
            PART_Items?.Children?.Clear();
            if (PageCount >= 9)
            {
                for (int i = 1; i <= 9; i++)
                {
                    var btn = new LayButtonBase();
                    if (i == 2 || i == 8)
                    {
                        btn.Content = $"...";
                        btn.IsEnabled = false;
                    }
                    else
                    {
                        btn.CommandParameter = i;
                        btn.Content = $"{i}";
                        btn.Click -= Btn_Click;
                        btn.Click += Btn_Click;
                    }
                    if (i == PageIndex) btn.Tag = true;
                    else btn.Tag = false;
                    PART_Items?.Children?.Add(btn);
                }
            }
            else {
                for (int i = 1; i <= PageCount; i++)
                {
                    var btn = new LayButtonBase(); 
                    btn.CommandParameter = i;
                    btn.Content = $"{i}";
                    btn.Click -= Btn_Click;
                    btn.Click += Btn_Click;
                    if (i == PageIndex) btn.Tag = true;
                    else btn.Tag = false;
                    PART_Items?.Children?.Add(btn);
                }
            }
        }

        private void Btn_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is LayButtonBase layButton)
            {
                if (layButton.CommandParameter is int index)
                { 
                    PageIndex = index;
                }
                UpdatePageCount();
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void Refresh()
        {
            OnPageIndexChanged();
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using LayUI.Avalonia.Enums;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 抽屉
    /// </summary>

    [TemplatePart(Name = nameof(PART_MaskButton), Type = typeof(Button))]
    public class LayDrawer : ContentControl, ILayControl
    {
        /// <summary>
        /// 按钮控制遮罩
        /// </summary>
        private bool _isBtnCloseMask = false;
        /// <summary>
        /// 遮罩层按钮
        /// </summary>
        private Button PART_MaskButton = null;
        static LayDrawer()
        {
            IsOpenProperty.Changed.AddClassHandler((LayDrawer x, AvaloniaPropertyChangedEventArgs e) => x.IsOpenChanged((AvaloniaPropertyChangedEventArgs<bool>)e));
        }

        private void IsOpenChanged(AvaloniaPropertyChangedEventArgs<bool> e)
        {
            if (_isBtnCloseMask) return;
            if (e.NewValue.HasValue)
            {
                RaiseEvent(new RoutedEventArgs(OpenedEvent));
            }
        } 
        /// <summary>
        /// Defines the <see cref="ShadowColor"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ShadowColorProperty =
            AvaloniaProperty.Register<LayDrawer, IBrush>(nameof(ShadowColor));

        /// <summary>
        /// 阴影颜色
        /// </summary>
        public IBrush ShadowColor
        {
            get { return GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }
        /// <summary>
        /// 抽屉开关事件
        /// </summary> 
        public event EventHandler<RoutedEventArgs> Opened
        {
            add
            {
                AddHandler(OpenedEvent, value);
            }
            remove
            {
                RemoveHandler(OpenedEvent, value);
            }
        }
        public static readonly RoutedEvent<RoutedEventArgs> OpenedEvent =
            RoutedEvent.Register<LayDrawer, RoutedEventArgs>("Opened", RoutingStrategies.Bubble);

        /// <summary>
        /// 开启抽屉
        /// </summary>
        public bool IsOpen
        {
            get { return GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsOpenProperty =
       AvaloniaProperty.Register<LayDrawer, bool>(nameof(IsOpen), false);

        /// <summary>
        /// 排版类型
        /// </summary>
        public DrawerType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="DrawerType"/>属性
        /// </summary>
        public static readonly StyledProperty<DrawerType> TypeProperty =
       AvaloniaProperty.Register<LayDrawer, DrawerType>(nameof(Type), DrawerType.Right);

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_MaskButton = e.NameScope.Find("PART_MaskButton") as Button;
            //新增遮罩点击事件
            if (PART_MaskButton != null)
            {
                PART_MaskButton.Click -= PART_MaskButton_Click;
                PART_MaskButton.Click += PART_MaskButton_Click;
            }
        }
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);
            //移除遮罩点击事件
            PART_MaskButton.Click -= PART_MaskButton_Click;
        }
        /// <summary>
        /// 关闭当前抽屉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PART_MaskButton_Click(object sender, RoutedEventArgs e)
        {
            _isBtnCloseMask = true;
            IsOpen = false;
            RaiseEvent(new RoutedEventArgs(OpenedEvent));
            _isBtnCloseMask = false;
        }
    }
}

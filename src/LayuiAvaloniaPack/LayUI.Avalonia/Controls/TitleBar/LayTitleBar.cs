using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Platform;
using Avalonia.Input;
using System.Reactive.Disposables;
using Avalonia.LogicalTree;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Chrome;
using Avalonia.Media;
using System.Linq;
using LayUI.Avalonia.Enums;
using Avalonia.Styling;
using Avalonia.Threading;

namespace LayUI.Avalonia.Controls
{
    [PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen")]
    public class LayTitleBar : ContentControl
    {
        private Grid PART_TopGrid = null;
        private Panel PART_HeaderBody = null;
        private Panel PART_WindowButtonGrid = null;
        private CompositeDisposable _disposables;
        static LayTitleBar()
        {

        }
        /// <summary>
        /// 顶部标题栏文字颜色
        /// </summary>
        public IBrush HeaderForeground
        {
            get { return GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IBrush"/>属性
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderForegroundProperty =
       AvaloniaProperty.Register<LayTitleBar, IBrush>(nameof(HeaderForeground), null);


        /// <summary>
        /// Defines the <see cref="HeaderVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> HeaderVisibleProperty =
            AvaloniaProperty.Register<Control, bool>(nameof(HeaderVisible), true);

        /// <summary>
        /// 是否显示顶部状态栏
        /// </summary>
        public bool HeaderVisible
        {
            get { return GetValue(HeaderVisibleProperty); }
            set { SetValue(HeaderVisibleProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderHeight"/> property.
        /// </summary>
        public static readonly StyledProperty<double> HeaderHeightProperty =
            AvaloniaProperty.Register<LayTitleBar, double>(nameof(HeaderHeight));

        /// <summary>
        /// 顶部状态栏高度
        /// </summary>
        public double HeaderHeight
        {
            get { return GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> HeaderContentProperty =
            AvaloniaProperty.Register<LayTitleBar, object>(nameof(HeaderContent));

        /// <summary>
        /// 顶部类容
        /// </summary>
        public object HeaderContent
        {
            get { return GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            if (e.ClickCount > 1) return;
            if (e.Source == PART_HeaderBody)
            {
                if (VisualRoot is Window window)
                {
                    if (window.WindowState == WindowState.FullScreen)
                        return;
                    window.BeginMoveDrag(e);
                }
            }

        }

        /// <summary>
        /// 这是依赖属性名称
        /// </summary>
        public IBrush HeaderBackground
        {
            get { return GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IBrush?"/>属性
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
       AvaloniaProperty.Register<LayTitleBar, IBrush>(nameof(HeaderBackground), null);

        /// <summary>
        /// Defines the <see cref="SystemDecorations"/> property.
        /// </summary>
        public static readonly StyledProperty<SystemDecorations> SystemDecorationsProperty =
            AvaloniaProperty.Register<LayTitleBar, SystemDecorations>(nameof(SystemDecorations), SystemDecorations.None);

        /// <summary>
        /// Sets the system decorations (title bar, border, etc)
        /// </summary>
        public SystemDecorations SystemDecorations
        {
            get { return GetValue(SystemDecorationsProperty); }
            set { SetValue(SystemDecorationsProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="TransparencyLevelHint"/> property.
        /// </summary>
        public static readonly StyledProperty<WindowTransparencyLevel> TransparencyLevelHintProperty =
            AvaloniaProperty.Register<LayTitleBar, WindowTransparencyLevel>(nameof(TransparencyLevelHint), WindowTransparencyLevel.Transparent);

        /// <summary>
        /// Gets or sets the <see cref="WindowTransparencyLevel"/> that the TopLevel should use when possible.
        /// </summary>
        public WindowTransparencyLevel TransparencyLevelHint
        {
            get { return GetValue(TransparencyLevelHintProperty); }
            set { SetValue(TransparencyLevelHintProperty, value); }
        }
        /// <summary>
        /// 修改Window样式
        /// </summary>
        private void UpdateWindowStyle()
        {
            if (VisualRoot is Window window)
            {
                window.TransparencyLevelHint = TransparencyLevelHint;
                window.SystemDecorations = SystemDecorations;
            }
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_HeaderBody = e.NameScope.Find<Panel>("PART_HeaderBody");
            PART_WindowButtonGrid = e.NameScope.Find<Panel>("PART_WindowButtonGrid");
            PART_TopGrid = e.NameScope.Find<Grid>("PART_TopGrid");
            if (VisualRoot is Window window)
            {
                _disposables = new CompositeDisposable
                {
                    window.GetObservable(Window.WindowStateProperty)
                        .Subscribe(delegate(WindowState x)
                        {
                            if(x==WindowState.Maximized||x==WindowState.FullScreen)
                            {
                                PART_TopGrid.ColumnDefinitions=new ColumnDefinitions("0,*,0");
                                PART_TopGrid.RowDefinitions=new RowDefinitions("0,*,0");
                            }else{
                                PART_TopGrid.ColumnDefinitions=new ColumnDefinitions("2,*,2");
                                PART_TopGrid.RowDefinitions=new RowDefinitions("2,*,2");
                            }
                            PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                            PseudoClasses.Set(":normal", x == WindowState.Normal);
                            PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                            PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                        }),
                    window.GetObservable(Window.SystemDecorationsProperty)
                        .Subscribe(delegate(SystemDecorations x)
                        {
                          x= SystemDecorations;
                        }),
                    window.GetObservable(Window.TransparencyLevelHintProperty)
                        .Subscribe(delegate(WindowTransparencyLevel x)
                        {
                          x= TransparencyLevelHint;
                        }),
                };
            }
            if (PART_HeaderBody != null) AddOrRemoveHandler(true);
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped -= PART_HeaderBody_DoubleTapped;
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped += PART_HeaderBody_DoubleTapped;
            UpdateWindowStyle();
            SetWindowSide();
        }
        private void AddOrRemoveHandler(bool isAdd)
        {
            var items = PART_WindowButtonGrid.Children.ToList();
            foreach (var item in items)
            {
                if (item is Button button)
                {
                    if (button.CommandParameter is WindowButtonType)
                    {
                        if (isAdd) item.AddHandler(Button.ClickEvent, UpdateWindowState);
                        else item.RemoveHandler(Button.ClickEvent, UpdateWindowState);
                    }
                }
            }
        }
        private void UpdateWindowState(object sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.CommandParameter is WindowButtonType type)
                {
                    if (VisualRoot is Window window)
                    {
                        switch (type)
                        {
                            case WindowButtonType.FullScreen:
                                if (window.WindowState == WindowState.Maximized)
                                {
                                    window.WindowState = WindowState.FullScreen;
                                }
                                else if (window.WindowState == WindowState.FullScreen)
                                {
                                    window.WindowState = WindowState.Normal;
                                }
                                else window.WindowState = WindowState.FullScreen;
                                break;
                            case WindowButtonType.Minimized:
                                window.WindowState = WindowState.Minimized;
                                break;
                            case WindowButtonType.MaximizedOrNormal:
                                if (window.WindowState == WindowState.Maximized) window.WindowState = WindowState.Normal;
                                else window.WindowState = WindowState.Maximized;
                                break;
                            case WindowButtonType.Closed:
                                window.Close();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 设置当前Window可拖拽区域
        /// </summary>
        void SetWindowSide()
        {
            SetupSide("Left", StandardCursorType.LeftSide, WindowEdge.West);
            SetupSide("Right", StandardCursorType.RightSide, WindowEdge.East);
            SetupSide("Top", StandardCursorType.TopSide, WindowEdge.North);
            SetupSide("Bottom", StandardCursorType.BottomSide, WindowEdge.South);
            SetupSide("TopLeft", StandardCursorType.TopLeftCorner, WindowEdge.NorthWest);
            SetupSide("TopRight", StandardCursorType.TopRightCorner, WindowEdge.NorthEast);
            SetupSide("BottomLeft", StandardCursorType.BottomLeftCorner, WindowEdge.SouthWest);
            SetupSide("BottomRight", StandardCursorType.BottomRightCorner, WindowEdge.SouthEast);
        }
        internal void SetupSide(string name, StandardCursorType cursor, WindowEdge edge)
        {
            if (PART_TopGrid == null) return;
            if (VisualRoot is Window window)
            {
                var ctl = PART_TopGrid.Children.Where(o => o.Name == name).FirstOrDefault() as Control;
                if (ctl == null) return;
                ctl.Cursor = new Cursor(cursor);
                ctl.PointerPressed += (i, e) =>
                {
                    Dispatcher.UIThread.Post(() => {
                        window.PlatformImpl?.BeginResizeDrag(edge, e);
                    });
                };
            }
        }
        private void PART_HeaderBody_DoubleTapped(object sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {

            if (e.Source != PART_HeaderBody) return;
            if (VisualRoot is Window window)
            {
                if (window.WindowState == WindowState.Normal) window.WindowState = WindowState.Maximized;
                else window.WindowState = WindowState.Normal;
            }
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
            _disposables?.Dispose();
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped -= PART_HeaderBody_DoubleTapped;
            if (PART_HeaderBody != null) AddOrRemoveHandler(false);
        }
    }
}

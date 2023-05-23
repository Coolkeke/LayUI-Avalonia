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

        /// <summary>
        /// Defines the <see cref="IsWindowToolsVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsWindowToolsVisibleProperty =
            AvaloniaProperty.Register<LayTitleBar, bool>(nameof(IsWindowToolsVisible), true);

        /// <summary>
        /// 工具栏可见
        /// </summary>
        public bool IsWindowToolsVisible
        {
            get { return GetValue(IsWindowToolsVisibleProperty); }
            set { SetValue(IsWindowToolsVisibleProperty, value); }
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
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_HeaderBody = e.NameScope.Find<Panel>("PART_HeaderBody");
            PART_WindowButtonGrid = e.NameScope.Find<Panel>("PART_WindowButtonGrid");
            if (VisualRoot is Window window)
            {
                _disposables = new CompositeDisposable
                {
                    window.GetObservable(Window.WindowStateProperty)
                        .Subscribe(delegate(WindowState x)
                        {
                            PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                            PseudoClasses.Set(":normal", x == WindowState.Normal);
                            PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                            PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                        }),
                };
            }
            WindowBeginResizeDrag(e);
            if (PART_HeaderBody != null) AddOrRemoveHandler(true);
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped -= PART_HeaderBody_DoubleTapped;
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped += PART_HeaderBody_DoubleTapped;
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
        private void WindowBeginResizeDrag(TemplateAppliedEventArgs e)
        {
            SetupSide(e.NameScope.Find<Control>("PART_Left"), StandardCursorType.LeftSide, WindowEdge.West);
            SetupSide(e.NameScope.Find<Control>("PART_Right"), StandardCursorType.RightSide, WindowEdge.East);
            SetupSide(e.NameScope.Find<Control>("PART_Top"), StandardCursorType.TopSide, WindowEdge.North);
            SetupSide(e.NameScope.Find<Control>("PART_Bottom"), StandardCursorType.BottomSide, WindowEdge.South);
            SetupSide(e.NameScope.Find<Control>("PART_LeftTop"), StandardCursorType.TopLeftCorner, WindowEdge.NorthWest);
            SetupSide(e.NameScope.Find<Control>("PART_RightTop"), StandardCursorType.TopRightCorner, WindowEdge.NorthEast);
            SetupSide(e.NameScope.Find<Control>("PART_LeftBottom"), StandardCursorType.BottomLeftCorner, WindowEdge.SouthWest);
            SetupSide(e.NameScope.Find<Control>("PART_RightBottom"), StandardCursorType.BottomRightCorner, WindowEdge.SouthEast);
        }
        private void SetupSide(Control ui, StandardCursorType cursor, WindowEdge edge)
        {
            if (VisualRoot is Window window)
            { 

                EventHandler<PointerPressedEventArgs> PointerPressed = (i, e) =>
                {
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        window.PlatformImpl?.BeginResizeDrag(edge, e);
                    });
                };
                ui.Cursor = new Cursor(cursor);
                ui.PointerPressed += PointerPressed;
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

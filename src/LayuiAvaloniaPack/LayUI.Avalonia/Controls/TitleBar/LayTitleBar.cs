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
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            if (PART_HeaderBody != null && e.Source == PART_HeaderBody)
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
        /// Defines the <see cref="ExtendClientAreaTitleBarHeightHint"/> property.
        /// </summary>
        public static readonly StyledProperty<double> ExtendClientAreaTitleBarHeightHintProperty =
            AvaloniaProperty.Register<LayTitleBar, double>(nameof(ExtendClientAreaTitleBarHeightHint), 7);

        /// <summary>
        /// Gets or Sets the TitlebarHeightHint for when the client area is extended.
        /// A value of -1 will cause the titlebar to be auto sized to the OS default.
        /// Any other positive value will cause the titlebar to assume that height.
        /// </summary>
        public double ExtendClientAreaTitleBarHeightHint
        {
            get { return GetValue(ExtendClientAreaTitleBarHeightHintProperty); }
            set { SetValue(ExtendClientAreaTitleBarHeightHintProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SystemDecorations"/> property.
        /// </summary>
        public static readonly StyledProperty<SystemDecorations> SystemDecorationsProperty =
            AvaloniaProperty.Register<LayTitleBar, SystemDecorations>(nameof(SystemDecorations), SystemDecorations.Full);

        /// <summary>
        /// Sets the system decorations (title bar, border, etc)
        /// </summary>
        public SystemDecorations SystemDecorations
        {
            get { return GetValue(SystemDecorationsProperty); }
            set { SetValue(SystemDecorationsProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ExtendClientAreaToDecorationsHint"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> ExtendClientAreaToDecorationsHintProperty =
            AvaloniaProperty.Register<LayTitleBar, bool>(nameof(ExtendClientAreaToDecorationsHint), true);

        /// <summary>
        /// Gets or sets if the ClientArea is Extended into the Window Decorations (chrome or border).
        /// </summary>
        public bool ExtendClientAreaToDecorationsHint
        {
            get { return GetValue(ExtendClientAreaToDecorationsHintProperty); }
            set { SetValue(ExtendClientAreaToDecorationsHintProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ExtendClientAreaChromeHints"/> property.
        /// </summary>
        public static readonly StyledProperty<ExtendClientAreaChromeHints> ExtendClientAreaChromeHintsProperty =
            AvaloniaProperty.Register<LayTitleBar, ExtendClientAreaChromeHints>(nameof(ExtendClientAreaChromeHints), ExtendClientAreaChromeHints.NoChrome);

        /// <summary>
        /// Gets or Sets the <see cref="Avalonia.Platform.ExtendClientAreaChromeHints"/> that control
        /// how the chrome looks when the client area is extended.
        /// </summary>
        public ExtendClientAreaChromeHints ExtendClientAreaChromeHints
        {
            get { return GetValue(ExtendClientAreaChromeHintsProperty); }
            set { SetValue(ExtendClientAreaChromeHintsProperty, value); }
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
                window.ExtendClientAreaTitleBarHeightHint = ExtendClientAreaTitleBarHeightHint;
                window.SystemDecorations = SystemDecorations;
                window.ExtendClientAreaToDecorationsHint = ExtendClientAreaToDecorationsHint;
                window.ExtendClientAreaChromeHints = ExtendClientAreaChromeHints;
                window.TransparencyLevelHint = TransparencyLevelHint;
            }
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateWindowStyle();
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
            PART_HeaderBody = e.NameScope.Find<Panel>("PART_HeaderBody");
            PART_WindowButtonGrid = e.NameScope.Find<Panel>("PART_WindowButtonGrid");
            if (PART_HeaderBody != null) AddOreRemoveHandler(true);
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped -= PART_HeaderBody_DoubleTapped;
            if (PART_HeaderBody != null) PART_HeaderBody.DoubleTapped += PART_HeaderBody_DoubleTapped;
        }
        private void AddOreRemoveHandler(bool isAdd)
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
                                if (window.WindowState == WindowState.FullScreen) window.WindowState = WindowState.Normal;
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
            if (PART_HeaderBody != null) AddOreRemoveHandler(false);
        }
    }
}

using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using LayUI.Avalonia.Enums;

namespace LayUI.Avalonia.Controls
{
    [PseudoClasses(new string[] { ":expanded", ":left", ":right" })]
    public class LayNavExpander : HeaderedContentControl, ILayControl
    {
        #region 保留原有的Expander逻辑
        public static readonly StyledProperty<NavExpanderStyle> TypeProperty = AvaloniaProperty.Register<LayNavExpander, NavExpanderStyle>("Type", NavExpanderStyle.Right);

        public static readonly StyledProperty<bool> IsExpandedProperty = AvaloniaProperty.Register<LayNavExpander, bool>("IsExpanded", defaultValue: false, inherits: false, BindingMode.TwoWay, null, new Func<AvaloniaObject, bool, bool>(CoerceIsExpanded));

        public static readonly RoutedEvent<RoutedEventArgs> CollapsedEvent = RoutedEvent.Register<LayNavExpander, RoutedEventArgs>("Collapsed", RoutingStrategies.Bubble);

        public static readonly RoutedEvent<CancelRoutedEventArgs> CollapsingEvent = RoutedEvent.Register<LayNavExpander, CancelRoutedEventArgs>("Collapsing", RoutingStrategies.Bubble);

        public static readonly RoutedEvent<RoutedEventArgs> ExpandedEvent = RoutedEvent.Register<LayNavExpander, RoutedEventArgs>("Expanded", RoutingStrategies.Bubble);

        public static readonly RoutedEvent<CancelRoutedEventArgs> ExpandingEvent = RoutedEvent.Register<LayNavExpander, CancelRoutedEventArgs>("Expanding", RoutingStrategies.Bubble);

        private bool _ignorePropertyChanged;
        public bool IsExpanded
        {
            get { return GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }
        public event EventHandler<RoutedEventArgs>? Collapsed
        {
            add
            {
                AddHandler(CollapsedEvent, value);
            }
            remove
            {
                RemoveHandler(CollapsedEvent, value);
            }
        }
        public event EventHandler<CancelRoutedEventArgs>? Collapsing
        {
            add
            {
                AddHandler(CollapsingEvent, value);
            }
            remove
            {
                RemoveHandler(CollapsingEvent, value);
            }
        }
        public event EventHandler<RoutedEventArgs>? Expanded
        {
            add
            {
                AddHandler(ExpandedEvent, value);
            }
            remove
            {
                RemoveHandler(ExpandedEvent, value);
            }
        }
        public event EventHandler<CancelRoutedEventArgs>? Expanding
        {
            add
            {
                AddHandler(ExpandingEvent, value);
            }
            remove
            {
                RemoveHandler(ExpandingEvent, value);
            }
        }
        public LayNavExpander()
        {
            UpdatePseudoClasses();
        }
        protected virtual void OnCollapsed(RoutedEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }
        protected virtual void OnCollapsing(CancelRoutedEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }
        protected virtual void OnExpanded(RoutedEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }
        protected virtual void OnExpanding(CancelRoutedEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }
        private void StartContentTransition()
        {
            Dispatcher.UIThread.Post(delegate
            {
                if (IsExpanded)
                {
                    OnExpanded(new RoutedEventArgs(ExpandedEvent, this));
                }
                else
                {
                    OnCollapsed(new RoutedEventArgs(CollapsedEvent, this));
                }
            });
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (!_ignorePropertyChanged)
            {
                if (change.Property == TypeProperty)
                {
                    UpdatePseudoClasses();
                }
                else if (change.Property == IsExpandedProperty)
                {
                    StartContentTransition();
                    UpdatePseudoClasses();
                }
            }
        }
        private void UpdatePseudoClasses()
        {
            NavExpanderStyle type = Type;
            base.PseudoClasses.Set(":left", type == NavExpanderStyle.Left);
            base.PseudoClasses.Set(":right", type == NavExpanderStyle.Right);
            base.PseudoClasses.Set(":expanded", IsExpanded);
        }
        protected virtual bool OnCoerceIsExpanded(bool value)
        {
            CancelRoutedEventArgs cancelRoutedEventArgs;
            if (value)
            {
                cancelRoutedEventArgs = new CancelRoutedEventArgs(ExpandingEvent, this);
                OnExpanding(cancelRoutedEventArgs);
            }
            else
            {
                cancelRoutedEventArgs = new CancelRoutedEventArgs(CollapsingEvent, this);
                OnCollapsing(cancelRoutedEventArgs);
            }

            if (cancelRoutedEventArgs.Cancel)
            {
                _ignorePropertyChanged = true;
                SetValue(IsExpandedProperty, value);
                _ignorePropertyChanged = false;
                return !value;
            }

            return value;
        }

        private static bool CoerceIsExpanded(AvaloniaObject instance, bool value)
        {
            return (instance as LayNavExpander)?.OnCoerceIsExpanded(value) ?? value;
        }
        #endregion 
        public NavExpanderStyle Type
        {
            get
            {
                return GetValue(TypeProperty);
            }
            set
            {
                SetValue(TypeProperty, value);
            }
        }

        /// <summary>
        /// Defines the <see cref="HeaderBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
            AvaloniaProperty.Register<LayNavExpander, IBrush>(nameof(HeaderBackground)); 
        public IBrush HeaderBackground
        {
            get { return GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        } 
        
        /// <summary>
        /// Defines the <see cref="HeaderPadding"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> HeaderPaddingProperty =
            AvaloniaProperty.Register<LayNavExpander, Thickness>(nameof(HeaderPadding), new Thickness(0));

        /// <summary>
        /// 头部内容内边距
        /// </summary>
        public Thickness HeaderPadding
        {
            get { return GetValue(HeaderPaddingProperty); }
            set { SetValue(HeaderPaddingProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="HeaderHorizontalAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<HorizontalAlignment> HeaderHorizontalAlignmentProperty =
            AvaloniaProperty.Register<LayNavExpander, HorizontalAlignment>(nameof(HeaderHorizontalAlignment), HorizontalAlignment.Left);

        /// <summary>
        /// 头部内容水平位置
        /// </summary>
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return GetValue(HeaderHorizontalAlignmentProperty); }
            set { SetValue(HeaderHorizontalAlignmentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderVerticalAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<VerticalAlignment> HeaderVerticalAlignmentProperty =
            AvaloniaProperty.Register<LayNavExpander, VerticalAlignment>(nameof(HeaderVerticalAlignment), VerticalAlignment.Center);

        /// <summary>
        /// 头部内容垂直位置
        /// </summary>
        public VerticalAlignment HeaderVerticalAlignment
        {
            get { return GetValue(HeaderVerticalAlignmentProperty); }
            set { SetValue(HeaderVerticalAlignmentProperty, value); }
        }
    }
}

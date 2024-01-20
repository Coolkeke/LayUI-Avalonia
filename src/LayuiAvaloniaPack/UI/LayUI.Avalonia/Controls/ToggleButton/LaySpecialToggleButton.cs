using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 开关(可用于定制其他控件基类,用法与ToggleButton相同)
    /// <para>唯一区别是IsChecked属性不在为Null</para>
    /// </summary>
    [PseudoClasses(":checked", ":unchecked")]
    public class LaySpecialToggleButton : Button, ILayControl
    {
        /// <summary>
        /// Defines the <see cref="UncheckedContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> UncheckedContentProperty =
            AvaloniaProperty.Register<LaySpecialToggleButton, object>(nameof(UncheckedContent));

        /// <summary>
        /// UncheckedContent
        /// </summary>
        public object UncheckedContent
        {
            get { return GetValue(UncheckedContentProperty); }
            set { SetValue(UncheckedContentProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="CheckedContent"/> property.
        /// </summary>
        public static readonly StyledProperty<object> CheckedContentProperty =
            AvaloniaProperty.Register<LaySpecialToggleButton, object>(nameof(CheckedContent));

        /// <summary>
        /// CheckedContent
        /// </summary>
        public object CheckedContent
        {
            get { return GetValue(CheckedContentProperty); }
            set { SetValue(CheckedContentProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="IsChecked"/> property.
        /// </summary>
        public static readonly DirectProperty<LaySpecialToggleButton, bool> IsCheckedProperty =
            AvaloniaProperty.RegisterDirect<LaySpecialToggleButton, bool>(
                nameof(IsChecked),
                o => o.IsChecked,
                (o, v) => o.IsChecked = v,
                defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Defines the <see cref="Checked"/> event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> CheckedEvent =
            RoutedEvent.Register<LaySpecialToggleButton, RoutedEventArgs>(nameof(Checked), RoutingStrategies.Bubble);

        /// <summary>
        /// Defines the <see cref="Unchecked"/> event.
        /// </summary>
        public static readonly RoutedEvent<RoutedEventArgs> UncheckedEvent =
            RoutedEvent.Register<LaySpecialToggleButton, RoutedEventArgs>(nameof(Unchecked), RoutingStrategies.Bubble);

        private bool _isChecked = false; 

        public LaySpecialToggleButton()
        {
            IsCheckedProperty.Changed.AddClassHandler<LaySpecialToggleButton>((x, e) => x.OnIsCheckedChanged(e));
            UpdatePseudoClasses(IsChecked);
        }

        /// <summary>
        /// Raised when a <see cref="LaySpecialToggleButton"/> is checked.
        /// </summary>
        public event EventHandler<RoutedEventArgs> Checked
        {
            add => AddHandler(CheckedEvent, value);
            remove => RemoveHandler(CheckedEvent, value);
        }

        /// <summary>
        /// Raised when a <see cref="LaySpecialToggleButton"/> is unchecked.
        /// </summary>
        public event EventHandler<RoutedEventArgs> Unchecked
        {
            add => AddHandler(UncheckedEvent, value);
            remove => RemoveHandler(UncheckedEvent, value);
        }

        /// <summary>
        /// Gets or sets whether the <see cref="LaySpecialToggleButton"/> is checked.
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                SetAndRaise(IsCheckedProperty, ref _isChecked, value);
                UpdatePseudoClasses(IsChecked);
            }
        }

        protected override void OnClick()
        {
            Toggle();
            base.OnClick();
        }

        /// <summary>
        /// Toggles the <see cref="IsChecked"/> property.
        /// </summary>
        protected virtual void Toggle()
        {
            if (_isChecked) IsChecked = false;
            else IsChecked = true;
        }

        /// <summary>
        /// Called when <see cref="IsChecked"/> becomes true.
        /// </summary>
        /// <param name="e">Event arguments for the routed event that is raised by the default implementation of this method.</param>
        protected virtual void OnChecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// Called when <see cref="IsChecked"/> becomes false.
        /// </summary>
        /// <param name="e">Event arguments for the routed event that is raised by the default implementation of this method.</param>
        protected virtual void OnUnchecked(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        /// <summary>
        /// Called when <see cref="IsChecked"/> becomes null.
        /// </summary>
        /// <param name="e">Event arguments for the routed event that is raised by the default implementation of this method.</param>
        protected virtual void OnIndeterminate(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        private void OnIsCheckedChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var newValue = (bool?)e.NewValue;

            switch (newValue)
            {
                case true:
                    OnChecked(new RoutedEventArgs(CheckedEvent));
                    break;
                case false:
                    OnUnchecked(new RoutedEventArgs(UncheckedEvent));
                    break;
                default:
                    break;
            }
        }

        private void UpdatePseudoClasses(bool isChecked)
        {
            PseudoClasses.Set(":checked", isChecked == true);
            PseudoClasses.Set(":unchecked", isChecked == false);
        }
    }
}

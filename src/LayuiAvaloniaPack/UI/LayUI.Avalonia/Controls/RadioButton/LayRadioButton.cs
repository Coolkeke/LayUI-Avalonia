using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayRadioButton: RadioButton
    {
        /// <summary>
        /// Defines the <see cref="Size"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SizeProperty =
            AvaloniaProperty.Register<LayRadioButton, double>(nameof(Size), 20.0);

        /// <summary>
        /// Comment
        /// </summary>
        public double Size
        {
            get { return GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }
    }
}

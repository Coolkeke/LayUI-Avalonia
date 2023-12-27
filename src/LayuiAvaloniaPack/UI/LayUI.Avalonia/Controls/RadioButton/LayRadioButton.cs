using Avalonia;
using Avalonia.Controls;
using LayUI.Avalonia.Enums;

namespace LayUI.Avalonia.Controls
{
    public class LayRadioButton: RadioButton, ILayControl
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

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<RadioButtonType> TypeProperty =
            AvaloniaProperty.Register<Control, RadioButtonType>(nameof(Type), RadioButtonType.Default);

        /// <summary>
        /// 类型
        /// </summary>
        public RadioButtonType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
    }
}

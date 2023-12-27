using Avalonia;
using Avalonia.Controls;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 复选框
    /// </summary>
    public class LayCheckBox: CheckBox, ILayControl
    {

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<CheckBoxType> TypeProperty =
            AvaloniaProperty.Register<Control, CheckBoxType>(nameof(Type), CheckBoxType.Default);

        /// <summary>
        /// 类型
        /// </summary>
        public CheckBoxType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Size"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SizeProperty =
            AvaloniaProperty.Register<Control, double>(nameof(Size), 20.0);

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

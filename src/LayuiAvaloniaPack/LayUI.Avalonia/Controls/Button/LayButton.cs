using Avalonia;
using Avalonia.Controls;
using LayUI.Avalonia.Enums.Button;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 这是按钮
    /// </summary>
    public class LayButton : Button
    {
        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<ButtonStyle> TypeProperty =
            AvaloniaProperty.Register<Control, ButtonStyle>(nameof(Type));

        /// <summary>
        /// 按钮类型
        /// </summary>
        public ButtonStyle Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
    }
}

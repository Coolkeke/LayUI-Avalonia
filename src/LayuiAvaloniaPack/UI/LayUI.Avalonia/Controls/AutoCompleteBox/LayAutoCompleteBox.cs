using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayAutoCompleteBox: AutoCompleteBox
    {
        /// <summary>
        /// 是否聚焦
        /// </summary>
        public bool IsFocus
        {
            get { return GetValue(IsFocusProperty); }
            set { SetValue(IsFocusProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsFocusProperty =
       AvaloniaProperty.Register<LayAutoCompleteBox, bool>(nameof(IsFocus));
    }
}

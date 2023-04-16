using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 过度动画
    /// </summary>
    public class LayTransitions : ContentControl
    {

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive
        {
            get { return GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsActiveProperty =
       AvaloniaProperty.Register<LayTransitions, bool>(nameof(IsActive), false);

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<AnimationType> TypeProperty =
            AvaloniaProperty.Register<LayTransitions, AnimationType>(nameof(Type), AnimationType.Default);

        /// <summary>
        /// 动画类型
        /// </summary>
        public AnimationType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
    }
}

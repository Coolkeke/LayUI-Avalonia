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
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<AnimationType> TypeProperty =
            AvaloniaProperty.Register<LayTransitions, AnimationType>(nameof(Type), AnimationType.Default);

        /// <summary>
        /// Comment
        /// </summary>
        public AnimationType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
    }
}

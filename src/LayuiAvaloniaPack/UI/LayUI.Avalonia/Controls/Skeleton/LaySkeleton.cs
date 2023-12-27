using Avalonia;
using Avalonia.Controls;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 骨架
    /// </summary>
    public class LaySkeleton : ContentControl, ILayControl
    { 
        /// <summary>
        /// Defines the <see cref="IsActive"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsActiveProperty =
            AvaloniaProperty.Register<Control, bool>(nameof(IsActive));

        /// <summary>
        /// 激活
        /// </summary>
        public bool IsActive
        {
            get { return GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<SkeletonType> TypeProperty =
            AvaloniaProperty.Register<Control, SkeletonType>(nameof(Type));

        /// <summary>
        /// 样式
        /// </summary>
        public SkeletonType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
    }
}

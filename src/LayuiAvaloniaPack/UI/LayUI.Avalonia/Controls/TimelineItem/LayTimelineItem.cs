using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 时间线子项
    /// </summary>
    
    public class LayTimelineItem : HeaderedContentControl
    {
        private const string PC_First = ":first";
        private const string PC_Last = ":last"; 
        private const string PC_None = ":none";
        internal void SetIndex(bool isFirst, bool isLast, bool isNone)
        {
            PseudoClasses.Set(PC_First, isFirst);
            PseudoClasses.Set(PC_Last, isLast);
            PseudoClasses.Set(PC_None, isNone);
        }


        /// <summary>
        /// Defines the <see cref="IconCornerRadius"/> property.
        /// </summary>
        internal static readonly StyledProperty<CornerRadius> IconCornerRadiusProperty =
            AvaloniaProperty.Register<LayTimelineItem, CornerRadius>(nameof(IconCornerRadius));

        /// <summary>
        /// 图标圆角效果
        /// </summary>
        internal CornerRadius IconCornerRadius
        {
            get { return GetValue(IconCornerRadiusProperty); }
            set { SetValue(IconCornerRadiusProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="IconWidth"/> property.
        /// </summary>
        internal static readonly StyledProperty<double> IconWidthProperty =
            AvaloniaProperty.Register<LayTimelineItem, double>(nameof(IconWidth));

        /// <summary>
        /// 图标可视化宽度
        /// </summary>
        internal double IconWidth
        {
            get { return GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IconHeight"/> property.
        /// </summary>
        internal static readonly StyledProperty<double> IconHeightProperty =
            AvaloniaProperty.Register<LayTimelineItem, double>(nameof(IconHeight));

        /// <summary>
        /// 图标可视化高度
        /// </summary>
        internal double IconHeight
        {
            get { return GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Icon"/> property.
        /// </summary>
        public static readonly StyledProperty<object> IconProperty =
            AvaloniaProperty.Register<LayTimelineItem, object>(nameof(Icon));

        /// <summary>
        /// 图标
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}

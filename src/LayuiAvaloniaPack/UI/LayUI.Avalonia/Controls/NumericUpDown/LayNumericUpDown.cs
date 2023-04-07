using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayNumericUpDown: NumericUpDown
    {

        /// <summary>
        /// Defines the <see cref="SelectionBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> SelectionBrushProperty =
            AvaloniaProperty.Register<Control, IBrush>(nameof(SelectionBrush));

        /// <summary>
        /// 选中背景色
        /// </summary>
        public IBrush SelectionBrush
        {
            get { return GetValue(SelectionBrushProperty); }
            set { SetValue(SelectionBrushProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="SelectionBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> SelectionForegroundBrushProperty =
            AvaloniaProperty.Register<Control, IBrush>(nameof(SelectionForegroundBrush));

        /// <summary>
        /// 文字选中颜色
        /// </summary>
        public IBrush SelectionForegroundBrush
        {
            get { return GetValue(SelectionForegroundBrushProperty); }
            set { SetValue(SelectionForegroundBrushProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="Orientation"/> property.
        /// </summary>
        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<Control, Orientation>(nameof(Orientation), Orientation.Horizontal);

        /// <summary>
        /// 排版方向
        /// </summary>
        public Orientation Orientation
        {
            get { return GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 计数器
    /// </summary>
    public class LayNumericUpDown: NumericUpDown, ILayControl
    {

        /// <summary>
        /// Defines the <see cref="SelectionForegroundBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> SelectionForegroundBrushProperty =
            AvaloniaProperty.Register<LayNumericUpDown, IBrush>(nameof(SelectionForegroundBrush));

        /// <summary>
        /// 文字选中色
        /// </summary>
        public IBrush SelectionForegroundBrush
        {
            get { return GetValue(SelectionForegroundBrushProperty); }
            set { SetValue(SelectionForegroundBrushProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="SelectionBrush"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> SelectionBrushProperty =
            AvaloniaProperty.Register<LayNumericUpDown, IBrush>(nameof(SelectionBrush));

        /// <summary>
        /// 光标色
        /// </summary>
        public IBrush SelectionBrush
        {
            get { return GetValue(SelectionBrushProperty); }
            set { SetValue(SelectionBrushProperty, value); }
        }
    }
}

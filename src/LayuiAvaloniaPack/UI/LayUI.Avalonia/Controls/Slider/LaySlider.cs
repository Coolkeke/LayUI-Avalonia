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
    public class LaySlider: Slider, ILayControl
    {

        /// <summary>
        /// Defines the <see cref="TipsPosition"/> property.
        /// </summary>
        public static readonly StyledProperty<TipsPosition> TipsPositionProperty =
            AvaloniaProperty.Register<Control, TipsPosition>(nameof(TipsPosition));

        /// <summary>
        /// 信息提示位置
        /// </summary>
        public TipsPosition TipsPosition
        {
            get { return GetValue(TipsPositionProperty); }
            set { SetValue(TipsPositionProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="LineSize"/> property.
        /// </summary>
        public static readonly StyledProperty<double> LineSizeProperty =
            AvaloniaProperty.Register<LaySlider, double>(nameof(LineSize), 4.0);

        /// <summary>
        /// 线高度
        /// </summary>
        public double LineSize
        {
            get { return GetValue(LineSizeProperty); }
            set { SetValue(LineSizeProperty, value); }
        }
    }
}

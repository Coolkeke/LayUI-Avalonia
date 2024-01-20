using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 开关
    /// </summary>
    public class LaySwitchButton: LaySpecialToggleButton
    {

        /// <summary>
        /// Defines the <see cref="SwitchWidth"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SwitchWidthProperty =
            AvaloniaProperty.Register<LaySwitchButton, double>(nameof(SwitchWidth));

        /// <summary>
        /// 开关宽度
        /// </summary>
        public double SwitchWidth
        {
            get { return GetValue(SwitchWidthProperty); }
            set { SetValue(SwitchWidthProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="SwitchHeight"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SwitchHeightProperty =
            AvaloniaProperty.Register<LaySwitchButton, double>(nameof(SwitchHeight));

        /// <summary>
        /// 开关高度
        /// </summary>
        public double SwitchHeight
        {
            get { return GetValue(SwitchHeightProperty); }
            set { SetValue(SwitchHeightProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsContentVisible"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsContentVisibleProperty =
            AvaloniaProperty.Register<LaySwitchButton, bool>(nameof(IsContentVisible));

        /// <summary>
        /// 显示内容
        /// </summary>
        public bool IsContentVisible
        {
            get { return GetValue(IsContentVisibleProperty); }
            set { SetValue(IsContentVisibleProperty, value); }
        }
    }
}

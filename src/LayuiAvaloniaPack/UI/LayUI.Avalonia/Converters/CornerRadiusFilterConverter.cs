using Avalonia.Controls.Converters;
using Avalonia.Data.Converters;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LayUI.Avalonia.Converters
{
    public class CornerRadiusFilterConverter : IValueConverter
    {
        private static CornerRadiusFilterConverter _Instance;
        public static CornerRadiusFilterConverter Instance => _Instance ?? (_Instance = new CornerRadiusFilterConverter());
        /// <summary>
        /// 获得圆角效果<see cref="CornerRadiusFilterConverter"/>.
        /// </summary>
        public CornerRadiusFilterKinds Filter { get; set; }

        /// <summary>
        /// Gets or sets the scale multiplier applied to the <see cref="CornerRadiusFilterConverter"/>.
        /// </summary>
        public double Scale { get; set; } = 1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is CornerRadius radius))
            {
                return value;
            }

            return new CornerRadius(
                Filter.HasAllFlags(CornerRadiusFilterKinds.TopLeft) ? radius.TopLeft * Scale : 0,
                Filter.HasAllFlags(CornerRadiusFilterKinds.TopRight) ? radius.TopRight * Scale : 0,
                Filter.HasAllFlags(CornerRadiusFilterKinds.BottomRight) ? radius.BottomRight * Scale : 0,
                Filter.HasAllFlags(CornerRadiusFilterKinds.BottomLeft) ? radius.BottomLeft * Scale : 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

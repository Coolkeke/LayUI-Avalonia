using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LayUI.Avalonia.Converters
{
    public class DoubleToThicknessConverter : IValueConverter
    {
        private static DoubleToThicknessConverter _Instance;
        public static DoubleToThicknessConverter Instance => _Instance ?? (_Instance = new DoubleToThicknessConverter());
        public ExpandDirection Filter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return new Thickness(0, 0, 0, 0);
            switch (Filter)
            {
                case ExpandDirection.Down: 
                    return new Thickness(0,  0, 0, (double)value);
                case ExpandDirection.Up:
                    return new Thickness(0, (double)value, 0, 0);
                case ExpandDirection.Left:
                    return new Thickness((double)value,0, 0, 0);
                case ExpandDirection.Right:
                    return new Thickness(0, 0, (double)value, 0);
                default:
                    return new Thickness(0, 0, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

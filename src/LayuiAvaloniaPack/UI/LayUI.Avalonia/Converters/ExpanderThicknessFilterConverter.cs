using Avalonia.Controls.Converters;
using Avalonia.Data.Converters;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Avalonia.Controls;

namespace LayUI.Avalonia.Converters
{
    /// <summary>
    /// 折叠板边框线转换器
    /// </summary>
    public class ExpanderThicknessFilterConverter : IValueConverter
    {
        private static ExpanderThicknessFilterConverter _Instance;
        public static ExpanderThicknessFilterConverter Instance => _Instance ?? (_Instance = new ExpanderThicknessFilterConverter());
        public ExpandDirection Filter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Thickness thickness))
            {
                return value;
            }  
            switch (Filter)
            {
                case ExpandDirection.Down: 
                    return new Thickness(thickness.Left,0, thickness.Right, thickness.Bottom);
                case ExpandDirection.Up:
                    return new Thickness(thickness.Left, thickness.Top, thickness.Right, 0);
                case ExpandDirection.Left:
                    return new Thickness(thickness.Left, thickness.Top, 0, thickness.Bottom);
                case ExpandDirection.Right:
                    return new Thickness(0, thickness.Top, thickness.Right, thickness.Bottom);
            }
            return thickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

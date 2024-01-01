using Avalonia.Data.Converters;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Converters
{
    public class CarouselItemsGridMarginConverter : IValueConverter
    {
        private static CarouselItemsGridMarginConverter _Instance;
        public static CarouselItemsGridMarginConverter Instance => _Instance ?? (_Instance = new CarouselItemsGridMarginConverter());
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double Offset = 0;
            if (value is double Num)
            {
                Offset = Num;
            }
            return new Thickness(-Offset, 0, Offset, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

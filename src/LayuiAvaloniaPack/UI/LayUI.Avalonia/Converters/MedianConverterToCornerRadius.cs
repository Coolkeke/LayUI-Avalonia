using Avalonia.Data.Converters;
using Avalonia; 
using System.Globalization; 

namespace LayUI.Avalonia.Converters
{
    public class MedianConverterToCornerRadius : IValueConverter
    {
        private static MedianConverterToCornerRadius _Instance;
        public static MedianConverterToCornerRadius Instance => _Instance ?? (_Instance = new MedianConverterToCornerRadius());
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                return new CornerRadius((double)MedianConverter.Instance.Convert(result, null, null, null));
            }
            else
                return new CornerRadius(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

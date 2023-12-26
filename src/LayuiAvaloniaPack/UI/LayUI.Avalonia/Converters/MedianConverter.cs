using Avalonia.Data.Converters; 
using System.Globalization; 

namespace LayUI.Avalonia.Converters
{
    public class MedianConverter : IValueConverter
    {
        private static MedianConverter _Instance;
        public static MedianConverter Instance => _Instance ?? (_Instance = new MedianConverter());
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                if (parameter != null && parameter.ToString() == "-") return -(result / 2);
                else return result / 2;
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

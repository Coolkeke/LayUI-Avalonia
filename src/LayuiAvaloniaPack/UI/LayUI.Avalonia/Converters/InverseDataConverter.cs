using Avalonia.Data;
using Avalonia.Data.Converters;
using System.Globalization;

namespace LayUI.Avalonia.Converters
{
    /// <summary>
    /// 数据取反转换器
    /// </summary>
    public class InverseDataConverter : IValueConverter
    {
        private static InverseDataConverter _Instance;
        /// <summary>
        /// 数据取反转换器
        /// </summary>
        public static InverseDataConverter Instance => _Instance ?? (_Instance = new InverseDataConverter());

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is double d_value)
                {
                    if (parameter is bool data)
                    {
                        if (data) return -d_value;
                    }
                    else
                    {
                        return d_value;
                    };
                }
                if (value is bool b_value)
                {
                    if (parameter is bool data)
                    {
                        if (data) return !b_value;
                    }
                    else
                    {
                        return b_value;
                    };
                }
                return value;
            }
            catch (Exception ex)
            {
                return new BindingNotification(new InvalidCastException(ex.Message), BindingErrorType.Error);
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

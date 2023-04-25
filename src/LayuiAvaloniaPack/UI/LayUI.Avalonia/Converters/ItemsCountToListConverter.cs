using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LayUI.Avalonia.Converters
{
    /// <summary>
    /// 数量转集合
    /// </summary>
    public class ItemsCountToListConverter : IValueConverter
    {
        private static ItemsCountToListConverter _Instance;
        /// <summary>
        /// 数量转集合转换器
        /// </summary>
        public static ItemsCountToListConverter Instance => _Instance ?? (_Instance = new ItemsCountToListConverter());
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = new List<object>();
            if (value is int count)
            {
                for (int i = 0; i < count; i++)
                {
                    items.Add(new { });
                }

            }
            return items;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

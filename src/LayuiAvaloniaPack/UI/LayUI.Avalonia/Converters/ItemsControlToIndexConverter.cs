using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LayUI.Avalonia.Converters
{
    /// <summary>
    /// 列表序号转换器
    /// </summary>
    public class ItemsControlToIndexConverter : IMultiValueConverter
    {
        private static ItemsControlToIndexConverter _Instance;
        /// <summary>
        /// 列表序号转换器
        /// </summary>
        public static ItemsControlToIndexConverter Instance => _Instance ?? (_Instance = new ItemsControlToIndexConverter());
        public object Item { get; set; }
        public IList Items { get; set; }
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is UnsetValueType || values[1] is UnsetValueType || values[2] is UnsetValueType) return 0;
            var item = values[0];
            if (values[1] is IList list)
            {
                var index = list.IndexOf(item) + 1;
                return index;
            }
            return 0;
        }
    }
}

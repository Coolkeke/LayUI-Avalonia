using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Converters
{
    public class CollectionElementIndexConverter : IMultiValueConverter
    {
        private static CollectionElementIndexConverter? _Instance;
        /// <summary>
        /// 列表序号转换器
        /// </summary>
        public static CollectionElementIndexConverter Instance
        {
            get
            {
                if (_Instance == null) _Instance = new CollectionElementIndexConverter();
                return _Instance;
            }
        }

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is UnsetValueType || values[1] is UnsetValueType) return 0;
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

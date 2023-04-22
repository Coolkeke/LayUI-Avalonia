using Avalonia;
using Avalonia.Data.Converters;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LayUI.Avalonia.Converters
{
    /// <summary>
    /// 步骤条选项横线控制
    /// </summary>
    public class StepBarItemConverter : IMultiValueConverter
    {
        private static StepBarItemConverter _Instance;
        /// <summary>
        /// 
        /// </summary>
        public static StepBarItemConverter Instance => _Instance ?? (_Instance = new StepBarItemConverter());
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is UnsetValueType || values[1] is UnsetValueType) return true;
            var index = values[0];
            var count = values[1];
            if (parameter is bool value)
            {
                if (value) return !((int)index <= 1);
                else return !((int)index >=(int)count);
            }
            return true;
        }
    }
}

using Avalonia.Data.Converters;
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
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}

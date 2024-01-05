using Avalonia.Data.Converters;
using Avalonia.Media.Transformation;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Converters
{
    public class DrawerTranslateConverter : IValueConverter
    {
        private static DrawerTranslateConverter _Instance;
        public static DrawerTranslateConverter Instance => _Instance ?? (_Instance = new DrawerTranslateConverter());
        /// <summary>
        /// 排版类型
        /// </summary>
        public DrawerType drawerType { get; set; }
        /// <summary>
        /// 动画位移类型
        /// </summary>
        public TranslateType translate { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return TransformOperations.Parse("translate(0px)");
            double Offset = (double)value;
            if (drawerType == DrawerType.Left || drawerType == DrawerType.Top) Offset = -Offset;
            switch (translate)
            {
                case TranslateType.X:
                    return TransformOperations.Parse($"translateX({Offset}px)");
                case TranslateType.Y:
                    return TransformOperations.Parse($"translateY({Offset}px)");
            }
            return TransformOperations.Parse("translate(0px)");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

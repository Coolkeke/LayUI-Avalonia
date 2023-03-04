using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayNotificationControl:ContentControl
    {

        /// <summary>
        /// Defines the <see cref="DataTemplate"/> property.
        /// </summary>
        public static readonly StyledProperty<DataTemplate> DataTemplateProperty =
            AvaloniaProperty.Register<LayNotificationControl, DataTemplate>(nameof(DataTemplate), null);

        /// <summary>
        /// 数据模板
        /// </summary>
        public DataTemplate DataTemplate
        {
            get { return GetValue(DataTemplateProperty); }
            set { SetValue(DataTemplateProperty, value); }
        }
    }
}

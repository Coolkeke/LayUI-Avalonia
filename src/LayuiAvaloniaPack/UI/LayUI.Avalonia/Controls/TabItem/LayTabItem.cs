using Avalonia;
using Avalonia.Controls;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayTabItem: TabItem, ILayControl
    {
        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        internal static readonly StyledProperty<TabControlType> TypeProperty =
            AvaloniaProperty.Register<LayTabItem, TabControlType>(nameof(Type));

        /// <summary>
        /// 类型
        /// </summary>
        internal TabControlType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
    }
}

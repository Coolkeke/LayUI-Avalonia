using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayListBoxItem : ListBoxItem, ILayControl
    {

        /// <summary>
        /// Defines the <see cref="Number"/> property.
        /// </summary>
        public static readonly StyledProperty<int> NumberProperty =
            AvaloniaProperty.Register<Control, int>(nameof(Number));

        /// <summary>
        /// 序号
        /// </summary>
        public int Number
        {
            get { return GetValue(NumberProperty); }
            internal set { SetValue(NumberProperty, value); }
        }
    }
}

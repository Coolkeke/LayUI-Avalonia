using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayTextBlock : TextBlock, ILayControl
    {
        public LayTextBlock()
        {
            LayTextProperty.Changed.AddClassHandler<LayTextBlock>((o, e) => o.OnTextChanged(e));
        }

        private void OnTextChanged(AvaloniaPropertyChangedEventArgs e)
        {
            Text = (string?)e.NewValue;
        }

        /// <summary>
        /// Defines the <see cref="LayText"/> property.
        /// </summary>
        public static readonly StyledProperty<string?> LayTextProperty =
            AvaloniaProperty.Register<Control, string?>(nameof(LayText));

        /// <summary>
        /// 支持样式内部绑定文本
        /// </summary>
        public string? LayText
        {
            get { return GetValue(LayTextProperty); }
            set { SetValue(LayTextProperty, value); }
        }
    }
}

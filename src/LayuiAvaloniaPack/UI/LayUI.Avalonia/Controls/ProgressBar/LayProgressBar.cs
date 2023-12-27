using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 进度条
    /// </summary>
    public class LayProgressBar: ProgressBar, ILayControl
    {

        /// <summary>
        /// Defines the <see cref="ProgressTextForeground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ProgressTextForegroundProperty =
            AvaloniaProperty.Register<Control, IBrush>(nameof(ProgressTextForeground));

        /// <summary>
        /// 文字颜色
        /// </summary>
        public IBrush ProgressTextForeground
        {
            get { return GetValue(ProgressTextForegroundProperty); }
            set { SetValue(ProgressTextForegroundProperty, value); }
        }
    }
}

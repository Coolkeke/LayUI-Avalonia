using Avalonia.Controls.Primitives;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 面板
    /// </summary>
    public class LayGroupBox : HeaderedContentControl, ILayControl
    {

        /// <summary>
        /// Defines the <see cref="Line"/> property.
        /// </summary>
        public static readonly StyledProperty<double> LineProperty =
            AvaloniaProperty.Register<LayGroupBox, double>(nameof(Line));

        /// <summary>
        /// 线粗细
        /// </summary>
        public double Line
        {
            get { return GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }
    }
}

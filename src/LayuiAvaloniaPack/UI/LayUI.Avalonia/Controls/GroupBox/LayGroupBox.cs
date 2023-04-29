using Avalonia;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 面板
    /// </summary>
    public class LayGroupBox:HeaderedContentControl
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

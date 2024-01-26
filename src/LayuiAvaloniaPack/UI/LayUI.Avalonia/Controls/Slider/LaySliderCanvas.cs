using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 滑块专用画布
    /// </summary>
    internal class LaySliderCanvas : Canvas, ILayControl
    {
        /// <summary>
        /// Defines the <see cref="Orientation"/> property.
        /// </summary>
        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<LaySliderCanvas, Orientation>(nameof(Orientation));

        [Bindable(true)]
        /// <summary>
        /// Comment
        /// </summary>
        public Orientation Orientation
        {
            get { return GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (Control internalChild in base.Children)
            {
                if (internalChild == null) continue;

                var x = 0.0;
                var y = 0.0;

                if (Orientation == Orientation.Horizontal)
                {
                    x = (arrangeSize.Width - internalChild.DesiredSize.Width) / 2;

                    var top = GetTop(internalChild);
                    if (!double.IsNaN(top))
                    {
                        y = top;
                    }
                    else
                    {
                        var bottom = GetBottom(internalChild);
                        if (!double.IsNaN(bottom))
                            y = arrangeSize.Height - internalChild.DesiredSize.Height - bottom;
                    }
                }
                else
                {
                    y = (arrangeSize.Height - internalChild.DesiredSize.Height) / 2;

                    var left = GetLeft(internalChild);
                    if (!double.IsNaN(left))
                    {
                        x = left;
                    }
                    else
                    {
                        var right = GetRight(internalChild);
                        if (!double.IsNaN(right))
                            x = arrangeSize.Width - internalChild.DesiredSize.Width - right;
                    }
                }

                internalChild.Arrange(new Rect(new Point(x, y), internalChild.DesiredSize));
            }
            return arrangeSize;
        }
    }
}

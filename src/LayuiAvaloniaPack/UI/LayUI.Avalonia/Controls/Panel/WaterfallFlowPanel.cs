using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class WaterfallFlowPanel : Panel, ILayControl
    {
        /// <summary>
        /// Defines the <see cref="Column"/> property.
        /// </summary>
        public static readonly StyledProperty<int> ColumnProperty =
            AvaloniaProperty.Register<WaterfallFlowPanel, int>(nameof(Column), 5);

        /// <summary>
        /// Comment
        /// </summary>
        public int Column
        {
            get { return GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            Size layoutSlotSize = availableSize;
            layoutSlotSize = layoutSlotSize.WithWidth(Double.PositiveInfinity);
            double layHeight = 0;
            //计算当前列 
            if (Column > 0)
            {
                double[] arrHeight;
                if (VisualChildren.Count > Column) arrHeight = new double[Column];
                else arrHeight = new double[VisualChildren.Count];
                for (int i = 0; i < VisualChildren.Count; i++)
                {
                    if (VisualChildren[i] is Layoutable layoutable)
                    {
                        layoutable.Measure(availableSize);
                        if (i < Column)
                        {
                            arrHeight[i] = layoutable.DesiredSize.Height;
                        }
                        else
                        {
                            var minHeight = arrHeight.Min();
                            var index = Array.IndexOf(arrHeight, minHeight);
                            arrHeight[index] = minHeight + layoutable.DesiredSize.Height;
                        }
                    }
                }
                layHeight = arrHeight.Max(); 
            }
            layoutSlotSize = layoutSlotSize.WithHeight(layHeight);
            return layoutSlotSize;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            //计算当前列 
            if (Column > 0)
            {
                double[] arrHeight;
                if (Children.Count > Column)
                {
                    arrHeight = new double[Column];
                }
                else arrHeight = new double[Children.Count];
                for (int i = 0; i < Children.Count; i++)
                {
                    if (Children[i] is Layoutable layoutable)
                    {
                        layoutable.Width = finalSize.Width / Column;
                        if (i < Column)
                        {
                            arrHeight[i] = layoutable.DesiredSize.Height;
                            double x = layoutable.DesiredSize.Width * i;
                            layoutable.Arrange(new Rect(new Point(x, 0), layoutable.DesiredSize));
                        }
                        else
                        {
                            var minHeight = arrHeight.Min();
                            var index = Array.IndexOf(arrHeight, minHeight);
                            arrHeight[index] = (minHeight + layoutable.DesiredSize.Height);
                            double x = index * layoutable.DesiredSize.Width;
                            double y = minHeight;
                            layoutable.Arrange(new Rect(new Point(x, y), layoutable.DesiredSize));
                        }
                    }
                }
            }

            return finalSize;
        }
    }
}

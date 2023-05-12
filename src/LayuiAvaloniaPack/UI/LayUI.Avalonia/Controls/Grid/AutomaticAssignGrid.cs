using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 自动排版容器 
    /// </summary>
    public class AutomaticAssignGrid:Panel
    {
        /// <summary>
        /// 行
        /// </summary>
        private int _rows;
        /// <summary>
        /// 列
        /// </summary>
        private int _columns;
        static AutomaticAssignGrid()
        {
            AffectsMeasure<AutomaticAssignGrid>(new AvaloniaProperty[3]
            {
                RowsProperty,
                ColumnsProperty,
                FirstColumnProperty
            });
        }

        /// <summary>
        /// 指定行数。如果设置为0，将自动计算行数
        /// </summary>
        private int Rows
        {
            get => GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        public static readonly StyledProperty<int> RowsProperty =
            AvaloniaProperty.Register<AutomaticAssignGrid, int>("Rows", 0);
        /// <summary>
        /// 指定列计数。如果设置为0，则将自动计算列计数。
        /// </summary>
        private int Columns
        {
            get=> GetValue(ColumnsProperty);
            set=> SetValue(ColumnsProperty, value);
        }

        public static readonly StyledProperty<int> ColumnsProperty = 
            AvaloniaProperty.Register<AutomaticAssignGrid, int>("Columns", 0);

        /// <summary>
        /// 为第一行指定项目应开始的列。
        /// </summary>
        private int FirstColumn
        {
            get => GetValue(FirstColumnProperty); 
            set => SetValue(FirstColumnProperty, value);
        }

        public static readonly StyledProperty<int> FirstColumnProperty = 
            AvaloniaProperty.Register<AutomaticAssignGrid, int>("FirstColumn", 0);

        protected override Size MeasureOverride(Size availableSize)
        {
            UpdateRowsAndColumns();
            double num = 0.0;
            double num2 = 0.0;
            Size availableSize2 = new Size(availableSize.Width / (double)_columns, availableSize.Height / (double)_rows);
            foreach (IControl child in base.Children)
            {
                child.Measure(availableSize2);
                if (child.DesiredSize.Width > num)
                {
                    num = child.DesiredSize.Width;
                }

                if (child.DesiredSize.Height > num2)
                {
                    num2 = child.DesiredSize.Height;
                }
            }

            return new Size(num * (double)_columns, num2 * (double)_rows);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int num = FirstColumn;
            int num2 = 0;
            double num3 = finalSize.Width / (double)_columns;
            double num4 = finalSize.Height / (double)_rows;
            foreach (IControl child in base.Children)
            {
                if (child.IsVisible)
                {
                    child.Arrange(new Rect((double)num * num3, (double)num2 * num4, num3, num4));
                    num++;
                    if (num >= _columns)
                    {
                        num = 0;
                        num2++;
                    }
                }
            }

            return finalSize;
        }

        private void UpdateRowsAndColumns()
        {
            _rows = Rows;
            _columns = Columns;
            if (FirstColumn >= Columns)
            {
                FirstColumn = 0;
            }

            int num = FirstColumn;
            foreach (IControl child in base.Children)
            {
                if (child.IsVisible)
                {
                    num++;
                }
            }

            if (_rows == 0)
            {
                if (_columns == 0)
                {
                    _rows = (_columns = (int)Math.Ceiling(Math.Sqrt(num)));
                    return;
                }

                _rows = Math.DivRem(num, _columns, out var result);
                if (result != 0)
                {
                    _rows++;
                }
            }
            else if (_columns == 0)
            {
                _columns = Math.DivRem(num, _rows, out var result2);
                if (result2 != 0)
                {
                    _columns++;
                }
            }
        }
    }
}

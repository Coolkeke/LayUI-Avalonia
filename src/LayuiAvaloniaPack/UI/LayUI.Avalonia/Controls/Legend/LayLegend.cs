using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Controls.Templates;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Metadata;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 标题水平线
    /// </summary>
    public class LayLegend : TemplatedControl, ILayControl
    {
        public LayLegend()
        {
            ContentProperty.Changed.AddClassHandler<LayLegend>((x, e) => x.ContentChanged(e));
        } 
        /// <summary>
        /// 内容
        /// </summary>
        [Content]
        [DependsOn(nameof(ContentTemplate))]
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="Content"/> property.
        /// </summary>
        public static readonly StyledProperty<object> ContentProperty =
            AvaloniaProperty.Register<LayLegend, object>(nameof(Content), null);
        public IDataTemplate ContentTemplate
        {
            get { return GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="LineThickness"/> property.
        /// </summary>
        public static readonly StyledProperty<double> LineThicknessProperty =
            AvaloniaProperty.Register<LayLegend, double>(nameof(LineThickness), 1.0);

        /// <summary>
        /// 线粗细
        /// </summary>
        public double LineThickness
        {
            get { return GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="LineColor"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> LineColorProperty =
            AvaloniaProperty.Register<LayLegend, IBrush>(nameof(LineColor));

        /// <summary>
        /// 线颜色
        /// </summary>
        public IBrush LineColor
        {
            get { return GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="ContentTemplate"/> property.
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> ContentTemplateProperty =
            AvaloniaProperty.Register<LayLegend, IDataTemplate>(nameof(ContentTemplate));


        /// <summary>
        /// Defines the <see cref="HorizontalOffset"/> property.
        /// </summary>
        public static readonly StyledProperty<double> HorizontalOffsetProperty =
            AvaloniaProperty.Register<LayLegend, double>(nameof(HorizontalOffset), 10.0);


        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<LegendType> TypeProperty =
            AvaloniaProperty.Register<Control, LegendType>(nameof(Type));

        /// <summary>
        /// 类型
        /// </summary>
        public LegendType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// 水平位移距离
        /// </summary>
        public double HorizontalOffset
        {
            get { return GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        private void ContentChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue is ILogical oldChild)
            {
                LogicalChildren.Remove(oldChild);
            }

            if (e.NewValue is ILogical newChild)
            {
                LogicalChildren.Add(newChild);
            }
        }
    }
}

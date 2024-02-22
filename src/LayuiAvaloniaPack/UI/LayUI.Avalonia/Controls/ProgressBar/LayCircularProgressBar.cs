using Avalonia;
using Avalonia.Controls.Metadata;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    [TemplatePart("PART_Arc", typeof(Border))]
    public class LayCircularProgressBar : RangeBase, ILayControl
    {
        private Arc? PART_Arc;
        public LayCircularProgressBar()
        {
            ValueChanged -= LayCircularProgressBar_ValueChanged;
            ValueChanged += LayCircularProgressBar_ValueChanged;
        } 
        private void LayCircularProgressBar_ValueChanged(object? sender, RangeBaseValueChangedEventArgs e) => Refresh();
        /// <summary>
        /// Defines the <see cref="LineThickness"/> property.
        /// </summary>
        public static readonly StyledProperty<double> LineThicknessProperty =
            AvaloniaProperty.Register<LayCircularProgressBar, double>(nameof(LineThickness), 10.0);


        /// <summary>
        /// Defines the <see cref=" "/> property.
        /// </summary>
        public static readonly StyledProperty<double> PercentageProperty =
            AvaloniaProperty.Register<LayCircularProgressBar, double>(nameof(Percentage));

        /// <summary>
        /// Percentage
        /// </summary>
        public double Percentage
        {
            get { return GetValue(PercentageProperty); }
            private set { SetValue(PercentageProperty, value); }
        }

        /// <summary>
        /// 线条厚度
        /// </summary>
        public double LineThickness
        {
            get { return GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }
        private void Refresh()
        {
            if (PART_Arc == null) return;
            Percentage = (Value / Maximum * 100);
            PART_Arc.SweepAngle = Percentage * 360 / 100;
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            PART_Arc = e.NameScope.Find(nameof(PART_Arc)) as Arc;
            Refresh();
        }
    }
}

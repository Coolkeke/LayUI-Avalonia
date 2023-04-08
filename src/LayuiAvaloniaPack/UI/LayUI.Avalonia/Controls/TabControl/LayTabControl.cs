using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 选项卡容器
    /// </summary>
    public class LayTabControl : TabControl
    {

        /// <summary>
        /// Defines the <see cref="LineClolor"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> LineClolorProperty =
            AvaloniaProperty.Register<Control, IBrush>(nameof(LineClolor));

        /// <summary>
        /// 线颜色
        /// </summary>
        public IBrush LineClolor
        {
            get { return GetValue(LineClolorProperty); }
            set { SetValue(LineClolorProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="LineThickness"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> LineThicknessProperty =
            AvaloniaProperty.Register<Control, Thickness>(nameof(LineThickness));

        /// <summary>
        /// 线粗细
        /// </summary>
        public Thickness LineThickness
        {
            get { return GetValue(LineThicknessProperty); }
            internal set { SetValue(LineThicknessProperty, value); }
        }
        /// <summary>
        /// 重写子项控件（LayTabItem）
        /// </summary>
        /// <returns></returns>
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new LayTabItemContainerGenerator(this);
        }
    }
}

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
            AvaloniaProperty.Register<LayTabControl, IBrush>(nameof(LineClolor));

        /// <summary>
        /// 线颜色
        /// </summary>
        public IBrush LineClolor
        {
            get { return GetValue(LineClolorProperty); }
            set { SetValue(LineClolorProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Line"/> property.
        /// </summary>
        public static readonly StyledProperty<double> LineProperty =
            AvaloniaProperty.Register<LayTabControl, double>(nameof(Line));

        /// <summary>
        /// 线粗细
        /// </summary>
        public double Line
        {
            get { return GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
            AvaloniaProperty.Register<LayTabControl, IBrush>(nameof(HeaderBackground));

        /// <summary>
        /// 头部颜色
        /// </summary>
        public IBrush HeaderBackground
        {
            get { return GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
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

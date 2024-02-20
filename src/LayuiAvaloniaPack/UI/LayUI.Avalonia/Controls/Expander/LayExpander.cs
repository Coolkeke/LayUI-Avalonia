using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 折叠板
    /// </summary>
    public class LayExpander: Expander, ILayControl
    {
        /// <summary>
        /// Defines the <see cref="HeaderBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
            AvaloniaProperty.Register<LayExpander, IBrush>(nameof(HeaderBackground));
        public IBrush HeaderBackground
        {
            get { return GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderPadding"/> property.
        /// </summary>
        public static readonly StyledProperty<Thickness> HeaderPaddingProperty =
            AvaloniaProperty.Register<LayExpander, Thickness>(nameof(HeaderPadding), new Thickness(0));

        /// <summary>
        /// 头部内容内边距
        /// </summary>
        public Thickness HeaderPadding
        {
            get { return GetValue(HeaderPaddingProperty); }
            set { SetValue(HeaderPaddingProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="HeaderHorizontalAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<HorizontalAlignment> HeaderHorizontalAlignmentProperty =
            AvaloniaProperty.Register<LayExpander, HorizontalAlignment>(nameof(HeaderHorizontalAlignment), HorizontalAlignment.Left);

        /// <summary>
        /// 头部内容水平位置
        /// </summary>
        public HorizontalAlignment HeaderHorizontalAlignment
        {
            get { return GetValue(HeaderHorizontalAlignmentProperty); }
            set { SetValue(HeaderHorizontalAlignmentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderVerticalAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<VerticalAlignment> HeaderVerticalAlignmentProperty =
            AvaloniaProperty.Register<LayExpander, VerticalAlignment>(nameof(HeaderVerticalAlignment), VerticalAlignment.Center);

        /// <summary>
        /// 头部内容垂直位置
        /// </summary>
        public VerticalAlignment HeaderVerticalAlignment
        {
            get { return GetValue(HeaderVerticalAlignmentProperty); }
            set { SetValue(HeaderVerticalAlignmentProperty, value); }
        }
    }
}

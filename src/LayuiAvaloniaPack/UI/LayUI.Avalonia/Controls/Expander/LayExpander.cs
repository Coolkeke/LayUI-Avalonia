using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayExpander: Expander
    {


        /// <summary>
        /// Defines the <see cref="IconAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<ExpanderIconAlignment> IconAlignmentProperty =
            AvaloniaProperty.Register<LayExpander, ExpanderIconAlignment>(nameof(IconAlignment), ExpanderIconAlignment.Right);

        /// <summary>
        /// 图标水平位置
        /// </summary>
        public ExpanderIconAlignment IconAlignment
        {
            get { return GetValue(IconAlignmentProperty); }
            set { SetValue(IconAlignmentProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderForeground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderForegroundProperty =
            AvaloniaProperty.Register<LayExpander, IBrush>(nameof(HeaderForeground), null);

        /// <summary>
        /// 头部文字颜色
        /// </summary>
        public IBrush HeaderForeground
        {
            get { return GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="HeaderBackground"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> HeaderBackgroundProperty =
            AvaloniaProperty.Register<Control, IBrush>(nameof(HeaderBackground), null);

        /// <summary>
        /// 头部背景颜色
        /// </summary>
        public IBrush HeaderBackground
        {
            get { return GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
    }
}

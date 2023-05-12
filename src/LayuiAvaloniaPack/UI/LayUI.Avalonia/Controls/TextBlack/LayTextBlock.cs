using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayTextBlock : TextBlock
    {
        /// <summary>
        /// 是否被修剪
        /// </summary>
        public bool IsTextTrimmed
        {
            get { return GetValue(IsTextTrimmedProperty); }
            private set { SetValue(IsTextTrimmedProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsTextTrimmedProperty =
       AvaloniaProperty.Register<LayTextBlock, bool>(nameof(IsTextTrimmed), false);

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            IsTextTrimmed = GetIsTextTrimmed();
        }
        /// <summary>
        /// 判断文本本否被裁剪
        /// </summary>
        /// <returns></returns>
        private bool GetIsTextTrimmed()
        {
            if (TextLayout == null) return false;
            var layout = new TextLayout(
                text: Text ?? string.Empty,
                typeface: new Typeface(FontFamily, FontStyle, FontWeight),
                fontSize: FontSize,
                foreground: Foreground,
                textAlignment: TextAlignment,
                textWrapping: TextWrapping,
                textTrimming: TextTrimming.None,
                textDecorations: TextDecorations,
                maxWidth: TextLayout.Size.Width,
                maxHeight: TextLayout.Size.Height,
                maxLines: MaxLines,
                lineHeight: LineHeight);
            FormattedText formattedText = new FormattedText(
                Text,
                new Typeface(
                FontFamily,
                FontStyle,
                FontWeight),
                FontSize,
                TextAlignment,
                TextWrapping,
                layout.Size
                );
            if (TextWrapping == TextWrapping.NoWrap)
            {
                return formattedText.Bounds.Width > Bounds.Width;
            }
            return formattedText.Bounds.Height > Bounds.Height;
        }
    }
}

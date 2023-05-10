using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LaySpecialTextBlock: TemplatedControl
    {

		/// <summary>
		/// 内容
		/// </summary>
		public string Text
		{
			get { return GetValue(TextProperty); }
			set { SetValue(TextProperty, value);}
		}
		/// <summary>
		/// 定义<see cref="string"/>属性
		/// </summary>
		public static readonly StyledProperty<string> TextProperty =
	   AvaloniaProperty.Register<LaySpecialTextBlock, string>(nameof(Text), string.Empty); 
		/// <summary>
		/// 这是依赖属性名称
		/// </summary>
		public string Title
        {
			get { return GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value);}
		}
		/// <summary>
		/// 定义<see cref="string"/>属性
		/// </summary>
		public static readonly StyledProperty<string> TitleProperty =
	   AvaloniaProperty.Register<LaySpecialTextBlock, string>(nameof(Title), string.Empty);

	}
}

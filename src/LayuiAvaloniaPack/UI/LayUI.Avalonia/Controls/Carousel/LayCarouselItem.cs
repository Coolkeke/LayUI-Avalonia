using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
	/// <summary>
	/// 轮播子项
	/// </summary>
    public class LayCarouselItem : ContentControl
    { 
		/// <summary>
		/// 选中
		/// </summary>
		public bool IsSelected
        {
			get { return GetValue(IsSelectedProperty); }
			private set { SetValue(IsSelectedProperty, value); }
		}
		/// <summary>
		/// 定义<see cref="bool"/>属性
		/// </summary>
		public static readonly StyledProperty<bool> IsSelectedProperty =
	   AvaloniaProperty.Register<LayCarouselItem, bool>(nameof(IsSelected), false); 
    }
}

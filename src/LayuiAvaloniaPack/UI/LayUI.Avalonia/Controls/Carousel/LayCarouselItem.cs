using Avalonia.Controls;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
	/// 轮播子项
	/// </summary>
    public class LayCarouselItem : ContentControl, ILayControl
    {
        /// <summary>
        /// 选中
        /// </summary>
        public bool IsSelected
        {
            get { return GetValue(IsSelectedProperty); }
            internal set { SetValue(IsSelectedProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectedProperty =
       AvaloniaProperty.Register<LayCarouselItem, bool>(nameof(IsSelected), false);
    }
}

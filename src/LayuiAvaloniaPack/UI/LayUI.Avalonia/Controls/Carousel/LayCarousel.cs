using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayCarousel : TemplatedControl, ICarousel
    {
        /// <summary>
        /// 这是依赖属性名称
        /// </summary>
        public CarouselType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="CarouselType"/>属性
        /// </summary>
        public static readonly StyledProperty<CarouselType> TypeProperty =
       AvaloniaProperty.Register<LayCarousel, CarouselType>(nameof(Type), CarouselType.Gradient);


    }
}

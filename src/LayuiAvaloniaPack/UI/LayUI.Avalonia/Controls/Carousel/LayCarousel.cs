using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using System.Collections;
using System.Collections.Generic;
using Avalonia.Metadata;
using LayUI.Avalonia.Interface;
using System.Linq;
using System.Collections.Specialized;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Controls.Generators;
using System;
using Avalonia.Media;
using LayUI.Avalonia.Enums;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 轮播图基类
    /// </summary>
    public class LayCarousel : TemplatedControl, ICarousel
    {
        /// <summary>
        /// 创建或标识用于显示给定项的元素
        /// </summary>
        /// <returns>用于显示给定项的元素</returns>
        protected virtual AvaloniaObject GetContainerForItemOverride()
        {
            return new LayCarouselItem();
        }
        /// <summary>
        /// 确定指定项是否是（或者是否可以作为）其自己的容器
        /// </summary>
        /// <param name="item">要检查的项</param>
        /// <returns>如果该项是（或者可以作为）其自己的容器，则为 true；否则为 false</returns>
        protected virtual bool IsItemItsOwnContainerOverride(object item)
        {
            return item is LayCarouselItem;
        }
        private int selectedIndex = 0;
        /// <summary>
        /// 当前项
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetAndRaise(SelectedIndexProperty, ref selectedIndex, value); }
        }
        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly DirectProperty<LayCarousel, int> SelectedIndexProperty =
       AvaloniaProperty.RegisterDirect<LayCarousel, int>(nameof(SelectedIndex), o => o.SelectedIndex, (o, v) => o.SelectedIndex = v);
        private IEnumerable _items = new AvaloniaList<object>();
        /// <summary>
        /// 集合
        /// </summary>
        [Content]
        public IEnumerable Items
        {
            get { return _items; }
            set { SetAndRaise(ItemsProperty, ref _items, value); }
        }
        /// <summary>
        /// 定义<see cref="IEnumerable"/>属性
        /// </summary>
        public static readonly DirectProperty<LayCarousel, IEnumerable> ItemsProperty =
       AvaloniaProperty.RegisterDirect<LayCarousel, IEnumerable>(nameof(Items), o => o.Items, (o, v) => o.Items = v);


        /// <summary>
        /// 轮播图数量
        /// </summary>
        public int ItemsCount
        {
            get { return GetValue(ItemsCountProperty); }
            internal set { SetValue(ItemsCountProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly StyledProperty<int> ItemsCountProperty =
       AvaloniaProperty.Register<LayCarousel, int>(nameof(ItemsCount), 0);

        /// <summary>
        /// 轮播图模板
        /// </summary>
        public IDataTemplate ItemTemplate
        {
            get { return GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IDataTemplate"/>属性
        /// </summary>
        public static readonly StyledProperty<IDataTemplate> ItemTemplateProperty =
       AvaloniaProperty.Register<LayCarousel, IDataTemplate>(nameof(ItemTemplate), null);


        /// <summary>
        /// 指示器状态
        /// </summary>
        public bool IsIndicatorVisible
        {
            get { return GetValue(IsIndicatorVisibleProperty); }
            set { SetValue(IsIndicatorVisibleProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsIndicatorVisibleProperty =
       AvaloniaProperty.Register<LayCarousel, bool>(nameof(IsIndicatorVisible), true);


        /// <summary>
        /// 轮播箭头类型
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
       AvaloniaProperty.Register<LayCarousel, CarouselType>(nameof(Type), CarouselType.Always);



        public void Next()
        {
            if (SelectedIndex >= (ItemsCount - 1)) SelectedIndex = 0;
            else SelectedIndex++;
        }

        public void Previous()
        {
            if (SelectedIndex <= 0) SelectedIndex = ItemsCount - 1;
            else SelectedIndex--;
        }

    }
}

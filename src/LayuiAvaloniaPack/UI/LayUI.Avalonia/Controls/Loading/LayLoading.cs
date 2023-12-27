using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Media;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 加载动画
    /// </summary> 
    public class LayLoading:ContentControl, ILayControl
    {

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<LoadingType> TypeProperty =
            AvaloniaProperty.Register<LayLoading, LoadingType>(nameof(Type));

        /// <summary>
        /// 加载动画类型
        /// </summary>
        public LoadingType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Title"/> property.
        /// </summary>
        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<LayLoading, string>(nameof(Title), null);

        /// <summary>
        /// 加载信息提示
        /// </summary>
        public string Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        /// <summary>
        /// Defines the <see cref="IsActive"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsActiveProperty =
            AvaloniaProperty.Register<LayLoading, bool>(nameof(IsActive), false);

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive
        {
            get { return GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Color"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ColorProperty =
            AvaloniaProperty.Register<LayLoading, IBrush>(nameof(Color), null);

        /// <summary>
        /// 加载动画颜色
        /// </summary>
        public IBrush Color
        {
            get { return GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Size"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SizeProperty =
            AvaloniaProperty.Register<LayLoading, double>(nameof(Size), 50);

        /// <summary>
        /// 加载动画大小
        /// </summary>
        public double Size
        {
            get { return GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }
    }
}

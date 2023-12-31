using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 卡片
    /// </summary>
    public class LayCard : ContentControl, ILayControl
    {
        static LayCard()
        {
            ShadowColorProperty.Changed.AddClassHandler<LayCard>((s, e) => s.UpdateBoxShadow());
            BlurProperty.Changed.AddClassHandler<LayCard>((s, e) => s.UpdateBoxShadow());
            OffsetXProperty.Changed.AddClassHandler<LayCard>((s, e) => s.UpdateBoxShadow());
            OffsetYProperty.Changed.AddClassHandler<LayCard>((s, e) => s.UpdateBoxShadow());
        }
        /// <summary>
        /// 修改阴影效果
        /// </summary>
        private void UpdateBoxShadow()
        {
            if (ShadowColor is ISolidColorBrush brush)
            {
                BoxShadow = new BoxShadows(new BoxShadow()
                {
                    Color = brush.Color,
                    Blur = Blur,
                    OffsetY = OffsetY,
                    OffsetX = OffsetX,
                    Spread = Spread,
                    IsInset = IsInset
                });
            } 
        }
        /// <summary>
        /// Defines the <see cref="BoxShadow"/> property.
        /// </summary>
        internal static readonly StyledProperty<BoxShadows> BoxShadowProperty =
            AvaloniaProperty.Register<LayCard, BoxShadows>(nameof(BoxShadow));

        /// <summary>
        /// BoxShadow
        /// </summary>
        internal BoxShadows BoxShadow
        {
            get { return GetValue(BoxShadowProperty); }
            private set { SetValue(BoxShadowProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="IsInset"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsInsetProperty =
            AvaloniaProperty.Register<LayCard, bool>(nameof(IsInset));

        /// <summary>
        /// IsInset
        /// </summary>
        public bool IsInset
        {
            get { return GetValue(IsInsetProperty); }
            set { SetValue(IsInsetProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="ShadowColor"/> property.
        /// </summary>
        public static readonly StyledProperty<IBrush> ShadowColorProperty =
            AvaloniaProperty.Register<LayCard, IBrush>(nameof(ShadowColor));

        /// <summary>
        /// 阴影颜色
        /// </summary>
        public IBrush ShadowColor
        {
            get { return GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="Blur"/> property.
        /// </summary>
        public static readonly StyledProperty<double> BlurProperty =
            AvaloniaProperty.Register<LayCard, double>(nameof(Blur));

        /// <summary>
        /// 模糊程度
        /// </summary>
        public double Blur
        {
            get { return GetValue(BlurProperty); }
            set { SetValue(BlurProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="Spread"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SpreadProperty =
            AvaloniaProperty.Register<LayCard, double>(nameof(Spread));

        /// <summary>
        /// 扩散范围
        /// </summary>
        public double Spread
        {
            get { return GetValue(SpreadProperty); }
            set { SetValue(SpreadProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="OffsetX"/> property.
        /// </summary>
        public static readonly StyledProperty<double> OffsetXProperty =
            AvaloniaProperty.Register<LayCard, double>(nameof(OffsetX));

        /// <summary>
        /// 水平方向位移
        /// </summary>
        public double OffsetX
        {
            get { return GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }
        /// <summary>
        /// Defines the <see cref="OffsetY"/> property.
        /// </summary>
        public static readonly StyledProperty<double> OffsetYProperty =
            AvaloniaProperty.Register<LayCard, double>(nameof(OffsetY));

        /// <summary>
        /// 垂直方向位移
        /// </summary>
        public double OffsetY
        {
            get { return GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        /// <summary>
        /// 阴影类型
        /// </summary>
        public ShadowType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="ShadowType"/>属性
        /// </summary>
        public static readonly StyledProperty<ShadowType> TypeProperty =
       AvaloniaProperty.Register<LayCard, ShadowType>(nameof(Type), ShadowType.Always);

    }
}

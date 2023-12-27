using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 徽章
    /// </summary>
    public class LayBadge : ContentControl, ILayControl
    {
        static LayBadge()
        {
            MaxValueProperty.Changed.AddClassHandler<LayBadge>((o, e) => o.RefreshView());
            ValueProperty.Changed.AddClassHandler<LayBadge>((o, e) => o.RefreshView());
        }
        /// <summary>
        /// 刷新视图
        /// </summary>
        private void RefreshView()
        {
            if (string.IsNullOrEmpty(Value))
            {
                IsVisibleBadge=false;
                return;
            }
            if (double.TryParse(Value, out double value))
            {
                if(value<1) 
                {
                    IsVisibleBadge = false;
                    return;
                }
                if (value > MaxValue) Value = $"{MaxValue}+";
            }
            IsVisibleBadge = true;
        }

        /// <summary>
        /// 徽章效果显示
        /// </summary>
        public bool IsVisibleBadge
        {
            get { return GetValue(IsVisibleBadgeProperty); }
            internal set { SetValue(IsVisibleBadgeProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsVisibleBadgeProperty =
       AvaloniaProperty.Register<LayBadge, bool>(nameof(IsVisibleBadge), true);

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }

        }
        /// <summary>
        /// 定义<see cref="double"/>属性
        /// </summary>
        public static readonly StyledProperty<double> MaxValueProperty =
       AvaloniaProperty.Register<LayBadge, double>(nameof(MaxValue), 99.0);

        /// <summary>
        /// 徽章文字颜色
        /// </summary>
        public IBrush Color
        {
            get { return GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IBrush"/>属性
        /// </summary>
        public static readonly StyledProperty<IBrush> ColorProperty =
       AvaloniaProperty.Register<LayBadge, IBrush>(nameof(Color));


        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="string"/>属性
        /// </summary>
        public static readonly StyledProperty<string> ValueProperty =
       AvaloniaProperty.Register<LayBadge, string>(nameof(Value), string.Empty);

        /// <summary>
        /// 是否为小红点
        /// </summary>
        public bool IsDot
        {
            get { return GetValue(IsDotProperty); }
            set { SetValue(IsDotProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsDotProperty =
       AvaloniaProperty.Register<LayBadge, bool>(nameof(IsDot), false);
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e); 
            RefreshView();
        }

    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Media;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 按钮容器
    /// </summary>
    public class LayButtonGroup : StackPanel
    {
        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<ButtonType> TypeProperty =
            AvaloniaProperty.Register<Control, ButtonType>(nameof(Type));

        /// <summary>
        /// 类型
        /// </summary>
        public ButtonType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        /// <summary>
        /// 修改列表中按钮样式
        /// </summary>
        private void UpdateButtonsStyle()
        {
            var btns = Children.Cast<LayButton>().ToList();
            if (btns.Count() < 1) return;
            btns.ForEach(button =>
            {
                button.Type= Type;
                button.CornerRadius = new CornerRadius(0);
                button.BorderThickness = new Thickness(0);
            });
        }
        public override void Render(DrawingContext context)
        {
            base.Render(context);
            UpdateButtonsStyle();
        }
    }
}

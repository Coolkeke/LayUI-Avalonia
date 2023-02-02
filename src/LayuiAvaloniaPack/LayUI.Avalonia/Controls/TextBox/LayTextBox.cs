using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 输入框
    /// </summary>
    public class LayTextBox : TextBox
    {
        /// <summary>
        /// 输入类型
        /// </summary>
        public InputType InputType
        {
            get { return GetValue(InputTypeProperty); }
            set { SetValue(InputTypeProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="InputType"/>属性
        /// </summary>
        public static readonly StyledProperty<InputType> InputTypeProperty =
       AvaloniaProperty.Register<LayTextBox, InputType>(nameof(InputType), InputType.Default);
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            this.RemoveHandler(TextInputEvent, TxtBox_TextInput);
            this.AddHandler(TextInputEvent, TxtBox_TextInput, RoutingStrategies.Tunnel);
        }

        private void TxtBox_TextInput(object sender, TextInputEventArgs e)
        {
            switch (InputType)
            {
                case InputType.Phone:
                    e.Handled = IsPhone(e);
                    break;
                case InputType.Number:
                    e.Handled = new Regex(@"[^0-9|\-|\.]").IsMatch(e.Text);
                    break;
                default:
                    break;
            }
        }

        /// 检验手机号
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static bool IsPhone(TextInputEventArgs e)
        {
            if ((e.Source as TextBox).Text?.ToCharArray().Length > 10) return true;
            return Regex.IsMatch(e.Text, @"[^(1)\d{10}$]");
        }
    }
}

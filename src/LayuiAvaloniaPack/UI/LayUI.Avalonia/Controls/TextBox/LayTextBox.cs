using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.LogicalTree;
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
    public class LayTextBox : TextBox, ILayControl
    { 
        /// <summary>
        /// 是否聚焦
        /// </summary>
        public bool IsFocus
        {
            get { return GetValue(IsFocusProperty); }
            set { SetValue(IsFocusProperty, value); OnIsFocusedChanged(); }
        }
        /// <summary>
        /// 定义<see cref="bool"/>属性
        /// </summary>
        public static readonly StyledProperty<bool> IsFocusProperty =
       AvaloniaProperty.Register<LayTextBox, bool>(nameof(IsFocus));
        /// <summary>
        /// 调整设置文本聚焦问题
        /// </summary>
        private void OnIsFocusedChanged()
        {
            if (IsFocus)
            { 

                Focus();
                if (!string.IsNullOrEmpty(Text)) CaretIndex = Text.Length;
            }
        }
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
        protected override void OnTextInput(TextInputEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                   ?.Log("OnTextInput", "", ex);
            }
            base.OnTextInput(e); 
        }
        /// 检验手机号
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsPhone(TextInputEventArgs e)
        {
            try
            {
                if ((e.Source as TextBox).Text?.ToCharArray().Length > 10) return true;
                return Regex.IsMatch(e.Text, @"[^(1)\d{10}$]");
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                   ?.Log("IsPhone", "", ex);
            }
            return false;
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            IsFocus = false;
        } 
        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            OnIsFocusedChanged();
        }
    }
}

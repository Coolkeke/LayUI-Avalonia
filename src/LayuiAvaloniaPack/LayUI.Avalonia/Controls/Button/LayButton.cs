using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 按钮
    /// </summary>
    public class LayButton : Button
    {
        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<ButtonType> TypeProperty =
            AvaloniaProperty.Register<Control, ButtonType>(nameof(Type));

        /// <summary>
        /// 按钮类型
        /// </summary>
        public ButtonType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly StyledProperty<string> UriProperty =
            AvaloniaProperty.Register<Control, string>(nameof(Uri));
        /// <summary>
        /// 用于打开外部链接或者程序
        /// </summary>
        public string Uri
        {
            get { return GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        protected override void OnClick()
        {
            base.OnClick();
            if (!string.IsNullOrEmpty(Uri))
            {
                //点击调整网址
                Process.Start(new ProcessStartInfo
                {
                    FileName = Uri,
                    UseShellExecute = true
                });
            }

        }
    }
}

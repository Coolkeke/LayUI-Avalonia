using Avalonia;
using Avalonia.Controls;
using LayUI.Avalonia.Enums;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            AvaloniaProperty.Register<LayButton, ButtonType>(nameof(Type));

        /// <summary>
        /// 按钮类型
        /// </summary>
        public ButtonType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly StyledProperty<Uri> UriProperty =
            AvaloniaProperty.Register<LayButton, Uri>(nameof(Uri));
        /// <summary>
        /// 用于打开外部链接或者程序
        /// </summary>
        public Uri Uri
        {
            get { return GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        protected override void OnClick()
        {
            base.OnClick();
            if (Uri != null)
            {
                if (Uri.Scheme == Uri.UriSchemeHttp || Uri.Scheme == Uri.UriSchemeHttps)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        var proc = new Process { StartInfo = { UseShellExecute = true, FileName = Uri.ToString() } };
                        proc.Start();
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Process.Start("x-www-browser", Uri.ToString());
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        Process.Start("open", Uri.ToString());
                    }
                }
            }
        }
    }
}

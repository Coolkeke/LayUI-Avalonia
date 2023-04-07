using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Extends
{
    /// <summary>
    /// 窗体帮助类
    /// </summary>
    public class WindowHelper
    {
        static WindowHelper()
        {
            IsDialogProperty.Changed.Subscribe(OnIsDialogChanged);
        }

        private static void OnIsDialogChanged(AvaloniaPropertyChangedEventArgs<bool> obj)
        {
            if (obj.Sender is Window window)
            {
                if (GetIsDialog(window))
                {
                    window.Opened += Window_Opened;
                }
                else
                {
                    window.Opened -= Window_Opened;
                }
            }
        }

        private static void Window_Opened(object sender, EventArgs e)
        {
            if (sender is Window window)
            {
                var main = Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
                if (window.WindowStartupLocation == WindowStartupLocation.CenterOwner)
                {
                    var CenterX = main.MainWindow.Position.X + (main.MainWindow.Bounds.Width / 2);
                    var CenterY = main.MainWindow.Position.Y + (main.MainWindow.Bounds.Height / 2);
                    var X = CenterX - (window.Bounds.Width / 2);
                    var Y = CenterY - (window.Bounds.Height / 2);
                    Dispatcher.UIThread.Post(() =>
                    {
                        window.Position = new PixelPoint((int)X, (int)Y);
                    });
                }
            }
        }

        /// <summary>
        /// 标记窗体为对话框
        /// </summary>
        public static readonly AttachedProperty<bool> IsDialogProperty =
            AvaloniaProperty.RegisterAttached<Window, IAvaloniaObject, bool>(
            "IsDialog", false);


        /// <summary>
        /// Accessor for Attached property <see cref="IsDialogProperty"/>.
        /// </summary>
        public static void SetIsDialog(Window element, bool value)
        {
            element.SetValue(IsDialogProperty, value);
        }

        /// <summary>
        /// Accessor for Attached property <see cref="IsDialogProperty"/>.
        /// </summary>
        public static bool GetIsDialog(Window element)
        {
            return element.GetValue(IsDialogProperty);
        }

    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayWindow: Window
    {

        /// <summary>
        /// Defines the <see cref="IsDialog"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsDialogProperty =
            AvaloniaProperty.Register<LayWindow, bool>(nameof(IsDialog), false);

        /// <summary>
        /// 是否为对话框
        /// </summary>
        public bool IsDialog
        {
            get { return GetValue(IsDialogProperty); }
            set { SetValue(IsDialogProperty, value); }
        }
        public override void Show()
        {
            base.Show();
            //判断当前窗体是否为对话框模式
            if (IsDialog&&WindowStartupLocation == WindowStartupLocation.CenterOwner)
            {
                var main = Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
                var CenterX = main.MainWindow.Position.X + (main.MainWindow.Bounds.Width / 2);
                var CenterY = main.MainWindow.Position.Y + (main.MainWindow.Bounds.Height / 2);
                var X = CenterX - (Bounds.Width / 2);
                var Y = CenterY - (Bounds.Height / 2);
                Dispatcher.UIThread.Post(() =>
                {
                    Position = new PixelPoint((int)X, (int)Y);
                });
            }
        }
    }
}

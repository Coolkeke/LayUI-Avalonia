using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Controls
{
    public class LayWindow : Window
    {

        /// <summary>
        /// Defines the <see cref="IsOwner"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsOwnerProperty =
            AvaloniaProperty.Register<LayWindow, bool>(nameof(IsOwner));

        /// <summary>
        /// 是否激活窗体拥有者
        /// </summary>
        public bool IsOwner
        {
            get { return GetValue(IsOwnerProperty); }
            set { SetValue(IsOwnerProperty, value); }
        }
        public override async void Show()
        {
            if (Design.IsDesignMode)
            {
                base.Show();
            }
            else
            {
                if (IsOwner)
                {
                    var owner = GetOwner();
                    base.Show(owner);
                }
                else base.Show();
            }
            await Task.Delay(1);
            SetWindowStartupLocationWorkaround();

        }
        /// <summary>
        /// 获取或设置窗口的所有者。
        /// </summary>
        /// <returns></returns>
        private Window GetOwner()
        {
            if (!IsOwner) return null;
            Window owner = null;
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime app)
            {
                owner = app.MainWindow;
            }
            return owner;
        }
        /// <summary>
        /// 解决Linux以及MacOS定位问题
        /// </summary>
        private void SetWindowStartupLocationWorkaround()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            double scale = PlatformImpl?.DesktopScaling ?? 1.0;
            IWindowBaseImpl powner = Owner?.PlatformImpl;
            if (powner != null) scale = powner.DesktopScaling;
            PixelRect rect = new PixelRect(PixelPoint.Origin,
               PixelSize.FromSize(ClientSize, scale));
            if (WindowStartupLocation == WindowStartupLocation.CenterScreen)
            {
                Screen screen = Screens.ScreenFromPoint(powner?.Position ?? Position);
                if (screen == null) return;
                Position = screen.WorkingArea.CenterRect(rect).Position;
            }
            else
            {
                if (powner == null || WindowStartupLocation != WindowStartupLocation.CenterOwner) return;
                Position = new PixelRect(powner.Position,
                   PixelSize.FromSize(powner.ClientSize, scale)).CenterRect(rect).Position;
            }
        }
    }
}

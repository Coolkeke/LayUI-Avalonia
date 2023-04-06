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
    public class LayWindow: Window
    {
        public override async void Show()
        {
            base.Show();
            await Task.Delay(1);
            SetWindowStartupLocationWorkaround();
        }
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

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Media;
using Layui.Tools.Logs;
using Prism.Ioc;
using System;

namespace LayuiApp
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                //初始化日志配置信息
                log4net.Config.XmlConfigurator.Configure();
                BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, nameof(Program)).GetValueOrDefault().Log(nameof(Main), "", ex);
            }
            finally
            {
                AvaloniaLocator.Current.GetService<IContainerProvider>()?.Resolve<ILayLogger>()?.Dispose();
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .With(new X11PlatformOptions { EnableMultiTouch = true, UseDBusMenu = true, })
                .With(new Win32PlatformOptions { EnableMultitouch = true, AllowEglInitialization = true, })
                .UseSkia()
                //日志过滤
                .LogToTrace(LogEventLevel.Error, LogArea.Property, LogArea.Layout);
        }
    }
}

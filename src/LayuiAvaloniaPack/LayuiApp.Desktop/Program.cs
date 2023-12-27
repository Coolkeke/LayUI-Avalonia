using Avalonia;
using Avalonia.Logging;
using Layui.Core;
using Layui.Main;
using Prism.Ioc;
using System;
using System.Text;

namespace LayuiApp.Desktop
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
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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
                ContainerLocator.Current?.Resolve<ILayLogger>()?.Dispose();
            } 
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect() 
                .UseSkia()
                //日志过滤
                .LogToTrace(LogEventLevel.Error, LogArea.Property, LogArea.Layout);
        }
    }
}
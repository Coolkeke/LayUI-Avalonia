using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Media;
using System;
using System.IO;

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
                BuildAvaloniaApp()
               .With(new FontManagerOptions
               {
                   FontFallbacks = new[]
                   {
                        new FontFallback
                        {
                            FontFamily = new FontFamily("avares://LayuiApp/Assets/Fonts/微软雅黑.ttf#Microsoft YaHei")
                        }
                   }
               })
               .StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex)
            {
                // 在此处理异常，例如新增到日志文件
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // 这段代码块是可选项
                // 使用 finally 代码块释放资源

            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .With(new X11PlatformOptions
                {
                    EnableMultiTouch = true,
                    UseDBusMenu = true,
                    EnableIme = true
                })
                .With(new Win32PlatformOptions
                {
                    EnableMultitouch = true
                })
                .LogToTrace();
        }
    }
}

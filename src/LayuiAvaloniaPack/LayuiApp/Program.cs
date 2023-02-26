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
                            FontFamily = new FontFamily("avares://LayuiApp/Assets/Fonts/΢���ź�.ttf#Microsoft YaHei")
                        }
                   }
               })
               .StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex)
            {
                // �ڴ˴����쳣��������������־�ļ�
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // ��δ�����ǿ�ѡ��
                // ʹ�� finally ������ͷ���Դ

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

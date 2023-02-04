using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using LayUI.Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Extends
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageHelper
    {
        static ImageHelper()
        {
            SourceProperty.Changed.Subscribe(OnSourceChanged);
        }
        private static async void OnSourceChanged(AvaloniaPropertyChangedEventArgs obj)
        {
            if (obj.Sender is Image image)
            {
                Uri uri = null;
                if (obj.NewValue is string rawUri)
                {
                    if (rawUri.StartsWith("assembly://"))
                    {
                        SetIsLoaded(image, true);
                        var appDirectory = System.IO.Directory.GetCurrentDirectory();
                        uri = new Uri($"{appDirectory}/{rawUri.Replace("assembly://", "")}");
                        image.Source = new Bitmap(uri.LocalPath);
                        SetIsLoaded(image, false);
                        return;
                    }
                    else if (rawUri.Trim().StartsWith("http://") || rawUri.Trim().StartsWith("https://"))
                    {
                        await Task.Run(async () =>
                        {
                            using (WebClient client = new WebClient())
                            {
                                await Dispatcher.UIThread.InvokeAsync(() => SetIsLoaded(image, true));
                                var bytes = await client.DownloadDataTaskAsync(new Uri(rawUri));
                                Stream stream = new MemoryStream(bytes);
                                await Dispatcher.UIThread.InvokeAsync(() =>
                                {
                                    image.Source = new Bitmap(stream);
                                    SetIsLoaded(image, false);
                                });
                            }
                        });
                    }
                    else if (rawUri.StartsWith("avares://"))
                    {
                        SetIsLoaded(image, true);
                        uri = new Uri(rawUri);
                        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                        var asset = assets.Open(uri);
                        image.Source = new Bitmap(asset);
                        SetIsLoaded(image, false);
                    }
                    else
                    {
                        SetIsLoaded(image, true);
                        image.Source = new Bitmap(rawUri);
                        SetIsLoaded(image, true);
                    }
                }
                if (obj.NewValue is IImage)
                {
                    image.Source = obj.NewValue as IImage;
                }
            }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public static readonly AttachedProperty<object> SourceProperty = AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, object>(
            "Source", null);


        /// <summary>
        /// Accessor for Attached property <see cref="SourceProperty"/>.
        /// </summary>
        public static void SetSource(AvaloniaObject element, object value)
        {
            element.SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Accessor for Attached property <see cref="SourceProperty"/>.
        /// </summary>
        public static object GetSource(AvaloniaObject element)
        {
            return element.GetValue(SourceProperty);
        }

        /// <summary>
        /// 是否在加载
        /// </summary>
        public static readonly AttachedProperty<bool> IsLoadedProperty =
            AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, bool>(
            "IsLoaded", false,false, BindingMode.TwoWay);
        /// <summary>
        /// 设置图片加载状态
        /// </summary>
        public static void SetIsLoaded(AvaloniaObject element, bool value)
        {
            element.SetValue(IsLoadedProperty, value);
        }

        /// <summary>
        /// 获取加载状态
        /// </summary>
        public static bool GetIsLoaded(AvaloniaObject element)
        {
            return element.GetValue(IsLoadedProperty);
        }
    }
}

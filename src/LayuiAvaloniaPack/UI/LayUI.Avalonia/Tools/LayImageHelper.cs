﻿using Avalonia;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Tools
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class LayImageHelper
    { 
        /// <summary>
        /// 异步获取图片
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns></returns>
        public static async Task<IImage> CreateImageAsync(string path)
        {
            if (string.IsNullOrEmpty(path)) return new Bitmap(string.Empty);
            if (path.StartsWith("assembly://"))
            {
                var appDirectory = System.IO.Directory.GetCurrentDirectory();
                var uri = new Uri($"{appDirectory}/{path.Replace("assembly://", "")}");

                return new Bitmap(uri.LocalPath);
            }
            else if (path.Trim().StartsWith("http://") || path.Trim().StartsWith("https://"))
            {
                using (HttpClient client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync(new Uri(path));
                    Stream stream = new MemoryStream(bytes);
                    return new Bitmap(stream);
                }
            }
            else if (path.StartsWith("avares://")) return new Bitmap(AssetLoader.Open(new Uri(path)));
            else return new Bitmap(string.Empty);
        }
    }
}

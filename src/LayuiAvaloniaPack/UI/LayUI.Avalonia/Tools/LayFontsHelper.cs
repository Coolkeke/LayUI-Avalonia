using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LayUI.Avalonia
{
    public class LayFontsHelper
    {
        private static string uri = "<ResourceDictionary xmlns=\"https://github.com/avaloniaui\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\r\n    <FontFamily x:Key=\"Icon\">avares://LayUI.Avalonia/Fonts#iconfont</FontFamily>\r\n</ResourceDictionary>";
        private static ResourceDictionary _Fonts;
        private static ResourceDictionary Fonts
        {
            get
            {
                if (_Fonts == null)
                {
                    try
                    {
                        _Fonts = (ResourceDictionary)AvaloniaRuntimeXamlLoader.Load(uri);
                    }
                    catch (Exception ex)
                    {
                        Logger.TryGet(LogEventLevel.Error, nameof(LayFontsHelper)).GetValueOrDefault().Log(nameof(Fonts), "", ex);
                    }
                    ///Fonts = ;
                }
                return _Fonts;
            }
        }
        /// <summary>
        /// 读取字体图标中的Unicode编码
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetIconFontUnicode(string fontName)
        {
            var items = new Dictionary<string, string>();
            var font = Fonts[fontName] as FontFamily;
            var glyphTypeface = new Typeface(font);
            return items;
        }
        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StringToUnicode(string value)
        {
            StringBuilder unicode = new StringBuilder();
            string[] strlist = value.Replace("&#", "").Replace(";", "").Split('x');
            for (int i = 1; i < strlist.Length; i++)
            {
                unicode.Append((char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber));
            }
            return unicode.ToString();
        } 
    }
}

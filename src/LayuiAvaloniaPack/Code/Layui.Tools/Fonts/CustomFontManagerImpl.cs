using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Fonts
{
    public class CustomFontManagerImpl : IFontManagerImpl
    {
        private readonly Typeface[] _customTypefaces;
        private readonly string _defaultFamilyName;

        private readonly Typeface _defaultTypeface =
            new Typeface("resm:LayuiApp.Desktop.Assets.Fonts.微软雅黑.ttf?assembly=LayuiApp.Desktop#Microsoft YaHei"); 
        private readonly Typeface _emojiTypeface =
            new Typeface("resm:LayuiApp.Desktop.Assets.Fonts.iconfont.ttf?assembly=LayuiApp.Desktop#iconfont");

        public CustomFontManagerImpl()
        {
            _customTypefaces = new[] { _emojiTypeface, _defaultTypeface };
            _defaultFamilyName = _defaultTypeface.FontFamily.FamilyNames.PrimaryFamilyName;
        }

        public string GetDefaultFontFamilyName()
        {
            return _defaultFamilyName;
        }

        public IEnumerable<string> GetInstalledFontFamilyNames(bool checkForUpdates = false)
        {
            return _customTypefaces.Select(x => x.FontFamily.Name);
        }

        private readonly string[] _bcp47 = { CultureInfo.CurrentCulture.ThreeLetterISOLanguageName, CultureInfo.CurrentCulture.TwoLetterISOLanguageName };

        public bool TryMatchCharacter(int codepoint, FontStyle fontStyle, FontWeight fontWeight, FontFamily fontFamily,
            CultureInfo culture, out Typeface typeface)
        {
            foreach (var customTypeface in _customTypefaces)
            {
                if (customTypeface.GlyphTypeface.GetGlyph((uint)codepoint) == 0)
                {
                    continue;
                }

                typeface = new Typeface(customTypeface.FontFamily, fontStyle, fontWeight);

                return true;
            }

            var fallback = SKFontManager.Default.MatchCharacter(fontFamily?.Name, (SKFontStyleWeight)fontWeight,
                SKFontStyleWidth.Normal, (SKFontStyleSlant)fontStyle, _bcp47, codepoint);

            typeface = new Typeface(fallback?.FamilyName ?? _defaultFamilyName, fontStyle, fontWeight);

            return true;
        }

        public IGlyphTypefaceImpl CreateGlyphTypeface(Typeface typeface)
        {
            SKTypeface skTypeface;

            switch (typeface.FontFamily.Name)
            {
                case "Icon":
                    {
                        var typefaceCollection = SKTypefaceCollectionCache.GetOrAddTypefaceCollection(_emojiTypeface.FontFamily);
                        skTypeface = typefaceCollection.Get(typeface);
                        break;
                    }
                case FontFamily.DefaultFontFamilyName: 
                default:
                    {
                        skTypeface = SKTypeface.FromFamilyName(typeface.FontFamily.Name,
                            (SKFontStyleWeight)typeface.Weight, SKFontStyleWidth.Normal, (SKFontStyleSlant)typeface.Style);
                        break;
                    }
            }

            return new GlyphTypefaceImpl(skTypeface);
        }
    }
}

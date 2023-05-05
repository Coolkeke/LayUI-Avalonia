using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LayUI.Avalonia.Manager
{
    /// <summary>
    /// 字体管理器
    /// <para>解决Avalonia在目标系统上显示无对应字体问题</para>
    /// </summary>
    public class LayCustomFontManager : IFontManagerImpl
    {
        private Dictionary<string, Typeface> _fontFamilies;
        /// <summary>
        /// 系统字体集合
        /// </summary>
        public Dictionary<string, Typeface> FontFamilies
        {
            get
            {
                return _fontFamilies ?? (_fontFamilies = new Dictionary<string, Typeface>());
            }
        }

        private readonly string _defaultFamilyName;

        private readonly Typeface _defaultTypeface =
            new Typeface("resm:LayUI.Avalonia.Fonts.SourceHanSansCN-Regular.ttf?assembly=LayUI.Avalonia#Source Han Sans CN");

        public LayCustomFontManager()
        {
            Init();
            _defaultFamilyName = _defaultTypeface.FontFamily.FamilyNames.PrimaryFamilyName;
        }
        /// <summary>
        /// 自定义字体集合
        /// </summary>
        public virtual void InitFontFamiles()
        {
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            try
            {
                AddFontFamily("SourceHanSans", _defaultTypeface);
                InitFontFamiles();
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                    ?.Log("Init", "", ex);
            }
        }
        /// <summary>
        /// 添加字体
        /// </summary>
        public void AddFontFamily(string fontFamilyName, Typeface typeface)
        {
            try
            {
                if (string.IsNullOrEmpty(fontFamilyName)) return;
                if (FontFamilies.ContainsKey(fontFamilyName)) return;
                FontFamilies.Add(fontFamilyName, typeface);
            }
            catch (Exception ex)
            {

                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                    ?.Log("AddFontFamily", "", ex);
            }
        }
        public string GetDefaultFontFamilyName()
        {
            return _defaultFamilyName;
        }

        public IEnumerable<string> GetInstalledFontFamilyNames(bool checkForUpdates = false)
        {
            return FontFamilies.Select(x => x.Value.FontFamily.Name);
        }

        private readonly string[] _bcp47 = { CultureInfo.CurrentCulture.ThreeLetterISOLanguageName, CultureInfo.CurrentCulture.TwoLetterISOLanguageName };

        public bool TryMatchCharacter(int codepoint, FontStyle fontStyle, FontWeight fontWeight, FontFamily fontFamily,
            CultureInfo culture, out Typeface typeface)
        {
            foreach (var customTypeface in FontFamilies)
            {
                if (customTypeface.Value.GlyphTypeface.GetGlyph((uint)codepoint) == 0)
                {
                    continue;
                }

                typeface = new Typeface(customTypeface.Value.FontFamily, fontStyle, fontWeight);
                return true;
            }

            var fallback = SKFontManager.Default.MatchCharacter(fontFamily?.Name, (SKFontStyleWeight)fontWeight,
                SKFontStyleWidth.Normal, (SKFontStyleSlant)fontStyle, _bcp47, codepoint);

            typeface = new Typeface(fallback?.FamilyName ?? _defaultFamilyName, fontStyle, fontWeight);

            return true;
        }
        /// <summary>
        /// 获取自定义字体
        /// </summary>
        /// <param name="fontFamilyName">字体名称</param>
        /// <returns></returns>
        private SKTypeface GetSKTypeface(string fontFamilyName)
        {
            foreach (var customTypeface in FontFamilies)
            {
                if (customTypeface.Key == fontFamilyName)
                {
                    var typefaceCollection = SKTypefaceCollectionCache.GetOrAddTypefaceCollection(customTypeface.Value.FontFamily);
                    SKTypeface skTypeface = typefaceCollection.Get(customTypeface.Value);
                    return skTypeface;
                }
            }
            return null;
        }
        public IGlyphTypefaceImpl CreateGlyphTypeface(Typeface typeface)
        {
            SKTypeface skTypeface = null;
            try
            {
                skTypeface = GetSKTypeface(typeface.FontFamily.Name);
                if (skTypeface == null)
                {
                    switch (typeface.FontFamily.Name)
                    {
                        case FontFamily.DefaultFontFamilyName:
                            {
                                var typefaceCollection = SKTypefaceCollectionCache.GetOrAddTypefaceCollection(_defaultTypeface.FontFamily);
                                skTypeface = typefaceCollection.Get(typeface);
                                break;
                            }
                        default:
                            {
                                skTypeface = SKTypeface.FromFamilyName(typeface.FontFamily.Name,
                                    (SKFontStyleWeight)typeface.Weight, SKFontStyleWidth.Normal, (SKFontStyleSlant)typeface.Style);
                                break;
                            }
                    }
                }

                return new GlyphTypefaceImpl(skTypeface);
            }
            catch
            {
                var typefaceCollection = SKTypefaceCollectionCache.GetOrAddTypefaceCollection(_defaultTypeface.FontFamily);
                skTypeface = typefaceCollection.Get(typeface);
            }
            return new GlyphTypefaceImpl(skTypeface);
        }
    }
}

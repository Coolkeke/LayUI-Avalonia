using Avalonia.Media; 
using LayUI.Avalonia.Manager; 
namespace Layui.Tools.Fonts
{
    /// <summary>
    /// 字体管理器
    /// </summary>
    public class CustomFontManagerImpl : LayCustomFontManager
    {
        private readonly Typeface _emojiTypeface =
            new Typeface("resm:LayuiApp.Desktop.Assets.Fonts.iconfont.ttf?assembly=LayuiApp#iconfont");
        public override void InitFontFamiles()
        {
            AddFontFamily("Icon", _emojiTypeface);
        }

    }
}

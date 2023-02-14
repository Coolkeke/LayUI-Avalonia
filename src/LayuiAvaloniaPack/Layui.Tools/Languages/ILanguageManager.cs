using Avalonia.Controls;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Languages
{
    public interface ILanguageManager
    {
        /// <summary>
        /// 获取资源字典
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        ResourceDictionary GetResourceDictionary(string uri);
    }
}

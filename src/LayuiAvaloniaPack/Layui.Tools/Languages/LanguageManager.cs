
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Utilities;
using System.Xml;

namespace Layui.Tools.Languages
{
    public class LanguageManager : ILanguageManager
    {
        public ResourceDictionary GetResourceDictionary(string uri)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create(uri);
            xmlDoc.Load(reader);
            reader.Close();
            ResourceDictionary res = AvaloniaRuntimeXamlLoader.Load(xmlDoc.InnerXml) as ResourceDictionary??new ResourceDictionary();
            return res;
        }
    }
}

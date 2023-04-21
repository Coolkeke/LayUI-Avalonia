using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Prism.Mvvm;
using System.ComponentModel;
using System.Xml;

namespace Layui.Tools.Languages
{
    public class LanguageManager : BindableBase
    {
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";
        public static LanguageManager Instance { get; set; } = new LanguageManager();
        public LanguageManager()
        {
        }
        private ResourceDictionary _LanguageDictionary;
        public ResourceDictionary LanguageDictionary
        {
            get { return _LanguageDictionary; }
            set { SetProperty(ref _LanguageDictionary, value); }
        }


        public bool LoadLanguage(string uri)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create($"{Directory.GetCurrentDirectory()}/{uri}");
            xmlDoc.Load(reader);
            reader.Close();
            var languageXaml = AvaloniaRuntimeXamlLoader.Load(xmlDoc.InnerXml);
            if (languageXaml is ResourceDictionary xaml)
            {
                LanguageDictionary = xaml;
                Invalidate();
                return true;
            }
            return false;
        }
        public string this[string key]
        {
            get
            {
                try
                {
                    if (LanguageDictionary == null) return key; 
                    return LanguageDictionary[key].ToString();
                }
                catch
                {
                    return key;
                }
            }
        }
        public void Invalidate()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(IndexerName));
            OnPropertyChanged(new PropertyChangedEventArgs(IndexerArrayName));
        }
    }
}

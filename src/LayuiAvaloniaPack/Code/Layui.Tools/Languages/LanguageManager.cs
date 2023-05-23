using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DryIoc;
using Prism.Mvvm;
using System.ComponentModel;
using System.Xml;
using Tmds.DBus;

namespace Layui.Tools.Languages
{
    public class LanguageManager : BindableBase
    {
        /// <summary>
        /// 用于发送语言修改通知
        /// </summary>
        public event Action UpdateLanguageChanged;
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";
        public static LanguageManager Instance { get; } = new LanguageManager();
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
        public object this[string key]
        {
            get
            {
                if (LanguageDictionary == null) return key;
                //检测当前Key在资源字典里面是存在
                var flag = LanguageDictionary.ContainsKey(key);
                if (flag) return LanguageDictionary[key].ToString();
                else return key;
            }
        }
        public void Invalidate()
        {
            UpdateLanguageChanged?.Invoke();
            OnPropertyChanged(new PropertyChangedEventArgs(IndexerName));
            OnPropertyChanged(new PropertyChangedEventArgs(IndexerArrayName));
        }
    }
}

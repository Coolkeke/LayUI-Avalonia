using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Extensions
{
    public class LanguageExtension : MarkupExtension
    {
        private class LanguageConverter : IMultiValueConverter
        {
            public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
            {
                if (values == null || values.Count != 2 || values[0] is null || !(values[1] is ResourceDictionary)) return string.Empty;
                if (values[0].Equals(AvaloniaProperty.UnsetValue)) return parameter;
                var key = values[0]?.ToString();
                var lanugages = (ResourceDictionary)values[1];
                var value = lanugages.ContainsKey(key) ? lanugages[key] : key;
                return value;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        private static Action? action;
        private object _Key;
        public LanguageExtension() { }
        public LanguageExtension(object key) : this()
        {
            _Key = key;
        }/// <summary>
         /// 绑定源
         /// </summary>
        public object Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        /// <summary>
        /// 临时翻译数据源
        /// </summary>
        private static ResourceDictionary? localSource;

        private static ResourceDictionary? source;
        /// <summary>
        /// 翻译数据内存源数据
        /// </summary>
        private static ResourceDictionary Source
        {
            get
            {
                if (source == null) source = new ResourceDictionary();
                return source;
            }
            set
            {
                if (source != value)
                {
                    source = value;
                    Refresh();
                }
            }
        }
        /// <summary>
        /// 判断初始化加载
        /// </summary>
        private static bool IsLoadedLanguage { get; set; }

        /// <summary>
        /// 获取内存中已加载的翻译结果
        /// </summary>
        /// <param name="key">唯一标识</param>
        /// <returns></returns>
        public static object? GetValue(string key)
        {
            if (Source != null && Source.ContainsKey(key)) return Source[key];
            return key;
        }
        /// <summary>
        /// 根据系统资源名称加载多语言
        /// </summary>
        /// <param name="key"></param>
        public static void LoadResourceKey(string key)
        {
            var languageDatas = Application.Current?.FindResource(key);
            if (languageDatas is ResourceDictionary dictionary) LoadDictionary(dictionary);
            else LoadDictionary(new ResourceDictionary());
        }
        /// <summary>
        /// 根据路径加载多语言
        /// </summary>
        /// <param name="path">翻译文件路径</param>
        public static void LoadPath(string path)
        {
            ResourceDictionary? languageDictionary = null;
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    languageDictionary = (ResourceDictionary)AvaloniaRuntimeXamlLoader.Load(fs);
                }
            }
            else
            {
                languageDictionary = new ResourceDictionary();
                languageDictionary.MergedDictionaries.Add(new ResourceInclude(new Uri(path)));
            }
            LoadDictionary(languageDictionary);
        }

        /// <summary>
        /// 根据指定的资源字典加载多语言
        /// </summary>
        /// <param name="languageDictionary"></param>
        public static void LoadDictionary(ResourceDictionary languageDictionary)
        {
            Source = languageDictionary;
        }
        /// <summary>
        /// 刷新页面可视化语言
        /// </summary>
        public static void Refresh()
        {
            lock (Source)
            {
                if (action == null)
                    AddApplicationResourcesLanguage();
                action?.Invoke();
            }
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget)) return this;
            if (provideValueTarget.TargetObject.GetType().FullName == "System.Windows.SharedDp") return this;
            if (!(provideValueTarget.TargetObject is AvaloniaObject targetObject)) return this;
            if (!(provideValueTarget.TargetProperty is AvaloniaProperty targetProperty)) return this;
            try
            {
                Action? LanguageEvent = null;
                LanguageEvent = async () =>
                {
                   await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        targetObject.Bind(targetProperty, CreateBinding(Key));
                    });
                };
                EventHandler<RoutedEventArgs>? loaded = null;
                EventHandler<RoutedEventArgs>? unLoaded = null;
                loaded = (o, e) =>
                {
                    action += LanguageEvent;
                    if (o is Control control)
                    {
                        control.Loaded -= loaded;
                        control.Loaded += loaded;
                    }
                };
                unLoaded = (o, e) =>
                {
                    action -= LanguageEvent;
                    if (o is Control control)
                    {
                        control.Unloaded -= unLoaded;
                        control.Unloaded += unLoaded;
                    }
                };
                if (targetObject is Control element)
                {
                    element.Loaded += loaded;
                    element.Unloaded += unLoaded;
                    EventHandler? elementDataContextChanged = null;
                    elementDataContextChanged +=  (o, e) =>
                    {
                        element.DataContextChanged -= elementDataContextChanged;
                        element.DataContextChanged += elementDataContextChanged;
                        element.Bind(targetProperty, CreateBinding(Key));
                    };
                    element.DataContextChanged += elementDataContextChanged;
                }
                if (!IsLoadedLanguage) IsLoadedLanguage = AddApplicationResourcesLanguage();
                return CreateBinding(Key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 给系统资源新增多语言文件
        /// </summary>
        private static bool AddApplicationResourcesLanguage()
        {
            try
            {
                if (localSource == null) localSource = source;
                var lang = Application.Current?.Resources?.MergedDictionaries?.Where(o => o == localSource)?.FirstOrDefault();
                if (lang != null) Application.Current?.Resources?.MergedDictionaries?.Remove(lang);
                Application.Current?.Resources?.MergedDictionaries?.Add(Source);
                localSource = Source;
                return true;
            }
            catch { }
            return false;
        }
        /// <summary>
        /// 创建多语言绑定代码
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="property"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private MultiBinding CreateBinding(object key)
        {
            MultiBinding binding = new MultiBinding();
            if (key is Binding sorueBinding)
            {
                binding.ConverterParameter = sorueBinding.Path;
                binding.Bindings.Add(sorueBinding);
            }
            else
            {
                binding.ConverterParameter = key;
                binding.Bindings.Add(new Binding()
                {
                    Source = key,
                    Mode = BindingMode.OneWay
                });
            }
            binding.Bindings.Add(new Binding()
            {
                Source = Source,
                Mode = BindingMode.OneWay
            });
            binding.Converter = new LanguageConverter();
            return binding;
        }
    }
}

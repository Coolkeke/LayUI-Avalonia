using Avalonia;
using Avalonia.Data; 
using System.Reactive.Subjects;
using Avalonia.Threading; 

namespace Layui.Tools.Languages
{

    /// <summary>
    /// 绑定翻译
    /// </summary>
    public class LocalizeBindingExtension : AvaloniaObject
    {
        static LocalizeBindingExtension()
        {
            PathProperty.Changed.AddClassHandler<LocalizeBindingExtension>((o, e) => o.UpdateLanguage());
        }
        /// <summary>
        /// 修改翻译
        /// </summary>
        private void UpdateLanguage()
        {
            Dispatcher.UIThread.InvokeAsync(() => Subject?.OnNext(LanguageManager.Instance[Path]));
        }

        public LocalizeBindingExtension(IBinding binding)
        {
            this.Bind(PathProperty, binding);
        }
         
        internal static readonly StyledProperty<string> PathProperty =
            AvaloniaProperty.Register<LocalizeBindingExtension, string>(nameof(Path), string.Empty); 
        /// <summary>
        /// 翻译Key
        /// </summary>
        internal string Path
        {
            get { return GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        private BehaviorSubject<object>? Subject { get; set; }
        public object ProvideValue()
        {
            Subject = new BehaviorSubject<object>(string.Empty);
            LanguageManager.Instance.UpdateLanguageChanged -= Instance_UpdateLanguageChanged;
            LanguageManager.Instance.UpdateLanguageChanged += Instance_UpdateLanguageChanged;
            var binding = new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"Subject^",
                Source = this,
            };
            return binding;
        }

        private void Instance_UpdateLanguageChanged()
        {
            Dispatcher.UIThread.InvokeAsync(() => Subject?.OnNext(LanguageManager.Instance[Path]));
        }
    }
}

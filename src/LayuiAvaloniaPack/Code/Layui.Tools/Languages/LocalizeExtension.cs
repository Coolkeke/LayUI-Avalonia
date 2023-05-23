using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Data.Core;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Languages
{
    /// <summary>
    /// 本地翻译
    /// </summary>
    public class LocalizeExtension : MarkupExtension
    { 
        public LocalizeExtension(string key)  
        {
            this.Key = key;
        }  
        public string Key { get; set; } 

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                var binding = new ReflectionBindingExtension($"[{Key}]")
                {  
                    Source = LanguageManager.Instance,
                    Mode = BindingMode.OneWay,
                };
                return binding.ProvideValue(serviceProvider); 
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                    ?.Log("ProvideValue", "", ex);
            }
            return string.Empty;
        } 
    }
}

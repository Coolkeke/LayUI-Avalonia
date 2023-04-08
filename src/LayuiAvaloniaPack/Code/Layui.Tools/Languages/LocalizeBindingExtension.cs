using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Logging;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using System.ComponentModel;
using System.Drawing;

namespace Layui.Tools.Languages
{
    /// <summary>
    /// 待测试
    /// </summary>
    internal class LocalizeBindingExtension
    {
        public LocalizeBindingExtension(string path)
        {
            Path = path;
        }
        public LocalizeBindingExtension(IBinding binding)
        {
            Binding = binding;
        }
        public IBinding Binding { get; set; }
        public string Path { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        { 
           return Binding;
        }
    }
}

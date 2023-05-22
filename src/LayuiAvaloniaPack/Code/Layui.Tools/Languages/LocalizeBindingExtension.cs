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
using DryIoc;
using System.Reactive;
using System.Reactive.Subjects;
using Avalonia.Threading;
using static ImTools.ImMap;

namespace Layui.Tools.Languages
{

    /// <summary>
    /// 待测试
    /// </summary>
    public class LocalizeBindingExtension : AvaloniaObject
    {
        static LocalizeBindingExtension() {
            PathProperty.Changed.AddClassHandler<LocalizeBindingExtension>((o, e) =>
            {
                if (o is null)
                    return; 
            });
        }
        /// <summary>
        /// Comment
        /// </summary>
        private IBinding Binding { get; set; }
        private object Source { get; set; }
        public LocalizeBindingExtension(IBinding binding) 
        {
            Source = LanguageManager.Instance;
            Binding = binding;
            this.Bind(PathProperty, binding); 
        }
        /// <summary>
        /// Defines the <see cref="Path"/> property.
        /// </summary>
        internal static readonly StyledProperty<string> PathProperty =
            AvaloniaProperty.Register<LocalizeBindingExtension, string>(nameof(Path), string.Empty); 
        /// <summary>
        /// Comment
        /// </summary>
        internal string Path
        {
            get { return GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        } 
        public object ProvideValue()
        { 
            return Binding; 
        }
    }
}

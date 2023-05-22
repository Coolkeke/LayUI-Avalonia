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

namespace Layui.Tools.Languages
{
    /// <summary>
    /// 待测试
    /// </summary>
    public class LocalizeBindingExtension : AvaloniaObject
    { 
        public LocalizeBindingExtension(IBinding binding)
        {
            Binding = binding;
        }
        static LocalizeBindingExtension(){
            BindingProperty.Changed.AddClassHandler<LocalizeBindingExtension>((o, e) => o.OnBindingChanged());
        }

        private void OnBindingChanged()
        {
           
        }
        private BindingPriority _priority;
        /// <summary>
        /// 这是依赖属性名称
        /// </summary>
        public IBinding Binding
        {
            get { return GetValue(BindingProperty); }
            set { SetValue(BindingProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="IBinding"/>属性
        /// </summary>
        public static readonly StyledProperty<IBinding> BindingProperty =
       AvaloniaProperty.Register<LocalizeBindingExtension, IBinding>(nameof(Binding), null);

        public object ProvideValue(IServiceProvider serviceProvider)
        { 
            if (Binding is Binding bind)
            { 
                if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget &&
                provideValueTarget.TargetObject is AvaloniaObject targetObject &&
                provideValueTarget.TargetProperty is AvaloniaProperty targetProperty)
                {  
                    if (targetObject.GetValue(targetProperty) is object value)
                    {
                        var binding = new ReflectionBindingExtension($"[{value}]")
                        {
                            Source = LanguageManager.Instance,
                            Mode = BindingMode.OneWay,

                        };
                        return binding.ProvideValue(serviceProvider);
                    } 
                }
                
            }
            return AvaloniaProperty.UnsetValue;
        }
    }
}

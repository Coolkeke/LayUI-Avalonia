﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Data.Core;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Languages
{
    public class LocalizeExtension : MarkupExtension
    {
        private AvaloniaObject uiElement = null;
        private readonly AvaloniaObject _proxy;
        static LocalizeExtension() {
            TagProperty.Changed.Subscribe((e) =>
            {
                var data = e.Sender;
            });
        }
        public LocalizeExtension()
        {
            _proxy = new AvaloniaObject();
            Source = LanguageManager.Instance;
        }
        public LocalizeExtension(object key) : this()
        {
            this.Key = key;
        }
        /// <summary>
        /// 
        /// </summary>
        public static void SetTag(AvaloniaObject element, object value)
        {
            element.SetValue(TagProperty, value);
        }
        /// <summary>
        /// 
        /// </summary>
        public static object GetTag(AvaloniaObject element)
        {
            return element.GetValue(TagProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly AttachedProperty<object> TagProperty = 
            AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, object>(  "Tag");

        public object Key
        {
            get { return _proxy.GetValue(KeyProperty); }
            set { _proxy.SetValue(KeyProperty, value); }
        }
        public static readonly AttachedProperty<object> KeyProperty =
            AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, object>("Key");
        public static void SetTargetProperty(AvaloniaObject element, AvaloniaProperty value)
        {
            element.SetValue(TargetPropertyProperty, value);
        }
        public static AvaloniaProperty GetTargetProperty(AvaloniaObject element)
        {
            return element.GetValue(TargetPropertyProperty);
        }
        public static readonly AttachedProperty<AvaloniaProperty> TargetPropertyProperty =
            AvaloniaProperty.RegisterAttached<AvaloniaObject, IAvaloniaObject, AvaloniaProperty>(
            "TargetProperty");

        public BindingMode Mode { get; set; }

        public IValueConverter Converter { get; set; }

        public object ConverterParameter { get; set; }
        public object Source { get; set; }
        public string Context { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget)) return this;
            if (!(provideValueTarget.TargetObject is AvaloniaObject targetObject)) return this;
            if (!(provideValueTarget.TargetProperty is AvaloniaProperty targetProperty)) return this;
            var ui = GetTag(targetObject);
            switch (Key)
            {
                case string key:
                    {
                        var binding = new ReflectionBindingExtension($"[{Key}]")
                        {
                            Converter = Converter,
                            ConverterParameter = ConverterParameter,
                            Source = Source,
                            Mode = BindingMode.OneWay
                        };
                        return binding.ProvideValue(serviceProvider);
                    }
                case Binding keyBinding when targetObject is StyledElement element:
                    {
                        if (element.DataContext != null)
                        {
                            var binding = SetLangBinding(element, targetProperty, keyBinding.Path, element.DataContext);
                            return binding.ProvideValue(serviceProvider);
                        }
                        SetTargetProperty(element, targetProperty);
                        element.DataContextChanged += LangExtension_DataContextChanged;
                        break;
                    }
            }

            return string.Empty;
        }

        private void LangExtension_DataContextChanged(object? sender, EventArgs e)
        {
            switch (sender)
            {
                case StyledElement element:
                    {
                        element.DataContextChanged -= LangExtension_DataContextChanged;
                        if (!(Key is Binding keyBinding)) return;
                        var targetProperty = GetTargetProperty(element);
                        SetTargetProperty(element, null);
                        SetLangBinding(element, targetProperty, keyBinding.Path, element.DataContext);
                        break;
                    }
            }
        }

        private ReflectionBindingExtension SetLangBinding(StyledElement targetObject, AvaloniaProperty targetProperty, string path, object dataContext)
        {
            if (targetProperty == null) return null;
            var key = targetObject.GetValue(targetProperty) as string;
            var binding = CreateLangBinding(key);
            return binding;
        }

        private ReflectionBindingExtension CreateLangBinding(string key) => new ReflectionBindingExtension($"[{Key}]")
        {
            Converter = Converter,
            ConverterParameter = ConverterParameter,
            Source = Source,
            Mode = BindingMode.OneWay
        };
    }
}

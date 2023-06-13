using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.Desktop.DataTemplateSelector
{
    /// <summary>
    /// 模板选择器
    /// </summary>
    public class ColorDataTemplateSelector : IDataTemplate
    {
        public IDataTemplate Block_DataTemplate { get; set; }
        public IDataTemplate Blue_DataTemplate { get; set; }
        public IControl Build(object param)
        {
            if (param is bool b)
            {
                if (b) return Block_DataTemplate.Build(param);
                else return Blue_DataTemplate.Build(param);
            }
            return new Control();
        }

        public bool Match(object data)
        {
            return true;
        }
    }
}

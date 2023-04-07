using Avalonia.Controls;
using Avalonia.Controls.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayComboBox: ComboBox
    {
        /// <summary>
        /// 重写指定下拉选项下拉控件，替换为指定下拉控件
        /// </summary>
        /// <returns></returns>
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<LayComboBoxItem>(
                this,
                LayComboBoxItem.ContentProperty,
                LayComboBoxItem.ContentTemplateProperty);
        }
    }
}

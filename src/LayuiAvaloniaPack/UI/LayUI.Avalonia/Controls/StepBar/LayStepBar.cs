using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators; 
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayStepBar : ListBox
    {
        /// <summary>
        /// 重写指定下步骤条项，替换为指定下拉控件
        /// </summary>
        /// <returns></returns>
        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<LayStepBarItem>(
                this,
                LayComboBoxItem.ContentProperty,
                LayComboBoxItem.ContentTemplateProperty);
        }
        protected override void OnContainersDematerialized(ItemContainerEventArgs e)
        {
            base.OnContainersDematerialized(e);
            UpdateItemIndex(e.Containers);
        }
        protected override void OnContainersMaterialized(ItemContainerEventArgs e)
        {
            base.OnContainersMaterialized(e);
            UpdateItemIndex(e.Containers);
        }
        /// <summary>
        /// 修改Item子项界面显示索引
        /// </summary>
        private void UpdateItemIndex(IList<ItemContainerInfo> itemContainers)
        {
            foreach (ItemContainerInfo info in itemContainers)
            {
               if(info.ContainerControl is LayStepBarItem item) item.Index = info.Index+1;
            }
        }
    }
}

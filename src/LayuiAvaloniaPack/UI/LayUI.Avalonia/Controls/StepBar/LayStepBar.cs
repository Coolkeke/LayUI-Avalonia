using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayStepBar : ItemsControl
    {
        static LayStepBar()
        {
            StepIndexProperty.Changed.AddClassHandler(delegate (LayStepBar x, AvaloniaPropertyChangedEventArgs e)
            {
                x.OnStepChanged();
            });
        }
        private void OnStepChanged()
        {
            foreach (ItemContainerInfo item in ItemContainerGenerator.Containers)
            {
                OnStepItemChanged(item);
            }
        }
        protected virtual void OnStepItemChanged(ItemContainerInfo info)
        {
            if (!(info.ContainerControl is LayStepBarItem item)) return;
            if (item.Index <= StepIndex|| StepIndex < 1 || StepIndex > ItemCount) UpdateItemType();   
        }
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
            OnStepChanged();
        }
        protected override void OnContainersMaterialized(ItemContainerEventArgs e)
        {
            base.OnContainersMaterialized(e);
            UpdateItemIndex(e.Containers); 
            OnStepChanged();
        }
        /// <summary>
        /// 当前步骤
        /// </summary>
        public int StepIndex
        {
            get { return GetValue(StepIndexProperty); }
            set { SetValue(StepIndexProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly StyledProperty<int> StepIndexProperty =
       AvaloniaProperty.Register<LayStepBar, int>(nameof(StepIndex), 0);

        /// <summary>
        /// 修改当前Item的状态
        /// </summary>
        /// <param name="stepIndex">当前步骤</param>
        private void UpdateItemType()
        {
            for (int i = 0; i < StepIndex; i++)
            {
                if (ItemContainerGenerator.ContainerFromIndex(i - 1) is LayStepBarItem completeItem) completeItem.Type = StepType.Complete;
            }
            for (int i = StepIndex; i < ItemCount; i++)
            {
                if (ItemContainerGenerator.ContainerFromIndex(i) is LayStepBarItem waitingItem) waitingItem.Type = StepType.Waiting;
            }
            if (ItemContainerGenerator.ContainerFromIndex(StepIndex - 1) is LayStepBarItem executingItem) executingItem.Type = StepType.Executing;
            if (StepIndex > ItemCount && ItemContainerGenerator.ContainerFromIndex(ItemCount - 1) is LayStepBarItem item) item.Type = StepType.Complete;
        }
        /// <summary>
        /// 修改Item子项界面显示索引
        /// </summary>
        private void UpdateItemIndex(IList<ItemContainerInfo> itemContainers)
        {
            foreach (ItemContainerInfo info in itemContainers)
            {
                if (info.ContainerControl is LayStepBarItem item) item.Index = info.Index + 1;
            }
        }
    }
}

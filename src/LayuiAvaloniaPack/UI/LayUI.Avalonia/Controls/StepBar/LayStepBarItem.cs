using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    /// <summary>
    /// 步骤子项
    /// </summary>
    public class LayStepBarItem : ListBoxItem
    {
        /// <summary>
        /// 记录当前步骤索引
        /// </summary>
        public int Index
        {
            get { return GetValue(IndexProperty); }
            internal set { SetValue(IndexProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="int"/>属性
        /// </summary>
        public static readonly StyledProperty<int> IndexProperty =
       AvaloniaProperty.Register<LayStepBarItem, int>(nameof(Index));
         
        /// <summary>
        /// 选中项执行状态
        /// </summary>
        public StepType Type
        {
            get { return GetValue(TypeProperty); }
            internal set { SetValue(TypeProperty, value); }
        }
        /// <summary>
        /// 定义<see cref="StepType"/>属性
        /// </summary>
        public static readonly StyledProperty<StepType> TypeProperty =
       AvaloniaProperty.Register<LayStepBarItem, StepType>(nameof(Type), StepType.None);


    }
}

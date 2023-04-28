using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Interface
{
    /// <summary>
    /// 轮播图接口
    /// </summary>
    public interface ICarousel
    {
        /// <summary>
        /// 当前项索引
        /// </summary>
        int SelectedIndex { get; set; }
        /// <summary>
        /// 当前项内容
        /// </summary>
        object SelectedItem { get; set; }
        /// <summary>
        /// 轮播图集合
        /// </summary>
        [Content]
        IEnumerable Items { get; set; }
        /// <summary>
        /// 集合数量
        /// </summary>
        int ItemCount { get; }
        /// <summary>
        /// 显示轮播指示器
        /// </summary>
        bool IsIndicatorVisible { get; set; }
        /// <summary>
        /// 子集模板
        /// </summary>
        IDataTemplate ItemTemplate { get; set; }
        /// <summary>
        /// 下一页
        /// </summary>
        void Next();
        /// <summary>
        /// 上一页
        /// </summary>
        void Previous();
        /// <summary>
        /// 获取项目覆盖的容器
        /// </summary>
        /// <returns></returns>
        AvaloniaObject GetContainerForItemOverride();
        /// <summary>
        /// 项目是否覆盖其自己的容器
        /// </summary>
        /// <param name="item">子项</param>
        /// <returns></returns>
        bool IsItemItsOwnContainerOverride(object item); 
    }
}

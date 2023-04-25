using Avalonia.Controls.Templates;
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
        /// 当前项
        /// </summary>
        int SelectedIndex { get; set; }
        /// <summary>
        /// 轮播图集合
        /// </summary>
        IEnumerable Items { get; set; }
        /// <summary>
        /// 集合项
        /// </summary>
        int ItemsCount { get; }
        /// <summary>
        /// 子集模板
        /// </summary>
        IDataTemplate ItemTemplate { get; set; }
        /// <summary>
        /// 上一页
        /// </summary>
        void Next();
        /// <summary>
        /// 下一页
        /// </summary>
        void Previous();
    }
}

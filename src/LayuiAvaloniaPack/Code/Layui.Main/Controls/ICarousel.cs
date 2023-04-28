using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Main.Controls
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
        [Content]
        IEnumerable Items { get; set; }
        /// <summary>
        /// 集合数量
        /// </summary>
        int ItemCount { get; }
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

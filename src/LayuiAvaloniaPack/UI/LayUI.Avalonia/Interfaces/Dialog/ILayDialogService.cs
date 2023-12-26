using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Interfaces
{
    /// <summary>
    ///  ILayDialogService
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2023-02-09 下午 1:42:10</para>
    /// </summary>
    public interface ILayDialogService : ILayDialog
    {
        /// <summary>
        /// 注入弹窗试图
        /// </summary>
        /// <typeparam name="TView">需要注入的类型</typeparam>
        /// <param name="dialogName">视图名称</param>
        void RegisterDialog<TView>(string dialogName);
    }
}

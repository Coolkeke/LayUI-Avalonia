using Avalonia.Controls;
using Avalonia.Logging;
using LayUI.Avalonia.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Global
{
    /// <summary>
    ///  LayDialogService
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2023-02-09 下午 1:41:51</para>
    /// </summary>
    public class LayDialogService : LayDialog,ILayDialogService 
    {

        public void RegisterDialog<TView>(string dialogName)
        {
            try
            {
                if (DialogViews.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图多次注入");
                DialogViews.Add(dialogName, typeof(TView));
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                       ?.Log("RegisterDialog", "对话框注册异常", ex);
            }
        }
    }
}

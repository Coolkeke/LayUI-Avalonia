using Avalonia.Controls;
using LayUI.Avalonia.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Dialog
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
                throw ex;
            }
        }
        public void RegisterDialog<TView, TViewModel>(string dialogName) where TViewModel : ILayDialogAware
        {
            try
            {
                if (DialogViews.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图多次注入");
                if (DialogViewModels.ContainsKey(dialogName)) throw new Exception($"{dialogName}弹窗视图ViewModel多次注入");
                DialogViews.Add(dialogName, typeof(TView));
                DialogViewModels.Add(dialogName, typeof(TViewModel));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

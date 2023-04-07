using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class PrismMessageBoxViewModel : BindableBase,IDialogAware
    {
        public PrismMessageBoxViewModel()
        {

        }

        public string Title => "对话框"; 

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {  
            return true;    
        }

        public void OnDialogClosed()
        {
           
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           
        }
    }
}

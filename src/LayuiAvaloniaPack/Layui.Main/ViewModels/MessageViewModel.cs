using LayUI.Avalonia;
using LayUI.Avalonia.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class MessageViewModel : BindableBase,ILayDialogAware
    {
        private ILayDialogService layDialog;
        public MessageViewModel(ILayDialogService dialogService)
        {
            layDialog = dialogService;
        }
        private DelegateCommand _CloseCommand;

        public event Action<ILayDialogResult> RequestClose;

        public DelegateCommand CloseCommand =>
            _CloseCommand ?? (_CloseCommand = new DelegateCommand(ExecuteCloseCommand));

        void ExecuteCloseCommand()
        {
            RequestClose?.Invoke(null);
        }
        private DelegateCommand _GoCommand;
        public DelegateCommand GoCommand =>
            _GoCommand ?? (_GoCommand = new DelegateCommand(ExecuteGoCommand));

        void ExecuteGoCommand()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://space.bilibili.com/48808444?spm_id_from=..0.0",
                UseShellExecute = true
            });
            RequestClose?.Invoke(null);
        }
        public void OnDialogOpened(ILayDialogParameter parameters)
        {
           
        }
    }
}

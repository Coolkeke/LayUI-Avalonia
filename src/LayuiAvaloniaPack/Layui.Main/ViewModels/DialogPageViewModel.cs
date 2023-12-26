using Layui.Core;
using LayUI.Avalonia.Interfaces;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class DialogPageViewModel : ViewModelBase
    {
        private ILayDialogService layDialog;
        public DialogPageViewModel(IContainerExtension container) : base(container)
        {
            layDialog = container.Resolve<ILayDialogService>(); 
        }
        private DelegateCommand<string> _DlalogCommand;
        public DelegateCommand<string> DlalogCommand =>
            _DlalogCommand ?? (_DlalogCommand = new DelegateCommand<string>(ExecuteDlalogCommand));

        void ExecuteDlalogCommand(string messageRootName)
        {
            layDialog.Show("Message", null, res =>
            {
                var data = res;
            }, messageRootName);
        }
        private DelegateCommand _PrismDlalogCommand;
        public DelegateCommand PrismDlalogCommand =>
            _PrismDlalogCommand ?? (_PrismDlalogCommand = new DelegateCommand(ExecutePrismDlalogCommand));

        void ExecutePrismDlalogCommand()
        {
            Dialog.Show("MessageBox", null, null);
        }

        protected override void Loaded()
        {
            
        }

        protected override void Unloaded()
        {
            
        }
    }
}

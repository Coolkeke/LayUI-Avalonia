using LayUI.Avalonia.Dialog;
using Prism.Commands;
using Prism.Mvvm;

namespace Layui.Main.ViewModels
{
    public class DialogPageViewModel : BindableBase
    {
        private ILayDialogService layDialog;
        public DialogPageViewModel(ILayDialogService dialogService)
        {
            layDialog = dialogService;
        }
        private DelegateCommand _DlalogCommand;
        public DelegateCommand DlalogCommand =>
            _DlalogCommand ?? (_DlalogCommand = new DelegateCommand(ExecuteDlalogCommand));

        void ExecuteDlalogCommand()
        {
             layDialog.Show("Message", null, "RootDialog");
        }
    }
}

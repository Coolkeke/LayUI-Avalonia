using Layui.Core.Mvvm;
using LayUI.Avalonia.Interface;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;

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
    }
}

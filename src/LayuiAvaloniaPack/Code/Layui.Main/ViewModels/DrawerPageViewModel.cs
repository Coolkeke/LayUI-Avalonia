using Layui.Core.Mvvm;
using Layui.Main.Views;
using LayUI.Avalonia.Interface;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Main.ViewModels
{
    public class DrawerPageViewModel : ViewModelBase
    {
        private bool _IsOpen;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetProperty(ref _IsOpen, value); }
        }
        private ILayMessage message;
        public DrawerPageViewModel(IContainerExtension container) : base(container)
        {
            message = container.Resolve<ILayMessage>();
        }
        private DelegateCommand _OpenedCommand;
        public DelegateCommand OpenedCommand =>
            _OpenedCommand ?? (_OpenedCommand = new DelegateCommand(ExecuteOpenedCommand));

        void ExecuteOpenedCommand()
        {
            message.Show($"状态:{IsOpen}", "RootMessage", TimeSpan.FromMilliseconds(3000));
        }
    }
}

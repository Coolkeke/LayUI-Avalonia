using Layui.Core;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Main.ViewModels
{
    public class ListBoxPageViewModel : ViewModelBase
    {
        public ListBoxPageViewModel(IContainerExtension container) : base(container)
        {
        }
        private ObservableCollection<string> _Items;
        public ObservableCollection<string> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        protected override void Loaded()
        {
            Items=new ObservableCollection<string>();
            for (int i = 0; i < 100; i++)
            {
                Items.Add($"{i}");
            }
        }
        private DelegateCommand<string> _RemoveCommand;
        public DelegateCommand<string> RemoveCommand =>
            _RemoveCommand ?? (_RemoveCommand = new DelegateCommand<string>(ExecuteRemoveCommand));

        void ExecuteRemoveCommand(string parameter)
        {
            Items.Remove(parameter);
        }
        protected override void Unloaded()
        {
             
        }
    }
}

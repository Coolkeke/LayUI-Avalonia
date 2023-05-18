using Layui.Core.Mvvm;
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
    public class ListPageViewModel : ViewModelBase
    {
        public ListPageViewModel(IContainerExtension container) : base(container)
        {
        }
        private ObservableCollection<string> _Datas=new ObservableCollection<string>() { "asd","sdf","ss","aa","dd","bb"};
        public ObservableCollection<string> Datas
        {
            get { return _Datas; }
            set { SetProperty(ref _Datas, value); }
        }
        private DelegateCommand<string> _DeleteCommand;
        public DelegateCommand<string> DeleteCommand =>
            _DeleteCommand ?? (_DeleteCommand = new DelegateCommand<string>(ExecuteDeleteCommand));

        void ExecuteDeleteCommand(string data)
        {
            Datas.Remove(data);
        }
    }
}

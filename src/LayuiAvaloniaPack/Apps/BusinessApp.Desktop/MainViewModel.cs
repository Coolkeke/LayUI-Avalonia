using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.Desktop
{
    public class MainViewModel : BindableBase
    {
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        public MainViewModel()
        {

        }
        private DelegateCommand _TestCommand;
        public DelegateCommand TestCommand =>
            _TestCommand ?? (_TestCommand = new DelegateCommand(ExecuteTestCommand));

        async void ExecuteTestCommand()
        {
           IsActive = true;
           await Task.Delay(2000);
           IsActive = false;
        }
    }
}

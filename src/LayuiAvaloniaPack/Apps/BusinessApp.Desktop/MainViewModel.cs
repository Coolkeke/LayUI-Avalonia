using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.Desktop
{
    public class MainViewModel : BindableBase
    {
        private ObservableCollection<object> _List=new ObservableCollection<object>();
        public ObservableCollection<object> List
        {
            get { return _List; }
            set { SetProperty(ref _List, value); }
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        public MainViewModel()
        {
            ExecuteTestCommand();
        }
        private DelegateCommand _TestCommand;
        public DelegateCommand TestCommand =>
            _TestCommand ?? (_TestCommand = new DelegateCommand(ExecuteTestCommand));

        async void ExecuteTestCommand()
        {
           IsActive = true;
            await Task.Delay(1000);
            Random ran = new Random();
            int count = ran.Next(1, 100);
            List.Clear();
            for (int i = 0; i < count; i++)
            {
                var num = ran.Next(1, 100);
                List.Add(new { Number= $"{num}",Type= (num%2)==0 });
            }
            IsActive = false;
        }
    }
}

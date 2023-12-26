using Layui.Core;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class SkeletonPageViewModel : ViewModelBase
    {
        public SkeletonPageViewModel(IContainerExtension container) : base(container)
        {
        }

        private ObservableCollection<bool> _Items;
        public ObservableCollection<bool> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        private DelegateCommand _TestCommand;
        public DelegateCommand TestCommand =>
            _TestCommand ?? (_TestCommand = new DelegateCommand(ExecuteTestCommand));

        async void ExecuteTestCommand()
        {
            if (Items == null) Items = new ObservableCollection<bool>();
            Items.Clear();
            for (int i = 0; i < 20; i++)
            {
                Items.Add(true);
            }
            await Task.Delay(3000);
            Items.Clear();
            for (int i = 0; i < 20; i++)
            {
                Items.Add(false);
            }
        }

        protected override void Loaded()
        {
             
        }

        protected override void Unloaded()
        {
            
        }
    }
}

using Layui.Core;
using Layui.Main.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class FlowItemsControlPageViewModel : ViewModelBase
    {
        public FlowItemsControlPageViewModel(IContainerExtension container) : base(container)
        {
        }

        private ObservableCollection<Data> _Items;
        public ObservableCollection<Data> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        private DelegateCommand _AppendCommand;
        public DelegateCommand AppendCommand =>
            _AppendCommand ?? (_AppendCommand = new DelegateCommand(ExecuteAppendCommand));

        async void ExecuteAppendCommand()
        {
            var items = new ObservableCollection<Data>();
            for (int i = 0; i < 10; i++)
            {
                items.Add(new Data() { Title = "看山不是山，看水不是水，看山还是山，看水还是水", IsActive = true });
            }
            foreach (var item in items)
            {
                Items.Add(item);
            }
            await Task.Delay(3000);
            foreach (var item in items)
            {
                item.IsActive = false;
            }

        }
        protected override void Loaded()
        {
            if (Items == null) Items = new ObservableCollection<Data>();
        }

        protected override void Unloaded()
        {

        }
    }
}

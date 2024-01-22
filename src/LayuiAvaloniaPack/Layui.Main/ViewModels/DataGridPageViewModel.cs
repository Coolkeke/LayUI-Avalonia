using Layui.Main.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class DataGridPageViewModel : BindableBase
    {

        private ObservableCollection<Data> _Items;
        public ObservableCollection<Data> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        public DataGridPageViewModel()
        {
            var items = new ObservableCollection<Data>() { new Data() { Title = "看山不是山，看水不是水，看山还是山，看水还是水" } };
            for (int i = 0; i < 10; i++)
            {
                items.Add(new Data() { Title = "看山不是山，看水不是水，看山还是山，看水还是水" });
            }
            Items = items;
        }
    }
}

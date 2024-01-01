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
    public class CarouselPageViewModel : ViewModelBase
    {
        public CarouselPageViewModel(IContainerExtension container) : base(container)
        {
        }
        private ObservableCollection<object> _Items = new ObservableCollection<object> {
            new { Title="轮播一",Message="" },
            new { Title="轮播一二",Message="" },
            new { Title="轮播一三",Message="" },
            new { Title="轮播一四",Message="" },
        };
        public ObservableCollection<object> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        private int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { SetProperty(ref _SelectedIndex, value); }
        }
        protected override void Loaded()
        {
            
        }

        protected override void Unloaded()
        {
            
        }
    }
}

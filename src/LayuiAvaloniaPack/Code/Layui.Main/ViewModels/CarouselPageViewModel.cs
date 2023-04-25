using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Main.ViewModels
{
    public class CarouselPageViewModel : BindableBase
    {
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
    }
}

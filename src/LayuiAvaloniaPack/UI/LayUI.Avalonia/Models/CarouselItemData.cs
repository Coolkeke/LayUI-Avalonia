using LayUI.Avalonia.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Models
{
    internal class CarouselItemData: LayBindableBase
    {
        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetProperty(ref _Index, value); }
        }
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { SetProperty(ref _IsSelected, value); }
        }
    }
}

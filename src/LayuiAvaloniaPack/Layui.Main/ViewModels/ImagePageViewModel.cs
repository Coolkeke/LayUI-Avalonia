using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class ImagePageViewModel : BindableBase
    {
        public ImagePageViewModel()
        {

        }
        private bool _ImageIsLoaded;
        public bool ImageIsLoaded
        {
            get { return _ImageIsLoaded; }
            set { SetProperty(ref _ImageIsLoaded, value); }
        }
    }
}

using LayuiApp.Core;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiApp.ViewModels
{
    public class FormPageViewModel : ViewModelBase
    {
        public FormPageViewModel(IContainerExtension container) : base(container) { } 
        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
    }
}

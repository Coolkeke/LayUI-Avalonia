using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayuiApp.ViewModels
{
    public class FormPageViewModel : BindableBase
    {
        public FormPageViewModel()
        {

        }
        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
    }
}

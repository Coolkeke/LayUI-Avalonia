using Layui.Core;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class TextBoxPageViewModel : ViewModelBase
    { 
        private int? _Value;

        public TextBoxPageViewModel(IContainerExtension container) : base(container)
        {
        }

        public int? Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }

        protected override void Loaded()
        {
            
        }

        protected override void Unloaded()
        {
           
        }
    }
}

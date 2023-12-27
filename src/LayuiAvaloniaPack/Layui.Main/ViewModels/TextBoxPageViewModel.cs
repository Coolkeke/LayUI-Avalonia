﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class TextBoxPageViewModel : BindableBase
    {
        public TextBoxPageViewModel()
        {

        }
        private int? _Value; 
        public int? Value
        {
            get { return _Value; }
            set { SetProperty(ref _Value, value); }
        }
    }
}

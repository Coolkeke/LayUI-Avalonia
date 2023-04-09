using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData.Binding;
using Layui.Core.Mvvm;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Layui.Main.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(IContainerExtension container) : base(container) { }
        
    }
}
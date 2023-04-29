using System;
using System.Collections.Generic;
using Layui.Core.Mvvm;
using Prism.Ioc;
using Prism.Mvvm;

namespace Layui.Main.ViewModels
{
    public class GroupBoxViewModel : ViewModelBase
    {
        public GroupBoxViewModel(IContainerExtension container) : base(container)
        {
        }
    }
}
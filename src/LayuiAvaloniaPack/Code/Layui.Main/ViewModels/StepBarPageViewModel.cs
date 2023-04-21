using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class StepBarPageViewModel : BindableBase
    {
        public StepBarPageViewModel()
        {

        }
        private List<string> _Items=new List<string>() { "步骤一", "步骤二", "步骤三", "步骤四" };
        public List<string> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
    }
}

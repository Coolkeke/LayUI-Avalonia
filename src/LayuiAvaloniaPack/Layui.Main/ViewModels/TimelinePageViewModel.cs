using Layui.Core;
using Layui.Main.Models;
using LayUI.Avalonia;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class TimelinePageViewModel : ViewModelBase
    {
        public TimelinePageViewModel(IContainerExtension container) : base(container)
        {
        }
        private ObservableCollection<object> _Items;
        public ObservableCollection<object> Items
        {
            get { return _Items; }
            set { SetProperty(ref _Items, value); }
        }
        protected override void Loaded()
        {
            Items=new ObservableCollection<object>();
            for (int i = 10; i < 50; i++)
            {
                var icon = LayFontsHelper.StringToUnicode($"&#xea{i};");
                Items.Add(new {Icon= icon, Header="LayUI-Avalonia",Content= "一切都是为了爱好" });
            }
        }

        protected override void Unloaded()
        {
            
        }
    }
}

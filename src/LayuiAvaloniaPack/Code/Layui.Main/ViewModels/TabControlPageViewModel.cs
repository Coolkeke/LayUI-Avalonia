using Layui.Core.Mvvm;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class TabControlPageViewModel : ViewModelBase
    {
        private object _SelectedItem;
        public object SelectedItem
        {
            get { return _SelectedItem; }
            set { SetProperty(ref _SelectedItem, value); }
        }
        private ObservableCollection<object> _Tabs = new ObservableCollection<object> {
            new { Title="选项卡一",Message="" },
            new { Title="选项卡二",Message="" },
            new { Title="选项卡三",Message="" },
            new { Title="选项卡四",Message="" },
        };
        public ObservableCollection<object> Tabs
        {
            get { return _Tabs; }
            set { SetProperty(ref _Tabs, value); }
        }
        public TabControlPageViewModel(IContainerExtension container) : base(container)
        {
            SelectedItem = Tabs?[2];//默认选中选项卡第三项（选项卡三）
        }
        
        private DelegateCommand<object> _CloseCommand;
        public DelegateCommand<object> CloseCommand =>
            _CloseCommand ?? (_CloseCommand = new DelegateCommand<object>(ExecuteCloseCommand));

        void ExecuteCloseCommand(object obj)
        {
            try
            {
                Tabs.Remove(obj);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}

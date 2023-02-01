using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData.Binding;
using LayuiApp.Core;
using LayuiApp.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace LayuiApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IContainerExtension container) : base(container) { }
        private ObservableCollection<MenuInfo> _Menus;
        /// <summary>
        /// 菜单集合
        /// </summary>
        public ObservableCollection<MenuInfo> Menus
        {
            get { return _Menus; }
            set { SetProperty(ref _Menus, value); }
        }
        private DelegateCommand<MenuInfo> _GoPageCommand;
        public DelegateCommand<MenuInfo> GoPageCommand =>
            _GoPageCommand ?? (_GoPageCommand = new DelegateCommand<MenuInfo>(ExecuteGoPageCommand));

        void ExecuteGoPageCommand(MenuInfo info)
        {
            Region.RequestNavigate("Nav_MainContent", info.PageKey);
        }
        private ObservableCollection<MenuInfo> CreateMenus()
        {
            ObservableCollection<MenuInfo> menus = new ObservableCollection<MenuInfo>
            {
                new MenuInfo() { FontIcon = "", PageKey = "ButtonPage", Title = "按钮" },
                new MenuInfo() { FontIcon = "", PageKey = "FormPage", Title = "表单" },
            };
            return menus;
        }
        public override void ExecuteLoadedCommand()
        {
            Menus = CreateMenus();
        }
    }
}
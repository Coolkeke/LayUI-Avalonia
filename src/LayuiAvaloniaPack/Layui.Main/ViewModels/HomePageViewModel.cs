using Layui.Core.Mvvm;
using Layui.Core.Resources;
using Layui.Main.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Layui.Main.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(IContainerExtension container) : base(container) { }
        private MenuInfo _MenuInfo;
        public MenuInfo MenuInfo
        {
            get { return _MenuInfo; }
            set { SetProperty(ref _MenuInfo, value); }
        }
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
            Region.RequestNavigate(SystemResource.Nav_HomeContent, info.PageKey);
        }
        private ObservableCollection<MenuInfo> CreateMenus()
        {
            ObservableCollection<MenuInfo> menus = new ObservableCollection<MenuInfo>
            {
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.ButtonPage, Title = "按钮" },
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.FormPage, Title = "表单" },
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.ImagePage, Title = "图片" },
            };
            return menus;
        }
        public override void ExecuteLoadedCommand()
        {
            Menus = CreateMenus();
            MenuInfo= Menus?.FirstOrDefault();
            if(MenuInfo!=null) Region.RegisterViewWithRegion(SystemResource.Nav_HomeContent, MenuInfo.PageKey);
        }
    }
}

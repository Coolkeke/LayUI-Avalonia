using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging;
using Layui.Core.Mvvm;
using Layui.Core.Resources;
using Layui.Main.Models;
using Layui.Tools.Languages;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy; 

namespace Layui.Main.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private ResourceDictionary _Language;
        public ResourceDictionary Language
        {
            get { return _Language; }
            set { SetProperty(ref _Language, value); }
        }
        private bool _IsEn;
        /// <summary>
        /// 是否为英文
        /// </summary>
        public bool IsEn
        {
            get { return _IsEn; }
            set { SetProperty(ref _IsEn, value); OnLanugageChanged(value); }
        }
        /// <summary>
        /// 语言切换
        /// </summary>
        /// <param name="value"></param>
        private void OnLanugageChanged(bool value)
        {
            if(value) LanguageManager.Instance.LoadLanguage("Languages/ch-en.axaml");
            else LanguageManager.Instance.LoadLanguage("Languages/zh-cn.axaml"); 
        }

        public HomePageViewModel(IContainerExtension container) : base(container) 
        {
        }
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
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.ButtonPage, Title ="Button"},
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.FormPage, Title = "Form" },
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.ImagePage, Title ="Image" },
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.ProgressBarPage, Title ="ProgressBar" },
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.KeyboardPage, Title = "Keyboard" },
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.DialogPage, Title = "Dialog" },
                new MenuInfo() { FontIcon = "", PageKey = SystemResource.CardPage, Title = "CardPage" },
            };
            return menus;
        }
        public override void ExecuteLoadedCommand()
        {
            OnLanugageChanged(false);
            Menus = CreateMenus();
            MenuInfo= Menus?.FirstOrDefault();
            if(MenuInfo!=null) Region.RegisterViewWithRegion(SystemResource.Nav_HomeContent, MenuInfo.PageKey);
        }
    }
}

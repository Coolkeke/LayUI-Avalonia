using ImTools;
using Layui.Core;
using Layui.Main.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
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
            if (info != null) Region.RequestNavigate(SystemResource.Nav_HomeContent, info.PageKey);
        } 
        private ObservableCollection<MenuInfo> CreateMenus()
        {
            string Unicode = $"{(char)int.Parse("ebf1", System.Globalization.NumberStyles.HexNumber)}";
            ObservableCollection<MenuInfo> menus = new ObservableCollection<MenuInfo>
            {
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.IconPage, Title = "Icon" ,IsShow=true},
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ColorPage, Title ="Color"},
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ButtonPage, Title ="Button"},
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.RadioButtonPage, Title = "RadioButton" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.CheckBoxPage, Title = "CheckBox" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.SwitchButtonPage, Title = "Switch" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.TextBoxPage, Title = "TextBox" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.NumericUpDownPage, Title = "NumericUpDown" },
                //new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.FormPage, Title = "Form" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ImagePage, Title ="Image" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ComboBoxPage, Title ="ComboBox" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ProgressBarPage, Title ="ProgressBar" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.DialogPage, Title = "Dialog" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.CardPage, Title = "Card" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.MessagePage, Title = "Message" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.NotificationPage, Title = "Notification" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.NoticeBarPage, Title = "NoticeBar" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.LoadingPage, Title = "Loading" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.RipplePage, Title = "Ripple" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ToolTipPage, Title = "ToolTip" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.PopupBoxPage, Title = "PopupBox" },
                //new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.TabControlPage, Title = "TabControl" },
                //new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.StepBarPage, Title = "StepBar" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.CarouselPage, Title = "Carousel" },
                //new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ExpanderPage, Title = "Expander" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.NavExpanderPage, Title = "NavExpander" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.GroupBoxPage, Title = "GroupBox" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.LegendPage, Title = "Legend" },
                //new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.TreeViewPage, Title = "TreeView" },
                //new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.LegendPage, Title = "Legend" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ListBoxPage, Title = "ListBox" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.BadgePage, Title = "Badge" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.SkeletonPage, Title = "Skeleton" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.FlowItemsControlPage, Title = "FlowItemsControl" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.DrawerPage, Title = "Drawer" }, 
                //new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.MenuPage, Title = "Menu" },
                //new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.ListPage, Title = "List" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.TimelinePage, Title = "Timeline" },
                new MenuInfo() { FontIcon = $"{Unicode}", PageKey = SystemResource.KeyboardPage, Title = "Keyboard" },
            };
            return menus;
        }

        protected override async void Loaded()
        {
            await Task.Delay(100);
            Menus = CreateMenus(); 
            MenuInfo = Menus[0];
            GoPageCommand.Execute(MenuInfo);  
        }

        protected override void Unloaded()
        {

        }
    }
}

using Layui.Core.Resources;
using Layui.Main.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Layui.Main
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(SystemResource.Nav_MainContent, typeof(HomePage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            containerRegistry.RegisterForNavigation<ButtonPage>(SystemResource.ButtonPage);
            containerRegistry.RegisterForNavigation<FormPage>(SystemResource.FormPage);
            containerRegistry.RegisterForNavigation<HomePage>(SystemResource.HomePage);
            containerRegistry.RegisterForNavigation<ImagePage>(SystemResource.ImagePage);
            containerRegistry.RegisterForNavigation<ProgressBarPage>(SystemResource.ProgressBarPage);
        }
    }
}

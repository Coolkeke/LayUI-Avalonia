
using Avalonia.Threading;
using DryIoc;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Layui.Core
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IRegionMemberLifetime, IConfirmNavigationRequest, IJournalAware
    {
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value); }
        }
        private bool _CanGoBack;
        /// <summary>
        /// 能否后退
        /// </summary>
        public bool CanGoBack
        {
            get { return _CanGoBack; }
            private set { SetProperty(ref _CanGoBack, value); }
        }
        private bool _CanGoForward;
        /// <summary>
        /// 能否前进
        /// </summary>
        public bool CanGoForward
        {
            get { return _CanGoForward; }
            private set { SetProperty(ref _CanGoForward, value); }
        }
        /// <summary>
        /// 导航器
        /// </summary>
        protected readonly IRegionManager Region;
        /// <summary>
        /// 弹窗服务
        /// </summary>
        protected readonly IDialogService Dialog;
        /// <summary>
        /// 事件聚合器
        /// </summary>
        protected readonly IEventAggregator Event;
        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILayLogger Logger;
        /// <summary>
        /// 接收被传递过来的参数
        /// </summary>
        protected NavigationParameters Parameters { get; private set; }
        /// <summary>
        /// 导航服务
        /// </summary>
        private IRegionNavigationService NavigationService { get; set; }
        /// <summary>
        /// VM基类
        /// </summary>
        public ViewModelBase(IContainerExtension container)
        {
            this.Region = container.Resolve<IRegionManager>();
            this.Dialog = container.Resolve<IDialogService>();
            this.Event = container.Resolve<IEventAggregator>();
            this.Logger = container.Resolve<ILayLogger>();
        }
        /// <summary>
        ///初始化
        /// </summary>
        protected abstract void Loaded();
        /// <summary>
        /// 卸载
        /// </summary>
        protected abstract void Unloaded();
        /// <summary>
        /// 标记上一个视图时候被销毁
        /// </summary>
        public bool KeepAlive => false;

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) => continuationCallback(true);
        /// <summary>
        /// 导航后的目标视图是否缓存,有的话就使用之前的否者就创建新的
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => false;
        /// <summary>
        /// 导航前
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) => Unloaded();

        /// <summary>
        /// 导航后
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            Parameters = navigationContext.Parameters;
            NavigationService= navigationContext.NavigationService;
            CanGoForward = NavigationService.Journal.CanGoForward;
            CanGoBack = NavigationService.Journal.CanGoBack;
            Loaded();
        }
        /// <summary>
        /// 前进
        /// </summary>
        public void GoForward()
        {
            if (CanGoForward) NavigationService.Journal.GoForward();
        }
        /// <summary>
        /// 后退
        /// </summary>
        public void GoBack()
        {
            if (CanGoBack) NavigationService.Journal.GoBack();
        }
        /// <summary>
        /// 返回 false，页面可以选择不添加到日记历史记录中。
        /// </summary>
        /// <returns></returns>
        public virtual bool PersistInHistory() => true;
    }
}
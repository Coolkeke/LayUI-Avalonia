using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using LayUI.Avalonia.Controls;
using LayUI.Avalonia.Enums;
using LayUI.Avalonia.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Dialog
{
    public class LayDialog : ILayDialog
    {
        private static Dictionary<string, Type> _DialogViews;
        /// <summary>
        /// 被注入的窗体集合
        /// </summary>
        internal static Dictionary<string, Type> DialogViews
        {
            get => _DialogViews = _DialogViews ?? new Dictionary<string, Type>();
        }
        private static Dictionary<string, Type> _DialogViewModeles;
        /// <summary>
        /// 被注入的窗体ViewModel集合
        /// </summary>
        internal static Dictionary<string, Type> DialogViewModels
        {
            get => _DialogViewModeles = _DialogViewModeles ?? new Dictionary<string, Type>();
        }
        private static Dictionary<string, LayDialogHost> _DialogHosts;
        /// <summary>
        /// 对话框组件
        /// </summary>
        internal static Dictionary<string, LayDialogHost> DialogHosts
        {
            get => _DialogHosts = _DialogHosts ?? new Dictionary<string, LayDialogHost>();
        }
        static LayDialog()
        {
            TokenProperty.Changed.Subscribe(OnTokenChanged);
        }

        private static void OnTokenChanged(AvaloniaPropertyChangedEventArgs<string> obj)
        {
            if (obj.Sender is LayDialogHost host)
            {
                var token = GetToken(host);
                if (token == null) token = Guid.NewGuid().ToString();
                if (DialogHosts.ContainsKey(token)) DialogHosts?.Remove(token);
                host.GUID = Guid.NewGuid().ToString();
                DialogHosts?.Add(token, host);
            }
        }
        /// <summary>
        /// 设置唯一值
        /// </summary>
        public static void SetToken(AvaloniaObject element, string value)
        {
            element.SetValue(TokenProperty, value);
        }

        /// <summary>
        /// 获取唯一值
        /// </summary>
        public static string GetToken(AvaloniaObject element)
        {
            return element.GetValue(TokenProperty);
        }

        /// <summary>
        /// 获取唯一值
        /// </summary>
        public static readonly AttachedProperty<string> TokenProperty = AvaloniaProperty.RegisterAttached<IAvaloniaObject, IAvaloniaObject, string>(
            "Token", null);

        public Task Show(string dialogName, ILayDialogParameter parameters, string token)
        {
            return Alert(dialogName, parameters, null, token);
        }

        public Task Show(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token)
        {
            return Alert(dialogName, parameters, callback, token);
        }
        /// <summary>
        /// 弹窗业务
        /// </summary>
        /// <param name="dialogName">视图</param>
        /// <param name="parameters">参数</param>
        /// <param name="callback">回调</param>
        /// <param name="isModel">是否为模态</param>
        /// <param name="token">需要通知弹窗的唯一健值,如果是对话框窗体该值给Null</param>
        private Task Alert(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token)
        {
            return Dispatcher.UIThread.InvokeAsync(async () =>
            {
                try
                {
                    if (string.IsNullOrEmpty(token)) throw new Exception($"{nameof(token)}不能为空");
                    if (string.IsNullOrEmpty(dialogName)) throw new Exception($"{nameof(dialogName)}不能为空");
                    if (!DialogHosts.ContainsKey(token)) throw new Exception($"未找到{nameof(token)}值为{token}的弹窗组件:{nameof(LayDialogHost)}");
                    if (!DialogViews.ContainsKey(dialogName)) throw new Exception($"未找到{nameof(dialogName)}为{dialogName}的视图");
                    //抓取当前展示弹窗容器
                    LayDialogHost host = DialogHosts[token];
                    if (host.Items == null) throw new Exception($"当前{nameof(LayDialogHost)}元素尚未初始化完成");
                    host.TaskCompletion = new TaskCompletionSource<object>();
                    //创建内容视图
                    var view = Activator.CreateInstance(DialogViews[dialogName]) as UserControl;
                    //检查VM初始化被注入情况
                    if (DialogViewModels.ContainsKey(dialogName))
                    {
                        //创建VM
                        view.DataContext = Activator.CreateInstance(DialogViewModels[dialogName]);
                    }
                    //创建弹窗
                    LayDialogWindow dialogView = new LayDialogWindow(callback, host)
                    {
                        Content = view,
                        DataContext = view.DataContext
                    };
                    //抓取当前需要传递的参数并且传递给对应视图的ViewModel
                    if (!(view.DataContext is ILayDialogAware viewModel))
                        throw new NullReferenceException("对话框的 ViewModel 必须实现 IDialogAware 接口 ");
                    //给对应的ViewModel传值
                    ViewAndViewModelAction<ILayDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
                    dialogView.IsOpen = true;
                    await host.TaskCompletion.Task;
                }
                catch (Exception ex)
                {
                    Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                        ?.Log("Dialog", "当前对话框出现异常", ex);
                }
            });
        }


        /// <summary>
        /// VM转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <param name="action"></param>
        private static void ViewAndViewModelAction<T>(object view, Action<T> action)
        {
            try
            {
                if (view is T viewAsT)
                    action(viewAsT);

                if (view is StyledElement element && element.DataContext is T viewModelAsT)
                {
                    action(viewModelAsT);
                }
            }
            catch (Exception ex)
            {
                Logger.TryGet(LogEventLevel.Error, "LayUI-Avalonia")
                                       ?.Log("ViewAndViewModelAction", "VM转换异常", ex);
            }
        }
        /// <summary>
        /// 调用信息循环，模拟模态弹窗（该功能属于尝试功能）
        /// </summary>
        /// <param name="dialogName"></param>
        /// <param name="parameters"></param>
        /// <param name="callback"></param>
        /// <param name="token"></param>
        private void ShowDialog(string dialogName, ILayDialogParameter parameters, Action<ILayDialogResult> callback, string token)
        {
            using (var source = new CancellationTokenSource())
            {
                source.Cancel();
                TaskScheduler.FromCurrentSynchronizationContext();
                Dispatcher.UIThread.MainLoop(source.Token);
            }

        }
    }
}

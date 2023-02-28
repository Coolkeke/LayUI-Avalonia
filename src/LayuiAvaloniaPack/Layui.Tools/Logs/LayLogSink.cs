using Avalonia;
using Avalonia.Logging;
using DynamicData.Binding;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Logs
{
    /// <summary>
    /// 用于重写Avalonia内部Log记录方式
    /// </summary>
    public class LayLogSink : ILogSink
    {
        private ILayLogger? _Logger;
        private ILayLogger? Logger
        {
            get
            {
                return _Logger ?? (_Logger = AvaloniaLocator.Current.GetService<IContainerProvider>()?.Resolve<ILayLogger>());
            }
        }
        public bool IsEnabled(LogEventLevel level, string area)
        {
            if (level != LogEventLevel.Verbose) return true;
            return false;
        }
        public void Log(LogEventLevel level, string area, object? source, string messageTemplate)
        {
            log(level, area, source, messageTemplate, null, null, null);
        }

        public void Log<T0>(LogEventLevel level, string area, object? source, string messageTemplate, T0 propertyValue0)
        {
            log(level, area, source, messageTemplate, propertyValue0, null, null);
        }

        public void Log<T0, T1>(LogEventLevel level, string area, object? source, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            log(level, area, source, messageTemplate, propertyValue0, propertyValue1, null);
        }

        public void Log<T0, T1, T2>(LogEventLevel level, string area, object? source, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {

            log(level, area, source, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Log(LogEventLevel level, string area, object? source, string messageTemplate, params object?[] propertyValues)
        {

            log(level, area, source, messageTemplate, propertyValues);
        }
        /// <summary>
        /// Logger记录
        /// </summary>
        /// <param name="level">异常等级</param>
        /// <param name="area"></param>
        /// <param name="source"></param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        private void log(LogEventLevel level, string area, object? source, string messageTemplate, object?[] propertyValues)
        {
            switch (level)
            {
                case LogEventLevel.Debug:
                    Logger?.Debug($"{area}{source}{messageTemplate}{propertyValues}");
                    break;
                case LogEventLevel.Information:
                    Logger?.Info($"{area}{source}{messageTemplate}{propertyValues}");
                    break;
                case LogEventLevel.Warning:
                    Logger?.Warn($"{area}{source}{messageTemplate}{propertyValues}");
                    break;
                case LogEventLevel.Error:
                    Logger?.Error($"{area}{source}{messageTemplate}{propertyValues}");
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Logger记录
        /// </summary>
        /// <param name="level">异常等级</param>
        /// <param name="area"></param>
        /// <param name="source"></param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValue0"></param>
        /// <param name="propertyValue1"></param>
        /// <param name="propertyValue2"></param>
        private void log(LogEventLevel level, string area, object? source, string? messageTemplate, object? propertyValue0, object? propertyValue1, object? propertyValue2)
        {
            switch (level)
            {
                case LogEventLevel.Debug:
                    Logger?.Debug($"{area}{source}{messageTemplate}{propertyValue0}{propertyValue1}{propertyValue2}");
                    break;
                case LogEventLevel.Information:
                    Logger?.Info($"{area}{source}{messageTemplate}{propertyValue0}{propertyValue1}{propertyValue2}");
                    break;
                case LogEventLevel.Warning:
                    Logger?.Warn($"{area}{source}{messageTemplate}{propertyValue0}{propertyValue1}{propertyValue2}");
                    break;
                case LogEventLevel.Error:
                    Logger?.Error($"{area}{source}{messageTemplate}{propertyValue0}{propertyValue1}{propertyValue2}");
                    break;
                default:
                    break;
            }
        }
    }
}

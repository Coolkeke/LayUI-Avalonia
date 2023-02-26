using Avalonia.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Logs
{
    public class LayLogSink : ILogSink
    {
        public bool IsEnabled(LogEventLevel level, string area)
        {
            return true;
        }

        public void Log(LogEventLevel level, string area, object? source, string messageTemplate)
        {
           
        }

        public void Log<T0>(LogEventLevel level, string area, object? source, string messageTemplate, T0 propertyValue0)
        {
            
        }

        public void Log<T0, T1>(LogEventLevel level, string area, object? source, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
           
        }

        public void Log<T0, T1, T2>(LogEventLevel level, string area, object? source, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
          
        }

        public void Log(LogEventLevel level, string area, object? source, string messageTemplate, params object?[] propertyValues)
        {
           
        }
    }
}

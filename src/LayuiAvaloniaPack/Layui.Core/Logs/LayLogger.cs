using log4net;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Core
{
    public class LayLogger : ILayLogger
    {
        public void Debug(object message)
        {
            LogManager.GetLogger($"==>").Debug(message);
        }

        public void Error(object message)
        {
            LogManager.GetLogger($"==>").Error(message);
        }

        public void Info(object message)
        {
            LogManager.GetLogger($"==>").Info(message);
        }

        public void Warn(object message)
        {
            LogManager.GetLogger($"==>").Warn(message);

        }
        public void Dispose()
        {
            LogManager.Shutdown();
        }
    }
}

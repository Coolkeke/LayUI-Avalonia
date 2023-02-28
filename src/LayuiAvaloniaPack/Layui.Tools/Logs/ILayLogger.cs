using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Logs
{
    /// <summary>
    ///  ILayLogger
    /// <para>创建者:YWK</para>
    /// <para>创建时间:2022-08-17 下午 3:51:01</para>
    /// </summary>
    public interface ILayLogger
    {
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="exception"></param>
        void Error(object message);
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        void Warn(object message);
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        void Debug(object message);
        /// <summary>
        /// 释放Log线程
        /// </summary>
        void Dispose();
    }
}

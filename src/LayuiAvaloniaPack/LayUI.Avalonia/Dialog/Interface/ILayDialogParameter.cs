using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia
{
    public interface ILayDialogParameter
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(string key, object value);
        // <summary>
        /// 获取参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetValue<T>(string key);
    }
}

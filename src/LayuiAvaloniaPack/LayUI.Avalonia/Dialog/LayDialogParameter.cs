﻿using System;
using System.Collections.Generic;
using System.Text;
using LayUI.Avalonia.Interface;

namespace LayUI.Avalonia.Dialog
{
    public class LayDialogParameter : ILayDialogParameter
    {
        private readonly List<KeyValuePair<string, object>> _entries = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            _entries.Add(new KeyValuePair<string, object>(key, value));
        }
        // <summary>
        /// 获取参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            return _entries.GetValue<T>(key);
        }
    }
}

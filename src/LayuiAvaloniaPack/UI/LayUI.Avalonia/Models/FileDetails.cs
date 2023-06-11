using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Models
{
    /// <summary>
    /// 文件上传信息
    /// </summary>
    public class FileDetails
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
        public byte[] FileBytes { get; set; }
    }
}

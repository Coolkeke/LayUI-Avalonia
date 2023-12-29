using Avalonia;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interfaces
{
    /// <summary>
    /// 剪切板
    /// </summary>
    public interface ILayClipboard
    { 
        /// 复制
        /// </summary>
        /// <param name="data"></param>
        void Copy(object data);
        /// <summary>
        ///获取剪切板的内容
        /// </summary>
        /// <returns></returns>
        Task<string> GetTextAsync();
    }
}
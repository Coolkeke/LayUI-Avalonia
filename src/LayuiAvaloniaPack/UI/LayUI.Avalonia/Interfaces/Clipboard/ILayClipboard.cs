using Avalonia;
using System.Threading.Tasks;

namespace LayUI.Avalonia.Interfaces
{
    public interface ILayClipboard
    {
        /// <summary>
        /// 初始化剪切板
        /// </summary>
        /// <param name="visual"></param>
        void InitializeClipboard(Visual? visual);
        /// <summary>
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
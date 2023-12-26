using Avalonia;
using Avalonia.Controls; 
using Avalonia.Input.Platform;
using LayUI.Avalonia.Interfaces; 

namespace LayUI.Avalonia
{
    /// <summary>
    /// 剪切板
    /// </summary>
    public class LayClipboard : ILayClipboard
    {
        private IClipboard? Clipboard;
        public void InitializeClipboard(Visual? visual)
        {
            if (visual != null) Clipboard = TopLevel.GetTopLevel(visual)?.Clipboard;
        }
        public void Copy(object data)
        {
            Clipboard?.SetTextAsync(data.ToString());
        }

        public async Task<string> GetTextAsync()
        {
            if (Clipboard != null)
            {
                var text = await Clipboard.GetTextAsync();
                return string.IsNullOrEmpty(text) == true ? await Task.FromResult(string.Empty) : await Task.FromResult(text);
            }
            return await Task.FromResult(string.Empty);
        }
    }
}

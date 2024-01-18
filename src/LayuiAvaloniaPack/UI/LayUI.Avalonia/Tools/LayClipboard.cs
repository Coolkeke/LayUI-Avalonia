 using LayUI.Avalonia.Interfaces; 

namespace LayUI.Avalonia
{
    /// <summary>
    /// 剪切板
    /// </summary>
    public class LayClipboard : ILayClipboard
    { 
        public void Copy(object data)
        {
            LayKeyboardHelper.TopLevel?.Clipboard?.SetTextAsync(data?.ToString());
        }

        public async Task<string> GetTextAsync()
        { 
            if (LayKeyboardHelper.TopLevel?.Clipboard==null) return await Task.FromResult(string.Empty);
            var text = await LayKeyboardHelper.TopLevel.Clipboard.GetTextAsync();
            return string.IsNullOrEmpty(text) == true ? await Task.FromResult(string.Empty) : await Task.FromResult(text);
        }
    }
}

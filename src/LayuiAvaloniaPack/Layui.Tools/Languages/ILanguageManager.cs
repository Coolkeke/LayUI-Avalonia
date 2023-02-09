using Layui.Tools.Models;

namespace Layui.Tools.Languages
{
    public interface ILanguageManager
    {
        LanguageModel CurrentLanguage { get; }

        LanguageModel DefaultLanguage { get; }

        IEnumerable<LanguageModel> AllLanguages { get; }

        void SetLanguage(string languageCode);

        void SetLanguage(LanguageModel languageModel);
    }
}

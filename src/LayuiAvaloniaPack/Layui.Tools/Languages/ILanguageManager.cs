using Layui.Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui.Tools.Models
{
    public class LanguageModel
    {
        public string Name { get; }

        public string NativeName { get; }

        public string Code { get; }

        public LanguageModel(string name, string nativeName, string code)
        {
            Name = name;
            NativeName = nativeName;
            Code = code;
        }
    }
}

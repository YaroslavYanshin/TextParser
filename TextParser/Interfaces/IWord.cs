using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Struct;

namespace TextParser.Interfaces
{
    public interface IWord : ISentenceItem
    {
        Symbol[] Symbols { get; }

        Symbol this[int index] { get; }

        int Lenght { get; }

        bool IsConsonant(string[] vowels);
    }
}

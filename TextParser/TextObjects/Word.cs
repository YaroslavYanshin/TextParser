using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Interfaces;
using TextParser.Struct;

namespace TextParser.TextObjects
{
    public class Word: IWord
    {

        public Word(string chars)
        {
            Symbols = chars?.Select(x => new Symbol(x)).ToArray();
            Length = Symbols?.Length ?? 0;
        }

        public string Chars
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var symbol in Symbols)
                {
                    sb.Append(symbol.Chars);
                }

                return sb.ToString();
            }
        }

        public Symbol[] Symbols { get; }

        public Symbol this[int index] => Symbols[index];

        public int Length { get; }

        public bool IsConsonant(string[] vowels)
        {
            return !vowels.Contains(Symbols[0].Chars);
        }

    }
}

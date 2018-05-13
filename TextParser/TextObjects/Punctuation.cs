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
    public class Punctuation : IPunctuation
    {
        public Symbol Symbols { get; }

        public string Chars => Symbols.Chars;
        public Punctuation(string chars)
        {
            Symbols = new Symbol(chars);
        }
    }
}

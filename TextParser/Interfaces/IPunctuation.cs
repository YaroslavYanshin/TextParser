using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Struct;

namespace TextParser.Interfaces
{
    interface IPunctuation : ISentenceItem
    {
        Symbol Symbol { get; }
    }
}

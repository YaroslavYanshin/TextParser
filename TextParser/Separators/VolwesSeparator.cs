using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.Separators
{
    class VolwesSeparator
    {
        public static string[] RussianVolwesSeparator { get; } = { "а", "у", "о", "ы", "и", "э", "я", "ю", "ё", "е" };
        public static string[] EnglishVolwesSeparator { get; } = {"a", "e", "i", "o", "u", "y"};
    }
}

using System.IO;
using TextParser.Interfaces;

namespace TextParser.Parser
{
    public abstract class Parser
    {
        public abstract Text Parse (StreamReader fileReader);
        public abstract ISentence ParseSentence(string sentence);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.Interfaces
{
    public interface ISentence
    {
        IList<ISentenceItem> Items { get; }
        bool IsInterrogative { get; }
        ISentence RemoveWordsBy(Func<IWord, bool> predicate);
        IEnumerable<ISentenceItem> ReplaceWordByElements(Func<IWord, bool> predicate, IList<ISentenceItem> items);
        IEnumerable<IWord> GetWordsWithoutRepetition(int length);
        string SentenceToString();
    }
}

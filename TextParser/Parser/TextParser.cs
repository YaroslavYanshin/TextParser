using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextParser.Struct;
using TextParser.Parser;
using TextParser.Helpers;
using TextParser.Interfaces;
using TextParser.Separators;
using TextParser.TextObjects;

namespace TextParser.Parser
{
    public class TextParser: Parser
    {
        private readonly Regex _lineToSentenceRegex = new Regex(@"(?<=[\.*!\?])\s+(?=[А-Я]|[A-Z])|(?=\W&([А-Я]|[A-Z]))", RegexOptions.Compiled);

        private readonly Regex _sentenceToWordsRegex = new Regex(@"(\W*)(\w+[\-|`]\w+)(\!\=|\>\=|\=\<|\/|\=\=|\?\!|\!\?|\.{3}|\W)|(\W*)(\w+|\d+)(\!\=|\>\=|\=\<|\/|\=\=|\?\!|\!\?|\.{3}|\W)|(.*)",RegexOptions.Compiled);

        public override Text Parse(StreamReader fileReader)
        {
            var textResult = new Text();

            try
            {
                string line;
                string buffer = null;

                while ((line = fileReader.ReadLine()) != null)
                {
                    if (Regex.Replace(line.Trim(), @"\s+", @" ") != "")
                    {
                        line = buffer + line;

                        var sentences = _lineToSentenceRegex.Split(line)
                            .Select(x => Regex.Replace(x.Trim(), @"\s+", @" "))
                            .ToArray();

                        if (!PunctuationSeparator.EndPunctuationSeparator.Contains(sentences.Last().Last().ToString()))
                        {
                            buffer = sentences.Last();
                            textResult.Sentences.AddRange(sentences.Select(x => x).Where(x => x != sentences.Last())
                                .Select(ParseSentence));
                        }
                        else
                        {
                            textResult.Sentences.AddRange(sentences.Select(ParseSentence));
                            buffer = null;
                        }
                    }
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception.Data.ToString());
                fileReader.Close();
            }
            finally
            {
                fileReader.Close();
                fileReader.Dispose();
            }

            return textResult;
        }

        public override ISentence ParseSentence(string sentence)
        {
            var result = new Sentence();

            Func<string,ISentenceItem> ToISentenceItem = item => (!PunctuationSeparator.AllSentenceSeparators.Contains(item) &&
                !DigitSeparator.ArabicDigits.Contains(item[0].ToString()))
                ? (ISentenceItem)new Word(item)
                    : (DigitSeparator.ArabicDigits.Contains(item[0].ToString()))
                        ? (ISentenceItem)new Digit(item)
                            : new Punctuation(item);

            foreach (Match match in _sentenceToWordsRegex.Matches(sentence))
            {
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    if (match.Groups[i].Value.Trim() != "")
                    {
                        result.Items.Add(ToISentenceItem(match.Groups[i].Value.Trim()));
                    }
                }
            }

            return result;
        }

    }

    
}

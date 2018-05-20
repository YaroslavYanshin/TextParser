using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Parser;
using TextParser.Helpers;
using TextParser.Interfaces;
using TextParser.Separators;
using TextParser.Struct;
using TextParser.TextObjects;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {
            const string line = "||============================================================||";
            string readText = ConfigurationManager.AppSettings["ReadText"];
            var parser = new Parser.TextParser();
            string FileSentence = ConfigurationManager.AppSettings["Sentence"];
            string FileWord = ConfigurationManager.AppSettings["Word"];
            string FileSentencesWithoutConsonants = ConfigurationManager.AppSettings["SentencesWithoutConsonants"];
            string FileReplaceWordInSentence = ConfigurationManager.AppSettings["ReplaceWordInSentence"];
            using (var streamReader = new StreamReader(readText, Encoding.Default))
            {
                var text = parser.Parse(streamReader);
                //1 Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.
                foreach (var sentence in text.GetSentencesInAscendingOrder())
                {
                    Console.WriteLine(sentence.SentenceToString());
                }

                using (StreamWriter writeSentence = new StreamWriter(FileSentence, false, Encoding.Default))
                {
                    foreach (var sentence in text.GetSentencesInAscendingOrder())
                    {
                        writeSentence.WriteLine(sentence.SentenceToString());
                    }
                }

                Console.WriteLine(line);
                //2 Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.
                foreach (var word in text.GetWordsFromInterrogativeSentences(3))
                {
                    Console.WriteLine(word.Chars);
                }

                using (StreamWriter writeWord = new StreamWriter(FileWord,false,Encoding.Default))
                {
                    foreach (var word in text.GetWordsFromInterrogativeSentences(3))
                    {
                        writeWord.WriteLine(word.Chars);
                    }
                }

                Console.WriteLine(line);


                //3 Из текста удалить все слова заданной длины, начинающиеся на согласную букву.
                text.SentencesWithoutConsonants(3);
                Console.WriteLine(text.TextToString());
                using (StreamWriter writeSentencesWithoutConsonants = new StreamWriter(FileSentencesWithoutConsonants,false,Encoding.Default))
                {
                    writeSentencesWithoutConsonants.WriteLine(text.TextToString());
                }
                Console.WriteLine(line);

                //4 В некотором предложении текста слова заданной длины заменить указанной подстрокой, 
                //длина которой может не совпадать с длиной слова.

                text.ReplaceWordInSentence(0, 4, "string, with different elements", parser.ParseSentence);

                Console.WriteLine(text.TextToString());
                using (StreamWriter writerReplaceWordInSentence = new StreamWriter(FileReplaceWordInSentence,false,Encoding.Default))
                {
                    writerReplaceWordInSentence.WriteLine(text.TextToString());
                }

                Console.WriteLine(line);
            }

            Console.ReadKey();
        }
    }
}

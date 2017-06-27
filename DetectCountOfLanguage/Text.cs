using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DetectCountOfLanguage
{
    public class Text
    {
        private char[] m_finishOfSentence = new char[]
        {
            '.','!', '?', ';', '-', '¿', '¡',':', '–', '"', '(', ')', '[', ']', '/', '\n', '\r'
        };

        private char[] m_punctuation = new char[]
        {
            ',',':','-'
        };


        private int m_countOfNumber = 0;
        public int CountOfNumber { get => m_countOfNumber; set => m_countOfNumber = value; }


        public Text()
        { }

        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public List<string> DivedetIntoSentences(string text)
        {
            return text.Split(m_finishOfSentence)
                .Where(s => !String.IsNullOrWhiteSpace(s)).ToList();
        }
        public string RemoveNumbers(string text)
        {

            List<string> tmp = text.Split(' ', '\t', '\n', '\r').ToList();

            List<string> words = tmp.Where(w =>
            w.Any(c => Char.IsLetter(c)) || w.All(c => !Char.IsDigit(c))).ToList();
            CountOfNumber += tmp.Count - words.Count;

            string result = String.Join(" ", words.ToArray());

            result.Trim();

            return result;
        }

        public List<string> RemoveNumbers(List<string> text)
        {
            for (int i = 0; i < text.Count; i++)
            {
                List<string> tmp = text[i].Split(' ', '\t', '\n').ToList();

                List<string> words = tmp.Where(w => w.All(c => !Char.IsDigit(c))).ToList();
                CountOfNumber += tmp.Count - words.Count;

                text[i] = "";
                words.ForEach(w => text[i] += w + " ");
                text[i].Trim();
            }
            return text;
        }

        public List<string> RemovePunctuation(List<string> text)
        {
            for (int i = 0; i < text.Count; i++)
            {
                //      text[i] = text[i].Select(c => c.Equals(m_punctuation.Any())).ToString();
                string s = text[i];
                foreach (var c in m_punctuation)
                {
                    if (s.Contains(c))
                        s = s.Remove(s.IndexOf(c), 1);
                    //  text[i] = text[i].Remove(text[i].IndexOf(c), 1);
                }
                text[i] = s;
            }
            return text;
        }

        public static int GetCountOfWords(string text)
        {
            string[] textArray = text.Split(new char[] { ' ' });

            return textArray.Length;
        }


    }

}

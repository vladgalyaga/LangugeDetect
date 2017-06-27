using LiteMiner.classes;
using Microsoft.WindowsAPICodePack.ExtendedLinguisticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;
using IvanAkcheurov.NTextCat.Lib;

namespace DetectCountOfLanguage
{
    public class LanguageDetectorWrapper
    {

        private object locker = new object { };
        Dictionary<string, int> m_wordCount = new Dictionary<string, int>();
        private int m_threadsCount;
        private List<string> m_text;
        LanguageDetector ld = new LanguageDetector();

        public LanguageDetectorWrapper(List<string> text, int threadsCount)
        {
            m_text = text;
            m_threadsCount = threadsCount;
        }
        public Dictionary<string, int> DetectLanguageInOneThread()
        {
            m_wordCount = new Dictionary<string, int>();
            DetectWordCountOnLanguage();
            return m_wordCount;
        }

        public Dictionary<string, int> DetectLanguageInThreads()
        {

            m_wordCount = new Dictionary<string, int>();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < m_threadsCount; i++)
            {
                int a = i;
                Task t = new Task(() => DetectLanguage(a));
                tasks.Add(t);
                t.Start();
            }
            Task.WaitAll(tasks.ToArray());

            return m_wordCount;
        }
        private void DetectLanguage(int numberCount)
        {
            for (int i = numberCount; i < m_text.Count; i += m_threadsCount)
            {
                DetectLanguage(m_text[i]);
            }
        }

        public void DetectLanguage(string text)
        {

            int count = Text.GetCountOfWords(text);

            string lanCode = ld.Detect(text);
            string languageNaturalName;
            if (lanCode == null)
            {
                languageNaturalName = "Not detected";
            }
            else
            {
                languageNaturalName = ld.GetLanguageNameByCode(lanCode);
            }
            lock (locker)
            {
                if (m_wordCount.ContainsKey(languageNaturalName))
                {
                    m_wordCount[languageNaturalName] += count;
                }
                else
                {
                    m_wordCount.Add(languageNaturalName, count);
                }
            }
        }

        public void DetectWordCountOnLanguage()
        {
            while (m_text.Count > 0)
                DetectWordCountOnLanguage(null);
        }
        public string DetectWordCountOnLanguage(string oldText)
        {

            string text = String.Join(" ", oldText, m_text[0]);
            m_text.RemoveAt(0);
            int count = Text.GetCountOfWords(text);

            string lanCode = ld.Detect(text);
            string languageNaturalName = null;
            if (lanCode == null)
            {
                languageNaturalName = DetectWordCountOnLanguage(text);

            }
            else
            {
                languageNaturalName = ld.GetLanguageNameByCode(lanCode);
            }
            if (m_wordCount.ContainsKey(languageNaturalName))
            {
                m_wordCount[languageNaturalName] += count;

            }
            else
            {
                m_wordCount.Add(languageNaturalName, count);
            }
            return languageNaturalName;


        }
        public void NewDetector(string oldText)
        {
            string text = String.Join(" ", oldText, m_text[0]);
            m_text.RemoveAt(0);
           
            int count = Text.GetCountOfWords(text);
            var languageDetection = new MappingService(MappingAvailableServices.LanguageDetection);
            using (MappingPropertyBag bag = languageDetection.RecognizeText(text, null))
            {
                MappingDataRange[] ranges = bag.GetResultRanges();
                if (ranges.First().FormatData(new StringArrayFormatter()).Length == 0)
                {

                    return;
                }
               string lang = ranges.First().
                    FormatData(new StringArrayFormatter()).First();
                AddPointToLang(lang, count);
                //if (m_wordCount.ContainsKey(lang))
                //{
                //    m_wordCount[lang] += count;
                //}
                //else
                //{
                //    m_wordCount.Add(lang, count);
                //}
            }

        }
        public Dictionary<string, int> NewDetector()
        {
            while (m_text.Count > 0)
            {
                NewDetector(null);
               
            }
            return m_wordCount;
        }

        public Dictionary<string, int> PytonDetector()
        {
            var engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

           

            engine.ExecuteFile(@"..\..\langdetect-1.0.7\langdetect\detector_factory.py");
           dynamic function = scope.GetVariable("detect");

            dynamic result = function(m_text[1]);


            throw new NotImplementedException();
        }

        public void DetectNcat(int a)
        {
            var factory = new RankedLanguageIdentifierFactory();
            var identifier = factory.Load("Core14.profile.xml");

            for (int i = a; i < m_text.Count; i += m_threadsCount)
            {
                string text = m_text[i];

                int count = Text.GetCountOfWords(text);

                
                string res = identifier.Identify(text).First().Item1.Iso639_2T;

                AddPointToLang(res, count);
                //m_wordCount.Add(res)
               
            }
        }
           // return m_wordCount;
        
        public Dictionary<string, int> DetectLanguageInThreadsNText()
        {

            m_wordCount = new Dictionary<string, int>();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < m_threadsCount; i++)
            {
                int a = i;
                Task t = new Task(() => DetectNcat(a));
                tasks.Add(t);
                t.Start();
            }
            Task.WaitAll(tasks.ToArray());

            return m_wordCount;
        }


        private void AddPointToLang(string lang, int count)
        {
            if (m_wordCount.ContainsKey(lang))
            {
                m_wordCount[lang] += count;
            }
            else
            {
                m_wordCount.Add(lang, count);
            }
        }

    }
}

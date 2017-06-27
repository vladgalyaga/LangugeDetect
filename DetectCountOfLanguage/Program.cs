
using IvanAkcheurov.NTextCat.Lib;
using IvanAkcheurov.NTextCat.Lib.Legacy;
using LiteMiner.classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DetectCountOfLanguage
{
    class Program
    {

        //AdmAccessToken admToken;
        static void Main(string[] args)
        {
            //    Console.WriteLine("Input full name of file");
            //    string path = Console.ReadLine();
            string path = @"C:\Users\zviad\Desktop\martin_dzhordzh_pir_stervyatnikov.txt";
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            int threadsCount = 4;
            //Text model = new Text(path);
            //var a = model.RemoveNumbers(model.DivedetIntoSentences(model.Value));

            //a.ForEach(i => Console.WriteLine(i));
            //Console.WriteLine("count of number =" + model.CountOfNumber);
            //model.RemovePunctuation(a).ForEach(s => Console.WriteLine(s));
            //Console.WriteLine(a.Count);
            //Console.ReadKey();



            Text model = new Text();
            string text = model.ReadFile(path);

            text = model.RemoveNumbers(text);
            List<string> sentences = model.DivedetIntoSentences(text);
            sentences = model.RemovePunctuation(sentences);

            Stopwatch w = new Stopwatch();
            w.Start();
            LanguageDetectorWrapper ldw = new LanguageDetectorWrapper(sentences, threadsCount);
            wordCount = ldw.PytonDetector();
         
            //wordCount = ldw.DetectLanguageInOneThread();
            w.Stop();
            Console.WriteLine(w.ElapsedMilliseconds);
            //foreach (var s in sentences)
            //{
            //    int count = model.GetCountOfWords(s);

            //    LanguageDetector ld = new LanguageDetector();
            //    string lanCode = ld.Detect(s);
            //    string languageNaturalName;
            //    if (lanCode == null)
            //    {
            //        languageNaturalName = "Not detected";
            //    }
            //    else
            //    {
            //        languageNaturalName = ld.GetLanguageNameByCode(lanCode);
            //    }

            //    if (wordCount.ContainsKey(languageNaturalName))
            //    {
            //        wordCount[languageNaturalName] += count;
            //    }
            //    else
            //    {
            //        wordCount.Add(languageNaturalName, count);
            //    }

            //}
            foreach (var t in wordCount)
            {
                Console.WriteLine(t.Key + " = " + t.Value);
            }





            //LanguageDetector ld = new LanguageDetector();
            //string lanCode = ld.Detect(" Кит Харриман вот уже двенадцать лет возглавлявший Исследовательский центр фирмы «Юнайтед Стейтс Роботс энд Мекэникл Мен Корпорейшн», совершенно не был уверен, что поступает правильно. Он провел кончиком языка по пухлым, но довольно бледным губам, и ему показалось, что голографическое изображение великой Сьюзен Кэлвин, сурово взиравшей на него сверху вниз, никогда еще не было таким хмурым. Обычно он загораживал чем - нибудь голографию величайшего в истории робопсихолога, потому что она действовала ему на нервы.Однако на сей раз он не осмелился даже на это, и ее пронзительный умерший взгляд буравил ему щеку.");
            //if (lanCode == null) throw new Exception("Cannot detect language");
            //string languageNaturalName = ld.GetLanguageNameByCode(lanCode); //returns "English" for language
            //Console.WriteLine(languageNaturalName);
            Console.WriteLine(model.CountOfNumber);

            Console.ReadKey();


        }
    }
}







//            LanguageIdentifier languageIdentifier = new LanguageIdentifier();
//            IEnumerable<Tuple<LanguageInfo, double>> languages = languageIdentifier.ClassifyText("لله والصلوات والطيبات، السلام عليك أيها النبي ورحمة لله وبركاته، السلام علينا و على عباد الله الصالحين، أشهد أن لا إله ", null).ToList();
//            var mostCertainLanguage = languages.FirstOrDefault();
//            if (mostCertainLanguage != null)
//                Console.WriteLine("Language of text is {0} with uncertainty {1}", mostCertainLanguage.Item1, mostCertainLanguage.Item2);
//            else
//                Console.WriteLine("Language couldn’t be identified with acceptable degree of certainty");

//            Console.ReadKey();


//            //  AdmAccessToken admToken;
//            AdmAccessToken admToken;
//            string headerValue;
//            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
//            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
//            AdmAuthentication admAuth = new AdmAuthentication("clientID", "client secret");
//            try
//            {
//                admToken = admAuth.GetAccessToken();
//                // Create a header with the access_token property of the returned token
//                headerValue = "Bearer " + admToken.access_token;
//                DetectMethod(headerValue);
//            }
//            catch (WebException e)
//            {
//                // ProcessWebException(e);
//                Console.WriteLine("Press any key to continue...");
//                Console.ReadKey(true);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                Console.WriteLine("Press any key to continue...");
//                Console.ReadKey(true);
//            }
//            Console.ReadKey();


//        }
//        private static void DetectMethod(string authToken)
//        {
//            Console.WriteLine("Enter Text to detect language:");
//            string textToDetect = "Enter Text to detect language";
//            //Keep appId parameter blank as we are sending access token in authorization header.
//            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Detect?text=" + textToDetect;
//            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
//            httpWebRequest.Headers.Add("Authorization", authToken);
//            WebResponse response = null;
//            try
//            {
//                response = httpWebRequest.GetResponse();
//                using (Stream stream = response.GetResponseStream())
//                {
//                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
//                    string languageDetected = (string)dcs.ReadObject(stream);
//                    Console.WriteLine(string.Format("Language detected:{0}", languageDetected));
//                    Console.WriteLine("Press any key to continue...");
//                    Console.ReadKey(true);
//                }
//            }
//            catch
//            {
//                throw;
//            }
//            finally
//            {
//                if (response != null)
//                {
//                    response.Close();
//                    response = null;
//                }
//            }
//        }
//        [DataContract]
//        public class AdmAccessToken
//        {
//            [DataMember]
//            public string access_token { get; set; }
//            [DataMember]
//            public string token_type { get; set; }
//            [DataMember]
//            public string expires_in { get; set; }
//            [DataMember]
//            public string scope { get; set; }
//        }
//        public class AdmAuthentication
//        {
//            public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
//            private string clientId;
//            private string clientSecret;
//            private string request;
//            private AdmAccessToken token;
//            private Timer accessTokenRenewer;
//            //Access token expires every 10 minutes. Renew it every 9 minutes only.
//            private const int RefreshTokenDuration = 9;
//            public AdmAuthentication(string clientId, string clientSecret)
//            {
//                this.clientId = clientId;
//                this.clientSecret = clientSecret;
//                //If clientid or client secret has special characters, encode before sending request
//                this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
//                this.token = HttpPost(DatamarketAccessUri, this.request);
//                //renew the token every specified minutes
//                accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback), this, TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
//            }
//            public AdmAccessToken GetAccessToken()
//            {
//                return this.token;
//            }
//            private void RenewAccessToken()
//            {
//                AdmAccessToken newAccessToken = HttpPost(DatamarketAccessUri, this.request);
//                //swap the new token with old one
//                //Note: the swap is thread unsafe
//                this.token = newAccessToken;
//                Console.WriteLine(string.Format("Renewed token for user: {0} is: {1}", this.clientId, this.token.access_token));
//            }
//            private void OnTokenExpiredCallback(object stateInfo)
//            {
//                try
//                {
//                    RenewAccessToken();
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(string.Format("Failed renewing access token. Details: {0}", ex.Message));
//                }
//                finally
//                {
//                    try
//                    {
//                        accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine(string.Format("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
//                    }
//                }
//            }
//            private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
//            {
//                //Prepare OAuth request 
//                WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
//                webRequest.ContentType = "application/x-www-form-urlencoded";
//                webRequest.Method = "POST";
//                byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
//                webRequest.ContentLength = bytes.Length;
//                using (Stream outputStream = webRequest.GetRequestStream())
//                {
//                    outputStream.Write(bytes, 0, bytes.Length);
//                }
//                using (WebResponse webResponse = webRequest.GetResponse())
//                {
//                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
//                    //Get deserialized object from JSON stream
//                    AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
//                    return token;
//                }
//            }
//        }
//    }
//}


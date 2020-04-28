using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;

namespace RuiConsole
{
    internal class Program
    {
        private static readonly string GoogleApiKey = "YOUR_GOOGLE_API";
        private static readonly string ToolbarPath = "2Shapes.rui";
        private static readonly string ToolbarPathCopy = "2Shapes (translated).rui";

        private static void Main(string[] args)
        {
            var service = new TranslateService(new BaseClientService.Initializer {ApiKey = GoogleApiKey});
            var client = new TranslationClientImpl(service);
            var results = new List<string>();

            string line;
            var file = new StreamReader(ToolbarPath);
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);

                if (line.Contains("locale_1033"))
                {
                    results.Add(line);

                    var onlyText = RemoveTags(line);

                    var translated = client.TranslateText(onlyText, "es", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1034"));

                    translated = client.TranslateText(onlyText, "fr", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1036"));

                    translated = client.TranslateText(onlyText, "it", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1040"));

                    translated = client.TranslateText(onlyText, "pt", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "2070"));

                    translated = client.TranslateText(onlyText, "de", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1031"));

                    translated = client.TranslateText(onlyText, "zh-cn", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "2052"));

                    translated = client.TranslateText(onlyText, "zh-tw", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1028"));

                    translated = client.TranslateText(onlyText, "ja", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1041"));

                    translated = client.TranslateText(onlyText, "cs", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1029"));

                    translated = client.TranslateText(onlyText, "ru", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1049"));

                    translated = client.TranslateText(onlyText, "pl", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1045"));

                    translated = client.TranslateText(onlyText, "ko", "en", null);
                    results.Add(AddTags(translated.TranslatedText, "1042"));
                }
                else
                {
                    results.Add(line);
                }
            }

            file.Close();
            var translatedRui = new StreamWriter(ToolbarPathCopy);
            foreach (var lineTranslated in results) translatedRui.WriteLine(lineTranslated);

            translatedRui.Close();
        }

        private static string RemoveTags(string original)
        {
            return original.Replace("<locale_1033>", "").Replace("</locale_1033>", "");
        }

        private static string AddTags(string original, string languageCode)
        {
            return $"<locale_{languageCode}>{original}</locale_{languageCode}>";
        }
    }
}
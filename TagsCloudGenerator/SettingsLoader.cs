using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace TagsCloudGenerator
{
    internal class SettingsLoader
    {
        private static JObject _jsonConfig;

        private static Color[] Colors =>
            _jsonConfig["colors"]
                .Select(item => (string) item)
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(ColorTranslator.FromHtml)
                .ToArray();

        private static Dictionary<string, string> SpellingDictionaries =>
            _jsonConfig["dictionaries"]
                .ToDictionary(
                    token => token[0].ToString(),
                    token => token[1].ToString());

        private static string FontName => _jsonConfig["fontName"].ToString();

        private static int TagsCount => (int) _jsonConfig["tagsCount"];

        private static int MinFontSize => _jsonConfig["fontSize"].Select(token => (int) token).ToArray()[0];

        private static int MaxFontSize => _jsonConfig["fontSize"].Select(token => (int) token).ToArray()[1];

        private static int Width => _jsonConfig["size"].Select(token => (int) token).ToArray()[0];

        private static int Height => _jsonConfig["size"].Select(token => (int) token).ToArray()[1];

        public static Settings LoadFromFile(string pathToConfig)
        {
            _jsonConfig = JObject.Parse(File.ReadAllText(pathToConfig));
            return new Settings
            {
                Colors = Colors,
                Height = Height,
                Width = Width,
                MinFontSize = MinFontSize,
                MaxFontSize = MaxFontSize,
                FontName = FontName,
                TagsCount = TagsCount,
                SpellingDictionaries = SpellingDictionaries
            };
        }
    }
}
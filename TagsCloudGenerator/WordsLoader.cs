using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudGenerator
{
    internal static class WordsLoader
    {
        public static IReadOnlyList<string> LoadWordsFromTxt(string pathToWords) =>
            File.ReadLines(pathToWords)
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(x => x.Trim())
                .ToList();

        public static HashSet<string> LoadBlackListFromTxt(string pathToBlackList) =>
            new HashSet<string>(LoadWordsFromTxt(pathToBlackList));
    }
}
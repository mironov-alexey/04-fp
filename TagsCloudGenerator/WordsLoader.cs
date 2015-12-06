using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudGenerator
{
    internal class WordsLoader
    {
        public static IEnumerable<string> LoadWordsFromTxt(string pathToWords) =>
            File.ReadLines(pathToWords)
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(x => x.Trim());

        public static HashSet<string> LoadBlackListFromTxt(string pathToBlackList) => 
            new HashSet<string>(LoadWordsFromTxt(pathToBlackList));
    }
}
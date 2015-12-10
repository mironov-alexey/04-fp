using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudGenerator
{
    internal static class WordsLoader
    {
        private static readonly Dictionary<string, Func<string, IReadOnlyList<string>>> LoadingFuncs = 
            new Dictionary<string, Func<string, IReadOnlyList<string>>>
        {
            {".txt", LoadFromTxt },
        };
        private static IReadOnlyList<string> LoadFromTxt(string path)
        {
            try
            {
                return File.ReadLines(path)
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(x => x.Trim())
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return new List<string>();
            }
        }
        public static IReadOnlyList<string> LoadWords(string pathToWords)
        {
            if (string.IsNullOrEmpty(pathToWords))
                throw new ArgumentException("The path to file does not be null or empty .");
            if (!File.Exists(pathToWords))
                throw new FileNotFoundException("The file specified does not exist.");
            return LoadingFuncs[Path.GetExtension(pathToWords)](pathToWords);
        }

        public static HashSet<string> LoadBlackList(string pathToBlackList) =>
            new HashSet<string>(LoadWords(pathToBlackList));
    }
}
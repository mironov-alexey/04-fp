using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudGenerator
{
    internal static class WordsLoader
    {
        public static IReadOnlyList<string> LoadWordsFromTxt(string pathToWords)
        {
            if (Path.GetExtension(pathToWords) != ".txt")
                throw new ArgumentException("The file must have the .txt extension.");
            if (!File.Exists(pathToWords))
                throw new FileNotFoundException("The file specified does not exist.");
            try
            {
                return File.ReadLines(pathToWords)
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

        public static HashSet<string> LoadBlackListFromTxt(string pathToBlackList) =>
            new HashSet<string>(LoadWordsFromTxt(pathToBlackList));
    }
}
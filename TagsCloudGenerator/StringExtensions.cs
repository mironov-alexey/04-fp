using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator
{
    public static class StringExtensions
    {
        public static IReadOnlyList<string> FilterBannedWords(this IEnumerable<string> words, HashSet<string> blackList)
            =>
                words.Where(w => !blackList.Contains(w)).ToList();

        public static IReadOnlyList<string> FilterBannedWords(this IEnumerable<string> words, string pathToBlackList) =>
            FilterBannedWords(words, WordsLoader.LoadBlackList(pathToBlackList));

        public static Statistic Calculate(this IEnumerable<string> words, Settings settings) =>
            Statistic.Calculate(words, settings);
    }
}
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator
{
    public static class StringExtensions
    {
        public static IEnumerable<string> FilterBannedWords(this IEnumerable<string> words, HashSet<string> blackList)
            =>
                words.Where(w => !blackList.Contains(w));
    }
}
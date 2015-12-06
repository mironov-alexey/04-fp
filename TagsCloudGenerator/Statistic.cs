using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator
{
    internal class Statistic
    {
        public List<Word> WordsWithFrequency{ get; }

        private Statistic(IEnumerable<Word> wordsWithFrequency)
        {
            WordsWithFrequency = wordsWithFrequency.ToList();
            MaxCount = WordsWithFrequency.Max(w => w.Frequency);
            MinCount = WordsWithFrequency.Min(w => w.Frequency);
        }

        public int MinCount{ get; }

        public int MaxCount{ get; }

        private static readonly Random Random = new Random();
        public static Statistic Calculate(IEnumerable<string> words, HashSet<string> blackList, Settings settings)
        {
            var wordsWithFreq = words
                .FilterBannedWords(blackList)
                .GroupBy(w => w)
                .OrderByDescending(g => g.Count())
                .Take(settings.TagsCount)
                .OrderByDescending(g => Random.Next())
                .Select(g => new Word(g.First(), g.Count()))
                .ToList();
            return new Statistic(wordsWithFreq);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator
{
    internal class Statistic
    {
        private static readonly Random Random = new Random();

        private Statistic(IEnumerable<Word> wordsWithFrequency)
        {
            WordsWithFrequency = wordsWithFrequency.ToList();
            MaxCount = WordsWithFrequency.Max(w => w.Frequency);
            MinCount = WordsWithFrequency.Min(w => w.Frequency);
        }

        public List<Word> WordsWithFrequency{ get; }

        public int MinCount{ get; }

        public int MaxCount{ get; }

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
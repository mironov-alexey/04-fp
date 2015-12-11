using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudGenerator
{
    internal class Statistic
    {
        private Statistic(IReadOnlyList<Word> wordsWithFrequency)
        {
            WordsWithFrequency = wordsWithFrequency;
            MaxCount = WordsWithFrequency.Max(w => w.Frequency);
            MinCount = WordsWithFrequency.Min(w => w.Frequency);
        }

        public IReadOnlyList<Word> WordsWithFrequency{ get; }

        public int MinCount{ get; }

        public int MaxCount{ get; }

        public static Statistic Calculate(IReadOnlyList<string> words, Settings settings)
        {
            var random = new Random();
            var wordsWithFreq = words
                .GroupBy(w => w)
                .OrderByDescending(g => g.Count())
                .Take(settings.TagsCount)
                .OrderByDescending(g => random.Next())
                .Select(g => new Word(g.First(), g.Count()))
                .ToList();
            return new Statistic(wordsWithFreq);
        }
    }
}
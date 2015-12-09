using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TagsCloudGenerator.Tests
{
    [TestFixture]
    public class StatisticCalculatorShould
    {
        [SetUp]
        public void SetUp()
        {
            _settings = new Settings
            {
                TagsCount = 3
            };
            _words = new List<string> {"a", "b", "b", "c", "c", "c"};
        }

        private Settings _settings;
        private List<string> _words;

        [Test]
        public void CorrectlyCalculate_MaxWordCount()
        {
            var statistic = Statistic.Calculate(_words, new HashSet<string>(), _settings);
            Assert.AreEqual(3, statistic.MaxCount);
        }

        [Test]
        public void CorrectlyCalculate_MinWordCount()
        {
            var statistic = Statistic.Calculate(_words, new HashSet<string>(), _settings);
            Assert.AreEqual(1, statistic.MinCount);
        }

        [Test]
        public void CorrectlyCalculate_WordFrequency()
        {
            var actualFrequencies = new List<int>();
            const int size = 1000;
            for (var i = 1; i <= size; i++)
            {
                var inputWords = string.Join(" ", Enumerable.Range(0, i).Select(_ => "a")).Split().ToList();
                var statistic = Statistic.Calculate(inputWords, new HashSet<string>(), _settings);
                actualFrequencies.Add(statistic.WordsWithFrequency[0].Frequency);
            }
            var expectedFrequencies = Enumerable.Range(1, size).ToList();
            CollectionAssert.AreEqual(expectedFrequencies, actualFrequencies);
        }

        [Test]
        public void ReturnCorrectWordsCount()
        {
            var statistic = Statistic.Calculate(_words, new HashSet<string>(), _settings);
            var words = statistic.WordsWithFrequency;
            Assert.AreEqual(3, words.Count);
        }

        [Test]
        public void ReturnsCorrectWordCount_WithTopConstraint()
        {
            _settings.TagsCount = 2;
            var statistic = Statistic.Calculate(_words, new HashSet<string>(), _settings);
            var words = statistic.WordsWithFrequency;
            Assert.AreEqual(2, words.Count);
        }
    }
}
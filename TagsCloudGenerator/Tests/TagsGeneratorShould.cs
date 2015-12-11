using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Nuclex.Game.Packing;
using NUnit.Framework;
using Point = Microsoft.Xna.Framework.Point;

namespace TagsCloudGenerator.Tests
{
    [TestFixture]
    public class TagsGeneratorShould
    {
        [SetUp]
        public void SetUp()
        {
            _settings = new Settings
            {
                Colors = new[] {Color.DarkRed, Color.Black, Color.Coral},
                FontName = "Arial",
                Height = 30,
                Width = 30,
                MaxFontSize = 20,
                MinFontSize = 10,
                SpellingDictionaries = new Dictionary<string, string>(),
                TagsCount = 3
            };
            _words = new List<string> {"a", "b", "b", "c", "c", "c"};
            _statistic = Statistic.Calculate(_words, _settings);
            _pack = new ArevaloRectanglePacker(int.MaxValue, int.MaxValue).Pack;
            _fontGenerator = (settings, statistic, word) => new Font("Arial", 10);
            _tags = TagsGenerator.BuildTags(_statistic, _settings, _pack,
                _fontGenerator);
        }

        private Func<Settings, Statistic, Word, Font> _fontGenerator;

        private Settings _settings;
        private Statistic _statistic;
        private IReadOnlyList<Tag> _tags;
        private Func<int, int, Point> _pack;
        private List<string> _words;

        [Test]
        public void Correctly_GetFont_WithMaxSize()
        {
            _fontGenerator = (settings, statistic, word) => new Font("Arial", 20);
            var tags = TagsGenerator.BuildTags(_statistic, _settings, _pack, _fontGenerator);
            foreach (var tag in tags)
                Assert.AreEqual(new Font("Arial", 20), tag.Font);
        }

        [Test]
        public void Correctly_GetFont_WithMinSize()
        {
            var tags = TagsGenerator.BuildTags(_statistic, _settings, _pack, _fontGenerator);
            foreach (var tag in tags)
                Assert.AreEqual(new Font("Arial", 10), tag.Font);
        }

        [Test]
        public void Correctly_GetTagsCount()
        {
            _settings.Width = 200;
            _settings.Height = 200;
            _statistic = Statistic.Calculate(_words, _settings);
            _tags = TagsGenerator.BuildTags(_statistic, _settings, _pack, _fontGenerator);
            Assert.AreEqual(_words.Distinct().Count(), _tags.Count());
        }

        [Test]
        public void Correctly_GetTopTags()
        {
            _settings.TagsCount = 2;
            _settings.Width = 200;
            _settings.Height = 200;
            _statistic = Statistic.Calculate(_words, _settings);
            _tags = TagsGenerator.BuildTags(_statistic, _settings, _pack, _fontGenerator);
            Assert.AreEqual(_settings.TagsCount, _tags.Count());
        }

        [Test]
        public void Correctly_GetWordInTags()
        {
            _settings.Width = 200;
            _settings.Height = 200;
            _statistic = Statistic.Calculate(_words, _settings);
            _tags = TagsGenerator.BuildTags(_statistic, _settings, _pack, _fontGenerator);
            CollectionAssert.AreEquivalent(
                _statistic.WordsWithFrequency.Select(w => w.Value),
                _tags.Select(t => t.Word.Value));
        }

        [Test]
        public void GenerateOnlyOneTagForWord()
        {
            CollectionAssert.AllItemsAreUnique(_tags.Select(t => t.Word.Value));
        }

        [Test]
        public void TrimDoesntFitTags()
        {
            _fontGenerator = (settings, statistic, word) => new Font("Arial", 20);
            _tags = TagsGenerator.BuildTags(_statistic, _settings, _pack, _fontGenerator);
            Assert.Less(_tags.Count(), 3);
        }
    }
}
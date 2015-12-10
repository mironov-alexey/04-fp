using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace TagsCloudGenerator.Tests
{
    [TestFixture]
    internal class FontCreatorShould
    {
        [SetUp]
        public void SetUp()
        {
            _settings = new Settings {MaxFontSize = 20, MinFontSize = 10, FontName = "Arial", TagsCount = 3};
            _statistic = Statistic.Calculate(
                new List<string>
                {
                    "a",
                    "a",
                    "a",
                    "a",
                    "a",
                    "a",
                    "a",
                    "a",
                    "a",
                    "a",
                    "b",
                    "b",
                    "b",
                    "c"
                },
                new HashSet<string>(),
                _settings
                );
        }

        private Settings _settings;
        private Statistic _statistic;

        [Test]
        public void Correctly_GetFontMaxSize()
        {
            var font = FontGenerator.GetFont(_settings, _statistic, new Word("a", 10));
            var expectedFont = new Font("Arial", 20);
            Assert.AreEqual(expectedFont, font);
        }

        [Test]
        public void Correctly_GetFontMiddleSize()
        {
            var font = FontGenerator.GetFont(_settings, _statistic, new Word("b", 3));
            var expectedFont = new Font("Arial", 14);
            Assert.AreEqual(expectedFont, font);
        }

        [Test]
        public void Correctly_GetFontMinSize()
        {
            var font = FontGenerator.GetFont(_settings, _statistic, new Word("c", 1));
            var expectedFont = new Font("Arial", 10);
            Assert.AreEqual(expectedFont, font);
        }
    }
}
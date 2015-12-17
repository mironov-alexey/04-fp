using System.Drawing;
using System.Linq;
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
                "aaaaaaaaaabbbc".ToCharArray().Select(c => c.ToString()).ToList(),
                _settings);
        }

        private Settings _settings;
        private Statistic _statistic;

        [Test]
        public void Correctly_GetFontMaxSize()
        {
            var expectedFont = new Font("Arial", 20);
            var font = FontGenerator.GetFont(_settings, _statistic.MaxCount, _statistic.MinCount, new Word("a", 10));
            Assert.AreEqual(expectedFont, font);
        }

        [Test]
        public void Correctly_GetFontMiddleSize()
        {
            var expectedFont = new Font("Arial", 14);
            var font = FontGenerator.GetFont(_settings, _statistic.MaxCount, _statistic.MinCount, new Word("b", 3));
            Assert.AreEqual(expectedFont, font);
        }

        [Test]
        public void Correctly_GetFontMinSize()
        {
            var expectedFont = new Font("Arial", 10);
            var font = FontGenerator.GetFont(_settings, _statistic.MaxCount, _statistic.MinCount, new Word("c", 1));
            Assert.AreEqual(expectedFont, font);
        }
    }
}
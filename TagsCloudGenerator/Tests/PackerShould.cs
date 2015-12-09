using System;
using System.Collections.Generic;
using System.Drawing;
using Nuclex.Game.Packing;
using NUnit.Framework;
using Point = Microsoft.Xna.Framework.Point;

namespace TagsCloudGenerator.Tests
{
    [TestFixture]
    public class PackerShould
    {
        [SetUp]
        public void SetUp()
        {
            _settings = new Settings
            {
                Colors = new[] {Color.Black},
                FontName = "Arial",
                MaxFontSize = 20,
                MinFontSize = 10,
                TagsCount = 10
            };
            _fontGenerator = (settings, statistic, word) => new Font("Arial", 10);
            var pointsEnumerator = Points.GetEnumerator();
            _pack = (width, height) =>
            {
                pointsEnumerator.MoveNext();
                return pointsEnumerator.Current;
            };
            _statistic = Statistic.Calculate(
                new List<string>
                {
                    "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                    "b"
                },
                new HashSet<string>(),
                _settings
            );
            _tags = TagsGenerator.BuildTags(_statistic, _settings, _pack, _fontGenerator);
        }

        private Func<int, int, Point> _pack;
        private Func<Settings, Statistic, Word, Font> _fontGenerator;
        private Settings _settings;
        private IEnumerable<Tag> _tags;
        private Statistic _statistic;

        [Test]
        public void Correctly_GetLocation()
        {
            var index = 0;
            foreach (var tag in _tags)
            {
                Assert.AreEqual(tag.Location, Points[index]);
                index++;
            }
        }

        private static readonly List<Point> Points = new List<Point> {new Point(1, 2), new Point(3, 4)};
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
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
                    "b"
                },
                _settings
                );
            _tags = TagsGenerator.BuildTags(_statistic, _settings, _pack, _fontGenerator);
        }

        private Func<int, int, Point> _pack;
        private Func<Settings, Statistic, Word, Font> _fontGenerator;
        private Settings _settings;
        private IReadOnlyList<Tag> _tags;
        private Statistic _statistic;

        private static readonly List<Point> Points = new List<Point> {new Point(1, 2), new Point(3, 4)};

        [Test]
        public void Correctly_GetLocation()
        {
            for (var i = 0; i < _tags.Count; i++)
                Assert.AreEqual(_tags[i].Location, Points[i]);
        }
    }
}
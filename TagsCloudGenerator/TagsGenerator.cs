using System;
using System.Collections.Generic;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;

namespace TagsCloudGenerator
{
    internal class TagsGenerator
    {
        private static readonly Random Random = new Random();

        public static Color GetRandomColor(Color[] colors) =>
            colors[Random.Next(colors.Length - 1)];

        public static IEnumerable<Tag> BuildTags(Statistic statistic, Options options, Settings settings,
            Func<int, int, Point> pack)
        {
            var currentHeight = 0;
            var currentWidth = 0;
            foreach (var word in statistic.WordsWithFrequency)
            {
                var font = FontGenerator.GetFont(settings, statistic, word);
                var rectangleSize = GetTagSize(word, font);
                var location = pack((int) rectangleSize.Width, (int) rectangleSize.Height);
                var color = GetRandomColor(settings.Colors);
                currentWidth = Math.Max(currentWidth, location.X + (int) rectangleSize.Width);
                currentHeight = Math.Max(currentHeight, location.X + (int) rectangleSize.Height);
                if (currentHeight > settings.Height || currentWidth > settings.Width)
                {
                    yield break;
                }
                yield return new Tag(word, location, font, color);
            }
        }

        private static SizeF GetTagSize(Word word, Font font)
        {
            using (Image img = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(img))
                return g.MeasureString(word.Value, font);
        }
    }
}
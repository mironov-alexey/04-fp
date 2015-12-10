using System;
using System.Collections.Generic;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;

namespace TagsCloudGenerator
{
    internal static class TagsGenerator
    {
        private static readonly Random Random = new Random();

        private static Color GetRandomColor(Color[] colors) =>
            colors[Random.Next(colors.Length - 1)];

        public static IReadOnlyList<Tag> BuildTags(Statistic statistic, Settings settings,
            Func<int, int, Point> pack,
            Func<Settings, Statistic, Word, Font> fontGenerator)
        {
            var currentHeight = 0;
            var currentWidth = 0;
            var tags = new List<Tag>();
            foreach (var word in statistic.WordsWithFrequency)
            {
                var font = fontGenerator(settings, statistic, word);
                var rectangleSize = GetTagSize(word, font);
                var location = pack((int) rectangleSize.Width, (int) rectangleSize.Height);
                var color = GetRandomColor(settings.Colors);
                currentWidth = Math.Max(currentWidth, location.X + (int) rectangleSize.Width);
                currentHeight = Math.Max(currentHeight, location.X + (int) rectangleSize.Height);
                if (currentHeight > settings.Height || currentWidth > settings.Width)
                    return tags;
                tags.Add(new Tag(word, location, font, color));
            }
            return tags;
        }

        private static SizeF GetTagSize(Word word, Font font)
        {
            using (Image img = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(img))
                return g.MeasureString(word.Value, font);
        }
    }
}
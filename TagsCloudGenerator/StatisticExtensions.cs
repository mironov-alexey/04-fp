using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Microsoft.Xna.Framework.Point;

namespace TagsCloudGenerator
{
    public static class StatisticExtensions
    {
        public static IEnumerable<Tag> BuildTags(this Statistic statistic, Settings settings,
            Func<int, int, Point> pack,
            Func<Settings, Statistic, Word, Font> fontGenerator) =>
            TagsGenerator.BuildTags(statistic, settings, pack, fontGenerator);
    }
}

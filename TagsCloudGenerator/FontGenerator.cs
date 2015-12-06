using System.Drawing;

namespace TagsCloudGenerator
{
    internal class FontGenerator
    {
        public static Font GetFont(Settings settings, Statistic statistic, Word word)
        {
            var size = settings.MaxFontSize * (word.Frequency - statistic.MinCount) /
                       (statistic.MaxCount - statistic.MinCount);
            size = size < settings.MinFontSize ? size + settings.MinFontSize : size;
            return new Font(settings.FontName, size);
        }
    }
}
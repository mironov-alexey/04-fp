using System.Drawing;

namespace TagsCloudGenerator
{
    internal static class FontGenerator
    {
        public static Font GetFont(Settings settings, int maxCount, int minCount, Word word)
        {
            var size = settings.MaxFontSize*(word.Frequency - minCount)/
                       (maxCount - minCount);
            size = size < settings.MinFontSize ? size + settings.MinFontSize : size;
            return new Font(settings.FontName, size);
        }
    }
}
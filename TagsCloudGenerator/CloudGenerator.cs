using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator
{
    internal static class CloudGenerator
    {
        private const int MaxImageSize = 5000;

        private static Image GeneratePreReleaseImage(IReadOnlyList<Tag> tags)
        {
            var img = new Bitmap(MaxImageSize, MaxImageSize);
            using (var graphics = Graphics.FromImage(img))
            {
                foreach (var tag in tags)
                    graphics.DrawString(tag.Word.Value, tag.Font, new SolidBrush(tag.Color), tag.Location.X,
                        tag.Location.Y);
                return img;
            }
        }

        public static Image GenerateCloudImage(IReadOnlyList<Tag> tags, Settings settings)
        {
            using (var preReleaseImage = GeneratePreReleaseImage(tags))
            {
                var releaseImage = new Bitmap(settings.Width, settings.Height);
                using (var releaseGraphics = Graphics.FromImage(releaseImage))
                {
                    releaseGraphics.Clear(Color.White);
                    releaseGraphics.DrawImage(preReleaseImage, 0, 0);
                    return releaseImage;
                }
            }
        }
    }
}
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudGenerator
{
    internal class CloudSaver
    {
        public static void Save(Image image, Options options)
        {
            image.Save(options.OutputPath, ImageFormat.Png);
        }
    }
}
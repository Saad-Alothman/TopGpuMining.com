using System.Drawing;
using System.IO;
using ImageResizer;

namespace CreaDev.Framework.Core.Imaging
{
    public static class ImageExtension
    {

        public static byte[] ToByteArray(this Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static double AspectRatio(this Bitmap image)
        {
            if (image.Width < 1)
                return 1;

            return (double) image.Height/image.Width;
        }
        public static Bitmap AdjustImageResolution(this Bitmap image, int quality = 90)
        {
            int height = image.Height, width = image.Width;

            Bitmap resizedImage = null;

            if (quality < 0 || quality > 100)
            {
                quality = 60;
            }
            ResizeSettings rsettings = new ResizeSettings
            {
                Width = width,
                Height = height,
                Mode = FitMode.Max,
                Quality = quality,
                Format = "jpg"
            };


            using (Stream stream = new MemoryStream())
            {
                ImageBuilder.Current.Build(image, stream, rsettings);
                resizedImage = new Bitmap(stream);
            }

            return resizedImage;

        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using CreaDev.Framework.Core.Imaging;
using ImageResizer;

namespace CreaDev.Framework.Core.Helpers
{
    public static class ImageHelper
    {
        public static Bitmap BtimapFromDataUri(string dataUri)
        {

            Bitmap Image = null;
            var base64Data = Regex.Match(dataUri, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            string type = Regex.Match(dataUri, @"data:image/(?<type>.+?),(?<data>.+)").Groups["type"].Value.Replace(";base64", "");
            var binData = Convert.FromBase64String(base64Data);

            // Disposing the Stream causes Generic error in GDI, so don't
            var stream = new MemoryStream();
            stream.Write(binData, 0, binData.Length);
            stream.Flush();
            Image = new Bitmap(stream);

            return Image;
        }

        public static byte[] LoadResizedImageFromDataUri(string uriData, bool isResize = true)
        {
            Bitmap bitmap = ImageHelper.BtimapFromDataUri(uriData);
            int maxWidth = 2000;

            if (isResize && bitmap.Width > maxWidth)
            {
                double height = (int)(bitmap.AspectRatio() * maxWidth);
                bitmap = Resizer.ResizeImage(bitmap, null, maxWidth.ToString(), height.ToString(), FitMode.Max);
            }
            string extension = "jpg";

            return bitmap.ToByteArray();


        }
        public static string SaveBitmap(Bitmap bitmap, string path, string fileName = null, int quality = 100, bool isResize = true)
        {
            int maxWidth = 2000;

            if (isResize && bitmap.Width > maxWidth)
            {
                double height = (int)(bitmap.AspectRatio() * maxWidth);
                bitmap = Resizer.ResizeImage(bitmap, null, maxWidth.ToString(), height.ToString(), FitMode.Max);
            }
            //int maxHeight = 2000;
            //if (isResize)
            //{
            //    //TODO:
            ////bitmap = Resizer.ResizeImage(bitmap, null, Width.ToString(), Height.ToString(), FitMode.Max);
            //}

            string extension = "jpg";



            if (string.IsNullOrWhiteSpace(fileName))
                fileName = FileHelper.GetRandomFileName();

            if (!path.EndsWith("\\"))
            {
                path = path + "\\";
            }
            /**/


            // Get an ImageCodecInfo object that represents the JPEG codec.
            var myImageCodecInfo = GetEncoderInfo("image/jpeg");
            // for the Quality parameter category.
            var myEncoder = Encoder.Quality;
            // EncoderParameter object in the array.
            var myEncoderParameters = new EncoderParameters(1);
            // Save the bitmap as a JPEG file with quality provided.
            var myEncoderParameter = new EncoderParameter(myEncoder, (long)quality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bitmap.Save(path + fileName + "." + extension, myImageCodecInfo, myEncoderParameters);

            return fileName + "." + extension;
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web;
using ImageResizer;

namespace CreaDev.Framework.Core.Imaging
{
    public static class Resizer
    {
       

        public static Bitmap ResizeImage(Bitmap image, string settingsString = null, string sWidth = "", string sHeight = "", FitMode fitMode= FitMode.Stretch)
        {
            Bitmap resizedImage = null;
            try
            {
                int width = 0;
                int height = 0;

                if (sWidth == null || !Int32.TryParse(sWidth, out width))
                {
                    width = 0;
                }
                if (sHeight == null || !Int32.TryParse(sHeight, out height))
                {
                    height = 0;
                }
                ResizeSettings rsettings = new ResizeSettings();
                if (!String.IsNullOrWhiteSpace(settingsString))
                {
                    rsettings = new ResizeSettings(settingsString);

                }
                if (width > 0) rsettings.Width = width;
                if (height > 0) rsettings.Height = height;


                rsettings.Mode = fitMode;
                bool isResize = width != 0 || height != 0;
                if (isResize)
                {
                    Debug.Write("");
                    DateTime b = DateTime.Now;
                    Stream stream = new MemoryStream();
                    ImageBuilder.Current.Build(image, stream, rsettings);
                    resizedImage = new Bitmap(stream);
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(settingsString))
                    {
                        rsettings = new ResizeSettings(settingsString);
                        Stream stream = new MemoryStream();
                        ImageBuilder.Current.Build(image, stream, rsettings);
                        resizedImage = new Bitmap(stream);


                    }
                    else
                    {
                        resizedImage = image;

                    }
                }
            }
            catch (Exception ex)
            {
                resizedImage = image;
             //   LogError("", ex);
            }

            return resizedImage;

        }
        public static void ResizeImage(HttpPostedFileBase postedFile,string destination, string settingsString = null, string sWidth = "", string sHeight = "", FitMode fitMode = FitMode.Stretch)
        {
           
                int width = 0;
                int height = 0;

                if (sWidth == null || !Int32.TryParse(sWidth, out width))
                {
                    width = 0;
                }
                if (sHeight == null || !Int32.TryParse(sHeight, out height))
                {
                    height = 0;
                }
                ResizeSettings rsettings = new ResizeSettings();
                if (!String.IsNullOrWhiteSpace(settingsString))
                {
                    rsettings = new ResizeSettings(settingsString);

                }
                if (width > 0) rsettings.Width = width;
                if (height > 0) rsettings.Height = height;


                rsettings.Mode = fitMode;
                bool isResize = width != 0 || height != 0;
                if (isResize)
                {
                    Debug.Write("");
                    ImageBuilder.Current.Build(postedFile.InputStream, destination, rsettings);
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(settingsString))
                    {
                        rsettings = new ResizeSettings(settingsString);
                        Stream stream = new MemoryStream();
                    ImageBuilder.Current.Build(postedFile.InputStream, destination, rsettings);


                    }
                    else
                    {
                        postedFile.SaveAs(destination);
                    }
                }
          


        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class CropperDataViewModel
    {
        public decimal? X { get; set; }

        public decimal? Y { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }

        public int? Rotate { get; set; }

        public IFormFile File { get; set; }

        public byte[] GetCroppedImage()
        {
            var stream = File.OpenReadStream();

            byte[] result = new byte[stream.Length];

            stream.Read(result, 0, result.Length);

            result = CropImage(result, this);

            return result;
        }

        public static byte[] CropImage(byte[] image, CropperDataViewModel data)
        {
            byte[] result = null;

            using (MemoryStream ms = new MemoryStream(image))
            {
                var originalBitmap = new Bitmap(ms);

                var croppedBitmap = new Bitmap((int)data.Width, (int)data.Height);

                using (var graphics = Graphics.FromImage(croppedBitmap))
                {
                    graphics.Clear(Color.White);

                    graphics.DrawImage(originalBitmap,
                        new Rectangle(0, 0, croppedBitmap.Width, croppedBitmap.Height),
                        new Rectangle((int)data.X, (int)data.Y, (int)data.Width, (int)data.Height),
                        GraphicsUnit.Pixel);

                    originalBitmap = croppedBitmap;
                }

                using (MemoryStream output = new MemoryStream())
                {
                    originalBitmap.Save(output, ImageFormat.Png);

                    result = output.ToArray();
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Convertly.Utils
{
    static class ImageProccesing
    {
        #region Методы для конвертации

        public static void SaveFileToPNG(Bitmap image, string PathFile)
        {
            image.Save(PathFile, ImageFormat.Png);
        }

        public static void SaveFileToJPEG(Bitmap image, string PathFile)
        {
            image.Save(PathFile, ImageFormat.Jpeg);
        }

        public static async Task<Bitmap> RemoveHalfPixelsAsync(Bitmap image)
        {
            Bitmap result = new Bitmap(image.Width / 2, image.Height / 2);

            await Task.Run(() =>
            {
                try
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        for (int x = 0; x < image.Width; x++)
                        {
                            bool isEvenX = x % 2 == 0;
                            bool isEvenY = y % 2 == 0;

                            if (isEvenX && isEvenY)
                            {
                                continue;
                            }

                            Color color = image.GetPixel(x, y);

                            result.SetPixel(x / 2, y / 2, color);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            });

            return result;
        }

        public static async Task<BitmapImage> RenderImageAsync(Bitmap image)
        {
            return await Task.Run(() =>
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    image.Save(memory, ImageFormat.Png);
                    memory.Seek(0, SeekOrigin.Begin);

                    BitmapImage bitmapImage = new BitmapImage();

                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memory;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    return bitmapImage;
                }
            });
        }

        public static Bitmap SaveRender(BitmapImage bitmapImage)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memory);
                memory.Seek(0, SeekOrigin.Begin);

                Bitmap bitmap = new Bitmap(memory);

                return bitmap;
            }
        }

        #endregion
    }
}

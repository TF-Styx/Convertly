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

        /// <summary>
        /// Сохранение изображения в PNG
        /// </summary>
        /// <param name="image"></param>
        /// <param name="PathFile"></param>
        public static void SaveFileToPNG(Bitmap image, string PathFile)
        {
            image.Save(PathFile, ImageFormat.Png);
        }

        /// <summary>
        /// Сохранение изображения в JPEG
        /// </summary>
        /// <param name="image"></param>
        /// <param name="PathFile"></param>
        public static void SaveFileToJPEG(Bitmap image, string PathFile)
        {
            image.Save(PathFile, ImageFormat.Jpeg);
        }

        /// <summary>
        /// Удалине половины пикселей изображения (Асинхронный медот)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Конвертация Bitmap в BitmapImage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static async Task<BitmapImage> BitmapToBitmapImageAsync(Bitmap image)
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

        /// <summary>
        /// Сохранение предпоказа изображения в оперативной памяти
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получение ширины и высоты изображения вместе (Асинхронный медот)
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static Task<string> GetImageResolution(string Url)
        {
            return Task.Run(() =>
            {
                try
                {
                    using (var image = Image.FromFile(Url))
                    {
                        string width = image.Width.ToString();
                        string height = image.Height.ToString();

                        return width + "x" + height;
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            });
        }

        /// <summary>
        /// Получение ширины и высоты изображения по отдельности
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static (int width, int height)GetImageWidthAndHeight(string Url)
        {
            try
            {
                using (Image image = Image.FromFile(Url))
                {
                    int width = image.Width;
                    int height = image.Height;

                    return (width, height);
                }
            }
            catch (Exception)
            {
                return (0, 0);
            }
        }

        /// <summary>
        /// Получение ширины и высоты изображения по отдельности (Асинхронная версия)
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static Task<(int width, int height)>GetImageWidthAndHeightAsync(string Url)
        {
            return Task.Run(() =>
            {
                try
                {
                    using (Image image = Image.FromFile(Url))
                    {
                        int width = image.Width;
                        int height = image.Height;

                        return (width, height);
                    }
                }
                catch (Exception)
                {
                    return (0, 0);
                }
            });
        }

        /// <summary>
        /// Изменение изображения, без его обрезания
        /// </summary>
        /// <returns>Изображение в Bitmap</returns>
        public static Task<Bitmap> ChangingSiceImage(Bitmap image, int width, int height)
        {
            return Task.Run(() =>
            {
                try
                {
                    Bitmap newImage = new Bitmap(width, height);

                    using (Graphics graphics = Graphics.FromImage(newImage))
                    {
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        graphics.DrawImage(image, 0, 0, width, height);
                    }
                    return newImage;
                }
                catch (Exception)
                {
                    if (width > 10000 || height > 10000)
                    {
                        MessageBox.Show("Вы установили не допустимый размер изображения!\nИзображение будет отрисовано с исходными параметрами!");
                    }
                    return (image);
                }
            });
        }

        #endregion
    }
}

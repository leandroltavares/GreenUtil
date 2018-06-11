using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace GreenUtil.Imaging
{
    /// <summary>
    /// Classe para lógicas relacionadas a imagem
    /// </summary>
    public static class ImageUtil
    {
        //TODO: Leandro! Aguardando versão final https://www.nuget.org/packages/System.Drawing.Common/

        /// <summary>
        /// Converte uma <see cref="string"/> codificada em Base64 para uma imagem
        /// </summary>
        /// <param name="source"><see cref="string"/> em Base64 a ser convertida</param>
        /// <returns></returns>
        public static Image FromBase64ToImage(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int separatorIndex = source.IndexOf(',');

            if (separatorIndex != -1 && source.Length > separatorIndex + 1)
                source = source.Substring(separatorIndex + 1);

            byte[] bytes = Convert.FromBase64String(source);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        /// <summary>
        /// Converte uma imagem para uma string em Base64
        /// </summary>
        /// <param name="source">Imagem a ser convertida</param>
        /// <returns></returns>
        public static string ToBase64(this Image source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            using (MemoryStream m = new MemoryStream())
            {
                var imageFormat = ImageFormat.MemoryBmp.Equals(source.RawFormat) ? ImageFormat.Bmp : source.RawFormat;

                source.Save(m, imageFormat);
                byte[] imageBytes = m.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);

                string mimeType = MimeType(source);

                if (!string.IsNullOrWhiteSpace(mimeType))
                    base64String = $"data:{mimeType};base64,{base64String}";
               
                return base64String;
            }
        }

        public static void Save(this Bitmap source, string fileName, ImageFormat imageFormat, double quality = 1.0)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            using (var encoderParameters = new EncoderParameters(1))
            using (encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, (int)(quality * 100)))
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

                source.Save(fileName, codecs.Single(codec => codec.FormatID == imageFormat.Guid), encoderParameters);
            }
        }

        public static string MimeType(this Image source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
    
            if (ImageFormat.Jpeg.Equals(source.RawFormat))
                return "image/jpeg"; //https://en.wikipedia.org/wiki/JPEG
            else if (ImageFormat.Emf.Equals(source.RawFormat))
                return "image/emf"; //https://en.wikipedia.org/wiki/Windows_Metafile
            else if (ImageFormat.Png.Equals(source.RawFormat))
                return "image/png"; //https://en.wikipedia.org/wiki/Portable_Network_Graphics
            else if (ImageFormat.Gif.Equals(source.RawFormat))
                return "image/gif"; //https://en.wikipedia.org/wiki/GIF
            else if (ImageFormat.Icon.Equals(source.RawFormat))
                return "image/x-icon"; //https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Basico_sobre_HTTP/MIME_types
            else if (ImageFormat.Tiff.Equals(source.RawFormat))
                return "image/tiff"; //https://en.wikipedia.org/wiki/TIFF
            else if (ImageFormat.Wmf.Equals(source.RawFormat))
                return "image/wmf"; //https://en.wikipedia.org/wiki/Windows_Metafile

            //if (ImageFormat.Bmp.Equals(source.RawFormat) || ImageFormat.MemoryBmp.Equals(source.RawFormat))
            return "image/bmp"; //https://en.wikipedia.org/wiki/BMP_file_format    
        }

        public static Bitmap Scale(this Bitmap source, int targetWidth, int targetHeight)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var target = new Bitmap(targetWidth, targetHeight, source.PixelFormat);

            using (var graphics = Graphics.FromImage(target))
            {
                graphics.DrawImage(source, 0, 0, targetWidth, targetHeight);
            }

            return target;
        }

        public static Bitmap Scale(this Bitmap source, double factor)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            double targetWidth = source.Width * factor;
            double targetHeight = source.Height * factor;

            return Scale(source, (int)targetWidth, (int)targetHeight);
        }

        /// <summary>
        /// Realizar o recorte da <see cref="Bitmap"/> fornecida
        /// </summary>
        /// <param name="sourceBitmap"><see cref="Bitmap"/> original</param>
        /// <param name="cropRectangle">Retângulo a ser recortado</param>
        /// <returns>Um <see cref="Bitmap"/> da região delimitada</returns>
        public static Bitmap Crop(this Bitmap sourceBitmap, Rectangle cropRectangle)
        {
            if (sourceBitmap == null)
                throw new ArgumentNullException(nameof(sourceBitmap));

            if (cropRectangle.Area() == 0)
                throw new ArgumentException("The rectangle must have a area (width x height) greater than zero.");

            var croppedBitmap = new Bitmap(cropRectangle.Width, cropRectangle.Height);

            using (Graphics g = Graphics.FromImage(croppedBitmap))
            {
                g.DrawImage(sourceBitmap, new Rectangle(0, 0, croppedBitmap.Width, croppedBitmap.Height), cropRectangle, GraphicsUnit.Pixel);
            }

            return croppedBitmap;
        }

        /// <summary>
        /// Determines the Area of a rectangle
        /// </summary>
        /// <param name="source">The source rectangle</param>
        /// <returns>The rectangle area</returns>
        public static long Area(this Rectangle source)
        {
            return source.Width * source.Height;
        }


        private const int OrientationKey = 0x0112;
        private const int NotSpecified = 0;
        private const int NormalOrientation = 1;
        private const int MirrorHorizontal = 2;
        private const int UpsideDown = 3;
        private const int MirrorVertical = 4;
        private const int MirrorHorizontalAndRotateRight = 5;
        private const int RotateLeft = 6;
        private const int MirorHorizontalAndRotateLeft = 7;
        private const int RotateRight = 8;


        /// <summary>
        /// Fix Image orientation (EXIF)
        /// </summary>
        /// <param name="source"></param>
        public static void FixOrientation(this Image source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // Fix orientation if needed.
            if (source.PropertyIdList.Contains(OrientationKey))
            {
                var orientation = (int)source.GetPropertyItem(OrientationKey).Value[0];
                switch (orientation)
                {
                    //case NotSpecified: // Assume it is good.
                    //case NormalOrientation:
                    //    // No rotation required.
                    //    break;
                    case MirrorHorizontal:
                        source.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case UpsideDown:
                        source.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case MirrorVertical:
                        source.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case MirrorHorizontalAndRotateRight:
                        source.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case RotateLeft:
                        source.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case MirorHorizontalAndRotateLeft:
                        source.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case RotateRight:
                        source.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
            }
        }

        //TODO: https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
    }
}

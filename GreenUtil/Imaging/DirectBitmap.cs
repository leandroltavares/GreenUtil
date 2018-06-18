using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GreenUtil.Imaging
{
    /// <summary>
    /// Class to encapsulate a Bitmap and manipulate the bits directly
    /// </summary>
    /// <remarks>
    /// Based on https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
    /// </remarks>
    public class DirectBitmap : IDisposable
    {
        /// <summary>
        /// Inner Bitmap
        /// </summary>
        public Bitmap Bitmap { get; private set; }

        /// <summary>
        /// Color bits
        /// </summary>
        public Int32[] Bits { get; private set; }

        /// <summary>
        /// Flag indicating if object was already disposed
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Image height
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Image width
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Garbage collector handle
        /// </summary>
        protected GCHandle BitsHandle { get; private set; }

        /// <summary>
        /// Creates a new DirectBitmap based on width and height and pixel format
        /// </summary>
        /// <param name="width">The image width</param>
        /// <param name="height">The image height</param>
        /// <param name="pixelFormat">The image PixelFormat</param>
        public DirectBitmap(int width, int height, PixelFormat pixelFormat = PixelFormat.Format32bppArgb)
        {
            if (pixelFormat != PixelFormat.Format32bppArgb)
                throw new NotSupportedException("Currently only the 'PixelFormat.Format32bppArgb' is supported. Please change to this pixel format");

            if (width <= 0)
                throw new ArgumentOutOfRangeException("The width must be greater than 0.", nameof(width));

            if (height <= 0)
                throw new ArgumentOutOfRangeException("The height must be greater than 0.", nameof(height));

            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, pixelFormat, BitsHandle.AddrOfPinnedObject());
        }

        /// <summary>
        /// Validate if the parameters are valid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void Validate(int x, int y)
        {
            if (Disposed)
                throw new ObjectDisposedException(nameof(Bits), "The current instance already been disposed.");

            if (x < 0)
                throw new ArgumentOutOfRangeException("The x parameter must be greater or equal to 0", nameof(x));

            if (x > Width)
                throw new ArgumentOutOfRangeException(string.Format("The x parameter must be less or equal to image width ({0})", Width - 1), nameof(x));

            if (y < 0)
                throw new ArgumentOutOfRangeException("The y parameter must be greater or equal to 0", nameof(y));

            if (y > Height)
                throw new ArgumentOutOfRangeException(string.Format("The y parameter must be less or equal to image width ({0})", Height - 1), nameof(y));
        }

        /// <summary>
        /// Creates a DirectBitmap from an Image
        /// </summary>
        /// <param name="image">The source image</param>
        /// <returns></returns>
        public static DirectBitmap FromImage(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var directBitmap = new DirectBitmap(image.Width, image.Height, image.PixelFormat);

            using (var graphics = Graphics.FromImage(directBitmap.Bitmap))
            {
                graphics.DrawImage(image, 0, 0);
            }

            return directBitmap;
        }

        /// <summary>
        /// Sets a specific pixel color
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="colour">The pixel color</param>
        public void SetPixel(int x, int y, Color colour)
        {
            Validate(x, y);

            int index = x + (y * Width);
            int col = colour.ToArgb();

            Bits[index] = col;
        }

        /// <summary>
        /// Gets a specific point color
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            Validate(x, y);

            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsExtensionSearchImageByGoogle.Searches
{
    public abstract class SearchImage
    {
        protected const int MinImageDimension = 200;
        protected const int MaxImageDimension = 800;

        /// <summary>
        /// Determines whether the input image should be resized,
        /// and if so, the optimal dimensions after resizing.
        /// </summary>
        /// <param name="originalSize">Original size of the image</param>
        /// <param name="newSize">Dimensions after resizing</param>
        /// <returns>true if the image should be resized; false otherwise</returns>
        protected static bool IsShouldResize(Size originalSize, out Size newSize)
        {
            // Compute resize ratio (at LEAST ratioMin, at MOST ratioMax).
            // ratioMin is used to prevent the image from getting too small.
            // Note that ratioMax is calculated on the LARGER image dimension,
            // whereas ratioMin is calculated on the SMALLER image dimension.
            var origW = originalSize.Width;
            var origH = originalSize.Height;
            var ratioMax = Math.Min(SearchImage.MaxImageDimension / (double) origW, SearchImage.MaxImageDimension / (double) origH);
            var ratioMin = Math.Max(SearchImage.MinImageDimension / (double) origW, SearchImage.MinImageDimension / (double) origH);
            var ratio = Math.Max(ratioMax, ratioMin);

            // If resizing it would make it bigger, then don't bother
            if (ratio >= 1)
            {
                newSize = originalSize;
                return false;
            }

            var newW = (int) (origW * ratio);
            var newH = (int) (origH * ratio);
            newSize = new Size(newW, newH);
            return true;
        }

        /// <summary>
        /// Loads an image from disk into a byte array.
        /// </summary>
        /// <param name="imagePath">Path to the image file</param>
        /// <param name="resize">Whether to allow resizing</param>
        /// <returns>The loaded image, represented as a byte array</returns>
        protected byte[] LoadImageData(string imagePath, bool resize)
        {
            // Resize the image if user enabled the option
            // and the image is reasonably large
            if (!resize)
                return File.ReadAllBytes(imagePath);

            try
            {
                using var bmp = new Bitmap(imagePath);
                if (IsShouldResize(bmp.Size, out var newSize))
                {
                    using var newBmp = new Bitmap(newSize.Width, newSize.Height);
                    using (var g = Graphics.FromImage(newBmp))
                    {
                        g.DrawImage(bmp, new Rectangle(0, 0, newSize.Width, newSize.Height));
                    }

                    // Save as JPEG (format doesn't have to match file extension,
                    // Google will take care of figuring out the correct format)
                    using var ms = new MemoryStream();
                    newBmp.Save(ms, ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {
                // Ignore exceptions (out of memory, invalid format, etc)
                // and fall back to just reading the raw file bytes
            }

            // No resizing required or image is too small,
            // just load the bytes from disk directly
            return File.ReadAllBytes(imagePath);
        }

        /// <summary>
        /// Converts a byte array into base-64 format, using
        /// a format compatible with Google Images.
        /// </summary>
        /// <param name="content">Raw bytes to encode</param>
        /// <returns>Base-64 encoded string</returns>
        protected string BinaryToBase64Compat(byte[] content)
        {
            // Uploaded image needs to be encoded in base-64,
            // with `+` replaced by `-` and `/` replaced by `_`
            var base64 = Convert.ToBase64String(content).Replace('+', '-').Replace('/', '_');
            return base64;
        }

        public abstract Task<string> Search(string imagePath, bool includeFileName, bool resizeOnUpload,
            CancellationToken cancelToken);
    }
}
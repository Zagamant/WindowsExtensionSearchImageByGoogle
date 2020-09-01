using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WindowsExtensionSearchImageByGoogle.Helpers;

namespace WindowsExtensionSearchImageByGoogle.Searches
{
    public class SearchImage
    {
        protected readonly int MinImageDimension;
        protected readonly int MaxImageDimension;

        public SearchImage() : this(800,200) { }
        
        public SearchImage(int minImageDimension, int maxImageDimension)
        {
            MinImageDimension = minImageDimension;
            MaxImageDimension = maxImageDimension;
        }

        /// <summary>
        /// Determines whether the input image should be resized,
        /// and if so, the optimal dimensions after resizing.
        /// </summary>
        /// <param name="originalSize">Original size of the image</param>
        /// <param name="newSize">Dimensions after resizing</param>
        /// <returns>true if the image should be resized; false otherwise</returns>
        protected bool IsShouldResize(Size originalSize, out Size newSize)
        {
            // Compute resize ratio (at LEAST ratioMin, at MOST ratioMax).
            // ratioMin is used to prevent the image from getting too small.
            // Note that ratioMax is calculated on the LARGER image dimension,
            // whereas ratioMin is calculated on the SMALLER image dimension.
            var origW = originalSize.Width;
            var origH = originalSize.Height;
            var ratioMax = Math.Min(MaxImageDimension / (double) origW, MaxImageDimension / (double) origH);
            var ratioMin = Math.Max(MinImageDimension / (double) origW, MinImageDimension / (double) origH);
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

        /// <summary>
        /// Asynchronously uploads the specified image to Google Images,
        /// and returns the URL of the results page.
        /// </summary>
        /// <param name="imagePath">Path to the image file</param>
        /// <param name="includeFileName">Whether to send the image file name to Google</param>
        /// <param name="resizeOnUpload">Whether to resize large images</param>
        /// <param name="searchEngine">Setting search engine</param>
        /// <param name="cancelToken">Allows for cancellation of the upload</param>
        /// <returns>String containing the URL of the results page</returns>
        public async Task<string> Search(string imagePath, bool includeFileName, bool resizeOnUpload, ISearchEngine searchEngine,
            CancellationToken cancelToken)
        {
            // Load the image, resizing it if necessary
            var data = LoadImageData(imagePath, resizeOnUpload);

            // Prevent auto redirect (we want to open the
            // redirect destination directly in the browser)
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            using var client = new HttpClient(handler);
            var form = new MultipartFormDataContentCompat
            {
                {
                    new StringContent(BinaryToBase64Compat(data)), "image_content"
                }
            };
            if (includeFileName) 
                form.Add(new StringContent(Path.GetFileName(imagePath)), "filename");
            
            var response = await client.PostAsync(searchEngine.RequestUrl, form, cancelToken);
            
            if (response.StatusCode != HttpStatusCode.Redirect)
                throw new IOException("Expected redirect to results page, got " + (int) response.StatusCode);
            
            var resultUrl = response.Headers.Location.ToString();
            
            return resultUrl;
        }
    }
}
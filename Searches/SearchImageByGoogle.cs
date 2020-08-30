using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WindowsExtensionSearchImageByGoogle.Helpers;

namespace WindowsExtensionSearchImageByGoogle.Searches
{
    public class SearchImageByGoogle : SearchImage
    {
        /// <summary>
        /// Asynchronously uploads the specified image to Google Images,
        /// and returns the URL of the results page.
        /// </summary>
        /// <param name="imagePath">Path to the image file</param>
        /// <param name="includeFileName">Whether to send the image file name to Google</param>
        /// <param name="resizeOnUpload">Whether to resize large images</param>
        /// <param name="cancelToken">Allows for cancellation of the upload</param>
        /// <returns>String containing the URL of the results page</returns>
        public override async Task<string> Search(string imagePath, bool includeFileName, bool resizeOnUpload,
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
            if (includeFileName) form.Add(new StringContent(Path.GetFileName(imagePath)), "filename");
            var response = await client.PostAsync("https://images.google.com/searchbyimage/upload", form, cancelToken);
            if (response.StatusCode != HttpStatusCode.Redirect)
                throw new IOException("Expected redirect to results page, got " + (int) response.StatusCode);
            var resultUrl = response.Headers.Location.ToString();
            return resultUrl;
        }
    }
}
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WindowsExtensionSearchImageByGoogle.Helpers
{
    /// <summary>
    /// Google Images has some oddities in the way it requires
    /// forms data to be uploaded. The main three that I could
    /// find are:
    ///
    /// 1. Content-Disposition name parameters must be quoted
    /// 2. Content-Type boundary parameter must NOT be quoted
    /// 3. Image base-64 encoding replaces `+` -> `-`, `/` -> `_`
    ///
    /// This class transparently handles the first two quirks.
    /// </summary>
    internal class MultipartFormDataContentCompat : MultipartContent
    {
        public MultipartFormDataContentCompat() : base("form-data")
        {
            FixBoundaryParameter();
        }

        public MultipartFormDataContentCompat(string boundary) : base("form-data", boundary)
        {
            FixBoundaryParameter();
        }

        public override void Add(HttpContent content)
        {
            base.Add(content);
            AddContentDisposition(content, null, null);
        }

        public void Add(HttpContent content, string name)
        {
            base.Add(content);
            AddContentDisposition(content, name, null);
        }

        public void Add(HttpContent content, string name, string fileName)
        {
            base.Add(content);
            AddContentDisposition(content, name, fileName);
        }

        private void AddContentDisposition(HttpContent content, string name, string fileName)
        {
            var headers = content.Headers;
            headers.ContentDisposition ??= new ContentDispositionHeaderValue("form-data")
            {
                Name = QuoteString(name),
                FileName = QuoteString(fileName)
            };
        }

        private void FixBoundaryParameter()
        {
            var boundary = Headers.ContentType.Parameters.Single(p => p.Name == "boundary");
            boundary.Value = boundary.Value.Trim('"');
        }

        private static string QuoteString(string str)
        {
            return '"' + str + '"';
        }
    }
}
namespace WindowsExtensionSearchImageByGoogle.Searches
{
    public class GoogleSearchEngine : ISearchEngine
    {
        public string RequestUrl { get; } = "https://images.google.com/searchbyimage/upload";
    }
}
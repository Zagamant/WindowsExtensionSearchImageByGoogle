namespace WindowsExtensionSearchImageByGoogle.Searches
{
    public class YandexSearchEngine : ISearchEngine
    {
        public string RequestUrl { get; } = "https://yandex.ru/images/search";
    }
}
using TinyURL.Models;

namespace TinyURL.Data
{
    public static class DataStore
    {
        public static Dictionary<string, ShortURLData> db = new Dictionary<string, ShortURLData>()
        {
            {"alias", new ShortURLData(){ ShortURL = "alias",LongURL="www.google.com"} }
        };
    }
}

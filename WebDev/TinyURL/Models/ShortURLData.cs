namespace TinyURL.Models
{
    public class ShortURLData
    {
        public string ShortURL { get; set; }
        public string LongURL { get; set; }
        public int AccessCount { get; set; }
        public List<string> AccessDateTime { get; set; }

    }
}

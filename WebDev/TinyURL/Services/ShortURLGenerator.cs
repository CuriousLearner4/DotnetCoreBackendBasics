using System.Text;

namespace TinyURL.Services
{
    public class ShortURLGenerator
    {
        public static string generate()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 7; i++) { 
                var c = chars[random.Next(26)];
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}

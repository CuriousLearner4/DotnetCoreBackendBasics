using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TinyURL.Models.DTO
{
    public class ShortenDTO
    {
        //public string CustomAlias { get; set; }
        [Required]
        public string LongURL { get; set; }
        
    }
}

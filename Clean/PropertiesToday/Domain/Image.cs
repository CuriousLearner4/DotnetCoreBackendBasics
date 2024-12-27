using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Image
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string path { get; set; }
        public bool IsUploaded { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }

    }
}

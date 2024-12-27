using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UpdateProperty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ErfSize { get; set; }
        public string FloorSize { get; set; }
        public decimal Price { get; set; }
        public decimal Levies { get; set; }
        public int PetsAllowed { get; set; }
        public int BedRooms { get; set; }
        public int Bathrooms { get; set; }
        public int Kitchens { get; set; }
        public int Louge { get; set; }
        public int Dining { get; set; }
    }
}

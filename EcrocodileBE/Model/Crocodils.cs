using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcrocodileBE.Model
{
    public class Crocodils
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Menufacturer { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quentity { get; set; }
        public DateTime ExpDate { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; }
        public string Type { get; set; }

    }
}

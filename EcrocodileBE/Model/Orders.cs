using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcrocodileBE.Model
{
    public class Orders
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string OrderNo { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcrocodileBE.Model
{
    public class OrderItems
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int CrocodileId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quentity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

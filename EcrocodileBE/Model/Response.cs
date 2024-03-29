﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcrocodileBE.Model
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<Users> listUsers { get; set; }
        public Users user { get; set; }
        public List<Crocodils> listCrocodils { get; set; }
        public Crocodils Crocodil { get; set; }
        public List<Cart> listCart { get; set; }
        // public Cart Cart { get; set; }
        public List<Orders> listOrders { get; set; }
        public Orders Order { get; set; }
        public List<OrderItems> listOrderItems { get; set; }
        public OrderItems OrderItem { get; set; }
    }
}

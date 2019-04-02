using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart2.Models
{
    public class CartList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string imageName { get; set; }
        public int count { get; set; }
    }
}
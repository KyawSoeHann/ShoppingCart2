using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart2.Models
{
    public class PurchaseItem
    {
        public string productName { get; set; }
        public string imageName { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public List<string> activationCode { get; set; }
        public DateTime purchaseTime { get; set; }


    }
}
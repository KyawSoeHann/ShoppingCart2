using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingCart2.Models;

namespace ShoppingCart2.Utility
{
    public static class ProductUtility
    {
        public static int Total(List<CartList> cl)
        {
            int total = cl.Sum(x => x.price * x.count);
            return total;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart2.Utility
{
    public static class CookieUtility
    {
        public static Dictionary<string,string> getProductKeyValues(HttpCookieCollection cookieCollection)
        {
            string[] arr = cookieCollection.AllKeys.Where(x => x != "userId" && x != "name").ToArray();
            Dictionary<string, string> kV = new Dictionary<string, string>();
            //make product id and no of item key value pair
            foreach (var i in arr)
            {
                kV.Add(i, cookieCollection[i].Value);
            }

            return kV;
        }
    }
}
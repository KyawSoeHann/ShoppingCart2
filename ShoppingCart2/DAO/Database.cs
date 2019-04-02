using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace ShoppingCart2.DAO
{
    public static class Database
    {
        public static string conName = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    }
}
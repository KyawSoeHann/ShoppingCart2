using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using ShoppingCart2.Models;
using ShoppingCart2.DAO;
using ShoppingCart2.Utility;

namespace ShoppingCart2.Controllers
{
    public class ViewCartController : Controller
    {
        // GET: ViewCart
        public ActionResult Index()
        {
            ViewData["ViewCart"] = "1";
            ViewData["Price"] = "1";
            ViewData["Cart"] = "0";
            ViewData["ContinueShopping"] = "0";

            Dictionary<string, string> kV = Utility.CookieUtility.getProductKeyValues(Request.Cookies);
            //make product id and no of item key value pair
            if (kV.Count > 0)
            {
                List<CartList> cl = ProductDao.getProductsByIds(kV);
                //calculate total to populate it in view
                int total = ProductUtility.Total(cl);

                ViewData["CartList"] = cl;
                ViewData["Total"] = total;
            }else
            {
                ViewData["Total"] = 0;
            }

            return View();
        }
    }
}
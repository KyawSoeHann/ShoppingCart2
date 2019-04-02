using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using ShoppingCart2.Models;
using ShoppingCart2.DAO;

namespace ShoppingCart2.Controllers
{
    public class ViewCartController : Controller
    {
        // GET: ViewCart
        public ActionResult Index()
        {
            ViewData["topleft"] = "View Cart";
            ViewData["FirstLinkName"] = "Continue Shopping";
            ViewData["FirstActionName"] = "#";
            ViewData["FirstControllerName"] = "#";
            ViewData["SecondLinkName"] = "Checkout";
            ViewData["SecondActionName"] = "#";
            ViewData["SecondControllerName"] = "#";
            
            HttpCookieCollection cookies = Request.Cookies;
            string[] arr = cookies.AllKeys;
            Dictionary<string, string> kV = new Dictionary<string,string>();
            foreach(var i in arr)
            {
                kV.Add(i, Request.Cookies[i].Value);
            }

            List<CartList> cl = ProductDao.getProductsByIds(arr, kV);
            ViewData["CartList"] = cl;
            return View();
        }
    }
}
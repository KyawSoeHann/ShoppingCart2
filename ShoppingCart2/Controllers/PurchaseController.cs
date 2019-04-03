using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart2.DAO;
using ShoppingCart2.Models;
using System.Diagnostics;

namespace ShoppingCart2.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            ViewData["topleft"] = "My Purchases";
            ViewData["FirstLinkName"] = "Home";
            ViewData["FirstActionName"] = "Index";
            ViewData["FirstControllerName"] = "Home";
            ViewData["SecondLinkName"] = "Logout";
            ViewData["SecondActionName"] = "Checkout";
            ViewData["SecondControllerName"] = "Purchase";
            ViewData["Price"] = "0";
            ViewData["Cart"] = "0";
            List<PurchaseItem> piList = PurchaseDao.getPurchaseItems(4);
            ViewData["PurchaseList"] = piList;

            return View();
        }

        public ActionResult Checkout()
        {
            HttpCookieCollection cookies = Request.Cookies;
            string[] arr = cookies.AllKeys;
            Dictionary<string, string> kV = new Dictionary<string, string>();
            //make product id and no of item key value pair
            foreach (var i in arr)
            {
                kV.Add(i, Request.Cookies[i].Value);
            }
            int status = PurchaseDao.InsertPurchase(4, kV);
            foreach (string k in arr)
            {
                if (k != "userId")
                {
                    HttpCookie cookie = new HttpCookie(k);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
            }
            return RedirectToAction("Index","Home");
        }
    }
}
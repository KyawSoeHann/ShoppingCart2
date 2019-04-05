using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart2.DAO;
using ShoppingCart2.Models;
using System.Diagnostics;
using ShoppingCart2.Filters;
using ShoppingCart2.Utility;

namespace ShoppingCart2.Controllers
{
    [AuthFilter]
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            ViewData["ContinueShopping"] = "1";
            ViewData["Price"] = "0";
            ViewData["Cart"] = "0";
            int userId = System.Convert.ToInt32(Request.Cookies["userId"].Value);
            List<PurchaseItem> piList = PurchaseDao.getPurchaseItems(userId);
            ViewData["PurchaseList"] = piList;

            return View();
        }

        public ActionResult Checkout()
        {

            int userId = System.Convert.ToInt32(Request.Cookies["userId"].Value);
            HttpCookieCollection cookies = Request.Cookies;
            Dictionary<string, string> kV = Utility.CookieUtility.getProductKeyValues(cookies);
            //make product id and no of item key value pair
            string[] arr = cookies.AllKeys;
            //persist in DB
            PurchaseDao.InsertPurchase(userId, kV);
            //remove items in the cart
            foreach (string k in arr)
            {   
                    if (k != "userId" && k != "name")
                {
                    HttpCookie cookie = new HttpCookie(k);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
                    
                    
            }
            
            return RedirectToAction("Index","Purchase");
        }
    }
}
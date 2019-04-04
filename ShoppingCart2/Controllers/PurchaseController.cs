using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart2.DAO;
using ShoppingCart2.Models;
using System.Diagnostics;
using ShoppingCart2.Filters;
namespace ShoppingCart2.Controllers
{
    [AuthFilter]
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
            int userId = System.Convert.ToInt32(Request.Cookies["userId"].Value);
            List<PurchaseItem> piList = PurchaseDao.getPurchaseItems(userId);
            ViewData["PurchaseList"] = piList;

            return View();
        }

        public ActionResult Checkout()
        {

            int userId = System.Convert.ToInt32(Request.Cookies["userId"].Value);
            HttpCookieCollection cookies = Request.Cookies;
            string[] arr = cookies.AllKeys.Where(x => x != "userId" && x != "name").ToArray();
            Dictionary<string, string> kV = new Dictionary<string, string>();
            //make product id and no of item key value pair
            foreach (var i in arr)
            {
                kV.Add(i, Request.Cookies[i].Value);
            }
            //persist in DB
            PurchaseDao.InsertPurchase(userId, kV);
            //remove items in the cart
            foreach (string k in arr)
            {
                    HttpCookie cookie = new HttpCookie(k);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                    
            }
            
            return RedirectToAction("Index","Home");
        }
    }
}
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
            ViewData["topleft"] = "View Cart";
            ViewData["FirstLinkName"] = "Continue Shopping";
            ViewData["FirstActionName"] = "Index";
            ViewData["FirstControllerName"] = "Home";
            ViewData["SecondLinkName"] = "Checkout";
            ViewData["SecondActionName"] = "Checkout";
            ViewData["SecondControllerName"] = "Purchase";
            ViewData["Price"] = "1";
            ViewData["Cart"] = "0";

            //collect product id and no of product from cookie
            HttpCookieCollection cookies = Request.Cookies;
            string[] arr = cookies.AllKeys;
            //pick data only when there are at least 1 item in the cart
            Debug.WriteLine(arr.Length);
            if (arr.Length > 0)
            {
                Dictionary<string, string> kV = new Dictionary<string, string>();
                //make product id and no of item key value pair
                foreach (var i in arr)
                {
                    kV.Add(i, Request.Cookies[i].Value);
                }

                List<CartList> cl = ProductDao.getProductsByIds(arr, kV);
                //calculate total to populate it in view
                int total = ProductUtility.Total(cl);

                ViewData["CartList"] = cl;
                ViewData["Total"] = total;
            }else
            {
                ViewData["Total"] = 0;
            }
            

            //testing

            return View();
        }
    }
}
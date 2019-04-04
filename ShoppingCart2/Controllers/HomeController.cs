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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["topleft"] = "Hello Kyawsoehan";
            ViewData["FirstLinkName"] = "My Purchases";
            ViewData["FirstActionName"] = "Index";
            ViewData["FirstControllerName"] = "Purchase";
            ViewData["SecondLinkName"] = "Logout";
            ViewData["SecondActionName"] = "Logout";
            ViewData["SecondControllerName"] = "Auth";
            ViewData["Price"] = "0";
            ViewData["Cart"] = "1";
            List<Product> products = ProductDao.getAllProducts();

            ViewData["products"] = products;            

            return View();
        }

    }
}
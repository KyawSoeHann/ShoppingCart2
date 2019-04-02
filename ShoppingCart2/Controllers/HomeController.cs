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
            ViewData["FirstActionName"] = "#";
            ViewData["FirstControllerName"] = "#";
            ViewData["SecondLinkName"] = "Logout";
            ViewData["SecondActionName"] = "#";
            ViewData["SecondControllerName"] = "#";
            List<Product> products = ProductDao.getAllProducts();
            ViewData["products"] = products;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
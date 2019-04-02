using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart2.Models;
using ShoppingCart2.DAO;

namespace ShoppingCart2.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string name)
        {
            List<Product> products = ProductDao.getByProductName(name);
            return Json(products,JsonRequestBehavior.AllowGet);
        }
    }
}
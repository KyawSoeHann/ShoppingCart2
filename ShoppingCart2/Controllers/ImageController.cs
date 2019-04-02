using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace ShoppingCart2.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public  ActionResult Index(string imagename)
        {
            var dir = Server.MapPath("/App_Data/Images");
            var path = dir + "/" + imagename + ".jpeg";
            return base.File(path,"image/jpeg");
        }
    }
}
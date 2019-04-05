using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart2.Models;
using System.Diagnostics;
using ShoppingCart2.DAO;
using ShoppingCart2.Utility;

namespace ShoppingCart2.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            bool valid = HttpContext.Request.Cookies.AllKeys.Contains("userId");
            if (valid)
            {
                return RedirectToAction("../Home/Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                login.password = Utility.HashUtility.ComputeSha256Hash(login.password);
                Login user = UserDao.findLoginUser(login);
                
                if (user.name != null)
                {
                    HttpCookie userId = new HttpCookie("userId") {
                           Path = @"/"
                    };
                    userId.Value = System.Convert.ToString(user.id);
                    HttpCookie name = new HttpCookie("name") {
                            Path= @"/"
                    };
                    name.Value = System.Convert.ToString(user.name);
                    Response.Cookies.Add(userId);
                    Response.Cookies.Add(name);
                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    ViewData["error"] = "Invalid Username or Password";
                    return View("Index");
                }

            }
            else
            {
                return View("Index");
            }

        }

        public ActionResult Logout()
        {
            HttpCookieCollection cookies = Request.Cookies;
            string[] arr = cookies.AllKeys;
            foreach (string k in arr)
            {
                HttpCookie cookie = new HttpCookie(k);
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);

            }
            return View("Index");
        }
    }
}
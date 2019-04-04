using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using ShoppingCart2.Models;
using ShoppingCart2.DAO;
using System.Web;


namespace ShoppingCart2.Filters
{
    public class AuthFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            bool loggedIn = filterContext.HttpContext.Request.Cookies.AllKeys.Contains("userId");
            if (loggedIn)
            {
                int userId = System.Convert.ToInt32(filterContext.HttpContext.Request.Cookies.Get("userId").Value);
                Login user = UserDao.findUserById(userId);
                if (user.name == null)
                {
                    HttpCookie userIdCookie = new HttpCookie("userId");
                    userIdCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpCookie name = new HttpCookie("name");
                    name.Expires = DateTime.Now.AddDays(-1);
                    filterContext.HttpContext.Response.Cookies.Add(userIdCookie);
                    filterContext.HttpContext.Response.Cookies.Add(name);

                    filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                        { "controller","Auth"},
                        { "action","Index"}
                   }
                   );

                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller","Auth"},
                        { "action","Index"}
                    }
                    );
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}
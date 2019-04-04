using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart2.Models
{
    public class Login
    {
        public int id { get; set; }
        public string name { get; set; }
        [Display(Name ="Username"),Required(ErrorMessage = "Username is required.")]
        public string username { get; set; }
        [Display(Name = "Password"),Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }
    }
}
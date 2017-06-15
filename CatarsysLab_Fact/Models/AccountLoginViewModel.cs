using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatarsysLab_Fact.Models
{
    public class AccountLoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string returnUrl { get; set; }
    }
}
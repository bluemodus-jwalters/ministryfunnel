using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Models
{
    public class AuthenticationModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string api_user { get; set; }
        public string email { get; set; }
    }
}
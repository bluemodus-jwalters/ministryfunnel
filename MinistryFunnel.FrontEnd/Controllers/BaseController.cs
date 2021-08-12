using MinistryFunnel.FrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinistryFunnel.FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        public readonly IApiHelper _apiHelper;

        public string _token
        {
            get
            {
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
                //You get the user’s first and last name below:
                var name = ViewBag.Name = userClaims?.FindFirst("name")?.Value;

                // The 'preferred_username' claim can be used for showing the username
                var email = ViewBag.Username = userClaims?.FindFirst("preferred_username")?.Value;

                // The role assigned
                var role = ViewBag.Role = userClaims?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                return _apiHelper.GetTokenPublic(Request, Response, name, email);
            }
        }

        public BaseController()
        {
            _apiHelper = new ApiHelper();

        }

    }
}
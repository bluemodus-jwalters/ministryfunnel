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
                return _apiHelper.GetTokenPublic(Request, Response);
            }
        }

        public BaseController()
        {
            _apiHelper = new ApiHelper();

        }

    }
}
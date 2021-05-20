using MinistryFunnel.FrontEnd.Helpers;
using MinistryFunnel.FrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinistryFunnel.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiHelper _apiHelper;
        private readonly IMinistryHelper _ministryHelper;
        public HomeController()
        {
            _apiHelper = new ApiHelper();
            _ministryHelper = new MinistryHelper();
        }

        public ActionResult Index()
        {
            var response = _apiHelper.Get(CompileUrl("/api/ministry/dashboard"));

            if (response.IsSuccessful)
            {
                var ministries = JsonConvert.DeserializeObject<IEnumerable<MinistryViewModel>>(response.Content);
                return View(ministries);
            }

            return View();
        }

       

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }
    }
}
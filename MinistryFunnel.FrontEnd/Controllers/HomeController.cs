﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
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
    public class HomeController : BaseController
    {
        private readonly IMinistryHelper _ministryHelper;
        public HomeController()
        {
            _ministryHelper = new MinistryHelper();
        }

        //TODO: figure out what to do with the home page
        [Authorize]
        public ActionResult Index()
        {
            var response = _apiHelper.Get(CompileUrl("/api/ministry/dashboard"), _token);

            if (response.IsSuccessful)
            {
                var ministries = JsonConvert.DeserializeObject<IEnumerable<MinistryViewModel>>(response.Content);
                return View(ministries);
            }

            return View();
        }

        [Authorize]
        public void Test()
        {
            var t = "hello";
        }

        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
            else
            {
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

                //You get the user’s first and last name below:
                ViewBag.Name = userClaims?.FindFirst("name")?.Value;

                // The 'preferred_username' claim can be used for showing the username
                ViewBag.Username = userClaims?.FindFirst("preferred_username")?.Value;

                // The subject/ NameIdentifier claim can be used to uniquely identify the user across the web
                ViewBag.Subject = userClaims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                // TenantId is the unique Tenant Id - which represents an organization in Azure AD
                ViewBag.TenantId = userClaims?.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value;
            }
        }



        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }
    }
}
using Microsoft.Owin.Security;
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

        [Authorize]
        public ActionResult Index()
        {
            try
            {
                var response = _apiHelper.Get(CompileUrl("/api/ministry/dashboard"), GetToken());

                if (response.IsSuccessful)
                {
                    var ministries = JsonConvert.DeserializeObject<IEnumerable<MinistryFrontEndViewModelMinimal>>(response.Content);
                    return View(ministries);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error");
            }

            return View();
        }

        [Authorize]
        public ActionResult Calendar()
        {
            return View(new EventViewModel());
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var events = new List<EventViewModel>();

            var response = _apiHelper.Get(CompileUrl("/api/ministry/calendar"), GetToken());

            if (response.IsSuccessful)
            {
                var ministries = JsonConvert.DeserializeObject<IEnumerable<MinistryFrontEndViewModelMinimal>>(response.Content);
                foreach (var ministry in ministries)
                {

                    if (ministry.Frequency == "Daily")
                    {
                        var iterativeDate = ministry.StartDate;
                        while (iterativeDate < ministry.EndDate)
                        {
                            var iterativeEndTime = iterativeDate.Date + ministry.EndDate.TimeOfDay;

                            events.Add(AddEvent(ministry.Event, iterativeDate, iterativeEndTime, ministry.Id, ministry.ApprovalName));

                            iterativeDate = iterativeDate.AddDays(1);
                        }
                    }
                    else if (ministry.Frequency == "Weekly")
                    {
                        var iterativeDate = ministry.StartDate;
                        while (iterativeDate < ministry.EndDate)
                        {
                            var iterativeEndTime = iterativeDate.Date + ministry.EndDate.TimeOfDay;

                            events.Add(AddEvent(ministry.Event, iterativeDate, iterativeEndTime, ministry.Id, ministry.ApprovalName));

                            iterativeDate = iterativeDate.AddDays(7);
                        }
                    }
                    else if (ministry.Frequency == "Monthly")
                    {
                        var iterativeDate = ministry.StartDate;
                        while (iterativeDate < ministry.EndDate)
                        {
                            var iterativeEndTime = iterativeDate.Date + ministry.EndDate.TimeOfDay;

                            events.Add(AddEvent(ministry.Event, iterativeDate, iterativeEndTime, ministry.Id, ministry.ApprovalName));

                            iterativeDate = iterativeDate.AddMonths(1);
                        }                      
                    }
                    else
                    {
                        events.Add(AddEvent(ministry.Event, ministry.StartDate, ministry.EndDate, ministry.Id, ministry.ApprovalName));
                    }    
                }            
            }

            return Json(events, JsonRequestBehavior.AllowGet);        

        }

        private EventViewModel AddEvent(string title, DateTime startDate, DateTime endDate, int ministryId, string approvalName)
        {

            string color;
            switch (approvalName)
            {
                case "Approved":
                    color = "#28a745";
                    break;
                case "Cancelled":
                    color = "#721c24";
                    break;
                case "Requesting Approval":
                    color = "#337ab7";
                    break;
                case "Pending Approval":
                    color = "#5bc0de";
                    break;
                case "Not Approved at this Time":
                    color = "#eea236";
                    break;
                default:
                    color = "#007bff";
                    break;
            }

            var newEvent = new EventViewModel {
                title = title,
                start = startDate.ToString("yyyy-MM-ddTHH:mm"),
                end = endDate.ToString("yyyy-MM-ddTHH:mm"),
                url = "/ministry/edit/" + ministryId,
                backgroundColor = color
            };

            return newEvent;
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

                ViewBag.Role = userClaims?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            }
        }



        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }
    }
}
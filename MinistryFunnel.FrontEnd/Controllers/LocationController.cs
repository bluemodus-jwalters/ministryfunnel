using Ganss.XSS;
using MinistryFunnel.FrontEnd.Helpers;
using MinistryFunnel.FrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MinistryFunnel.FrontEnd.Controllers
{
    public class LocationController : BaseController
    {
        //private readonly IApiHelper _apiHelper;
        
        //private string  token
        //{
        //    get
        //    {
        //        return _apiHelper.GetTokenPublic(Request, Response);
        //    }
        //}

        //public LocationController()
        //{           
        //    _apiHelper = new ApiHelper();
            
        //}
        const string apiAction = "/api/location";

        

        // GET: Location
        public ActionResult Index()
        {
            //TODO: I have the token setup so I need to add base class to all the controllers and also this token to the calls
            var x = _token;
            var response = _apiHelper.Get(CompileUrl(apiAction), Request, Response);

            if (response.IsSuccessful)
            {
                var locations = JsonConvert.DeserializeObject<IEnumerable<LocationViewModel>>(response.Content);
                return View(locations);
            }

            return View();
        }

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }

        // GET: Location/Details/5
        public ActionResult Details(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var location = JsonConvert.DeserializeObject<LocationViewModel>(response.Content);
                return View(location);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem finding this record. Please try again or contact your system administrator.";
            return View();
        }

        // GET: Location/Search/same
        public ActionResult Search(string searchText)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedText = sanitizer.Sanitize(searchText);

            var response = _apiHelper.Get(CompileUrl(apiAction) + $"?searchText={sanitizedText}");

            if (response.IsSuccessful)
            {
                var location = JsonConvert.DeserializeObject<IEnumerable<LocationViewModel>>(response.Content);
                return View("Index", location);
            }
            
            return View("Index");
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var json = new JavaScriptSerializer().Serialize(new LocationViewModel
                {
                    Name = Request.Form["Name"],
                    Archived = Request.Form["Archived"] == "true"
                });

                var response = _apiHelper.Post(CompileUrl(apiAction), json);

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Success";
                    TempData["Message"] = $"Location, {Request.Form["Name"]}, Created";
                    return RedirectToAction("Index");
                }

                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem creating this record. Please try again or contact your system administrator.";
                return View();
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem creating this record. Please try again or contact your system administrator.";
                return View();
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var location = JsonConvert.DeserializeObject<LocationViewModel>(response.Content);
                return View(location);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View("Edit", null);
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var updatedModel = new LocationViewModel
            {
                Id = id,
                Name = Request.Form["Name"],
                Archived = Request.Form["Archived"] == "true" 
            };

            try
            {
                var json = new JavaScriptSerializer().Serialize(updatedModel);
                var response = _apiHelper.Put(CompileUrl(apiAction) + $"?id={id}", json);

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Info";
                    TempData["Message"] = $"Location, {Request.Form["Name"]}, Edited";
                    return RedirectToAction("Index", ViewBag);
                }

                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem updating this record. Please try again or contact your system administrator.";
                return View();
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem updating this record. Please try again or contact your system administrator.";
                return View();
            }
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var location = JsonConvert.DeserializeObject<LocationViewModel>(response.Content);
                
                return View(location);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View();
        }

        // POST: Location/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                var response = _apiHelper.Delete(CompileUrl(apiAction), id);

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Info";
                    TempData["Message"] = $"Location Deleted";
                    return RedirectToAction("Index");
                }

                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem deleting this record. Please try again or contact your system administrator.";
                return View();
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem deleting this record. Please try again or contact your system administrator.";
                return View();
            }
        }
    }
}
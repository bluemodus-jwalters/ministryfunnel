using Ganss.XSS;
using MinistryFunnel.FrontEnd.Helpers;
using MinistryFunnel.FrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MinistryFunnel.FrontEnd.Controllers
{
    public class CampusController : Controller
    {
        private readonly IApiHelper _apiHelper;
        public CampusController()
        {
            _apiHelper = new ApiHelper();
        }
        const string apiAction = "/api/campus";

        // GET: Campus
        public ActionResult Index()
        {           
            var response = _apiHelper.Get(CompileUrl(apiAction));

            if (response.IsSuccessful)
            {
                var campuss = JsonConvert.DeserializeObject<IEnumerable<CampusViewModel>>(response.Content);
                return View(campuss);
            }

            return View();
        }

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }

        // GET: Campus/Details/5
        public ActionResult Details(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var campus = JsonConvert.DeserializeObject<CampusViewModel>(response.Content);
                return View(campus);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem finding this record. Please try again or contact your system administrator.";
            return View();
        }

        // GET: Campus/Search/same
        public ActionResult Search(string searchText)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedText = sanitizer.Sanitize(searchText);

            var response = _apiHelper.Get(CompileUrl(apiAction) + $"?searchText={sanitizedText}");

            if (response.IsSuccessful)
            {
                var campus = JsonConvert.DeserializeObject<IEnumerable<CampusViewModel>>(response.Content);
                return View("Index", campus);
            }
            
            return View("Index");
        }

        // GET: Campus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campus/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var json = new JavaScriptSerializer().Serialize(new CampusViewModel
                {
                    Name = Request.Form["Name"],
                    Archived = Request.Form["Archived"] == "true"
                });

                var response = _apiHelper.Post(CompileUrl(apiAction), json);

                TempData["MessageType"] = "Success"; 
                TempData["Message"] = $"Campus, {Request.Form["Name"]}, Created";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem creating this record. Please try again or contact your system administrator.";
                return View();
            }
        }

        // GET: Campus/Edit/5
        public ActionResult Edit(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var campus = JsonConvert.DeserializeObject<CampusViewModel>(response.Content);
                return View(campus);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View("Edit", null);
        }

        // POST: Campus/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var updatedModel = new CampusViewModel
            {
                Id = id,
                Name = Request.Form["Name"],
                Archived = Request.Form["Archived"] == "true" 
            };

            try
            {
                var json = new JavaScriptSerializer().Serialize(updatedModel);
                var response = _apiHelper.Put(CompileUrl(apiAction) + $"?id={id}", json);

                TempData["MessageType"] = "Info";
                TempData["Message"] = $"Campus, {Request.Form["Name"]}, Edited";
                return RedirectToAction("Index", ViewBag);
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem updating this record. Please try again or contact your system administrator.";
                return View();
            }
        }

        // GET: Campus/Delete/5
        public ActionResult Delete(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var campus = JsonConvert.DeserializeObject<CampusViewModel>(response.Content);
                
                return View(campus);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View();
        }

        // POST: Campus/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                var response = _apiHelper.Delete(CompileUrl(apiAction), id);

                TempData["MessageType"] = "Info";
                TempData["Message"] = $"Campus Deleted";
                return RedirectToAction("Index");
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
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
    public class ResourceInvolvementController : Controller
    {
        private readonly IApiHelper _apiHelper;
        public ResourceInvolvementController()
        {
            _apiHelper = new ApiHelper();
        }
        const string apiAction = "/api/resourceinvolvement";

        // GET: ResourceInvolvement
        public ActionResult Index()
        {           
            var response = _apiHelper.Get(CompileUrl(apiAction));

            if (response.IsSuccessful)
            {
                var resourceInvolvements = JsonConvert.DeserializeObject<IEnumerable<ResourceInvolvementViewModel>>(response.Content);
                return View(resourceInvolvements);
            }

            return View();
        }

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }

        // GET: ResourceInvolvement/Details/5
        public ActionResult Details(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var resourceInvolvement = JsonConvert.DeserializeObject<ResourceInvolvementViewModel>(response.Content);
                return View(resourceInvolvement);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem finding this record. Please try again or contact your system administrator.";
            return View();
        }

        // GET: ResourceInvolvement/Search/same
        public ActionResult Search(string searchText)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedText = sanitizer.Sanitize(searchText);

            var response = _apiHelper.Get(CompileUrl(apiAction) + $"?searchText={sanitizedText}");

            if (response.IsSuccessful)
            {
                var resourceInvolvement = JsonConvert.DeserializeObject<IEnumerable<ResourceInvolvementViewModel>>(response.Content);
                return View("Index", resourceInvolvement);
            }
            
            return View("Index");
        }

        // GET: ResourceInvolvement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResourceInvolvement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var json = new JavaScriptSerializer().Serialize(new ResourceInvolvementViewModel
                {
                    Name = Request.Form["Name"],
                    Archived = Request.Form["Archived"] == "true"
                });

                var response = _apiHelper.Post(CompileUrl(apiAction), json);

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Success";
                    TempData["Message"] = $"Resource Involvement, {Request.Form["Name"]}, Created";
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

        // GET: ResourceInvolvement/Edit/5
        public ActionResult Edit(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var resourceInvolvement = JsonConvert.DeserializeObject<ResourceInvolvementViewModel>(response.Content);
                return View(resourceInvolvement);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View("Edit", null);
        }

        // POST: ResourceInvolvement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var updatedModel = new ResourceInvolvementViewModel
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
                    TempData["Message"] = $"Resource Involvement, {Request.Form["Name"]}, Edited";
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

        // GET: ResourceInvolvement/Delete/5
        public ActionResult Delete(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var resourceInvolvement = JsonConvert.DeserializeObject<ResourceInvolvementViewModel>(response.Content);
                
                return View(resourceInvolvement);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View();
        }

        // POST: ResourceInvolvement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                var response = _apiHelper.Delete(CompileUrl(apiAction), id);

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Info";
                    TempData["Message"] = $"Resource Involvement Deleted";
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
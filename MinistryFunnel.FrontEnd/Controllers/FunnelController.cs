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
    [Authorize(Roles="Database.Admin")]
    public class FunnelController : BaseController
    {
        const string apiAction = "/api/funnel";

        // GET: Funnel
        public ActionResult Index()
        {           
            var response = _apiHelper.Get(CompileUrl(apiAction), GetToken());

            if (response.IsSuccessful)
            {
                var funnels = JsonConvert.DeserializeObject<IEnumerable<FunnelViewModel>>(response.Content);
                return View(funnels);
            }

            return View();
        }

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }

        // GET: Funnel/Details/5
        public ActionResult Details(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, GetToken());

            if (response.IsSuccessful)
            {
                var funnel = JsonConvert.DeserializeObject<FunnelViewModel>(response.Content);
                return View(funnel);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem finding this record. Please try again or contact your system administrator.";
            return View();
        }

        // GET: Funnel/Search/same
        public ActionResult Search(string searchText)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedText = sanitizer.Sanitize(searchText);

            var response = _apiHelper.Get(CompileUrl(apiAction) + $"?searchText={sanitizedText}", GetToken());

            if (response.IsSuccessful)
            {
                var funnel = JsonConvert.DeserializeObject<IEnumerable<FunnelViewModel>>(response.Content);
                return View("Index", funnel);
            }
            
            return View("Index");
        }

        // GET: Funnel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funnel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var json = new JavaScriptSerializer().Serialize(new FunnelViewModel
                {
                    Name = Request.Form["Name"],
                    Archived = Request.Form["Archived"] == "true"
                });

                var response = _apiHelper.Post(CompileUrl(apiAction), json, GetToken());

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Success";
                    TempData["Message"] = $"Funnel, {Request.Form["Name"]}, Created";
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

        // GET: Funnel/Edit/5
        public ActionResult Edit(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, GetToken());

            if (response.IsSuccessful)
            {
                var funnel = JsonConvert.DeserializeObject<FunnelViewModel>(response.Content);
                return View(funnel);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View("Edit", null);
        }

        // POST: Funnel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var updatedModel = new FunnelViewModel
            {
                Id = id,
                Name = Request.Form["Name"],
                Archived = Request.Form["Archived"] == "true" 
            };

            try
            {
                var json = new JavaScriptSerializer().Serialize(updatedModel);
                var response = _apiHelper.Put(CompileUrl(apiAction) + $"?id={id}", json, GetToken());

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Info";
                    TempData["Message"] = $"Funnel, {Request.Form["Name"]}, Edited";
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

        // GET: Funnel/Delete/5
        public ActionResult Delete(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, GetToken());

            if (response.IsSuccessful)
            {
                var funnel = JsonConvert.DeserializeObject<FunnelViewModel>(response.Content);
                
                return View(funnel);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View();
        }

        // POST: Funnel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                var response = _apiHelper.Delete(CompileUrl(apiAction), id, GetToken());

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Info";
                    TempData["Message"] = $"Funnel Deleted";
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
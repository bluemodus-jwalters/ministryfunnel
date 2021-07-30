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
    [Authorize]
    public class FrequencyController : BaseController
    {
        const string apiAction = "/api/frequency";

        // GET: Frequency
        public ActionResult Index()
        {           
            var response = _apiHelper.Get(CompileUrl(apiAction), _token);

            if (response.IsSuccessful)
            {
                var frequencys = JsonConvert.DeserializeObject<IEnumerable<FrequencyViewModel>>(response.Content);
                return View(frequencys);
            }

            return View();
        }

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }

        // GET: Frequency/Details/5
        public ActionResult Details(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, _token);

            if (response.IsSuccessful)
            {
                var frequency = JsonConvert.DeserializeObject<FrequencyViewModel>(response.Content);
                return View(frequency);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem finding this record. Please try again or contact your system administrator.";
            return View();
        }

        // GET: Frequency/Search/same
        public ActionResult Search(string searchText)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedText = sanitizer.Sanitize(searchText);

            var response = _apiHelper.Get(CompileUrl(apiAction) + $"?searchText={sanitizedText}", _token);

            if (response.IsSuccessful)
            {
                var frequency = JsonConvert.DeserializeObject<IEnumerable<FrequencyViewModel>>(response.Content);
                return View("Index", frequency);
            }
            
            return View("Index");
        }

        // GET: Frequency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Frequency/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var json = new JavaScriptSerializer().Serialize(new FrequencyViewModel
                {
                    Name = Request.Form["Name"],
                    Archived = Request.Form["Archived"] == "true"
                });

                var response = _apiHelper.Post(CompileUrl(apiAction), json);

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Success";
                    TempData["Message"] = $"Frequency, {Request.Form["Name"]}, Created";
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

        // GET: Frequency/Edit/5
        public ActionResult Edit(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, _token);

            if (response.IsSuccessful)
            {
                var frequency = JsonConvert.DeserializeObject<FrequencyViewModel>(response.Content);
                return View(frequency);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View("Edit", null);
        }

        // POST: Frequency/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var updatedModel = new FrequencyViewModel
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
                    TempData["Message"] = $"Frequency, {Request.Form["Name"]}, Edited";
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

        // GET: Frequency/Delete/5
        public ActionResult Delete(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, _token);

            if (response.IsSuccessful)
            {
                var frequency = JsonConvert.DeserializeObject<FrequencyViewModel>(response.Content);
                
                return View(frequency);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View();
        }

        // POST: Frequency/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                var response = _apiHelper.Delete(CompileUrl(apiAction), id);

                if (response.IsSuccessful)
                {
                    TempData["MessageType"] = "Info";
                    TempData["Message"] = $"Frequency Deleted";
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
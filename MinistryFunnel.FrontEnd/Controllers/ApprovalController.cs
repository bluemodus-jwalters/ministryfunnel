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
    public class ApprovalController : Controller
    {
        private readonly IApiHelper _apiHelper;
        public ApprovalController()
        {
            _apiHelper = new ApiHelper();
        }
        const string apiAction = "/api/approval";

        // GET: Approval
        public ActionResult Index()
        {           
            var response = _apiHelper.Get(CompileUrl(apiAction));

            if (response.IsSuccessful)
            {
                var approvals = JsonConvert.DeserializeObject<IEnumerable<ApprovalViewModel>>(response.Content);
                return View(approvals);
            }

            return View();
        }

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }

        // GET: Approval/Details/5
        public ActionResult Details(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var approval = JsonConvert.DeserializeObject<ApprovalViewModel>(response.Content);
                return View(approval);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem finding this record. Please try again or contact your system administrator.";
            return View();
        }

        // GET: Approval/Search/same
        public ActionResult Search(string searchText)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitizedText = sanitizer.Sanitize(searchText);

            var response = _apiHelper.Get(CompileUrl(apiAction) + $"?searchText={sanitizedText}");

            if (response.IsSuccessful)
            {
                var approval = JsonConvert.DeserializeObject<IEnumerable<ApprovalViewModel>>(response.Content);
                return View("Index", approval);
            }
            
            return View("Index");
        }

        // GET: Approval/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Approval/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var json = new JavaScriptSerializer().Serialize(new ApprovalViewModel
                {
                    Name = Request.Form["Name"],
                    Archived = Request.Form["Archived"] == "true"
                });

                var response = _apiHelper.Post(CompileUrl(apiAction), json);

                TempData["MessageType"] = "Success"; 
                TempData["Message"] = $"Approval, {Request.Form["Name"]}, Created";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem creating this record. Please try again or contact your system administrator.";
                return View();
            }
        }

        // GET: Approval/Edit/5
        public ActionResult Edit(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var approval = JsonConvert.DeserializeObject<ApprovalViewModel>(response.Content);
                return View(approval);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View("Edit", null);
        }

        // POST: Approval/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var updatedModel = new ApprovalViewModel
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
                TempData["Message"] = $"Approval, {Request.Form["Name"]}, Edited";
                return RedirectToAction("Index", ViewBag);
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem updating this record. Please try again or contact your system administrator.";
                return View();
            }
        }

        // GET: Approval/Delete/5
        public ActionResult Delete(int id = -1)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var approval = JsonConvert.DeserializeObject<ApprovalViewModel>(response.Content);
                
                return View(approval);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View();
        }

        // POST: Approval/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                var response = _apiHelper.Delete(CompileUrl(apiAction), id);

                TempData["MessageType"] = "Info";
                TempData["Message"] = $"Approval Deleted";
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
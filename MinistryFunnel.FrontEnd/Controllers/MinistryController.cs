using MinistryFunnel.FrontEnd.Helpers;
using MinistryFunnel.FrontEnd.Models;
using MinistryFunnel.FrontEnd.Models.DropDowns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinistryFunnel.FrontEnd.Controllers
{
    public class MinistryController : Controller
    {
        private readonly IApiHelper _apiHelper;
        public MinistryController()
        {
            _apiHelper = new ApiHelper();
        }
        const string apiAction = "/api/ministry";

        // GET: Ministry
        public ActionResult Index()
        {
            var response = _apiHelper.Get(CompileUrl(apiAction));

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

        // GET: Ministry/Details/5
        public ActionResult Details(int id)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id);

            if (response.IsSuccessful)
            {
                var ministry = JsonConvert.DeserializeObject<MinistryViewModel>(response.Content);
                return View(ministry);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem finding this record. Please try again or contact your system administrator.";
            return View();
        }

        // GET: Ministry/Create
        public ActionResult Create()
        {
            var ministry= GetMinistryOwners();

            var dropDown = ministry.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var viewModel = new MinistryOwnerCreateViewModel { ministryOwners = dropDown };

            return View(viewModel);
        }

        private ICollection<MinistryOwnerViewModel> GetMinistryOwners()
        {
            var response = _apiHelper.Get(CompileUrl("/api/ministryowner"));

            if (response.IsSuccessful)
            {
                var ministryOwners = JsonConvert.DeserializeObject<ICollection<MinistryOwnerViewModel>>(response.Content);
                return ministryOwners;
            }
            return null;
        }

        // POST: Ministry/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ministry/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ministry/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ministry/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ministry/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

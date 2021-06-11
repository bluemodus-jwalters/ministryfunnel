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
using System.Web.Script.Serialization;

namespace MinistryFunnel.FrontEnd.Controllers
{
    public class MinistryController : BaseController
    {
        private readonly IMinistryHelper _ministryHelper;
        public MinistryController()
        {
            _ministryHelper = new MinistryHelper();
        }      

        const string apiAction = "/api/ministry";

        // GET: Ministry
        public ActionResult Index()
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), _token);

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
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, _token);

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
            var ministryOwners = _ministryHelper.GetMinistryOwners().Where(x => x.Archived == false);
            var practices = _ministryHelper.GetPractices().Where(x => x.Archived == false);
            var funnels = _ministryHelper.GetFunnels().Where(x => x.Archived == false);
            var campuses = _ministryHelper.GetCampuses().Where(x => x.Archived == false);
            var locations = _ministryHelper.GetLocations().Where(x => x.Archived == false);
            var levelOfImportances = _ministryHelper.GetLevelOfImportances().Where(x => x.Archived == false);
            var approvals = _ministryHelper.GetApprovals().Where(x => x.Archived == false);
            var frequencies = _ministryHelper.GetFrequencies().Where(x => x.Archived == false);
            var upInOuts = _ministryHelper.GetUpInOutOptions().Where(x => x.Archived == false);
            var resourceInvolvements = _ministryHelper.GetResourceInvolvementOptions().Where(x => x.Archived == false);
            

            var ministryOwnerDropDown = ministryOwners.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var practiceDropDown = practices.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var funnelDropDown = funnels.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var campusDropDown = campuses.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var locationDropDown = locations.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var levelOfImportanceDropDown = levelOfImportances.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var approvalDropDown = approvals.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var frequencyDropDown = frequencies.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var upInOutListBox = upInOuts.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var resourceInvolvementListBox = resourceInvolvements.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            

            var viewModel = new MinistryCreateViewModel { 
                MinistryOwners = ministryOwnerDropDown,
                Practices = practiceDropDown,
                Funnels = funnelDropDown,
                Campuses = campusDropDown,
                Locations = locationDropDown,
                Approvals = approvalDropDown,
                Frequencies = frequencyDropDown,
                LevelOfImportances = levelOfImportanceDropDown,
                UpInOuts = upInOutListBox,
                ResourceInvolvements = resourceInvolvementListBox,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };


            return View(viewModel);
        }


        // POST: Ministry/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //change them to nullables 
            //make the model below BEFORE serializing it and add nulls 

            try
            { 
                var json = new JavaScriptSerializer().Serialize(new MinistryApiModel
                {
                    Event = Request.Form["Event"],
                    Purpose = Request.Form["Purpose"],
                    DesiredOutcome = Request.Form["DesiredOutcome"],
                    MinistryOwnerId = int.Parse(Request.Form["MinistryOwnerId"]),
                    PracticeId = int.Parse(Request.Form["PracticeId"]),
                    FunnelId = int.Parse(Request.Form["FunnelId"]),
                    LocationId = int.Parse(Request.Form["LocationId"]),
                    CampusId = int.Parse(Request.Form["CampusId"]),
                    StartDate = Request.Form["StartDate"],
                    EndDate = Request.Form["EndDate"],
                    FrequencyId = int.Parse(Request.Form["FrequencyId"]),
                    KidCare = Request.Form["KidCare"] == "true",
                    LevelOfImportanceId = int.Parse(Request.Form["LevelOfImportanceId"]),
                    ApprovalId = int.Parse(Request.Form["ApprovalId"]),
                    Comments = Request.Form["Comments"],
                    Archived = false,
                    UpInOutIds = Request.Form["UpInOutIds"].Split(',').Select(int.Parse).ToArray(),
                    ResourceInvolvementIds = Request.Form["SelectedResourceInvolvementIds"].Split(',').Select(int.Parse).ToArray()
                });

                var response = _apiHelper.Post(CompileUrl(apiAction), json);

                TempData["MessageType"] = "Success";
                TempData["Message"] = $"Ministry, {Request.Form["Name"]}, Created";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem creating this record. Please try again or contact your system administrator.";
                return View();
            }
        }

        // GET: Ministry/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, _token);

            if (response.IsSuccessful)
            {
                var ministry = JsonConvert.DeserializeObject<MinistryViewModel>(response.Content);
                var modelForView = CompileEditMinistryModel(ministry);
                return View(modelForView);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem retrieving this record. Please try again or contact your system administrator.";
            return View("Edit", null);
        }

        private MinistryEditViewModel CompileEditMinistryModel(MinistryViewModel modelWithData)
        {
            var ministryOwners = _ministryHelper.GetMinistryOwners();
            var practices = _ministryHelper.GetPractices();
            var funnels = _ministryHelper.GetFunnels();
            var campuses = _ministryHelper.GetCampuses();
            var locations = _ministryHelper.GetLocations();
            var levelOfImportances = _ministryHelper.GetLevelOfImportances();
            var approvals = _ministryHelper.GetApprovals();
            var frequencies = _ministryHelper.GetFrequencies();
            var upInOuts = _ministryHelper.GetUpInOutOptions();
            var resourceInvolvements = _ministryHelper.GetResourceInvolvementOptions();


            var ministryOwnerDropDown = ministryOwners.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var practiceDropDown = practices.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var funnelDropDown = funnels.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var campusDropDown = campuses.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var locationDropDown = locations.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var levelOfImportanceDropDown = levelOfImportances.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var approvalDropDown = approvals.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var frequencyDropDown = frequencies.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var upInOutListBox = upInOuts.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var resourceInvolvementListBox = resourceInvolvements.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });


            var viewModel = new MinistryEditViewModel
            {
                MinistryOwners = ministryOwnerDropDown,
                Practices = practiceDropDown,
                Funnels = funnelDropDown,
                Campuses = campusDropDown,
                Locations = locationDropDown,
                Approvals = approvalDropDown,
                Frequencies = frequencyDropDown,
                LevelOfImportances = levelOfImportanceDropDown,
                UpInOuts = upInOutListBox,
                ResourceInvolvements = resourceInvolvementListBox,
                Id = modelWithData.Id,
                Event = modelWithData.Event,
                Purpose = modelWithData.Purpose,
                DesiredOutcome = modelWithData.DesiredOutcome,
                MinistryOwnerId = modelWithData.MinistryOwnerId,
                PracticeId = modelWithData.PracticeId,
                FunnelId = modelWithData.FunnelId,
                LocationId = modelWithData.LocationId,
                CampusId = modelWithData.CampusId,
                StartDate = modelWithData.StartDate,
                EndDate = modelWithData.EndDate,
                FrequencyId = modelWithData.FrequencyId,
                KidCare = modelWithData.KidCare,
                LevelOfImportanceId = modelWithData.LevelOfImportanceId,
                ApprovalId = modelWithData.ApprovalId,
                Comments = modelWithData.Comments,
                UpInOutIds = modelWithData.UpInOutRelationships.Select(x => x.UpInOutId).ToArray(),
                SelectedResourceInvolvementIds = modelWithData.ResourceInvolvementRelationships.Select(x => x.ResourceInvolvementId).ToArray(),
                Archived = modelWithData.Archived
            };

            return viewModel;
        }

        // POST: Ministry/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var json = new JavaScriptSerializer().Serialize(new MinistryEditApiModel
                {
                    Id = int.Parse(Request.Form["Id"]),
                    Event = Request.Form["Event"],
                    Purpose = Request.Form["Purpose"],
                    DesiredOutcome = Request.Form["DesiredOutcome"],
                    MinistryOwnerId = int.Parse(Request.Form["MinistryOwnerId"]),
                    PracticeId = int.Parse(Request.Form["PracticeId"]),
                    FunnelId = int.Parse(Request.Form["FunnelId"]),
                    LocationId = int.Parse(Request.Form["LocationId"]),
                    CampusId = int.Parse(Request.Form["CampusId"]),
                    StartDate = Request.Form["StartDate"],
                    EndDate = Request.Form["EndDate"],
                    FrequencyId = int.Parse(Request.Form["FrequencyId"]),
                    KidCare = Request.Form["KidCare"] == "true",
                    LevelOfImportanceId = int.Parse(Request.Form["LevelOfImportanceId"]),
                    ApprovalId = int.Parse(Request.Form["ApprovalId"]),
                    Comments = Request.Form["Comments"],
                    Archived = Request.Form["Archived"] == "true",
                    UpInOutIds = Request.Form["UpInOutIds"].Split(',').Select(int.Parse).ToArray(),
                    ResourceInvolvementIds = Request.Form["SelectedResourceInvolvementIds"].Split(',').Select(int.Parse).ToArray(),
                    
                });

                var response = _apiHelper.Put(CompileUrl(apiAction) + $"?id={id}", json);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["MessageType"] = "Success";
                    TempData["Message"] = $"Ministry, {Request.Form["Event"]}, Updated";
                }
                else
                {
                    TempData["MessageType"] = "Danger";
                    TempData["Message"] = $"There was a problem updating this record. Please try again or contact your system administrator.";
                }

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MessageType"] = "Danger";
                TempData["Message"] = $"There was a problem creating this record. Please try again or contact your system administrator.";
                return View();
            }
        }

        // GET: Ministry/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, _token);

            if (response.IsSuccessful)
            {
                var ministry = JsonConvert.DeserializeObject<MinistryViewModel>(response.Content);
                return View(ministry);
            }

            TempData["MessageType"] = "Danger";
            TempData["Message"] = $"There was a problem finding this record. Please try again or contact your system administrator.";
            return View();
        }

        // POST: Ministry/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                var response = _apiHelper.Delete(CompileUrl(apiAction), id);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["MessageType"] = "Info";
                    TempData["Message"] = $"Ministry Deleted";
                }
                else
                {
                    TempData["MessageType"] = "Danger";
                    TempData["Message"] = $"There was a problem deleting this record. Please try again or contact your system administrator.";
                }

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

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
    [Authorize]
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
            var response = _apiHelper.Get(CompileUrl(apiAction), GetToken());

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
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, GetToken());

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
            var ministryOwners = _ministryHelper.GetMinistryOwners(GetToken()).Where(x => x.Archived == false);
            var practices = _ministryHelper.GetPractices(GetToken()).Where(x => x.Archived == false);
            var funnels = _ministryHelper.GetFunnels(GetToken()).Where(x => x.Archived == false);
            var campuses = _ministryHelper.GetCampuses(GetToken()).Where(x => x.Archived == false);
            var locations = _ministryHelper.GetLocations(GetToken()).Where(x => x.Archived == false);
            var levelOfImportances = _ministryHelper.GetLevelOfImportances(GetToken()).Where(x => x.Archived == false);
            var approvals = _ministryHelper.GetApprovals(GetToken()).Where(x => x.Archived == false);
            var frequencies = _ministryHelper.GetFrequencies(GetToken()).Where(x => x.Archived == false);
            var upInOuts = _ministryHelper.GetUpInOutOptions(GetToken()).Where(x => x.Archived == false);
            var resourceInvolvements = _ministryHelper.GetResourceInvolvementOptions(GetToken()).Where(x => x.Archived == false);
            

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

            //Set the Not Approved value as default
            int approvalId = -1;
            var approvalDefault = viewModel.Approvals.FirstOrDefault(x => x.Text == "Not Approved");
            if (approvalDefault != null)
            {
                int.TryParse(approvalDefault.Value, out approvalId);              
            }
            viewModel.ApprovalId = approvalId;

            //Disable the approval drop down if not in the correct role
            viewModel.CanApprove = CanApprove();
            
            return View(viewModel);
        }

        private bool CanApprove()
        {
            var role = ViewBag.Role;
            if (role != null && (role == "Database.Approver" || role == "Database.Admin"))
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public ActionResult Create(MinistryCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var json = new JavaScriptSerializer().Serialize(new MinistryApiModel
                    {
                        Event = model.Event,
                        Purpose = model.Purpose,
                        DesiredOutcome = model.DesiredOutcome,
                        MinistryOwnerId = model.MinistryOwnerId,
                        PracticeId = model.PracticeId,
                        FunnelId = model.FunnelId,
                        LocationId = model.LocationId,
                        CampusId = model.CampusId,
                        StartDate = model.StartDate.ToString(),
                        EndDate = model.EndDate.ToString(),
                        FrequencyId = model.FrequencyId,
                        KidCare = model.KidCare.ToString() == "true",
                        LevelOfImportanceId = model.LevelOfImportanceId,
                        ApprovalId = model.ApprovalId,
                        Comments = model.Comments,
                        Archived = false,
                        UpInOutIds = model.UpInOutIds != null ?
                            model.UpInOutIds :
                            new int[0],
                        ResourceInvolvementIds = model.SelectedResourceInvolvementIds != null ?
                            model.SelectedResourceInvolvementIds :
                            new int[0]
                    });

                    var response = _apiHelper.Post(CompileUrl(apiAction), json, GetToken());

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

            return Create();
        }


        // GET: Ministry/Edit/5
        public ActionResult Edit(int id)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, GetToken());

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
            var ministryOwners = _ministryHelper.GetMinistryOwners(GetToken());
            var practices = _ministryHelper.GetPractices(GetToken());
            var funnels = _ministryHelper.GetFunnels(GetToken());
            var campuses = _ministryHelper.GetCampuses(GetToken());
            var locations = _ministryHelper.GetLocations(GetToken());
            var levelOfImportances = _ministryHelper.GetLevelOfImportances(GetToken());
            var approvals = _ministryHelper.GetApprovals(GetToken());
            var frequencies = _ministryHelper.GetFrequencies(GetToken());
            var upInOuts = _ministryHelper.GetUpInOutOptions(GetToken());
            var resourceInvolvements = _ministryHelper.GetResourceInvolvementOptions(GetToken());


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

            viewModel.CanApprove = CanApprove();

            return viewModel;
        }

        // POST: Ministry/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MinistryEditViewModel model)
        {

         if (ModelState.IsValid)
         {
                try
                {
                    var json = new JavaScriptSerializer().Serialize(new MinistryEditApiModel
                    {
                        Id = model.Id,
                        Event = model.Event,
                        Purpose = model.Purpose,
                        DesiredOutcome = model.DesiredOutcome,
                        MinistryOwnerId = model.MinistryOwnerId,
                        PracticeId = model.PracticeId,
                        FunnelId = model.FunnelId,
                        LocationId = model.LocationId,
                        CampusId = model.CampusId,
                        StartDate = model.StartDate.ToString(),
                        EndDate = model.EndDate.ToString(),
                        FrequencyId = model.FrequencyId,
                        KidCare = model.KidCare.ToString() == "true",
                        LevelOfImportanceId = model.LevelOfImportanceId,
                        ApprovalId = model.ApprovalId,
                        Comments = model.Comments,
                        Archived = false,
                        UpInOutIds = model.UpInOutIds != null ?
                                model.UpInOutIds :
                                new int[0],
                        ResourceInvolvementIds = model.SelectedResourceInvolvementIds != null ?
                                model.SelectedResourceInvolvementIds :
                                new int[0]
                    });

                    var response = _apiHelper.Put(CompileUrl(apiAction) + $"?id={id}", json, GetToken());

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

            return Edit(id);
            
        }

        // GET: Ministry/Delete/5
        public ActionResult Delete(int id)
        {
            var response = _apiHelper.Get(CompileUrl(apiAction), "id", id, GetToken());

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

                var response = _apiHelper.Delete(CompileUrl(apiAction), id, GetToken());

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

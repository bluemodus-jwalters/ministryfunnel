﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using MinistryFunnel.Data;
using MinistryFunnel.Managers;
using MinistryFunnel.Models;
using MinistryFunnel.Repository;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;

namespace MinistryFunnel.Controllers
{
    //TODO: add unit tests
    //TODO: versioning
    //TODO: add a way to insert the many to many fields 
    [Route("api/ministry")]
    [ApiAuthorization(Role = "discovery_api_edit")]
    public class MinistryController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IMinistryRepository _ministryRepository;
        private readonly IApprovalRepository _approvalRepository;
        private readonly IUpInOutRepository _upInOutRepository;
        private readonly IResourceInvolvementRepository _resourceInvolvementRepository;
        private readonly ILoggerService _loggerService;

        public MinistryController()
        {
            _ministryRepository = new MinistryRepository();
            _approvalRepository = new ApprovalRepository();
            _upInOutRepository = new UpInOutRepository();
            _resourceInvolvementRepository = new ResourceInvolvementRepository();
            _loggerService = new LoggerService();
        }

        //Change this to only grab the fields it needs.
        [HttpGet]
        [ResponseType(typeof(List<MinimalMinistryViewModel>))]
        [Route("api/ministry/dashboard")]
        public List<MinimalMinistryViewModel> GetDashboard()
        {
            var ministries = _ministryRepository.GetDashboardList();

            List<MinimalMinistryViewModel> viewModel = new List<MinimalMinistryViewModel>();
            foreach (var ministry in ministries)
            {
                if (ministry.EndDate >= System.DateTime.Now && ministry.EndDate <= System.DateTime.Now.AddDays(31))
                {
                    viewModel.Add(new MinimalMinistryViewModel
                    {
                        Id = ministry.Id,
                        ApprovalName = ministry.Approval.Name,
                        Event = ministry.Event,
                        Purpose = ministry.Purpose,
                        MinistryOwnerName = ministry.MinistryOwner.Name,
                        CampusName = ministry.Campus.Name,
                        DesiredOutcome = ministry.DesiredOutcome,
                        PracticeName = ministry.Practice.Name,
                        FunnelName = ministry.Funnel.Name,
                        LocationName = ministry.Location.Name,
                        LevelOfImportanceName = ministry.LevelOfImportance.Name,
                        StartDate = (System.DateTime)(ministry.StartDate.HasValue ? ministry.StartDate : System.DateTime.MinValue),
                        EndDate = (System.DateTime)(ministry.EndDate.HasValue ? ministry.EndDate : System.DateTime.MinValue),
                        BigRock = ministry.BigRock,
                        KidCare = ministry.KidCare                       
                    });
                }

            }

            return viewModel.OrderBy(x => x.StartDate).ToList();
        }

        [HttpGet]
        [ResponseType(typeof(List<MinimalMinistryViewModel>))]
        [Route("api/ministry/calendar")]
        public List<MinimalMinistryViewModel> GetCalendar()
        {
            var ministries = _ministryRepository.GetCalendarList();

            List<MinimalMinistryViewModel> viewModel = new List<MinimalMinistryViewModel>();
            foreach (var ministry in ministries)
            {
                viewModel.Add(new MinimalMinistryViewModel
                {
                    Id = ministry.Id,
                    ApprovalName = ministry.Approval.Name,
                    Event = ministry.Event,
                    Purpose = ministry.Purpose,
                    MinistryOwnerName = ministry.MinistryOwner.Name,
                    CampusName = ministry.Campus.Name,
                    DesiredOutcome = ministry.DesiredOutcome,
                    PracticeName = ministry.Practice.Name,
                    FunnelName = ministry.Funnel.Name,
                    LocationName = ministry.Location.Name,
                    LevelOfImportanceName = ministry.LevelOfImportance.Name,
                    StartDate = (System.DateTime)(ministry.StartDate.HasValue ? ministry.StartDate : System.DateTime.MinValue),
                    EndDate = (System.DateTime)(ministry.EndDate.HasValue ? ministry.EndDate : System.DateTime.MinValue),
                    Frequency = ministry.Frequency.Name
                });
            }

            return viewModel;
        }

        // GET: api/ministry
        [HttpGet]
        [ResponseType(typeof(List<MinimalMinistryViewModel>))]
        public List<MinimalMinistryViewModel> GetAll()
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryController", "Ministry", "GetAll", null, null);

            //var ministries = _ministryRepository.GetMinistries().Where(x => x.EndDate >= System.DateTime.Now.AddYears(-1));
            var ministries = _ministryRepository.GetMinistries();
            var resourceInvolvementList = _resourceInvolvementRepository.GetResourceInvolvements().ToList();
            var upInOutList = _upInOutRepository.GetUpInOuts().ToList();



            List<MinimalMinistryViewModel> viewModel = new List<MinimalMinistryViewModel>();
            var rightEndDate = System.DateTime.Now.AddYears(-1);
            foreach (var ministry in ministries.Where(x => x.EndDate >= rightEndDate))
            {
                viewModel.Add(new MinimalMinistryViewModel
                {
                    Id = ministry.Id,
                    ApprovalName = ministry.Approval.Name,
                    Event = ministry.Event,
                    Purpose = ministry.Purpose,
                    MinistryOwnerName = ministry.MinistryOwner.Name,
                    CampusName = ministry.Campus.Name,
                    DesiredOutcome = ministry.DesiredOutcome,
                    PracticeName = ministry.Practice.Name,
                    FunnelName = ministry.Funnel.Name,
                    LocationName = ministry.Location.Name,
                    LevelOfImportanceName = ministry.LevelOfImportance.Name,
                    StartDate = (System.DateTime)(ministry.StartDate.HasValue ? ministry.StartDate : System.DateTime.MinValue),
                    EndDate = (System.DateTime)(ministry.EndDate.HasValue ? ministry.EndDate : System.DateTime.MinValue),
                    BigRock = ministry.BigRock,
                    KidCare = ministry.KidCare,
                    ResourceInvolvementRelationships = ResourceInvolvementMapping(ministry.ResourceInvolvementRelationship, resourceInvolvementList),
                    UpInOutRelationships = UpInOutMapping(ministry.UpInOutRelationships, upInOutList)
                });
            }

            return viewModel;
            
        }


        //// GET: api/ministrys/5
        [HttpGet]
        [ResponseType(typeof(MinistryViewModel))]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryController", "Ministry", "Get By Id", id.ToString(), null);

            Ministry ministry = _ministryRepository.GetMinistryById(id);
            if (ministry == null)
            {
                return NotFound();
            }

            return Ok(MapViewModel(ministry));
        }

        private MinistryViewModel MapViewModel(Ministry ministry)
        {
            return new MinistryViewModel
            {
                Id = ministry.Id,
                Event = ministry.Event,
                Purpose = ministry.Purpose,
                DesiredOutcome = ministry.DesiredOutcome,
                MinistryOwnerId = ministry.MinistryOwnerId,
                MinistryOwnerName = ministry.MinistryOwner.Name,
                PracticeId = ministry.PracticeId,
                PracticeName = ministry.Practice.Name,
                FunnelId = ministry.FunnelId,
                FunnelName = ministry.Funnel.Name,
                LocationId = ministry.LocationId,
                LocationName = ministry.Location.Name,
                CampusId = ministry.CampusId,
                CampusName = ministry.Campus.Name,
                UpInOutRelationships = ReturnUpInOutRelationshipViewModel(ministry.UpInOutRelationships),
                ResourceInvolvementRelationships = ReturnResourceInvolvementRelationshipViewModel(ministry.ResourceInvolvementRelationship),
                StartDate = (System.DateTime)(ministry.StartDate.HasValue ? ministry.StartDate : System.DateTime.MinValue),
                EndDate = (System.DateTime)(ministry.EndDate.HasValue ? ministry.EndDate : System.DateTime.MinValue),
                FrequencyId = ministry.FrequencyId,
                FrequencyName = ministry.Frequency.Name,
                KidCare = ministry.KidCare,
                LevelOfImportanceId = ministry.LevelOfImportanceId,
                LevelOfImportanceName = ministry.LevelOfImportance.Name,
                ApprovalId = ministry.ApprovalId,
                ApprovalName = ministry.Approval.Name,
                Comments = ministry.Comments,
                CreatedDateTime = (System.DateTime)(ministry.CreatedDateTime.HasValue ? ministry.CreatedDateTime : System.DateTime.MinValue),
                ModifiedDateTime = (System.DateTime)(ministry.ModifiedDateTime.HasValue ? ministry.ModifiedDateTime : System.DateTime.MinValue),
                Archived = ministry.Archived,
                BigRock = ministry.BigRock
            };
        }

        

        private ICollection<UpInOutRelationshipViewModel> ReturnUpInOutRelationshipViewModel(ICollection<UpInOutRelationship> relationships)
        {
            var repo = new UpInOutRepository();
            if (relationships != null)
            {
                ICollection<UpInOutRelationshipViewModel> viewModel = new List<UpInOutRelationshipViewModel>();
                foreach (var relationship in relationships)
                {
                    viewModel.Add(new UpInOutRelationshipViewModel
                    {
                        UpInOutId = relationship.UpInOutId,
                        MinistryId = relationship.MinistryId,
                        UpInOutName = repo.GetUpInOutById(relationship.UpInOutId).Name
                    });
                }
                return viewModel;

            }

            return null;
            
        }

        private ICollection<ResourceInvolvementRelationshipViewModel> ReturnResourceInvolvementRelationshipViewModel(ICollection<ResourceInvolvementRelationship> relationships)
        {
            

            var repo = new ResourceInvolvementRepository();
            if (relationships != null)
            {
                ICollection<ResourceInvolvementRelationshipViewModel> viewModel = new List<ResourceInvolvementRelationshipViewModel>();
                foreach (var relationship in relationships)
                {
                    viewModel.Add(new ResourceInvolvementRelationshipViewModel
                    {
                        ResourceInvolvementId = relationship.ResourceInvolvementId,
                        MinistryId = relationship.MinistryId,
                        ResourceInvolvementName = repo.GetResourceInvolvementById(relationship.ResourceInvolvementId).Name
                    });
                }
                return viewModel;

            }

            return null;

        }

        private ICollection<ResourceInvolvementRelationshipViewModel> ResourceInvolvementMapping(ICollection<ResourceInvolvementRelationship> relationships, List<ResourceInvolvement> mappingList)
        {
            if (relationships != null)
            {
                ICollection<ResourceInvolvementRelationshipViewModel> viewModel = new List<ResourceInvolvementRelationshipViewModel>();
                foreach (var relationship in relationships)
                {
                    viewModel.Add(new ResourceInvolvementRelationshipViewModel
                    {
                        ResourceInvolvementId = relationship.ResourceInvolvementId,
                        MinistryId = relationship.MinistryId,
                        ResourceInvolvementName = mappingList.Single(s => s.Id == relationship.ResourceInvolvementId).Name
                    }); ;
                }
                return viewModel;

            }

            return null;
        }

        private ICollection<UpInOutRelationshipViewModel> UpInOutMapping(ICollection<UpInOutRelationship> upInOuts, List<UpInOut> mappingList)
        {
            if (upInOuts != null)
            {
                ICollection<UpInOutRelationshipViewModel> viewModel = new List<UpInOutRelationshipViewModel>();
                foreach (var relationship in upInOuts)
                {
                    viewModel.Add(new UpInOutRelationshipViewModel
                    {
                        UpInOutId = relationship.UpInOutId,
                        MinistryId = relationship.MinistryId,
                        UpInOutName = mappingList.Single(s => s.Id == relationship.UpInOutId).Name
                    }); ;
                }
                return viewModel;

            }

            return null;
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Ministry>))]
        public IHttpActionResult GetByEvent([FromUri] string searchText)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryController", "Ministry", "Get By Event", searchText, null);

            //TODO: sanitize text
            var results = _ministryRepository.SearchMinistryByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            IQueryable<MinistryViewModel> viewModel = results.Select(ministry => new MinistryViewModel
            {
                Id = ministry.Id,
                Event = ministry.Event,
                Purpose = ministry.Purpose,
                DesiredOutcome = ministry.DesiredOutcome,
                MinistryOwnerId = ministry.MinistryOwnerId,
                MinistryOwnerName = ministry.MinistryOwner.Name,
                PracticeId = ministry.PracticeId,
                PracticeName = ministry.Practice.Name,
                FunnelId = ministry.FunnelId,
                FunnelName = ministry.Funnel.Name,
                LocationId = ministry.LocationId,
                LocationName = ministry.Location.Name,
                CampusId = ministry.CampusId,
                CampusName = ministry.Campus.Name,
                UpInOutRelationships = ReturnUpInOutRelationshipViewModel(ministry.UpInOutRelationships),
                ResourceInvolvementRelationships = ReturnResourceInvolvementRelationshipViewModel(ministry.ResourceInvolvementRelationship),
                StartDate = (System.DateTime)(ministry.StartDate.HasValue ? ministry.StartDate : System.DateTime.MinValue),
                EndDate = (System.DateTime)(ministry.EndDate.HasValue ? ministry.EndDate : System.DateTime.MinValue),
                FrequencyId = ministry.FrequencyId,
                FrequencyName = ministry.Frequency.Name,
                KidCare = ministry.KidCare,
                LevelOfImportanceId = ministry.LevelOfImportanceId,
                LevelOfImportanceName = ministry.LevelOfImportance.Name,
                ApprovalId = ministry.ApprovalId,
                CreatedDateTime = (System.DateTime)(ministry.CreatedDateTime.HasValue ? ministry.CreatedDateTime : System.DateTime.MinValue),
                ModifiedDateTime = (System.DateTime)(ministry.ModifiedDateTime.HasValue ? ministry.ModifiedDateTime : System.DateTime.MinValue),
                Archived = ministry.Archived,
                BigRock = ministry.BigRock
            });

            return Ok(results);
        }

        // PUT: api/Ministry/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Ministry ministry)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryController", "Ministry", "Update", ministry.ToString(), null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ministry.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedMinistry = _ministryRepository.UpdateMinistry(ministry);

            if (updatedMinistry == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/ministry?id={ministry.Id}");
        }

        // POST: api/Ministry
        [HttpPost]
        [ResponseType(typeof(Ministry))]
        public IHttpActionResult Insert(Ministry ministry)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryController", "Ministry", "Insert", ministry.ToString(), null);

            //test trying to insert bad data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMinistry = _ministryRepository.InsertMinistry(ministry);

            //var updatedMinistry = _ministryRepository.GetMinistryById(createdMinistry.Id);

            if (createdMinistry == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/ministry?id={createdMinistry.Id}", createdMinistry);
        }

        // DELETE: api/Ministry/5
        [HttpDelete]
        [ResponseType(typeof(Ministry))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "MinistryController", "Ministry", "Delete", id.ToString(), null);

            var deletedMinistry = _ministryRepository.DeleteMinistry(id);

            if (deletedMinistry == null)
            {
                return BadRequest("There was a problem deleting your ministry. Please try again.");
            }

            return Ok(deletedMinistry);
        }

        [HttpPost]
        [Route("api/ministry/savemultiple")]
        public IHttpActionResult SaveMultipleMinistry([FromBody] string ids)
        {

            var listofids = ids.Split(';');
            int approvalId = GetApprovalId();
            bool errorHappened = false;

            foreach (var stringministryid in listofids)
            {
                int ministryid = int.Parse(stringministryid);

                var currentMinistryModel = _ministryRepository.GetMinistryById(ministryid);
                //The Resource Involvement Ids and Up In Out Ids are not being translated at the repo level. Maybe I should improve that.
                currentMinistryModel.ResourceInvolvementIds = currentMinistryModel.ResourceInvolvementRelationship.Select(x => x.ResourceInvolvementId).ToArray();
                currentMinistryModel.UpInOutIds = currentMinistryModel.UpInOutRelationships.Select(x => x.UpInOutId).ToArray();
                currentMinistryModel.ApprovalId = approvalId;

                //resource involvement and up in out are not populating correctly



                var updatedMinistry = _ministryRepository.UpdateMinistry(currentMinistryModel);

                if (updatedMinistry == null)
                {
                    errorHappened = true;
                }
            }

            if (errorHappened)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok("done");
        }

        private int GetApprovalId()
        {
            var approvalQuery = _approvalRepository.SearchApprovalByName("Approved");
            return approvalQuery.FirstOrDefault().Id;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
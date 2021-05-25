using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using MinistryFunnel.Data;
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
    public class MinistryController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IMinistryRepository _ministryRepository;
        private readonly ILoggerService _loggerService;
        private readonly string _user;

        public MinistryController()
        {
            _ministryRepository = new MinistryRepository();
            _loggerService = new LoggerService();
            _user = "Jordan";
        }

        [HttpGet]
        [ResponseType(typeof(List<MinistryViewModel>))]
        [Route("api/ministry/dashboard")]
        public List<MinistryViewModel> GetDashboard()
        {
            var ministries = _ministryRepository.GetDashboardList();

            List<MinistryViewModel> viewModel = new List<MinistryViewModel>();
            foreach (var ministry in ministries)
            {
                viewModel.Add(MapViewModel(ministry));
            }

            return viewModel;
        }

        // GET: api/ministry
        [HttpGet]
        [ResponseType(typeof(List<MinistryViewModel>))]
        public List<MinistryViewModel> GetAll()
        {
            _loggerService.CreateLog(_user, "API", "MinistryController", "Ministry", "GetAll", null, null);

            //This does not include the Up In Out Relationship names... not sure if it should yet or not. Seems to cause circular referencing
            var ministries = _ministryRepository.GetMinistries();


            List<MinistryViewModel> viewModel = new List<MinistryViewModel>();
            foreach (var ministry in ministries)
            {
                viewModel.Add(MapViewModel(ministry));
            }

            return viewModel;
            
        }


        //// GET: api/ministrys/5
        [HttpGet]
        [ResponseType(typeof(MinistryViewModel))]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(_user, "API", "MinistryController", "Ministry", "Get By Id", id.ToString(), null);

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
                Archived = ministry.Archived
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

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Ministry>))]
        public IHttpActionResult GetByEvent([FromUri] string searchText)
        {
            _loggerService.CreateLog(_user, "API", "MinistryController", "Ministry", "Get By Event", searchText, null);

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
                Archived = ministry.Archived
            });

            return Ok(results);
        }

        // PUT: api/Ministry/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Ministry ministry)
        {
            _loggerService.CreateLog(_user, "API", "MinistryController", "Ministry", "Update", ministry.ToString(), null);

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
            _loggerService.CreateLog(_user, "API", "MinistryController", "Ministry", "Insert", ministry.ToString(), null);

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
            _loggerService.CreateLog(_user, "API", "MinistryController", "Ministry", "Delete", id.ToString(), null);

            var deletedMinistry = _ministryRepository.DeleteMinistry(id);

            if (deletedMinistry == null)
            {
                return BadRequest("There was a problem deleting your ministry. Please try again.");
            }

            return Ok(deletedMinistry);
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
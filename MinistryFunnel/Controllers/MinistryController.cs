using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository;
using MinistryFunnel.Repository.Interfaces;

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

        public MinistryController()
        {
            _ministryRepository = new MinistryRepository();
        }

        // GET: api/ministry
        [HttpGet]
        [ResponseType(typeof(IQueryable<MinistryViewModel>))]
        public IQueryable<MinistryViewModel> GetAll()
        {
            //This does not include the Up In Out Relationship names... not sure if it should yet or not. Seems to cause circular referencing
            var ministries = _ministryRepository.GetMinistries();
            
            IQueryable<MinistryViewModel> viewModel = ministries.Select(min => new MinistryViewModel
            {
                Id = min.Id,
                Event = min.Event,
                Purpose = min.Purpose,
                DesiredOutcome = min.DesiredOutcome,
                MinistryOwnerId = min.MinistryOwnerId,
                PracticeId = min.PracticeId,
                FunnelId = min.FunnelId,
                LocationId = min.LocationId,
                CampusId = min.CampusId,
                UpInOutRelationships = min.UpInOutRelationships, 
                ResourceInvolvementRelationships = min.ResourceInvolvementRelationship,
                StartDate = (System.DateTime)(min.StartDate.HasValue ? min.StartDate : System.DateTime.MinValue),
                EndDate = (System.DateTime)(min.EndDate.HasValue ? min.EndDate : System.DateTime.MinValue),
                FrequencyId = min.FrequencyId,
                FrequencyName = min.Frequency.Name,
                KidCare = min.KidCare,
                LevelOfImportanceId = min.LevelOfImportanceId,
                ApprovalId = min.ApprovalId,
                Comments = min.Comments,
                CreatedDateTime = (System.DateTime)(min.CreatedDateTime.HasValue ? min.CreatedDateTime : System.DateTime.MinValue),
                ModifiedDateTime = (System.DateTime)(min.ModifiedDateTime.HasValue ? min.ModifiedDateTime : System.DateTime.MinValue),
                Archived = min.Archived
            });
            return viewModel;
            
        }


        //// GET: api/ministrys/5
        [HttpGet]
        [ResponseType(typeof(MinistryViewModel))]
        public IHttpActionResult GetById(int id)
        {
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
                PracticeId = ministry.PracticeId,
                FunnelId = ministry.FunnelId,
                LocationId = ministry.LocationId,
                CampusId = ministry.CampusId,
                UpInOutRelationships = ministry.UpInOutRelationships, //figure out how to filter these down
                //UpInOutRelationships = (IQueryable<UpInOutRelationship>)min.UpInOutRelationships.Select(upinout => new UpInOutRelationship
                //{
                //    MinistryId = upinout.MinistryId,
                //    UpInOutId = upinout.UpInOutId
                //})
                ResourceInvolvementRelationships = ministry.ResourceInvolvementRelationship,
                StartDate = (System.DateTime)(ministry.StartDate.HasValue ? ministry.StartDate : System.DateTime.MinValue),
                EndDate = (System.DateTime)(ministry.EndDate.HasValue ? ministry.EndDate : System.DateTime.MinValue),
                FrequencyId = ministry.FrequencyId,
                FrequencyName = ministry.Frequency.Name,
                KidCare = ministry.KidCare,
                LevelOfImportanceId = ministry.LevelOfImportanceId,
                ApprovalId = ministry.ApprovalId,
                CreatedDateTime = (System.DateTime)(ministry.CreatedDateTime.HasValue ? ministry.CreatedDateTime : System.DateTime.MinValue),
                ModifiedDateTime = (System.DateTime)(ministry.ModifiedDateTime.HasValue ? ministry.ModifiedDateTime : System.DateTime.MinValue),
                Archived = ministry.Archived
            };
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Ministry>))]
        public IHttpActionResult GetByEvent([FromUri] string searchText)
        {
            //TODO: sanitize text
            var results = _ministryRepository.SearchMinistryByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            IQueryable<MinistryViewModel> viewModel = results.Select(min => new MinistryViewModel
            {
                Id = min.Id,
                Event = min.Event,
                Purpose = min.Purpose,
                DesiredOutcome = min.DesiredOutcome,
                MinistryOwnerId = min.MinistryOwnerId,
                PracticeId = min.PracticeId,
                FunnelId = min.FunnelId,
                LocationId = min.LocationId,
                CampusId = min.CampusId,
                UpInOutRelationships = min.UpInOutRelationships, //figure out how to filter these down
                ResourceInvolvementRelationships = min.ResourceInvolvementRelationship,
                StartDate = (System.DateTime)(min.StartDate.HasValue ? min.StartDate : System.DateTime.MinValue),
                EndDate = (System.DateTime)(min.EndDate.HasValue ? min.EndDate : System.DateTime.MinValue),
                FrequencyId = min.FrequencyId,
                FrequencyName = min.Frequency.Name,
                KidCare = min.KidCare,
                LevelOfImportanceId = min.LevelOfImportanceId,
                ApprovalId = min.ApprovalId,
                Comments = min.Comments,
                CreatedDateTime = (System.DateTime)(min.CreatedDateTime.HasValue ? min.CreatedDateTime : System.DateTime.MinValue),
                ModifiedDateTime = (System.DateTime)(min.ModifiedDateTime.HasValue ? min.ModifiedDateTime : System.DateTime.MinValue),
                Archived = min.Archived
            });

            return Ok(results);
        }

        // PUT: api/Ministry/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Ministry ministry)
        {
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
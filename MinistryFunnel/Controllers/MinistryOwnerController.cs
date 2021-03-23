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
    [Route("api/ministryOwner")]
    public class MinistryOwnerController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IMinistryOwnerRepository _ministryOwnerRepository;

        public MinistryOwnerController()
        {
            _ministryOwnerRepository = new MinistryOwnerRepository();
        }

        // GET: api/MinistryOwners
        [HttpGet]
        [ResponseType(typeof(IQueryable<MinistryOwner>))]
        public IQueryable<MinistryOwner> GetAll()
        {
            return _ministryOwnerRepository.GetMinistryOwners();
        }

        // GET: api/MinistryOwners/5
        [HttpGet]
        [ResponseType(typeof(MinistryOwner))]
        public IHttpActionResult GetById(int id)
        {
            MinistryOwner ministryOwner = _ministryOwnerRepository.GetMinistryOwnerById(id);
            if (ministryOwner == null)
            {
                return NotFound();
            }

            return Ok(ministryOwner);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<MinistryOwner>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            var results = _ministryOwnerRepository.SearchMinistryOwnerByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/MinistryOwners/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, [FromBody] MinistryOwner ministryOwner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ministryOwner.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedMinistryOwner = _ministryOwnerRepository.UpdateMinistryOwner(ministryOwner);

            if (updatedMinistryOwner == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/ministryowner?id={ministryOwner.Id}");
        }

        // POST: api/MinistryOwners
        [HttpPost]
        [ResponseType(typeof(MinistryOwner))]
        public IHttpActionResult Insert(MinistryOwner ministryOwner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdMinistryOwner = _ministryOwnerRepository.InsertMinistryOwner(ministryOwner);

            if (createdMinistryOwner == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/ministryOwner?id={createdMinistryOwner.Id}", createdMinistryOwner);
        }

        // DELETE: api/MinistryOwners/5
        [HttpDelete]
        [ResponseType(typeof(MinistryOwner))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            var deletedMinistryOwner = _ministryOwnerRepository.DeleteMinistryOwner(id);

            if (deletedMinistryOwner == null)
            {
                return BadRequest("There was a problem deleting your ministryOwner. Please try again.");
            }

            return Ok(deletedMinistryOwner);
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
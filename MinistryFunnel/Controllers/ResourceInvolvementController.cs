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
    [Route("api/resourceInvolvement")]
    public class ResourceInvolvementController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IResourceInvolvementRepository _resourceInvolvementRepository;

        public ResourceInvolvementController()
        {
            _resourceInvolvementRepository = new ResourceInvolvementRepository();
        }

        // GET: api/ResourceInvolvements
        [HttpGet]
        [ResponseType(typeof(IQueryable<ResourceInvolvement>))]
        public IQueryable<ResourceInvolvement> GetAll()
        {
            return _resourceInvolvementRepository.GetResourceInvolvements();
        }

        // GET: api/ResourceInvolvements/5
        [HttpGet]
        [ResponseType(typeof(ResourceInvolvement))]
        public IHttpActionResult GetById(int id)
        {
            ResourceInvolvement resourceInvolvement = _resourceInvolvementRepository.GetResourceInvolvementById(id);
            if (resourceInvolvement == null)
            {
                return NotFound();
            }

            return Ok(resourceInvolvement);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<ResourceInvolvement>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            var results = _resourceInvolvementRepository.SearchResourceInvolvementByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/ResourceInvolvements/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, ResourceInvolvement resourceInvolvement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resourceInvolvement.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedResourceInvolvement = _resourceInvolvementRepository.UpdateResourceInvolvement(resourceInvolvement);

            if (updatedResourceInvolvement == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/resourceInvolvement?id={resourceInvolvement.Id}");
        }

        // POST: api/ResourceInvolvements
        [HttpPost]
        [ResponseType(typeof(ResourceInvolvement))]
        public IHttpActionResult Insert(ResourceInvolvement resourceInvolvement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdResourceInvolvement = _resourceInvolvementRepository.InsertResourceInvolvement(resourceInvolvement);

            if (createdResourceInvolvement == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/resourceInvolvement?id={createdResourceInvolvement.Id}", createdResourceInvolvement);
        }

        // DELETE: api/ResourceInvolvements/5
        [HttpDelete]
        [ResponseType(typeof(ResourceInvolvement))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            var deletedResourceInvolvement = _resourceInvolvementRepository.DeleteResourceInvolvement(id);

            if (deletedResourceInvolvement == null)
            {
                return BadRequest("There was a problem deleting your resourceInvolvement. Please try again.");
            }

            return Ok(deletedResourceInvolvement);
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
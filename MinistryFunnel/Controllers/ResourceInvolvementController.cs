using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
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
    [Route("api/resourceInvolvement")]
    [ApiAuthorization(Role = "discovery_api_edit")]
    public class ResourceInvolvementController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IResourceInvolvementRepository _resourceInvolvementRepository;
        private readonly ILoggerService _loggerService;

        public ResourceInvolvementController()
        {
            _resourceInvolvementRepository = new ResourceInvolvementRepository();
            _loggerService = new LoggerService();
        }

        // GET: api/ResourceInvolvements
        [HttpGet]
        [ResponseType(typeof(IQueryable<ResourceInvolvement>))]
        public IQueryable<ResourceInvolvement> GetAll()
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "ResourceInvolvementController", "ResourceInvolvement", "GetAll", null, null);

            return _resourceInvolvementRepository.GetResourceInvolvements();
        }

        // GET: api/ResourceInvolvements/5
        [HttpGet]
        [ResponseType(typeof(ResourceInvolvement))]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "ResourceInvolvementController", "ResourceInvolvement", "Get By Id", id.ToString(), null);

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
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "ResourceInvolvementController", "ResourceInvolvement", "Get by Name", searchText, null);

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
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "ResourceInvolvementController", "ResourceInvolvement", "Update", resourceInvolvement.ToString(), null);

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
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "ResourceInvolvementController", "ResourceInvolvement", "Insert", resourceInvolvement.ToString(), null);

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
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "ResourceInvolvementController", "ResourceInvolvement", "Delete", id.ToString(), null);

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
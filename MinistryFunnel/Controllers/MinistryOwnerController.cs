using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;

namespace MinistryFunnel.Controllers
{
    //TODO: add unit tests
    //TODO: versioning
    [Route("api/ministryOwner")]
    public class MinistryOwnerController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IMinistryOwnerRepository _ministryOwnerRepository;
        private readonly ILoggerService _loggerService;
        private readonly string _user;

        public MinistryOwnerController()
        {
            _ministryOwnerRepository = new MinistryOwnerRepository();
            _loggerService = new LoggerService();
            _user = "jordan"; //HttpContext.Current.Items["email"].ToString();
        }

        // GET: api/MinistryOwners
        [HttpGet]
        [ResponseType(typeof(IQueryable<MinistryOwner>))]
        public IQueryable<MinistryOwner> GetAll()
        {
            _loggerService.CreateLog(_user, "API", "MinistryOwnerController", "MinistryOwner", "GetAll", null, null);

            return _ministryOwnerRepository.GetMinistryOwners();
        }

        // GET: api/MinistryOwners/5
        [HttpGet]
        [ResponseType(typeof(MinistryOwner))]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(_user, "API", "MinistryOwnerController", "MinistryOwner", "Get By Id", id.ToString(), null);

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
            _loggerService.CreateLog(_user, "API", "MinistryOwnerController", "MinistryOwner", "Get by Name", searchText, null);

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
            _loggerService.CreateLog(_user, "API", "MinistryOwnerController", "MinistryOwner", "Update", ministryOwner.ToString(), null);

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
            _loggerService.CreateLog(_user, "API", "MinistryOwnerController", "MinistryOwner", "Insert", ministryOwner.ToString(), null);

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
            _loggerService.CreateLog(_user, "API", "MinistryOwnerController", "MinistryOwner", "Delete", id.ToString(), null);

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
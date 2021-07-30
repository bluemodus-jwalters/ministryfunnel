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
    [Route("api/levelOfImportance")]
    [ApiAuthorization(Role = "discovery_api_edit")]
    public class LevelOfImportanceController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILevelOfImportanceRepository _levelOfImportanceRepository;
        private readonly ILoggerService _loggerService;

        public LevelOfImportanceController()
        {
            _levelOfImportanceRepository = new LevelOfImportanceRepository();
            _loggerService = new LoggerService();
        }

        // GET: api/LevelOfImportances
        [HttpGet]
        [ResponseType(typeof(IQueryable<LevelOfImportance>))]
        public IQueryable<LevelOfImportance> GetAll()
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LevelOfImportanceController", "LevelOfImportance", "GetAll", null, null);

            return _levelOfImportanceRepository.GetLevelOfImportances();
        }

        // GET: api/LevelOfImportances/5
        [HttpGet]
        [ResponseType(typeof(LevelOfImportance))]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LevelOfImportanceController", "LevelOfImportance", "Get By Id", id.ToString(), null);

            LevelOfImportance levelOfImportance = _levelOfImportanceRepository.GetLevelOfImportanceById(id);
            if (levelOfImportance == null)
            {
                return NotFound();
            }

            return Ok(levelOfImportance);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<LevelOfImportance>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LevelOfImportanceController", "LevelOfImportance", "Get by Name", searchText, null);

            var results = _levelOfImportanceRepository.SearchLevelOfImportanceByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/LevelOfImportances/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, LevelOfImportance levelOfImportance)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LevelOfImportanceController", "LevelOfImportance", "Update", levelOfImportance.ToString(), null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != levelOfImportance.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedLevelOfImportance = _levelOfImportanceRepository.UpdateLevelOfImportance(levelOfImportance);

            if (updatedLevelOfImportance == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/levelOfImportance?id={levelOfImportance.Id}");
        }

        // POST: api/LevelOfImportances
        [HttpPost]
        [ResponseType(typeof(LevelOfImportance))]
        public IHttpActionResult Insert(LevelOfImportance levelOfImportance)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LevelOfImportanceController", "LevelOfImportance", "Insert", levelOfImportance.ToString(), null);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLevelOfImportance = _levelOfImportanceRepository.InsertLevelOfImportance(levelOfImportance);

            if (createdLevelOfImportance == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/levelOfImportance?id={createdLevelOfImportance.Id}", createdLevelOfImportance);
        }

        // DELETE: api/LevelOfImportances/5
        [HttpDelete]
        [ResponseType(typeof(LevelOfImportance))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            _loggerService.CreateLog(HttpContext.Current.Items["email"].ToString(), "API", "LevelOfImportanceController", "LevelOfImportance", "Delete", id.ToString(), null);

            var deletedLevelOfImportance = _levelOfImportanceRepository.DeleteLevelOfImportance(id);

            if (deletedLevelOfImportance == null)
            {
                return BadRequest("There was a problem deleting your levelOfImportance. Please try again.");
            }

            return Ok(deletedLevelOfImportance);
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
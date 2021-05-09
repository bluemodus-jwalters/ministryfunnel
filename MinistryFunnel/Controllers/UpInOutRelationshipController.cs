using System.Linq;
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
    [Route("api/upInOutRelationship")]
    public class UpInOutRelationshipController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IUpInOutRelatinshipRepository _upInOutRelationshipRepository;
        private readonly ILoggerService _loggerService;
        private readonly string _user;

        public UpInOutRelationshipController()
        {
            _upInOutRelationshipRepository = new UpInOutRelationshipRepository();
            _loggerService = new LoggerService();
            _user = "Jordan";
        }

        // GET: api/UpInOuts
        [HttpGet]
        [ResponseType(typeof(IQueryable<UpInOut>))]
        public IQueryable<UpInOutRelationship> GetAll()
        {
            _loggerService.CreateLog(_user, "API", "UpInOutRelationshipController", "UpInOutRelationship", "GetAll", null, null);

            return _upInOutRelationshipRepository.GetUpInOutRelationships();
        }

        // GET: api/UpInOuts
        [HttpGet]
        [ResponseType(typeof(IQueryable<UpInOut>))]
        public IQueryable<UpInOutRelationship> GetAllByMinistryId(int ministryId)
        {
            _loggerService.CreateLog(_user, "API", "UpInOutRelationshipController", "UpInOutRelationship", "Get By Id", ministryId.ToString(), null);

            return _upInOutRelationshipRepository.GetUpInOutRelationshipsByMinistryId(ministryId);
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
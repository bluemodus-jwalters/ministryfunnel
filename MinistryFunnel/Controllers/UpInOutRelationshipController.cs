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
    [Route("api/upInOutRelationship")]
    public class UpInOutRelationshipController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IUpInOutRelatinshipRepository _upInOutRelationshipRepository;

        public UpInOutRelationshipController()
        {
            _upInOutRelationshipRepository = new UpInOutRelationshipRepository();
        }

        // GET: api/UpInOuts
        [HttpGet]
        [ResponseType(typeof(IQueryable<UpInOut>))]
        public IQueryable<UpInOutRelationship> GetAll()
        {
            return _upInOutRelationshipRepository.GetUpInOutRelationships();
        }

        // GET: api/UpInOuts
        [HttpGet]
        [ResponseType(typeof(IQueryable<UpInOut>))]
        public IQueryable<UpInOutRelationship> GetAllByMinistryId(int ministryId)
        {
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
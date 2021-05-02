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
    [Route("api/approval")]
    public class ApprovalController : ApiController
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly IApprovalRepository _approvalRepository;
        private readonly ILoggerService _loggerService;
        private readonly string _user;

        public ApprovalController()
        {
            _approvalRepository = new ApprovalRepository();
            _loggerService = new LoggerService();
            _user = "Jordan";
        }

        // GET: api/Approvals
        [HttpGet]
        [ResponseType(typeof(IQueryable<Approval>))]
        public IQueryable<Approval> GetAll()
        {
            _loggerService.CreateLog(_user, "API", "ApprovalController", "Approval", "GetAll", null, null);
            return _approvalRepository.GetApprovals();
        }

        // GET: api/Approvals/5
        [HttpGet]
        [ResponseType(typeof(Approval))]
        public IHttpActionResult GetById(int id)
        {
            _loggerService.CreateLog(_user, "API", "ApprovalController", "Approval", "Get By Id", id.ToString(), null);
            Approval approval = _approvalRepository.GetApprovalById(id);
            if (approval == null)
            {
                return NotFound();
            }

            return Ok(approval);
        }

        //TODO: make this a /searchText one day
        [HttpGet]
        [ResponseType(typeof(IQueryable<Approval>))]
        public IHttpActionResult GetByName([FromUri] string searchText)
        {
            //TODO: sanitize text
            _loggerService.CreateLog(_user, "API", "ApprovalController", "Approval", "Get by Name", searchText, null);
            var results = _approvalRepository.SearchApprovalByName(searchText);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // PUT: api/Approvals/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int id, Approval approval)
        {
            _loggerService.CreateLog(_user, "API", "ApprovalController", "Approval", "Update", approval.ToString(), null);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != approval.Id)
            {
                return BadRequest("The Id's do not match");
            }

            var updatedApproval = _approvalRepository.UpdateApproval(approval);

            if (updatedApproval == null)
            {
                return BadRequest("There was a problem updating your record. Please try again");
            }

            return Ok($"api/approval?id={approval.Id}");
        }

        // POST: api/Approvals
        [HttpPost]
        [ResponseType(typeof(Approval))]
        public IHttpActionResult Insert(Approval approval)
        {
            _loggerService.CreateLog(_user, "API", "ApprovalController", "Approval", "Insert", approval.ToString(), null);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdApproval = _approvalRepository.InsertApproval(approval);

            if (createdApproval == null)
            {
                BadRequest("There was a problem inserting your record. Please try again.");
            }

            return Created($"api/approval?id={createdApproval.Id}", createdApproval);
        }

        // DELETE: api/Approvals/5
        [HttpDelete]
        [ResponseType(typeof(Approval))]
        public IHttpActionResult Delete([FromBody] int id)
        {
            _loggerService.CreateLog(_user, "API", "ApprovalController", "Approval", "Delete", id.ToString(), null);
            var deletedApproval = _approvalRepository.DeleteApproval(id);

            if (deletedApproval == null)
            {
                return BadRequest("There was a problem deleting your approval. Please try again.");
            }

            return Ok(deletedApproval);
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
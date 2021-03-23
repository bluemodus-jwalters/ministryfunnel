using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MinistryFunnel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinistryOwnerController : ControllerBase
    {
        private readonly MinistryOwner[] _ministryOwners;
        public MinistryOwnerController() {
            MinistryOwner[] ministryOwners = new MinistryOwner[]
            {
                new MinistryOwner { MinistryOwnerId = 1, MinistryOwnerName = "Brian Pikalow", CreatedDateTime = DateTime.Today, ModifiedDateTIme = DateTime.Today.AddDays(1), Archived = false},
                new MinistryOwner { MinistryOwnerId = 2, MinistryOwnerName = "John Parrot", CreatedDateTime = DateTime.Today, ModifiedDateTIme = DateTime.Today.AddDays(1), Archived = false},
                new MinistryOwner { MinistryOwnerId = 3, MinistryOwnerName = "Brian Quigley", CreatedDateTime = DateTime.Today, ModifiedDateTIme = DateTime.Today.AddDays(1), Archived = false},
                new MinistryOwner { MinistryOwnerId = 4, MinistryOwnerName = "Don Cousins", CreatedDateTime = DateTime.Today, ModifiedDateTIme = DateTime.Today.AddDays(1), Archived = false},
                new MinistryOwner { MinistryOwnerId = 5, MinistryOwnerName = "Rocky Barra", CreatedDateTime = DateTime.Today, ModifiedDateTIme = DateTime.Today.AddDays(1), Archived = false}
            };

            _ministryOwners = ministryOwners;
         }

        [HttpGet]
        public IEnumerable<MinistryOwner> GetAllMinistryOwners()
        {
            return _ministryOwners;
        }

        public ActionResult GetMinistryOwner(int id)
        {
            var ministryOwner = _ministryOwners.FirstOrDefault(x => x.MinistryOwnerId == id);

            if (ministryOwner == null)
            {
                return NotFound();
            }

            return Ok(ministryOwner);
        }
    }


    /*
     *
		
      public IHttpActionResult GetEmployee(int id){
         var employee = employees.FirstOrDefault((p) => p.ID == id);
         if (employee == null){
            return NotFound();
         }
         return Ok(employee);
      }
     * 
     */

}

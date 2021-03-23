using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("Approvals")]
    public class Approval : BaseModel
    {
        public ICollection<Ministry> Ministry { get; set; }
    }
}
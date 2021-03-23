using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("UpInOut")]
    public class UpInOut : BaseModel
    {
        public virtual ICollection<UpInOutRelationship> UpInOutRelationships { get; set; }
    }
}
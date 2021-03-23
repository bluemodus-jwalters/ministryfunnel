using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Models
{
    [Table("ResourceInvolvement")]
    public class ResourceInvolvement : BaseModel
    {
        public virtual ICollection<ResourceInvolvement> ResourceInvolvementRelationships { get; set; }
    }
}
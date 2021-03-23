using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("ResourceInvolvementRelationship")]
    public class ResourceInvolvementRelationship
    {
        [Key, Column(Order = 1)]
        public int MinistryId { get; set; }

        [Key, Column(Order = 2)]
        public int ResourceInvolvementId { get; set; }
        //public Ministry Ministry { get; set; }
        //public ResourceInvolvement ResourceInvolvement{ get; set; }
    }
}
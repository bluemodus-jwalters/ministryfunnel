using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("UpInOutRelationship")]
    public class UpInOutRelationship
    {

        [Key, Column(Order = 1)]
        public int MinistryId { get; set; }

        [Key, Column(Order = 2)]
        public int UpInOutId { get; set; }
        //public virtual Ministry Ministry { get; set; }
        //public virtual UpInOut UpInOut { get; set; }
    }
}
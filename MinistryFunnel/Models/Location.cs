using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Models
{
    [Table("Locations")]
    public class Location : BaseModel
    {
        public ICollection<Ministry> Ministry { get; set; }
    }
}
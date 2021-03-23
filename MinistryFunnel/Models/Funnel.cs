using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Models
{
    [Table("Funnels")]
    public class Funnel : BaseModel
    {
        public ICollection<Ministry> Ministry { get; set; }
    }
}
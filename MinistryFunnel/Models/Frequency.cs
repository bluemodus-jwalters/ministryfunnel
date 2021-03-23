using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("Frequency")]
    public class Frequency : BaseModel
    {
        public ICollection<Ministry> Ministry { get; set; }
    }
}
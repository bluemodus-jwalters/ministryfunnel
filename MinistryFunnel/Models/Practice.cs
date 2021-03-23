using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("Practices")]
    public class Practice : BaseModel
    {
        public ICollection<Ministry> Ministry { get; set; }
    }
}
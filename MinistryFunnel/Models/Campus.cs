using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("Campus")]
    public class Campus : BaseModel
    {
        public ICollection<Ministry> Ministry { get; set; }
    }
}
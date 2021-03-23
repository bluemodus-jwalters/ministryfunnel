using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("LevelOfImportance")]
    public class LevelOfImportance : BaseModel
    {
        public ICollection<Ministry> Ministry { get; set; }
    }
}
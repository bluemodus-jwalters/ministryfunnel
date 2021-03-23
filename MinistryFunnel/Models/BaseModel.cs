using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinistryFunnel.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool? Archived { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}, Created Date Time: {this.CreatedDateTime.ToString()}, Modified Date Time: {this.ModifiedDateTime}, Archived: {this.Archived.ToString()}";
        }
    }
}

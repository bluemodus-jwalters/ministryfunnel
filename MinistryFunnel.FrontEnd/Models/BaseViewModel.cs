using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.FrontEnd.Models
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool? Archived { get; set; }
    }
}
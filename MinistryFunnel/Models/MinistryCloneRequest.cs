using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Models
{
    public class MinistryCloneRequest
    {
        public int MinistryId { get; set; }
        public List<string> CloneDates { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Models
{
    public class MinimalMinistryViewModel
    {
        public int Id { get; set; }
        public string ApprovalName { get; set; }
        public string Event { get; set; }
        public string Purpose { get; set; }
        public string MinistryOwnerName { get; set; }
        public string CampusName { get; set; }
        public string DesiredOutcome { get; set; }
        public string PracticeName { get; set; }
        public string FunnelName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LevelOfImportanceName { get; set; }
        public string LocationName { get; set; }
        public string Frequency { get; set; }

    }
}
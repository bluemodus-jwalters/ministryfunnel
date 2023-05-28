using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.FrontEnd.Models
{
    public class MinistryFrontEndViewModelMinimal
    {
        public int Id { get; set; }
        public string ApprovalName { get; set; } 
        public string Event { get; set; }
        public string Purpose { get; set; } //remove
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
        public bool BigRock { get; set; }
        public bool KidCare { get; set; }
        public ICollection<ResourceInvolvementRelationshipViewModel> ResourceInvolvementRelationships { get; set; }
        public ICollection<UpInOutRelationshipViewModel> UpInOutRelationships { get; set; }

    }
}
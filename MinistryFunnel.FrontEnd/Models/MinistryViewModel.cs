using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MinistryFunnel.FrontEnd.Models
{
    public class MinistryViewModel
    {
        public int Id { get; set; }

        public ICollection<UpInOutRelationshipViewModel> UpInOutRelationships { get; set; }

        public ICollection<ResourceInvolvementRelationshipViewModel> ResourceInvolvementRelationships { get; set; }

        public string Event { get; set; }

        public string Purpose { get; set; }

        public string DesiredOutcome { get; set; }

        public int MinistryOwnerId { get; set; }
        public string MinistryOwnerName { get; set; }

        public int PracticeId { get; set; }
        public string PracticeName { get; set; }

        public int FunnelId { get; set; }
        public string FunnelName { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public int CampusId { get; set; }
        public string CampusName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int FrequencyId { get; set; }
        public string FrequencyName { get; set; }

        public bool KidCare { get; set; }

        public int LevelOfImportanceId { get; set; }
        public string LevelOfImportanceName { get; set; }

        public int ApprovalId { get; set; }
        [Display(Name = "Approval Status")]
        public string ApprovalName { get; set; }

        public string Comments { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public bool Archived { get; set; }
        public bool BigRock { get; set; }
    }

    public class ResourceInvolvementRelationshipViewModel
    {
        public int MinistryId { get; set; }

        public int ResourceInvolvementId { get; set; }
        public string ResourceInvolvementName { get; set; }
    }

    public class UpInOutRelationshipViewModel
    {

        public int MinistryId { get; set; }

        public int UpInOutId { get; set; }

        public string UpInOutName { get; set; }
    }
}
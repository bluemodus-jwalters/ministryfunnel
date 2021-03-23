using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Models
{
    public class MinistryViewModel
    {
        public int Id { get; set; }

        public ICollection<UpInOutRelationship> UpInOutRelationships { get; set; }

        public ICollection<ResourceInvolvementRelationship> ResourceInvolvementRelationships { get; set; }

        public string Event { get; set; }

        public string Purpose { get; set; }

        public string DesiredOutcome { get; set; }

        public int MinistryOwnerId { get; set; }

        public int PracticeId { get; set; }

        public int FunnelId { get; set; }

        public int LocationId { get; set; }

        public int CampusId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int FrequencyId { get; set; }
        public string FrequencyName { get; set; }

        public bool KidCare { get; set; }

        public int LevelOfImportanceId { get; set; }

        public int ApprovalId {get; set;}

        public string Comments { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public bool Archived { get; set; }
    }
}
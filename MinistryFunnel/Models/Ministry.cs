using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    [Table("Ministry")]
    public class Ministry
    {
        public int Id { get; set; }

        public string Event { get; set; }

        public string Purpose { get; set; }

        public string DesiredOutcome { get; set; }

        public int MinistryOwnerId { get; set; }
        public virtual MinistryOwner MinistryOwner { get; set; }       

        public int PracticeId { get; set; }
        public virtual Practice Practice { get; set; }

        public int FunnelId { get; set; }
        public virtual Funnel Funnel { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public int CampusId { get; set; }
        public virtual Campus Campus { get; set; }

        //TODO: make this so there is an insert view model and a return view model that won't show these?
        public int[] UpInOutIds { get; set; }
        public virtual ICollection<UpInOutRelationship> UpInOutRelationships { get; set; }
        public int[] ResourceInvolvementIds { get; set; } 
        public virtual ICollection<ResourceInvolvementRelationship> ResourceInvolvementRelationship { get; set; }


        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int FrequencyId { get; set; }
        public virtual Frequency Frequency { get; set; }

        public bool KidCare { get; set; }

        public int LevelOfImportanceId { get; set; }
        public virtual LevelOfImportance LevelOfImportance { get; set; }

        public int ApprovalId { get; set; }
        public virtual Approval Approval { get; set; }

        public string Comments { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public bool Archived { get; set; }
        public bool BigRock { get; set; }
    }
}
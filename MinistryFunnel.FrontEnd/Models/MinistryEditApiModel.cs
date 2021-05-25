using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.FrontEnd.Models
{
    public class MinistryEditApiModel
    {
        public int Id { get; set; }
        public string Event { get; set; }
        public string Purpose { get; set; }
        public string DesiredOutcome { get; set; }
        public int MinistryOwnerId { get; set; }
        public int PracticeId { get; set; }
        public int FunnelId { get; set; }
        public int LocationId { get; set; }
        public int CampusId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int FrequencyId { get; set; }
        public bool KidCare { get; set; }
        public int LevelOfImportanceId { get; set; }
        public int ApprovalId { get; set; }
        public string Comments { get; set; }
        public bool Archived { get; set; }
        public int[] UpInOutIds { get; set; }
        public int[] ResourceInvolvementIds { get; set; }
    }
}
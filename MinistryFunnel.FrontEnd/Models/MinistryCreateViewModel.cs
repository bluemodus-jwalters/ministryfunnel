using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinistryFunnel.FrontEnd.Models.DropDowns
{
    public class MinistryCreateViewModel
    {
        [Display(Name = "Ministry Owner")]
        public int MinistryOwnerId { get; set; }
        public IEnumerable<SelectListItem> MinistryOwners { get; set; }

        [Required]
        [Display(Name = "Event")]       
        public string Event { get; set; }

        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        [Display(Name = "Desired Outcome")]
        public string DesiredOutcome { get; set; }

        [Display(Name = "Practice")]
        public int PracticeId { get; set; }
        public IEnumerable<SelectListItem> Practices { get; set; }

        [Display(Name = "Funnel")]
        public int FunnelId { get; set; }
        public IEnumerable<SelectListItem> Funnels { get; set; }

        [Display(Name = "Campus")]
        public int CampusId { get; set; }
        public IEnumerable<SelectListItem> Campuses { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        [Display(Name = "Frequency")]
        public int FrequencyId { get; set; }
        public IEnumerable<SelectListItem> Frequencies { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Kid care")]
        public bool KidCare { get; set; }

        [Display(Name = "Level of Importance")]
        public int LevelOfImportanceId { get; set; }
        public IEnumerable<SelectListItem> LevelOfImportances { get; set; }

        [Display(Name = "Approval")]
        public int ApprovalId { get; set; }
        public IEnumerable<SelectListItem> Approvals { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Up In Out")]
        public int[] UpInOutIds { get; set; }
        public IEnumerable<SelectListItem> UpInOuts { get; set; }

        [Display(Name = "Resource Involvement")]
        public int[] SelectedResourceInvolvementIds { get; set; }
        public IEnumerable<SelectListItem> ResourceInvolvements { get; set; }
    }
}
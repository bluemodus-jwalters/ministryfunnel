using Foolproof;
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
        [Required]
        [Display(Name = "Ministry Owner")]
        public int MinistryOwnerId { get; set; }
        public IEnumerable<SelectListItem> MinistryOwners { get; set; }

        [Required]
        [Display(Name = "Event")]       
        public string Event { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Desired Outcome")]
        public string DesiredOutcome { get; set; }

        [Required]
        [Display(Name = "Practice")]
        public int PracticeId { get; set; }
        public IEnumerable<SelectListItem> Practices { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public int FunnelId { get; set; }
        public IEnumerable<SelectListItem> Funnels { get; set; }

        [Required]
        [Display(Name = "Campus Involved")]
        public int CampusId { get; set; }
        public IEnumerable<SelectListItem> Campuses { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        [Required]
        [Display(Name = "Frequency")]
        public int FrequencyId { get; set; }
        public IEnumerable<SelectListItem> Frequencies { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [GreaterThan("StartDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Kid care")]
        public bool KidCare { get; set; }

        [Required]
        [Display(Name = "Level of Importance")]
        public int LevelOfImportanceId { get; set; }
        public IEnumerable<SelectListItem> LevelOfImportances { get; set; }

        [Display(Name = "Approval Status")]
        public int ApprovalId { get; set; }
        public IEnumerable<SelectListItem> Approvals { get; set; }

        [Display(Name = "Comments")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [Display(Name = "Up In Out")]
        public int[] UpInOutIds { get; set; }
        public IEnumerable<SelectListItem> UpInOuts { get; set; }

        [Display(Name = "Resource Involvement")]
        public int[] SelectedResourceInvolvementIds { get; set; }
        public IEnumerable<SelectListItem> ResourceInvolvements { get; set; }

        [Display(Name = "Big Rock Ministry")]
        public bool BigRock { get; set; }
        public bool CanApprove { get; set; }
    }
}
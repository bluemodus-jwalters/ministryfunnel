using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinistryFunnel.FrontEnd.Models.DropDowns
{
    public class MinistryOwnerCreateViewModel
    {
        [Display(Name = "Ministry Owner")]
        public int MinistryOwnerId { get; set; }
        public IEnumerable<SelectListItem> ministryOwners { get; set; }
    }
}
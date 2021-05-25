using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    public class UpInOutRelationshipViewModel
    {

        public int MinistryId { get; set; }

        public int UpInOutId { get; set; }
        
        public string UpInOutName { get; set; }
    }
}
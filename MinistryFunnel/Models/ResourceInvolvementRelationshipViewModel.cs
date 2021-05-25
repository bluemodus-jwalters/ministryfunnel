using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinistryFunnel.Models
{
    public class ResourceInvolvementRelationshipViewModel
    {
        public int MinistryId { get; set; }

        public int ResourceInvolvementId { get; set; }
        public string ResourceInvolvementName { get; set; }   
    }
}
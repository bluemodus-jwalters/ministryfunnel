using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinistryFunnel.API
{
    public class MinistryOwner
    {
        public int MinistryOwnerId { get; set; }
        public string MinistryOwnerName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTIme { get; set; }
        public bool Archived { get; set; }
    }
}

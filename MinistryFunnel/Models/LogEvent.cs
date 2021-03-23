using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Models
{
    [Table("LogEvent")]
    public class LogEvent
    {
        [Key]
        public int LogId{ get; set; }

        public string User { get; set; }

        public string Action { get; set; }

        public string Event { get; set; }
        public string SearchText { get; set; }

        public string Notes { get; set; }

        public DateTime Created { get; set; }    
    }
}
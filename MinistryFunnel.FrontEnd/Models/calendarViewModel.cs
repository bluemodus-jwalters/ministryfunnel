using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.FrontEnd.Models
{
    public class fullcalendarIO
    {
        public calendarEvents[] eventSources { get; set; }
    }
    public class calendarEvents
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }

    public class EventViewModel
    {
        public Int64 groupId { get; set; }

        public String title { get; set; }

        public String start { get; set; }

        public String end { get; set; }
    }
}
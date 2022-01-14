using MinistryFunnel.Data;
using MinistryFunnel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.FrontEnd.Service
{
    public class LoggerService : ILoggerService
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();

        public void CreateLog(string user = null, string applicationName = null, string className = null, string action = null, string events = null, string searchText = null, string notes = null)
        {
            LogEvent logEvent = new LogEvent { User = user, ApplicationName = applicationName, ClassName = className, Action = action, Event = events, SearchText = searchText, Notes = notes, Created = DateTime.Now };
            db.LogEvent.Add(logEvent);
            db.SaveChanges();
        }

        public void CreateLog(string user = null, string action = null, string events = null, string searchText = null, string notes = null)
        {
            LogEvent logEvent = new LogEvent { User = user, ApplicationName = "application needs update",  ClassName = "class needs update", Action = action, Event = events, SearchText = searchText, Notes = notes, Created = DateTime.Now };
            db.LogEvent.Add(logEvent);
            db.SaveChanges();
        }      
    }
}
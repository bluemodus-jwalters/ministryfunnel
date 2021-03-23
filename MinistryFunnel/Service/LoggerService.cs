using MinistryFunnel.Data;
using MinistryFunnel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.Service
{
    public class LoggerService : ILoggerService
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();

        public void CreateLog(string user = null, string action = null, string events = null, string searchText = null, string notes = null)
        {
            LogEvent logEvent = new LogEvent { User = user, Action = action, Event = events, SearchText = searchText, Notes = notes, Created = DateTime.Now };
            db.LogEvent.Add(logEvent);
            db.SaveChanges();
        }      
    }
}
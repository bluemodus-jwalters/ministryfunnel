using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinistryFunnel.Service
{
    interface ILoggerService
    {
        void CreateLog(string user = null, string applicationName = null, string className = null, string action = null, string events = null, string searchText = null, string notes = null);
    }
}

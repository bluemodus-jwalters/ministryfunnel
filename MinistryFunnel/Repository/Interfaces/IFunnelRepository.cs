using MinistryFunnel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinistryFunnel.Repository.Interfaces
{
    interface IFunnelRepository
    {
        IQueryable<Funnel> GetFunnels();

        Funnel GetFunnelById(int id);

        IQueryable<Funnel> SearchFunnelByName(string searchText);

        Funnel UpdateFunnel(Funnel updatedFunnel);

        Funnel InsertFunnel(Funnel Funnel);

        Funnel DeleteFunnel(int id);
    }
}

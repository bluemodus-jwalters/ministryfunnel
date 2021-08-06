using MinistryFunnel.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinistryFunnel.FrontEnd.Helpers
{
    public interface IMinistryHelper
    {
        ICollection<ResourceInvolvementViewModel> GetResourceInvolvementOptions(string token);

        ICollection<MinistryOwnerViewModel> GetMinistryOwners(string token);

        ICollection<PracticeViewModel> GetPractices(string token);

        ICollection<FunnelViewModel> GetFunnels(string token);

        ICollection<CampusViewModel> GetCampuses(string token);

        ICollection<LocationViewModel> GetLocations(string token);

        ICollection<LevelOfImportanceViewModel> GetLevelOfImportances(string token);

        ICollection<UpInOutViewModel> GetUpInOutOptions(string token);

        ICollection<ApprovalViewModel> GetApprovals(string token);

        ICollection<FrequencyViewModel> GetFrequencies(string token);
    }
}

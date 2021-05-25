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
        ICollection<ResourceInvolvementViewModel> GetResourceInvolvementOptions();

        ICollection<MinistryOwnerViewModel> GetMinistryOwners();

        ICollection<PracticeViewModel> GetPractices();

        ICollection<FunnelViewModel> GetFunnels();

        ICollection<CampusViewModel> GetCampuses();

        ICollection<LocationViewModel> GetLocations();

        ICollection<LevelOfImportanceViewModel> GetLevelOfImportances();

        ICollection<UpInOutViewModel> GetUpInOutOptions();

        ICollection<ApprovalViewModel> GetApprovals();

        ICollection<FrequencyViewModel> GetFrequencies();
    }
}

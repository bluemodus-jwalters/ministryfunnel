using MinistryFunnel.FrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace MinistryFunnel.FrontEnd.Helpers
{
    public class MinistryHelper : IMinistryHelper
    {
        private readonly IApiHelper _apiHelper;
        public MinistryHelper()
        {
            _apiHelper = new ApiHelper();
        }

        public ICollection<ApprovalViewModel> GetApprovals(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/approval"), token);

            if (response.IsSuccessful)
            {
                var approvals = JsonConvert.DeserializeObject<ICollection<ApprovalViewModel>>(response.Content);
                return approvals;
            }
            return null;
        }

        public ICollection<CampusViewModel> GetCampuses(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/campus"), token);

            if (response.IsSuccessful)
            {
                var campuses = JsonConvert.DeserializeObject<ICollection<CampusViewModel>>(response.Content);
                return campuses;
            }
            return null;
        }

        public ICollection<FrequencyViewModel> GetFrequencies(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/frequency"), token);

            if (response.IsSuccessful)
            {
                var frequencies = JsonConvert.DeserializeObject<ICollection<FrequencyViewModel>>(response.Content);
                return frequencies;
            }
            return null;
        }

        public ICollection<FunnelViewModel> GetFunnels(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/funnel"), token);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ICollection<FunnelViewModel>>(response.Content);
            }
            return null;
        }

        public ICollection<LevelOfImportanceViewModel> GetLevelOfImportances(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/levelofimportance"), token);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ICollection<LevelOfImportanceViewModel>>(response.Content);
            }
            return null;
        }

        public ICollection<LocationViewModel> GetLocations(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/location"), token);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ICollection<LocationViewModel>>(response.Content);
            }
            return null;
        }

        public ICollection<MinistryOwnerViewModel> GetMinistryOwners(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/ministryowner"), token);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ICollection<MinistryOwnerViewModel>>(response.Content);
            }
            return null;
        }

        public ICollection<PracticeViewModel> GetPractices(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/practice"), token);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ICollection<PracticeViewModel>>(response.Content);
            }
            return null;
        }

        public ICollection<ResourceInvolvementViewModel> GetResourceInvolvementOptions(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/resourceinvolvement"), token);

            if (response.IsSuccessful)
            {
                var resourceInvolvements = JsonConvert.DeserializeObject<ICollection<ResourceInvolvementViewModel>>(response.Content);
                return resourceInvolvements;
            }
            return null;
        }

        public ICollection<UpInOutViewModel> GetUpInOutOptions(string token)
        {
            var response = _apiHelper.Get(CompileUrl("/api/upinout"), token);

            if (response.IsSuccessful)
            {
                var upinouts = JsonConvert.DeserializeObject<ICollection<UpInOutViewModel>>(response.Content);
                return upinouts;
            }
            return null;
        }

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }
    }
}
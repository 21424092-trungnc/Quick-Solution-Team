using Business.Entities;
using Core.Common.UI.Paging;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class GymSetupViewModel
    {
        public GymSetupViewModel()
        {
            Items = new List<GymSetupMap>();
            Search = new GymSetupParam();
            PagingFilteringContext = new GymSetupPagingFilteringModel();
        }
        public List<GymSetupMap> Items { get; set; }
        public GymSetupParam Search { get; set; }
        public GymSetupPagingFilteringModel PagingFilteringContext { get; set; }
    }
    public class GymSetupPagingFilteringModel : BasePageableModel
    {
    }
    //detail
    public class GymSetupDetailViewModel
    {
        public GymSetupDetail Detail { get; set; }
        public List<GymSetupMap> ListSameCategory { get; set; }
        public GymTrialAdd GymTrial { get; set; }
    }

}
using Business.Entities;
using Core.Common.UI.Paging;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class RecruitmentViewModel
    {
        public RecruitmentViewModel()
        {
            Items = new List<RecruitmentMap>();
            Search = new RecruitmentParam();
            PagingFilteringContext = new RecruitmentPagingFilteringModel();
        }
        public List<RecruitmentMap> Items { get; set; }
        public RecruitmentParam Search { get; set; }
        public RecruitmentPagingFilteringModel PagingFilteringContext { get; set; }
    }
    public class RecruitmentPagingFilteringModel : BasePageableModel
    {
    }
    //detail
    public class RecruitmentDetailViewModel
    {
        public RecruitmentDetail Detail { get; set; }
        public CandidateMapAdd Candidate { get; set; }
    }

}
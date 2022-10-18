using Business.Entities;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IRecruitmentService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "HR/HR_RECRUIT_GetList_Web")]
        ResultResponse<List<RecruitmentMap>> HR_RECRUIT_GetList_Web(RecruitmentParam model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "HR/HR_RECRUIT_GetRecruit_ByID?RecruitID={RecruitID}")]
        ResultResponse<RecruitmentDetail> HR_RECRUIT_GetRecruit_ByID(long RecruitID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "HR/HR_CANDIDATE_InsUpd_Web")]
        ResultResponse<bool> HR_CANDIDATE_InsUpd_Web(CandidateMapAdd model);
    }
}

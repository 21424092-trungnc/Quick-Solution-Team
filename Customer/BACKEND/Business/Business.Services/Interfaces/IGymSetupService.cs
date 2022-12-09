using Business.Entities;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IGymSetupService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GYM/CMS_SETUPSERVICE_GetList_Web")]
        ResultResponse<List<GymSetupMap>> CMS_SETUPSERVICE_GetList_Web(GymSetupParam model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GYM/CMS_SETUPSERVICE_GetService_ByID?GymSetupID={GymSetupID}")]
        ResultResponse<GymSetupDetail> CMS_SETUPSERVICE_GetService_ByID(long GymSetupID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GYM/CMS_SETUPSERVICE_REGISTER_InsUpd_Web")]
        ResultResponse<bool> CMS_SETUPSERVICE_REGISTER_InsUpd_Web(GymTrialAdd model);
    }
}

using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IAccountServices
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ACC/CustomerDataLeads_InsOrUpd")]
        ResultResponse<string> CustomerDataLeads_InsOrUpd(CustomerDataLeadsParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "ACC/CustomerDataLeads_GetByPhone")]
        ResultResponse<CustomerDataLeadsMap> CustomerDataLeads_GetByPhone(string model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ACC/Account_InsOrUpd")]
        ResultResponse<long> Account_InsOrUpd(AccountParam model);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ACC/CRM_ACCOUNT_CheckUserName_Web")]
        ResultResponse<LoginDataModel> CRM_ACCOUNT_CheckUserName_Web(string model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ACC/Account_CheckLogin")]
        ResultResponse<int> Account_CheckLogin(LoginDataModel model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ACC/Account_GetInfoByPhone")]
        ResultResponse<UserManager> Account_GetInfoByPhone(string phone);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
      UriTemplate = "ACC/Account_GetInfoBySSO")]
        ResultResponse<UserManager> Account_GetInfoBySSO(UserSSOParam user);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ACC/Account_Active")]
        ResultResponse<bool> Account_Active(string model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ACC/Account_GetInfoByID")]
        ResultResponse<UserManager> Account_GetInfoByID(int MemberID);
    }
}

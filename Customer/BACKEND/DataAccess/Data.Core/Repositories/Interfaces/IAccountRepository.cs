using Business.Entities.Domain;
using Core.Common.Utilities;

namespace Data.Core.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        string CustomerDataLeads_InsOrUpd(CustomerDataLeadsParam model, out ResponseModel restStatus);
        CustomerDataLeadsMap CustomerDataLeads_GetByPhone(string PhoneNumber, out ResponseModel restStatus);
        long Account_InsOrUpd(AccountParam model, out ResponseModel restStatus);
        LoginDataModel CRM_ACCOUNT_CheckUserName_Web(string model, out ResponseModel restStatus);
        int Account_CheckLogin(LoginDataModel model, out ResponseModel restStatus);
        UserManager Account_GetInfoByPhone(string phone, out ResponseModel restStatus);
        UserManager Account_GetInfoBySSO(string googleid, string facebookid, out ResponseModel restStatus);
        bool Account_Active(string model, out ResponseModel restStatus);
        UserManager Account_GetInfoByID(int MemberID, out ResponseModel restStatus);
    }
}

using Business.Entities.Domain;
using Business.Services.Interfaces;
using Core.Common.Utilities;
using Data.Core.Repositories.Interfaces;
using log4net;
using System.Configuration;

namespace Business.Services
{
    public class AccountServices : IAccountServices
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(AccountServices));
        #region Private Repository & contractor       
        private static readonly string CMSSolutionConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
        private static IAccountRepository _newsRepository;
        public AccountServices(IAccountRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        #endregion


        #region "CRM Customer Data Leads"
        public ResultResponse<string> CustomerDataLeads_InsOrUpd(CustomerDataLeadsParam model)
        {
            var response = new ResponseModel();
            var data = _newsRepository.CustomerDataLeads_InsOrUpd(model, out response);
            var result = new ResultResponse<string>(response, data);
            return result;
        }

        public ResultResponse<CustomerDataLeadsMap> CustomerDataLeads_GetByPhone(string model)
        {
            var response = new ResponseModel();
            var data = _newsRepository.CustomerDataLeads_GetByPhone(model, out response);
            var result = new ResultResponse<CustomerDataLeadsMap>(response, data);
            return result;
        }
        #endregion

        #region "Account"
        public ResultResponse<long> Account_InsOrUpd(AccountParam model)
        {
            var response = new ResponseModel();
            var data = _newsRepository.Account_InsOrUpd(model, out response);
            var result = new ResultResponse<long>(response, data);
            return result;
        }
        #endregion

        #region "Login"
        public ResultResponse<LoginDataModel> CRM_ACCOUNT_CheckUserName_Web(string model)
        {
            var response = new ResponseModel();
            var data = _newsRepository.CRM_ACCOUNT_CheckUserName_Web(model, out response);
            var result = new ResultResponse<LoginDataModel>(response, data);
            return result;
        }

        public ResultResponse<int> Account_CheckLogin(LoginDataModel model)
        {
            var response = new ResponseModel();
            var data = _newsRepository.Account_CheckLogin(model, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }

        public ResultResponse<UserManager> Account_GetInfoByPhone(string phone)
        {
            var response = new ResponseModel();
            var data = _newsRepository.Account_GetInfoByPhone(phone, out response);
            var result = new ResultResponse<UserManager>(response, data);
            return result;
        }
        public ResultResponse<UserManager> Account_GetInfoBySSO(UserSSOParam user)
        {
            var response = new ResponseModel();
            var data = _newsRepository.Account_GetInfoBySSO(user.GoogleID, user.FacebookID, out response);
            var result = new ResultResponse<UserManager>(response, data);
            return result;
        }

        public ResultResponse<bool> Account_Active(string model)
        {
            var response = new ResponseModel();
            var data = _newsRepository.Account_Active(model, out response);
            var result = new ResultResponse<bool>(response, data);
            return result;
        }

        public ResultResponse<UserManager> Account_GetInfoByID(int MemberID)
        {
            var response = new ResponseModel();
            var data = _newsRepository.Account_GetInfoByID(MemberID, out response);
            var result = new ResultResponse<UserManager>(response, data);
            return result;
        }
        #endregion

    }
}

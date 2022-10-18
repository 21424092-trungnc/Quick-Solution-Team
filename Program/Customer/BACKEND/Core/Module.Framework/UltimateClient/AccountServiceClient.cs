using Business.Entities.Domain;
using Core.Common.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Framework.UltimateClient
{
    public class AccountServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public AccountServiceClient() : base(AppSetting.AccountAppSetting)
        {
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }


        #region "CRM Customer Data Leads"
        public IRestResponse<ResultResponse<string>> CustomerDataLeads_InsOrUpd(CustomerDataLeadsParam model)
        {
            var request = new RestRequest("ACC/CustomerDataLeads_InsOrUpd", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<string>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<CustomerDataLeadsMap>> CustomerDataLeads_GetByPhone(string model)
        {
            var request = new RestRequest("ACC/CustomerDataLeads_GetByPhone", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<CustomerDataLeadsMap>>(request);
            return restResponse;
        }
        #endregion

        #region "Account"

        public IRestResponse<ResultResponse<long>> Account_InsOrUpd(AccountParam model)
        {
            var request = new RestRequest("ACC/Account_InsOrUpd", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        #endregion

        #region "Login"
        public IRestResponse<ResultResponse<LoginDataModel>> CRM_ACCOUNT_CheckUserName_Web(string model)
        {
            var request = new RestRequest("ACC/CRM_ACCOUNT_CheckUserName_Web", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<LoginDataModel>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<int>> Account_CheckLogin(LoginDataModel model)
        {
            var request = new RestRequest("ACC/Account_CheckLogin", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<UserManager>> Account_GetInfoByPhone(string phone)
        {
            var request = new RestRequest("ACC/Account_GetInfoByPhone", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(phone, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<UserManager>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<UserManager>> Account_GetInfoBySSO(UserSSOParam user)
        {
            var request = new RestRequest("ACC/Account_GetInfoBySSO", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(user, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<UserManager>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<bool>> Account_Active(string model)
        {
            var request = new RestRequest("ACC/Account_Active", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion

        public IRestResponse<ResultResponse<UserManager>> Account_GetInfoByID(string MemberID)
        {
            var request = new RestRequest("ACC/Account_GetInfoByID", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(MemberID, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<UserManager>>(request);
            return restResponse;
        }
    }
}

using Business.Entities;
using Core.Common.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework.UltimateClient
{
    public class GymSetupServiceClient : BaseClient, IDisposable
    {
        #region Dispose
        private bool _isDisposed;
        public GymSetupServiceClient() : base(AppSetting.GymSetupAppSetting)
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
        #endregion
        #region GymSetup
        public IRestResponse<ResultResponse<List<GymSetupMap>>> CMS_SETUPSERVICE_GetList_Web(GymSetupParam model)
        {
            var request = new RestRequest("GYM/CMS_SETUPSERVICE_GetList_Web", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<GymSetupMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<GymSetupDetail>> CMS_SETUPSERVICE_GetService_ByID(long GymSetupID)
        {
            var request = new RestRequest("GYM/CMS_SETUPSERVICE_GetService_ByID", Method.GET);
            request.AddParameter("GymSetupID", GymSetupID);

            var restResponse = Execute<ResultResponse<GymSetupDetail>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> CMS_SETUPSERVICE_REGISTER_InsUpd_Web(GymTrialAdd model)
        {
            var request = new RestRequest("GYM/CMS_SETUPSERVICE_REGISTER_InsUpd_Web", Method.POST)
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

    }
}

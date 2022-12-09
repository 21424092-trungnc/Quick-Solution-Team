using Business.Entities;
using Core.Common.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework.UltimateClient
{
    public class RecruitmentServiceClient : BaseClient, IDisposable
    {
        #region Dispose
        private bool _isDisposed;
        public RecruitmentServiceClient() : base(AppSetting.RecruitmentAppSetting)
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
        #region Recruitment
        public IRestResponse<ResultResponse<List<RecruitmentMap>>> HR_RECRUIT_GetList_Web(RecruitmentParam model)
        {
            var request = new RestRequest("HR/HR_RECRUIT_GetList_Web", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<RecruitmentMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<RecruitmentDetail>> HR_RECRUIT_GetRecruit_ByID(long RecruitID)
        {
            var request = new RestRequest("HR/HR_RECRUIT_GetRecruit_ByID", Method.GET);
            request.AddParameter("RecruitID", RecruitID);

            var restResponse = Execute<ResultResponse<RecruitmentDetail>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> HR_CANDIDATE_InsUpd_Web(CandidateMapAdd model)
        {
            var request = new RestRequest("HR/HR_CANDIDATE_InsUpd_Web", Method.POST)
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

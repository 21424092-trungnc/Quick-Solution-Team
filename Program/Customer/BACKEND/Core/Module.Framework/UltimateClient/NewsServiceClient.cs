using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework.UltimateClient
{
    public class NewsServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public NewsServiceClient() : base(AppSetting.NewsAppSetting)
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
        public IRestResponse<ResultResponse<List<NewsMap>>> NEWS_GetList(NewsParam model)
        {
            var request = new RestRequest("NEWS/NEWS_GetList", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<NewsMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<NewsMap>>> GetNewsSameCategoryByID(NewsSameParam model)
        {
            var request = new RestRequest("NEWS/GetNewsSameCategoryByID", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<NewsMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<NewsDetail>> GetNews_ByID(long NewsID)
        {
            var request = new RestRequest("NEWS/GetNews_ByID", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(NewsID, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<NewsDetail>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<NewsCategoryMap>>> GetNewsCategory_GetList(NewsCategoryParam model)
        {
            var request = new RestRequest("NEWS/GetNewsCategory_GetList", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<NewsCategoryMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<NewsCategoryDetail>> GetNewsCategory_ByID(long NewsCategoryID)
        {
            var request = new RestRequest("NEWS/GetNewsCategory_ByID", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(NewsCategoryID, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<NewsCategoryDetail>>(request);
            return restResponse;
        }
    }
}

using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class DungChungServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public DungChungServiceClient() : base(AppSetting.DC)
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
        public ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAll(CBO_DungChungParam model)
        {
            var request = new RestRequest("DC/CBO_DungChung_GetAll", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<List<CBO_DungChungViewModel>>>(request);
            return restResponse.Data;
        }
        public IRestResponse<ResultResponse<List<BannerMap>>> CMS_BANNER_GetList_Web()
        {
            var request = new RestRequest("DC/CMS_BANNER_GetList_Web", Method.GET);
            var restResponse = Execute<ResultResponse<List<BannerMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<UrlRecord>> GetUrlRecord(string slug)
        {
            var request = new RestRequest("DC/GetUrlRecord", Method.GET);
            request.AddParameter("slug", slug);
            var restResponse = Execute<ResultResponse<UrlRecord>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<SeoSettings>> CBO_GetSeoCommon_Web(SeoSettingParam model)
        {
            var request = new RestRequest("DC/CBO_GetSeoCommon_Web", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<SeoSettings>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<StaticContentMap>> CMS_StaticContent_Get(string systemName)
        {
            var request = new RestRequest("DC/CMS_StaticContent_Get", Method.GET);
            request.AddParameter("systemname", systemName);
            var restResponse = Execute<ResultResponse<StaticContentMap>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<BannerMap>> CMS_Banner_Get(string aliasname)
        {
            var request = new RestRequest("DC/CMS_Banner_Get", Method.GET);
            request.AddParameter("aliasname", aliasname);
            var restResponse = Execute<ResultResponse<BannerMap>> (request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<ServiceMap>>> CMS_SetupService_GetList(string key)
        {
            var request = new RestRequest("DC/CMS_SetupService_GetList", Method.GET);
            request.AddParameter("key", key);
            var restResponse = Execute<ResultResponse<List<ServiceMap>>>(request);
            return restResponse;
        }
        #region ContactUs
        public IRestResponse<ResultResponse<int>> CMS_ContactUs_Create(ContactUsMap model)
        {
            var request = new RestRequest("DC/CMS_ContactUs_Create", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion

        #region About
        public IRestResponse<ResultResponse<List<BranchMap>>> SYS_Branch_GetList(string key)
        {
            var request = new RestRequest("DC/SYS_Branch_GetList", Method.GET);
            request.AddParameter("key", key);
            var restResponse = Execute<ResultResponse<List<BranchMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<UserMap>>> SYS_User_GetList(string key)
        {
            var request = new RestRequest("DC/SYS_User_GetList", Method.GET);
            request.AddParameter("key", key);
            var restResponse = Execute<ResultResponse<List<UserMap>>>(request);
            return restResponse;
        }
        #endregion

        #region Header
        public IRestResponse<ResultResponse<List<MenuMap>>> CMS_WebsiteCategory_Get(string key)
        {
            var request = new RestRequest("DC/CMS_WebsiteCategory_Get", Method.GET);
            request.AddParameter("key", key);
            var restResponse = Execute<ResultResponse<List<MenuMap>>>(request);
            return restResponse;
        }
        #endregion

        #region "Address"
        public IRestResponse<ResultResponse<List<ProvinceMap>>> MD_PROVINCE_GetList(string model)
        {
            var request = new RestRequest("DC/MD_PROVINCE_GetList", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<List<ProvinceMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<DistictMap>>> MD_DISTRICT_GetList(string model)
        {
            var request = new RestRequest("DC/MD_DISTRICT_GetList", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<List<DistictMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<WardMap>>> MD_WARD_GetList(string model)
        {
            var request = new RestRequest("DC/MD_WARD_GetList", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<List<WardMap>>>(request);
            return restResponse;
        }
        #endregion

        #region "DisposeCache" 
        public IRestResponse<bool> DisposeCacheService(string key)
        {
            var request = new RestRequest("DC/DisposeCacheService", Method.GET);
            request.AddParameter("key", key);
            var restResponse = Execute<bool>(request);
            return restResponse;
        }
        #endregion
    }

}

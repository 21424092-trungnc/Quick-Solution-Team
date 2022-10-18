using Business.Entities.Domain;
using Core.Common.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework.UltimateClient
{
    public class ProductServiceClient: BaseClient, IDisposable
    {
        private bool _isDisposed;
        public ProductServiceClient() : base(AppSetting.ProductsAppSetting)
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
        public IRestResponse<ResultResponse<List<ProductMap>>> MD_PRODUCT_GetList_Web(ProductParam model)
        {
            var request = new RestRequest("MD/MD_PRODUCT_GetList_Web", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<ProductMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<ProductCategoryMap>>> MD_PRODUCTCATEGORY_GetList_Web(ProductCategoryParam model)
        {
            var request = new RestRequest("MD/MD_PRODUCTCATEGORY_GetList_Web", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<ProductCategoryMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<ProductAttributeMap>>> PRO_PRODUCTATTRIBUTE_GetList_Web(long categoryid)
        {
            var request = new RestRequest("MD/PRO_PRODUCTATTRIBUTE_GetList_Web", Method.GET);
            request.AddParameter("categoryid", categoryid);
            var restResponse = Execute<ResultResponse<List<ProductAttributeMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<ProductDetailAll>> MD_PRODUCT_GetById_Web(long productId)
        {
            var request = new RestRequest("MD/MD_PRODUCT_GetById_Web", Method.GET);
            request.AddParameter("productId", productId);
            var restResponse = Execute<ResultResponse<ProductDetailAll>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> PRO_COMMENT_CreateOrUpdate_Web(ProductCommentsAdd model)
        {
            var request = new RestRequest("MD/PRO_COMMENT_CreateOrUpdate_Web", Method.POST)
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
        public IRestResponse<ResultResponse<List<ProductMap>>> MD_PRODUCT_GetRelationship_Web(long productId)
        {
            var request = new RestRequest("MD/MD_PRODUCT_GetRelationship_Web", Method.GET);
            request.AddParameter("productId", productId);
            var restResponse = Execute<ResultResponse<List<ProductMap>>>(request);
            return restResponse;
        }
    }
}

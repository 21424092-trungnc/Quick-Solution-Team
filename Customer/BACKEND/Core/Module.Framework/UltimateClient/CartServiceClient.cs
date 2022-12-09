using Business.Entities.Domain;
using Core.Common.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework.UltimateClient
{
    public class CartServiceClient: BaseClient, IDisposable
    {
        private bool _isDisposed;
        public CartServiceClient() : base(AppSetting.CartAppSetting)
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
        #region  Cart
        public IRestResponse<ResultResponse<List<CartMap>>> SL_CART_GetList(CartParam model)
        {
            var request = new RestRequest("SL/SL_CART_GetList", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<CartMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<string>> SL_CART_Create_Web(CartAdd model)
        {
            var request = new RestRequest("SL/SL_CART_Create_Web", Method.POST)
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
        public IRestResponse<ResultResponse<bool>> SL_CART_Delete_Web(long cartId)
        {
            var request = new RestRequest("SL/SL_CART_Delete_Web", Method.GET);
            request.AddParameter("cartId", cartId);            
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion

        #region "Booking"
        public IRestResponse<ResultResponse<long>> SL_BOOKING_CreateOrUpdate_Web(BookingMap model)
        {
            var request = new RestRequest("SL/SL_BOOKING_CreateOrUpdate_Web", Method.POST)
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

        public IRestResponse<ResultResponse<bool>> SL_BOOKINGDETAIL_Insert_Web(BookingDetailMap model)
        {
            var request = new RestRequest("SL/SL_BOOKINGDETAIL_Insert_Web", Method.POST)
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

        public IRestResponse<ResultResponse<List<CartOffers>>> SL_BOOKING_GetOffers_Web(long memberId, string cookieId)
        {
            var request = new RestRequest("SL/SL_BOOKING_GetOffers_Web", Method.GET);
            request.AddParameter("memberId", memberId);
            request.AddParameter("cookieId", cookieId);
            var restResponse = Execute<ResultResponse<List<CartOffers>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<PromotionApplyProduct>>> SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web(long promotionId)
        {
            var request = new RestRequest("SL/SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web", Method.GET);
            request.AddParameter("promotionId", promotionId); 
            var restResponse = Execute<ResultResponse<List<PromotionApplyProduct>>>(request);
            return restResponse;
        }
        #endregion

    }
}

using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface ICartService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SL/SL_CART_GetList")]
        ResultResponse<List<CartMap>> SL_CART_GetList(CartParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SL/SL_CART_Delete_Web?cartId={cartId}")]
        ResultResponse<bool> SL_CART_Delete_Web(long cartId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SL/SL_CART_Create_Web")]
        ResultResponse<string> SL_CART_Create_Web(CartAdd model);

        #region "Booking"
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SL/SL_BOOKING_CreateOrUpdate_Web")]
        ResultResponse<long> SL_BOOKING_CreateOrUpdate_Web(BookingMap model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
       UriTemplate = "SL/SL_BOOKINGDETAIL_Insert_Web")]
        ResultResponse<bool> SL_BOOKINGDETAIL_Insert_Web(BookingDetailMap model);
         
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SL/SL_BOOKING_GetOffers_Web?memberId={memberId}&cookieId={cookieId}")]
        ResultResponse<List<CartOffers>> SL_BOOKING_GetOffers_Web(long memberId, string cookieId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
       UriTemplate = "SL/SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web?promotionId={promotionId}")]
        ResultResponse<List<PromotionApplyProduct>> SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web(long promotionId);
        #endregion
    }
}

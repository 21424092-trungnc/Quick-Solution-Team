using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface ICartRepository
    {
        List<CartMap> SL_CART_GetList(CartParam model, out ResponseModel restStatus);
        bool SL_CART_Delete_Web(long cartId, out ResponseModel restStatus);
        string SL_CART_Create_Web(CartAdd model, out ResponseModel restStatus);
        string SL_CARTPROMOTION_Create_Web(CartPromotion model, out ResponseModel restStatus);

        List<CartPromotion> SL_CARTPROMOTION_GetList_Web(PromotionPargram model, out ResponseModel restStatus);
       
        #region "Booking"
        long SL_BOOKING_CreateOrUpdate_Web(BookingMap model, out ResponseModel restStatus);

        bool SL_BOOKINGDETAIL_Insert_Web(BookingDetailMap model, out ResponseModel restStatus);
        List<CartOffers> SL_BOOKING_GetOffers_Web(long memberId, string cookieId, out ResponseModel restStatus);
        List<PromotionApplyProduct> SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web(long promotionId, out ResponseModel restStatus);
        long SM_BOOKINGPROMOTION_Create_Web(CartPromotion model, out ResponseModel restStatus);
        #endregion
    }
}

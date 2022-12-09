using Business.Entities.Domain;
using Business.Services.Interfaces;
using Core.Common.Utilities;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Business.Services
{
    public class CartService : ICartService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(CartService));

        #region Private Repository & contractor       
        private static readonly string CMSSolutionConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
        private static ICartRepository _cartRepository;
        private static IProductRepository _productRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        #endregion

        #region cart list
        public ResultResponse<List<CartMap>> SL_CART_GetList(CartParam model)
        {
            var response = new ResponseModel();
            var data = _cartRepository.SL_CART_GetList(model, out response);
            if(data != null && data.Count > 0)
            {
                var promoModel = new PromotionPargram();
                promoModel.CookieId = model.CookieId;
                var promotionData = _cartRepository.SL_CARTPROMOTION_GetList_Web(promoModel, out response);
                if(promotionData != null && promotionData.Count > 0)
                {
                    foreach (var i in data)
                    {
                        i.CartPromotion = promotionData.Where(x => x.Id == i.CartId).ToList();
                    }
                } 
            }
            var result = new ResultResponse<List<CartMap>>(response, data);
            return result;
        }
        #endregion

        #region cart delete
        public ResultResponse<bool> SL_CART_Delete_Web(long cartId)
        {
            var response = new ResponseModel();
            var data = _cartRepository.SL_CART_Delete_Web(cartId, out response);
            var result = new ResultResponse<bool>(response, data);
            return result;
        }
        #endregion

        #region cart create
        public ResultResponse<string> SL_CART_Create_Web(CartAdd model)
        {
            var response = new ResponseModel();
            var data = _cartRepository.SL_CART_Create_Web(model, out response);
            if (model.CartPromotion != null && model.CartPromotion.Count > 0)
            {
                foreach (var item in model.CartPromotion)
                {
                    item.CookieId = model.CookieId;
                    item.Id = Convert.ToInt64(data);//CARTID 
                    _cartRepository.SL_CARTPROMOTION_Create_Web(item, out response);
                }
            }
            var result = new ResultResponse<string>(response, data);
            return result;
        }
        #endregion

        #region "Booking"
        public ResultResponse<long> SL_BOOKING_CreateOrUpdate_Web(BookingMap model)
        {
            var response = new ResponseModel();
            var data = _cartRepository.SL_BOOKING_CreateOrUpdate_Web(model, out response);
            //if (model.CartPromotion != null && model.CartPromotion.Count > 0)
            //{
            //    foreach (var item in model.CartPromotion)
            //    {
            //        item.BookingId = data;
            //        _cartRepository.SM_BOOKINGPROMOTION_Create_Web(item, out response);
            //    }
            //}
            var result = new ResultResponse<long>(response, data);
            return result;
        }

        public ResultResponse<bool> SL_BOOKINGDETAIL_Insert_Web(BookingDetailMap model)
        {
            var response = new ResponseModel();
            var modelCart = new CartParam();
            var data = true;
            var result = new ResultResponse<bool>(response, data);

            modelCart.CookieId = model.CookieID;
            modelCart.MemberId = model.MemberID;
            var lstCart = _cartRepository.SL_CART_GetList(modelCart, out response);
            if(lstCart != null && lstCart.Count > 0)
            {
                foreach (var item in lstCart)
                {
                    model.CartID = item.CartId;
                    data = _cartRepository.SL_BOOKINGDETAIL_Insert_Web(model, out response);
                }
                result = new ResultResponse<bool>(response, true);
            }
            else
            {
                result = new ResultResponse<bool>(response, false);
            }
            return result;

        }

        public ResultResponse<List<CartOffers>> SL_BOOKING_GetOffers_Web(long memberId, string cookieId)
        {
            var response = new ResponseModel();
            var data = _cartRepository.SL_BOOKING_GetOffers_Web(memberId, cookieId, out response);
            if (data != null && data.Count > 0)
            {
                foreach (var i in data)
                {
                    i.ListGifts = _productRepository.SM_PROMOTIONOFFER_GIFT_listByOffer_Web(i.PromotionOfferId, out response);
                }
            } 
            var result = new ResultResponse<List<CartOffers>>(response, data);
            return result;

        }

        public ResultResponse<List<PromotionApplyProduct>> SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web(long promotionId)
        {
            var response = new ResponseModel();
            var data = _cartRepository.SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web(promotionId, out response);
            var result = new ResultResponse<List<PromotionApplyProduct>>(response, data);
            return result;
        }
        #endregion
    }
}

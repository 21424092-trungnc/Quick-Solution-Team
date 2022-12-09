using Business.Entities.Domain;
using Core.Common.Utilities;
using Dapper;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Core.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(CartRepository));
        private const string TableName = "";
        public CartRepository(ILog logger)
        {
            _logger = logger;
        }
        #region List Temp order
        public List<CartMap> SL_CART_GetList(CartParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("MEMBERID", model.MemberId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("COOKIEID", model.CookieId, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<CartMap>("SL_CART_GetList_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<CartMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SL_CART_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region delete cart
        public bool SL_CART_Delete_Web(long cartId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CARTID", cartId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("SL_CART_Delete_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    if (datas > 0) return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("PRO_PRODUCTATTRIBUTE_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
        #endregion

        #region create cart
        public string SL_CART_Create_Web(CartAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("MEMBERID", model.MemberId, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("COOKIEID", model.CookieId, DbType.String, ParameterDirection.Input);
                    paramters.Add("PRODUCTID", model.ProductId, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("QUANTITY", model.Quantity, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PRICE", model.Price, DbType.Decimal, ParameterDirection.Input);
                    paramters.Add("TOTALPRICEITEM", model.TotalPriceItem, DbType.Decimal, ParameterDirection.Input);
                    paramters.Add("CREATEDUSER", model.CreatedUser, DbType.String, ParameterDirection.Input);
                    paramters.Add("PROMOTIONID", model.PromotionId, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PROMOTIONOFFERID", model.PromotionOfferId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<string>("SL_CART_Create_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("PRO_PRODUCTATTRIBUTE_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
      
        #endregion
        #region "Cart Promotion"
        public string SL_CARTPROMOTION_Create_Web(CartPromotion model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PROMOTIONID", model.PromotionId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PROMOTIONOFFERID", model.PromotionOfferId, DbType.Int32, ParameterDirection.Input); 
                    paramters.Add("PRODUCTGIFTSID", model.ProductGiftsId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PRODUCTID", model.ProductId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("COOKIEID", model.CookieId, DbType.String, ParameterDirection.Input);
                    paramters.Add("CARTID", model.Id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<string>("SL_CARTPROMOTION_Create_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SL_CARTPROMOTION_Create_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<CartPromotion> SL_CARTPROMOTION_GetList_Web (PromotionPargram model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTID", model.ProductId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("COOKIEID", model.CookieId, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<CartPromotion>("SL_CARTPROMOTION_GetList_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<CartPromotion> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SL_CART_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion
        #region "Booking"
        public long SL_BOOKING_CreateOrUpdate_Web(BookingMap model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("BOOKINGID", model.BookingID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("MEMBERID", model.MemberID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("BOOKINGNO", model.BookingNo, DbType.String, ParameterDirection.Input);
                    paramters.Add("BOOKINGSTATUSID ", model.BookingStatusID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("BOOKINGDATE", model.BookingDate, DbType.String, ParameterDirection.Input);
                    paramters.Add("TOTALMONEY", model.TotalMoney, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("NOTE", model.Note, DbType.String, ParameterDirection.Input);
                    paramters.Add("CREATEDUSER", model.CreatedUser, DbType.String, ParameterDirection.Input);
                    paramters.Add("ISACTIVE ", model.IsActive, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("FULLNAME", model.FullName, DbType.String, ParameterDirection.Input);
                    paramters.Add("EMAIL", model.CreatedUser, DbType.String, ParameterDirection.Input);
                    paramters.Add("PHONENUMBER", model.PhoneNumber, DbType.String, ParameterDirection.Input);
                    paramters.Add("COUNTRYID ", model.CountryID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PROVINCEID ", model.ProvinceID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("DISTRICTID ", model.DistrictID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("WARDID ", model.WardID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ADDRESS ", model.Address, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("SL_BOOKING_CreateOrUpdate_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SL_BOOKING_CreateOrUpdate_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public bool SL_BOOKINGDETAIL_Insert_Web(BookingDetailMap model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("COOKIEID", model.CookieID, DbType.String, ParameterDirection.Input);
                    paramters.Add("BOOKINGID", model.BookingID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("USERID ", model.UserID, DbType.String, ParameterDirection.Input);
                    paramters.Add("MEMBERID", model.MemberID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("CARTID", model.CartID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<bool>("SL_BOOKINGDETAIL_Insert_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SL_BOOKINGDETAIL_Insert_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
        #endregion
        #region Promotion
        public List<CartOffers> SL_BOOKING_GetOffers_Web(long memberId, string cookieId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("@MEMBERID", memberId, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("@COOKIEID", cookieId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<CartOffers>("MD_BOOKING_GetOffers_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<CartOffers> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_BOOKING_GetOffers Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<PromotionApplyProduct> SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web (long promotionId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("@PROMOTIONID", promotionId, DbType.Int64, ParameterDirection.Input); 
                    var datas = conns.Query<PromotionApplyProduct>("SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<PromotionApplyProduct> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public long SM_BOOKINGPROMOTION_Create_Web(CartPromotion model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("BOOKINGID", model.BookingId, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PROMOTIONID", model.PromotionId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PROMOTIONOFFERID", model.PromotionOfferId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PRODUCTGIFTSID", model.ProductGiftsId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PRODUCTID", model.ProductId, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("SM_BOOKINGPROMOTION_Create_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SM_BOOKINGPROMOTION_Create_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion
    }

}

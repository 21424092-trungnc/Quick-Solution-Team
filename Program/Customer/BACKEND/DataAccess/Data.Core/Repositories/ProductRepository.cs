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
    public class ProductRepository : IProductRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ProductRepository));
        private const string TableName = "";
        public ProductRepository(ILog logger)
        {
            _logger = logger;
        }
        #region category
        public List<ProductCategoryMap> MD_PRODUCTCATEGORY_GetList_Web(ProductCategoryParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PAGESIZE", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PAGEINDEX", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<ProductCategoryMap>("MD_PRODUCTCATEGORY_GetList_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProductCategoryMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_PRODUCTCATEGORY_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region Attribute
        public List<ProductAttributeMap> PRO_PRODUCTATTRIBUTE_GetList_Web(long categoryid,out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTCATEGORYID", categoryid, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<ProductAttributeMap>("PRO_PRODUCTATTRIBUTE_GetList_Web", paramters, commandType: CommandType.StoredProcedure);
                   
                    restStatus = new ResponseModel();
                    return datas as List<ProductAttributeMap> ?? datas.ToList();
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

        #region product
        public List<ProductMap> MD_PRODUCT_GetList_Web(ProductParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("KEYWORD", model.Keyword, DbType.String, ParameterDirection.Input);
                    paramters.Add("PRODUCTCATEGORYID", model.ProductCategoryId, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("ISHIGHLIGHT", model.IsHighLigh, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISSELLWELL", model.IsSellWell, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISPROMOTION", model.IsPromotion, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PRODUCTATTRIBUTEID", model.ProductAttributeId, DbType.String, ParameterDirection.Input);
                    paramters.Add("PRICEFROM", model.PriceFrom, DbType.Decimal, ParameterDirection.Input);
                    paramters.Add("PRICETO", model.PriceTo, DbType.Decimal, ParameterDirection.Input);
                    paramters.Add("PAGESIZE", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PAGEINDEX", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("MANUFACTURERID", model.ManufacturerId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<ProductMap>("MD_PRODUCT_GetList_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProductMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_PRODUCT_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region detail

        #region product detail
        public ProductDetailMap MD_PRODUCT_GetById_Web(long productId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTID", productId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<ProductDetailMap>("MD_PRODUCT_GetById_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_PRODUCT_GetById_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region Product img
        public List<ProductPictures> MD_PRODUCT_GetPictures_Web(long productId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTID", productId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<ProductPictures>("MD_PRODUCT_GetPictures_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProductPictures> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_PRODUCT_GetPictures_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region Promotion Offer
        public List<ProductOffers> MD_PRODUCT_GetOffers_Web(long productId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTID", productId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<ProductOffers>("MD_PRODUCT_GetOffers_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProductOffers> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_PRODUCT_GetOffers_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<ProductOffersGift> SM_PROMOTIONOFFER_GIFT_listByOffer_Web(long promotionOfferId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PROMOTIONOFFERID", promotionOfferId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<ProductOffersGift>("SM_PROMOTIONOFFER_GIFT_listByOffer_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProductOffersGift> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SM_PROMOTIONOFFER_GIFT_listByOffer_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        #endregion

        #region Product Attribute
        public List<ProductAttributes> MD_PRODUCT_GetAttributes_Web(long productId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTID", productId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<ProductAttributes>("MD_PRODUCT_GetAttributes_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProductAttributes> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_PRODUCT_GetAttributes_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        #endregion

        #region Product comment
        public List<ProductComments> MD_PRODUCT_GetComment_Web(long productId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTID", productId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<ProductComments>("MD_PRODUCT_GetComment_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProductComments> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_PRODUCT_GetComment_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public bool PRO_COMMENT_CreateOrUpdate_Web(ProductCommentsAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTID", model.ProductId, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("FULLNAME", model.FullName, DbType.String, ParameterDirection.Input);
                    paramters.Add("EMAIL", model.Email, DbType.String, ParameterDirection.Input);
                    paramters.Add("CONTENTCOMMENT", model.ContentComment, DbType.String, ParameterDirection.Input);
                    paramters.Add("RATTINGVALUES", model.RattingValues, DbType.Int32, ParameterDirection.Input);
                   // paramters.Add("COMMENTREPLYID", model.CommentReplyId, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("PRO_COMMENT_CreateOrUpdate_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("PRO_COMMENT_CreateOrUpdate_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return false;
            }
        }

        #endregion

        #region Product relationship
        public List<ProductMap> MD_PRODUCT_GetRelationship_Web(long productId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PRODUCTID", productId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<ProductMap>("MD_PRODUCT_GetRelationship_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProductMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("MD_PRODUCT_GetRelationship_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion
        #endregion


    }

}

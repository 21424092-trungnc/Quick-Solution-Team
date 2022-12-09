using Business.Entities.Domain;
using Business.Services.Interfaces;
using Core.Common.Utilities;
using Data.Core.Repositories.Interfaces;
using log4net;
using System.Collections.Generic;
using System.Configuration;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(ProductService));
        #region Private Repository & contractor       
        private static readonly string CMSSolutionConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
        private static IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region product category
        public ResultResponse<List<ProductCategoryMap>> MD_PRODUCTCATEGORY_GetList_Web(ProductCategoryParam model)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            if(model.KeyCache != null && model.KeyCache != "")
            {
                cacheKey = CacheHelper.BuildKey(model.KeyCache, "MD_PRODUCTCATEGORY_GetList_Web_" + model.KeyCache);
            }
            var data = CacheHelper.GetData<List<ProductCategoryMap>>(cacheKey);
            if (data == null)
            {
                data = _productRepository.MD_PRODUCTCATEGORY_GetList_Web(model, out response);
                if (data != null && model.KeyCache != null && model.KeyCache != "")
                    CacheHelper.SetData(cacheKey, data);
            }
            // var data = _productRepository.MD_PRODUCTCATEGORY_GetList_Web(model, out response);
            var result = new ResultResponse<List<ProductCategoryMap>>(response, data);
            return result;
        }
        #endregion

        #region attribute
        public ResultResponse<List<ProductAttributeMap>> PRO_PRODUCTATTRIBUTE_GetList_Web(long categoryid)
        {
            var response = new ResponseModel();
            var data = _productRepository.PRO_PRODUCTATTRIBUTE_GetList_Web(categoryid,out response);
            var result = new ResultResponse<List<ProductAttributeMap>>(response, data);
            return result;
        }
        #endregion

        #region product
        public ResultResponse<List<ProductMap>> MD_PRODUCT_GetList_Web(ProductParam model)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            if(model.IsHighLigh != null && model.IsHighLigh == 1)//Hot
            {
                cacheKey = CacheHelper.BuildKey("MD_PRODUCT_GetList_IsHighLigh", "MD_PRODUCT_IsHighLigh_" + model.IsHighLigh);
            }
            else if(model.IsSellWell != null && model.IsSellWell == 1)//Bán chạy
            {
                cacheKey = CacheHelper.BuildKey("MD_PRODUCT_GetList_IsSellWell", "MD_PRODUCT_IsSellWell_" + model.IsSellWell);
            }
            var data = CacheHelper.GetData<List<ProductMap>>(cacheKey);
            if (data == null)
            {
                data = _productRepository.MD_PRODUCT_GetList_Web(model, out response);
                if (data != null && ((model.IsHighLigh != null && model.IsHighLigh == 1) || (model.IsSellWell != null && model.IsSellWell == 1)))
                    CacheHelper.SetData(cacheKey, data);
            }
            //var data = _productRepository.MD_PRODUCT_GetList_Web(model, out response);
            var result = new ResultResponse<List<ProductMap>>(response, data);
            return result;
        }
        public ResultResponse<ProductDetailAll> MD_PRODUCT_GetById_Web(long productId)
        {
            var response = new ResponseModel();
            var result = new ProductDetailAll();
            result.Product = _productRepository.MD_PRODUCT_GetById_Web(productId, out response);
            if (result!=null && result.Product != null)
            {
                result.Pictures = _productRepository.MD_PRODUCT_GetPictures_Web(productId, out response);
                result.Offers = _productRepository.MD_PRODUCT_GetOffers_Web(productId, out response);
                if(result.Offers!=null && result.Offers.Count > 0)
                {
                    foreach(var i in result.Offers)
                    {
                        i.ListGifts = _productRepository.SM_PROMOTIONOFFER_GIFT_listByOffer_Web(i.PromotionOfferId, out response);
                    }
                }
                result.Attributes = _productRepository.MD_PRODUCT_GetAttributes_Web(productId, out response);
                result.Comments = _productRepository.MD_PRODUCT_GetComment_Web(productId, out response);
            }
            return new ResultResponse<ProductDetailAll>(response, result);
        }
        public ResultResponse<bool> PRO_COMMENT_CreateOrUpdate_Web(ProductCommentsAdd model)
        {
            var response = new ResponseModel();
            var data = _productRepository.PRO_COMMENT_CreateOrUpdate_Web(model, out response);
            var result = new ResultResponse<bool>(response, data);
            return result;
        }
        public ResultResponse<List<ProductMap>> MD_PRODUCT_GetRelationship_Web(long productId)
        {
            var response = new ResponseModel();
            var data = _productRepository.MD_PRODUCT_GetRelationship_Web(productId, out response);
            var result = new ResultResponse<List<ProductMap>>(response, data);
            return result;
        }
        #endregion
    }
}

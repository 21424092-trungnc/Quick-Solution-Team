using Business.Entities.Domain;
using Core.Common.Utilities;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<ProductMap> MD_PRODUCT_GetList_Web(ProductParam model, out ResponseModel restStatus);
        List<ProductCategoryMap> MD_PRODUCTCATEGORY_GetList_Web(ProductCategoryParam model, out ResponseModel restStatus);
        List<ProductAttributeMap> PRO_PRODUCTATTRIBUTE_GetList_Web(long categoryid,out ResponseModel restStatus);

        #region detail
        ProductDetailMap MD_PRODUCT_GetById_Web(long productId, out ResponseModel restStatus);
        List<ProductPictures> MD_PRODUCT_GetPictures_Web(long productId, out ResponseModel restStatus);
        List<ProductOffers> MD_PRODUCT_GetOffers_Web(long productId, out ResponseModel restStatus);
        List<ProductAttributes> MD_PRODUCT_GetAttributes_Web(long productId, out ResponseModel restStatus);
        List<ProductComments> MD_PRODUCT_GetComment_Web(long productId, out ResponseModel restStatus);
        bool PRO_COMMENT_CreateOrUpdate_Web(ProductCommentsAdd model, out ResponseModel restStatus);
        List<ProductMap> MD_PRODUCT_GetRelationship_Web(long productId, out ResponseModel restStatus);
        List<ProductOffersGift> SM_PROMOTIONOFFER_GIFT_listByOffer_Web(long promotionOfferId, out ResponseModel restStatus);

        #endregion
    }
}

using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class ProductParam : PagesParamModel
    {
        public string Keyword { get; set; }
        public long ProductCategoryId { get; set; }
        public int? IsHighLigh { get; set; } //0: NOT, 1: HAS, 2: ALL
        public int? IsSellWell { get; set; } //0: NOT, 1: HAS, 2: ALL
        public int? IsPromotion { get; set; } //0: NOT, 1: HAS, 2: ALL
        public string ProductAttributeId { get; set; } //1,2,4, (cuối cũng cần có dấu phải)
        public float PriceFrom { get; set; }
        public float PriceTo { get; set; }

        public long ManufacturerId { get; set; }
    }

    public class ProductMap : PagesModel
    {
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductNameShowWeb { get; set; }
        public string PictureUrl { get; set; }
        public string PictureUrlAlias { get; set; }
        public bool Isnews { get; set; }
        public string Offer { get; set; }
        public string PriceOld { get; set; }
        public string PriceNew { get; set; }
        public float Rating { get; set; }
        public string SeoName { get; set; }
        public string SeoTitle { get; set; }
        public string ContentMetaData { get; set; }
        public string MetaDataName { get; set; }

    }
    public class ProductCategoryParam : PagesParamModel
    {
        public string KeyCache { get; set; }
    }
    public class ProductCategoryMap : PagesModel
    {
        public long ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string NameShowWeb { get; set; }
        public int CountProduct { get; set; }
        public string SeoName { get; set; }
        public string ImageUrl { get; set; }
        public int ParentId { get; set; }
    }
    public class ProductAttributeMap
    {
        public int ProductAttributeId { get; set; }
        public string AttributeName { get; set; }
        public int CountProduct { get; set; }
    }
    public class ProductDetailMap
    {
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductContentDetail { get; set; }
        public string ProductNameShowWeb { get; set; }
        public string ManufactureName { get; set; }
        public string Descriptions { get; set; }
        public bool Isnews { get; set; }
        public string Offer { get; set; }
        public string PriceOld { get; set; }
        public string PriceNew { get; set; }
        public float PriceNewMN { get; set; }
        public float Rating { get; set; }
        public int CountComment { get; set; }
        public string SeoName { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public string MetaTitle { get; set; }
    }
    public class ProductPictures
    {
        public int ProductPictureId { get; set; }
        public string PictureUrl { get; set; }
        public string PictureAlias { get; set; }
        public bool IsDefault { get; set; }
    }
    public class ProductOffers
    {
        public int PromotionOfferId { get; set; }
        public string PromotionOfferName { get; set; }
        public long PromotionId { get; set; }
        public bool IsFixPrice { get; set; }
        public float DisCountValue { get; set; }
        public bool IsPercentDisCount { get; set; }
        public bool IsFixedGift { get; set; }
        public bool IsDisCountBySetPrice { get; set; }
        public bool IsApplyWithOrderPromotion { get; set; }
        public bool IsPromotionByTotalMoney { get; set; }
        public bool IsPromotionByToTalQuantity { get; set; }
        public List<ProductOffersGift> ListGifts { get; set; }
    }
    public class ProductOffersGift
    {
        public long ProductGiftsId { get; set; }
        public long PromotionOfferId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }
    public class ProductAttributes
    {
        public int ProductAttributeId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValues { get; set; }
        public string UnitName { get; set; }
    }
    public class ProductComments
    {
        public int ProductCommentId { get; set; }
        public string UserComment { get; set; }
        public string FullName { get; set; }
        public string ContentComment { get; set; }
        public int CommentReplyId { get; set; }
        public int RattingValues { get; set; }
        public string ImageAvatar { get; set; }
        public long ProductId { get; set; }
    }
    public class ProductCommentsAdd
    {
        public int ProductId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContentComment { get; set; }
        public int RattingValues { get; set; }
        public int CommentReplyId { get; set; }
    }
    public class ProductDetailAll
    {
        public ProductDetailMap Product { get; set; }
        public List<ProductPictures> Pictures { get; set; }
        public List<ProductOffers> Offers { get; set; }
        public List<ProductAttributes> Attributes { get; set; }
        public List<ProductComments> Comments { get; set; }
    }
}

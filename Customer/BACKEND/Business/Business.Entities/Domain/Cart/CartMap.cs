using System;
using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class CartMap : PagesModel
    {
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductSeoName { get; set; }
        public int RatingValues { get; set; }
        public string CookieId { get; set; }
        public decimal Price { get; set; } //0: NOT, 1: HAS, 2: ALL
        public string PriceStr { get; set; } //0: NOT, 1: HAS, 2: ALL
        public int Quantity { get; set; } //0: NOT, 1: HAS, 2: ALL
        public decimal TotalPriceItem { get; set; } //1,2,4, (cuối cũng cần có dấu phải)
        public string TotalPriceItemStr { get; set; } //1,2,4, (cuối cũng cần có dấu phải)
        public List<CartPromotion> CartPromotion { get; set; }
    }
    public class CartAdd
    {
        public long MemberId { get; set; }
        public long ProductId { get; set; }
        public string CookieId { get; set; }
        public float Price { get; set; } //0: NOT, 1: HAS, 2: ALL
        public int Quantity { get; set; } //0: NOT, 1: HAS, 2: ALL
        public float TotalPriceItem { get; set; } //1,2,4, (cuối cũng cần có dấu phải)
        public string CreatedUser { get; set; }
        public bool IsOffers { get; set; }
        public int PromotionId { get; set; }
        public int PromotionOfferId { get; set; }
        public List<CartPromotion> CartPromotion { get; set; }
    }
    public class CartParam : PagesParamModel
    {
        public long MemberId { get; set; }
        public string CookieId { get; set; }
    }

    public class CheckOutInfoParam
    {
        public long BookingID { get; set; }
        public int MemberID { get; set; }
        public string BookingNo { get; set; }
        public int BookingStatusID { get; set; }
        public string BookingDate { get; set; }
        public long TotalMoney { get; set; }
        public string CreateUser { get; set; }
        public int IsActive { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DisTrictID { get; set; }
        public int CountryID { get; set; }
        public int WardID { get; set; }
        public string Address { get; set; }
        public string CookieID { get; set; }
    }

    public class CartPromotion
    { 
        public long Id { get; set; }
        public long BookingId { get; set; }
        public int PromotionId { get; set; }
        public string PromotionOfferName { get; set; }
        public int CartPromotionId { get; set; }
        public int PromotionOfferId { get; set; }
        public int PromotionOfferApplyId { get; set; }
        public int ProductGiftsId { get; set; }
        public string ProductGiftsCode { get; set; }
        public string ProductGiftsName { get; set; }
        public int ProductId { get; set; }
        public int DisCountValue { get; set; }
        public string CookieId { get; set; } 
        public bool IsPercentDiscount { get; set; }
        public bool IsDiscountBySetPrice { get; set; }
        public bool IsFixedGift { get; set; } 
        public bool IsFixPrice { get; set; }

    }
    public class PromotionPargram
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string CookieId { get; set; }
    }

    public class CartOffers
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
        public float MinPromotionTotalMoney { get; set; }
        public float MaxPromotionTotalMoney { get; set; }
        public bool IsPromotionByToTalQuantity { get; set; }
        public int MinPromotionTotalQuantity { get; set; }
        public int MaxPromotionTotalQuantity { get; set; }
        public List<ProductOffersGift> ListGifts { get; set; }
    }

    public class PromotionApplyProduct
    {
        public long PromotionApplyProductId { get; set; }
        public long PromotionId { get; set; }
        public string PromotionCode { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }

    }
}

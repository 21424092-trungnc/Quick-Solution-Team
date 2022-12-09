using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class BookingMap
    {
        public long BookingID { get; set; }
        public long MemberID { get; set; }
        public string BookingNo { get; set; }
        public int BookingStatusID { get; set; }
        public string BookingDate { get; set; }
        public long TotalMoney { get; set; }
        public string Note { get; set; }
        public string CreatedUser { get; set; }
        public int IsActive { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryID { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int WardID { get; set; }
        public string Address { get; set; }
        public List<CartPromotion> CartPromotion { get; set; }

    }
    public class BookingDetailMap
    {
        public string CookieID { get; set; }
        public long BookingID { get; set; }
        public string UserID { get; set; }
        public long MemberID { get; set; } 
        public long CartID { get; set; }
    }
}

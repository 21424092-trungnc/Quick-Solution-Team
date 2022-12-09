using Business.Entities.Domain;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class CheckOutViewModel
    {
        public UserManager AccountDetail { get; set; }
        public List<CartMap> ListProdctCart { get; set; }
        public List<ProvinceMap> ListProvince {get;set;}
        public List<DistictMap> ListDistrict { get; set; }
        public List<WardMap> ListWard { get; set; }
        public List<CartOffers> Offers { get; set; }
        public bool IsOffersQuantity { get; set; }
        public bool IsOffersMoney { get; set; }
        public bool IsOffers { get; set; }
    }
}
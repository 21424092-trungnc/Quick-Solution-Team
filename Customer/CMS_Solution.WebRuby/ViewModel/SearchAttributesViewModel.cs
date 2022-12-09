using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class SearchAttributesViewModel
    {
        public SearchAttributesViewModel()
        {
            this.searchPar = new List<SearchParModel>();
            this.searchPrice = new List<SearchPriceModel>();

        }
        public long CateoryId { get; set; }
        public string NameCatetory { get; set; }
        public List<SearchPriceModel> searchPrice { get; set; }
        public List<SearchParModel> searchPar { get; set; }
         
        public long ManufacturerId { get; set; }
    }
    public class SearchPriceModel
    {
        public float PriceFrom { get; set; }
        public float PriceTo { get; set; }
    }
    public class SearchParModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
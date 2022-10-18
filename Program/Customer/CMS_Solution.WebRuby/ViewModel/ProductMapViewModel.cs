using Business.Entities;
using Business.Entities.Domain;
using Core.Common.UI.Paging;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class ProductMapViewModel
    {
        public ProductMapViewModel()
        {
            Product = new List<ProductMap>();
            PagingFilteringContext = new ProductPagingFilteringModel();
            SeoSetting = new SeoSettings();
        }
        public List<ProductMap> Product { get; set; }
        public ProductPagingFilteringModel PagingFilteringContext { get; set; }
        public SeoSettings SeoSetting { get; set; }
    }
    public class ProductPagingFilteringModel : BasePageableModel
    {
    }
}
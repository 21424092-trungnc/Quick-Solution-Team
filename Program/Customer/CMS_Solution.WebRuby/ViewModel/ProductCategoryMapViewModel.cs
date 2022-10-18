using Business.Entities.Domain;
using Core.Common.UI.Paging;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class ProductCategoryMapViewModel
    {
        public ProductCategoryMapViewModel()
        {
            PagingFilteringContext = new ProductCategoryPagingFilteringModel();
            ProductCates = new List<ProductCategoryMap>();
        }
        public List<ProductCategoryMap> ProductCates { get; set; }
        public ProductCategoryPagingFilteringModel PagingFilteringContext { get; set; }
    }
    public  class ProductCategoryPagingFilteringModel : BasePageableModel
    {
    }
}
using Business.Entities;
using Business.Entities.Domain;
using Core.Common.UI.Paging;
using System.Collections.Generic;


namespace CMS_Solution.WebRuby.ViewModel
{
    public class ProductSearchViewModel
    {
        public ProductSearchViewModel()
        {
            Items = new List<ProductMap>();
            Search = new ProductParam();
            PagingFilteringContext = new ProductSearchPagingFilteringModel();
        }
        public List<ProductMap> Items { get; set; }
        public ProductParam Search { get; set; }
        public ProductSearchPagingFilteringModel PagingFilteringContext { get; set; }
    }
    public class ProductSearchPagingFilteringModel : BasePageableModel
    {
    }
}
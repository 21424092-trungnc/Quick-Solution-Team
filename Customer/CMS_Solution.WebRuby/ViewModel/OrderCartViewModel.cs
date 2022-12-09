using Business.Entities.Domain;
using Core.Common.UI.Paging;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class OrderCartViewModel
    {
        public OrderCartViewModel()
        {
            Items = new List<CartMap>();
            Search = new CartParam();
            PagingFilteringContext = new OrderCartPagingFilteringModel();
        }
        public List<CartMap> Items { get; set; }
        public CartParam Search { get; set; }
        public OrderCartPagingFilteringModel PagingFilteringContext { get; set; }
    }
    public class OrderCartPagingFilteringModel : BasePageableModel
    {
    }
}
using Business.Entities;
using Business.Entities.Domain;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class ProductDetailAllViewModel
    {
        public ProductDetailAllViewModel()
        {
            ProductDetail = new ProductDetailAll();
            SeoSetting = new SeoSettings();
            ProductRelate = new List<ProductMap>();
            ProductCommentsAdd = new ProductCommentsAdd();
            CardAdd = new CartAdd();
        }
        public ProductDetailAll ProductDetail { get; set; }
        public SeoSettings SeoSetting { get; set; }
        public List<ProductMap> ProductRelate { get; set; }
        public ProductCommentsAdd ProductCommentsAdd { get; set; }
        public CartAdd CardAdd { get; set; }
    }
}
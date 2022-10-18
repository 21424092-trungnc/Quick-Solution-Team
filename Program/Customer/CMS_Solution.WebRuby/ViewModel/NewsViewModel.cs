using Business.Entities;
using Core.Common.UI.Paging;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class ListNewsViewModel
    {
        public ListNewsViewModel()
        {
            listCategoryNews = new List<NewsCategoryMap>();
            listNews = new List<NewsMap>();
            PagingFilteringContext = new NewsCategoryPagingFilteringModel();
        }
        public List<NewsCategoryMap> listCategoryNews { get; set; }
        public List<NewsMap> listNews { get; set; }
        public NewsCategoryPagingFilteringModel PagingFilteringContext { get; set; }
    }
    public class NewsDetailViewModel
    {
        public NewsDetail NewsDetail { get; set; }
        public List<NewsMap> ListSameCategory { get; set; }
    }
    public class ListNewsCategoryViewModel
    {
        public ListNewsCategoryViewModel()
        {
            listNews = new List<NewsMap>();
            SeoSetting = new SeoSettings();
            PagingFilteringContext = new NewsCategoryPagingFilteringModel();
        }
        public List<NewsMap> listNews { get; set; }
        public SeoSettings SeoSetting { get; set; }
        public NewsCategoryPagingFilteringModel PagingFilteringContext { get; set; }
    }
    public class NewsCategoryPagingFilteringModel : BasePageableModel
    {
    }
}
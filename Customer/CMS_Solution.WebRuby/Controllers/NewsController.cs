using Business.Entities;
using Module.Framework.UltimateClient;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using log4net;
using Module.Framework;
using CMS_Solution.WebRuby.ViewModel;
using Core.Common.UI.Paging;

namespace CMS_Solution.WebRuby.Controllers
{
    public partial class NewsController : BasePublicController
    {
        #region dungchung
        private NewsServiceClient _newService;
        private DungChungServiceClient dungChungService;
        private int _pageSize;
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(BaseController));
        #endregion
        // GET: News
        public virtual ActionResult Index()
        {
            return View();
        }
        //[ChildActionOnly]
        [HttpGet]
        public virtual ActionResult HomePageNews()
        {
            var news = new List<NewsMap>();
            try
            {
                var search = new NewsParam();
                search.IsShowHome = 1;
                search.IsActive = 1;
                search.IsHighLight = 1;
                search.PageIndex = 1;
                search.PageSize = 3;
                using (_newService = new NewsServiceClient())
                {
                    var tempList = _newService.NEWS_GetList(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        news = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NewsController error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(news);
        }

        //[ChildActionOnly]
        public virtual ActionResult CateNewsNavigation()
        {
            List<NewsCategoryMap> modeView = new List<NewsCategoryMap>();
            try
            {
                var _categoryParam = new NewsCategoryParam();
                _categoryParam.IsActive = 1;
                _categoryParam.IsDelete = 0;
                _categoryParam.PageIndex = 1;
                _categoryParam.PageSize = 9999;
                using (_newService = new NewsServiceClient())
                {
                    var categoryList = _newService.GetNewsCategory_GetList(_categoryParam);
                    if (categoryList.Data != null && categoryList.Data.resultObject != null && categoryList.Data.resultObject.Count > 0)
                    {
                        modeView = categoryList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NewsController error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }

            return PartialView(modeView);
        }
        //all news
        public virtual ActionResult List(NewsCategoryPagingFilteringModel command)
        {
            var viewNews = new ListNewsViewModel();
            try
            {
                if(command == null)
                    return InvokeHttp404();
                var _newsParm = new NewsParam();
                if (command.PageIndex <= 0)
                    _newsParm.PageIndex = 1;
                else
                    _newsParm.PageIndex = command.PageNumber;
                _newsParm.IsActive = 1;
                _newsParm.PageSize = 10;
                using (_newService = new NewsServiceClient())
                {
                    var newsList = _newService.NEWS_GetList(_newsParm);
                    if (newsList.Data != null && newsList.Data.resultObject != null && newsList.Data.resultObject.Count > 0)
                    {
                        viewNews.listNews = newsList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = viewNews.listNews[0].TotalCount;
                        pagedList.PageIndex = viewNews.listNews[0].PageIndex - 1;
                        pagedList.PageSize = viewNews.listNews[0].PageSize;
                        viewNews.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NewsController - NewsCategory error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(viewNews);
        }
        //new category
        public virtual ActionResult NewsCategory(long cateNewsItemId, NewsCategoryPagingFilteringModel command)
        {
            var viewNews = new ListNewsCategoryViewModel();
            try
            {
                if (command == null)
                    return InvokeHttp404();
                var _newsParm = new NewsParam();
                if (command.PageIndex <= 0)
                    _newsParm.PageIndex = 1;
                else
                    _newsParm.PageIndex = command.PageNumber;
                _newsParm.IsActive = 1;
                _newsParm.PageSize = 10;
                _newsParm.NewsCategoryID = cateNewsItemId;
                using (_newService = new NewsServiceClient())
                {
                    //seosetting
                    var param = new SeoSettingParam();
                    param.TableName = "NEWS_NEWSCATEGORY";
                    param.ParentID = cateNewsItemId.ToString();
                    dungChungService = new DungChungServiceClient();
                    var seoSetting = dungChungService.CBO_GetSeoCommon_Web(param);
                    if (seoSetting != null && seoSetting.Data.resultObject != null)
                    {
                        viewNews.SeoSetting = seoSetting.Data.resultObject;
                    }
                    //danh sach tin
                    var newsList = _newService.NEWS_GetList(_newsParm);
                    if (newsList.Data != null && newsList.Data.resultObject != null && newsList.Data.resultObject.Count > 0)
                    {
                        viewNews.listNews = newsList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = viewNews.listNews[0].TotalCount;
                        pagedList.PageIndex = viewNews.listNews[0].PageIndex - 1;
                        pagedList.PageSize = viewNews.listNews[0].PageSize;
                        viewNews.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NewsController - NewsCategory error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(viewNews);
        }
        //detail news
        public ActionResult NewsItem(long newsItemId = 0)
        {
            var viewData = new NewsDetailViewModel();
            try
            { 
                var _newsParm = new NewsSameParam();
                _newsParm.NewsID = newsItemId;
                _newsParm.PageIndex = 1;
                _newsParm.PageSize = 3;
                using (_newService = new NewsServiceClient())
                { 
                    var newsDetail = _newService.GetNews_ByID(newsItemId);
                    if (newsDetail.Data != null && newsDetail.Data.resultObject != null)
                    {
                        viewData.NewsDetail = newsDetail.Data.resultObject;
                    }
                    else
                    {
                        viewData.NewsDetail = new NewsDetail();
                    }
                    NewsSameParam model = new NewsSameParam();
                    model.NewsID = newsItemId;
                    model.PageIndex = 1;
                    model.PageSize = 3;
                    var newsSame = _newService.GetNewsSameCategoryByID(model);
                    if (newsSame.Data != null && newsSame.Data.resultObject != null)
                    {
                        viewData.ListSameCategory = newsSame.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NewsController - NewsItem error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(viewData);
        }

    }
}
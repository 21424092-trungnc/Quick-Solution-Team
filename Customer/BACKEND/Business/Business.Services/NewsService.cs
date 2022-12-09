using Business.Entities;
using Business.Services.Interfaces;
using Core.Common.Utilities;
using Data.Core.Repositories.Interfaces;
using log4net;
using System.Collections.Generic;
using System.Configuration;

namespace Business.Services
{
    public class NewsService : INewsService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(NewsService));

        #region Private Repository & contractor       
        private static readonly string CMSSolutionConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
        private static INewsRepository _newsRepository;
        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        #endregion

        public ResultResponse<List<NewsMap>> NEWS_GetList(NewsParam model)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            if(model.IsShowHome !=null && model.IsShowHome == 1 && model.IsHighLight != null && model.IsHighLight == 1)
            {
                cacheKey = CacheHelper.BuildKey("NEWS_ShowHome", "NEWS_" + model.IsShowHome);
            } 
            var data = CacheHelper.GetData<List<NewsMap>>(cacheKey);
            if (data == null)
            {
                data = _newsRepository.NEWS_GetList(model, out response);
                if (data != null && model.IsShowHome != null && model.IsShowHome == 1 && model.IsHighLight != null && model.IsHighLight == 1)
                    CacheHelper.SetData(cacheKey, data);
            }
          //  var data = _newsRepository.NEWS_GetList(model, out response);
            var result = new ResultResponse<List<NewsMap>>(response, data);
            return result;
        }

        public ResultResponse<List<NewsMap>> GetNewsSameCategoryByID(NewsSameParam model)
        {
            var response = new ResponseModel();
            var data = _newsRepository.GetNewsSameCategoryByID(model, out response);
            var result = new ResultResponse<List<NewsMap>>(response, data);
            return result;
        }

        public ResultResponse<NewsDetail> GetNews_ByID(long NewsID)
        {
            var response = new ResponseModel();
            var data = _newsRepository.GetNews_ByID(NewsID, out response);
            var result = new ResultResponse<NewsDetail>(response, data);
            return result;
        }

        public ResultResponse<List<NewsCategoryMap>> GetNewsCategory_GetList(NewsCategoryParam model)
        {
            var response = new ResponseModel();
            var data = _newsRepository.GetNewsCategory_GetList(model, out response);
            var result = new ResultResponse<List<NewsCategoryMap>>(response, data);
            return result;
        }

        public ResultResponse<NewsCategoryDetail> GetNewsCategory_ByID(long NewsCategoryID)
        {
            var response = new ResponseModel();
            var data = _newsRepository.GetNewsCategory_ByID(NewsCategoryID, out response);
            var result = new ResultResponse<NewsCategoryDetail>(response, data);
            return result;
        }
    }
}

using Business.Entities;
using Core.Common.Utilities;
using Dapper;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Core.Repositories
{
    public class NewsRepository :  INewsRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(NewsRepository));
        private const string TableName = "";
        public NewsRepository(ILog logger) 
        {
            _logger = logger;
        }
        #region "News"
        public List<NewsMap> NEWS_GetList(NewsParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ISHOTNEWS", model.IsHotNews, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISHIGHLIGHT",  model.IsHighLight, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISVIDEO", model.IsVideo, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISSHOWHOME", model.IsShowHome, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISSHOWNOTIFY", model.IsShowNotify, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISACTIVE", model.IsActive, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ISSYSTEM", model.IsSystem, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("NEWSCATEGORYID", model.NewsCategoryID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<NewsMap>("NEWS_NEWS_GetList_Web", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return datas as List<NewsMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NEWS_NEWS_GetList Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public NewsDetail GetNews_ByID(long NewsID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("NewsID", NewsID, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<NewsDetail>("NEWS_NEWS_ByID_Web", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return datas as NewsDetail ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetNNew_ByID Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<NewsMap> GetNewsSameCategoryByID(NewsSameParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("NewID", model.NewsID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<NewsMap>("NEWS_NEWSSAMECATEGORY_ByID_Web", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return datas as List<NewsMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NEWS_NEWSSAMECATEGORY_ByID_Web Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region "Category"
        public List<NewsCategoryMap> GetNewsCategory_GetList(NewsCategoryParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ParentId", model.ParentId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CategoryLevel", model.CategoryLevel, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("IsCateVideo", model.IsCateVideo, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("IsActive", model.IsActive, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("IsSystem", model.IsSystem, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("IsDelete", model.IsDelete, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<NewsCategoryMap>("NEWS_NEWSCATEGORY_GetList_Web", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return datas as List<NewsCategoryMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NEWS_NEWS_GetList Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }


        public NewsCategoryDetail GetNewsCategory_ByID(long NewsCategoryID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("NewsCategoryID", NewsCategoryID, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<NewsCategoryDetail>("NEWS_NEWSCATEGORY_ByID_Web", paramters, commandType: CommandType.StoredProcedure);

                    restStatus = new ResponseModel();
                    return datas as NewsCategoryDetail ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NEWS_NEWS_GetList Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion
    }

}

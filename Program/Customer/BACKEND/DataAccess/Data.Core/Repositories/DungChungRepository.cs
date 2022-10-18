using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Dapper;
using Data.Core.Repositories.Base;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Data.Core.Repositories
{
    public class DungChungRepository :Repository<DM_DungChungViewModel>, IDungChungRepository
    {

        private readonly ILog _logger = LogManager.GetLogger(typeof(DungChungRepository));
        private const string TableName = "DM_DungChungPageModel";
        public DungChungRepository(ILog logger) : base(TableName)
        {
            _logger = logger;
        }
        public List<CBO_DungChungViewModel> CBO_DungChung_GetAll(CBO_DungChungParam model,out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TableName", model.TableName, DbType.String, ParameterDirection.Input);
                    paramters.Add("ParentID", model.ParentID, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<CBO_DungChungViewModel>("CBO_DungChung_GetAll", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<CBO_DungChungViewModel> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public SeoSettings CBO_GetSeoCommon_Web(SeoSettingParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TableName", model.TableName, DbType.String, ParameterDirection.Input);
                    paramters.Add("ParentID", model.ParentID, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<SeoSettings>("CBO_GetSeoCommon_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as SeoSettings ?? datas; ;
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<UrlRecord> CMS_URLRECORD_GetList(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var datas = conns.Query<UrlRecord>("CMS_URLRECORD_GetList", null, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<UrlRecord> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public StaticContentMap CMS_StaticContent_Get(string systemName, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("SystemName", systemName, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<StaticContentMap>("CMS_STATICCONTENT_GetList_Web", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public BannerMap CMS_Banner_Get(string aliasname, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("aliasname", aliasname, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<BannerMap>("CMS_BANNER_GetList_Web", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<UserMap> SYS_User_GetList(string key, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("Key", key, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<UserMap>("SYS_USER_GetByKey", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<UserMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<BranchMap> SYS_Branch_GetList(string key, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("Key", key, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<BranchMap>("CMS_STATICCONTENT_GetList_Web", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BranchMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<ServiceMap> CMS_SetupService_GetList(string key, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("Key", key, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<ServiceMap>("CMS_SETUPSERVICE_GetList_ByKey_Web", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ServiceMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #region ContactUs
        public int CMS_ContactUs_Create(ContactUsMap model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("SupportId", model.SupportId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("TopicId", model.TopicId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("FullName", model.FullName, DbType.String, ParameterDirection.Input);
                    parameters.Add("PhoneNumber", model.PhoneNumber, DbType.String, ParameterDirection.Input);
                    parameters.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    parameters.Add("ContentSupport", model.ContentSupport, DbType.String, ParameterDirection.Input);
                    parameters.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("User", model.User, DbType.String, ParameterDirection.Input);
                    parameters.Add("IsDeleted", model.IsDeleted, DbType.Boolean, ParameterDirection.Input);
                    var data = conns.ExecuteScalar<int>("CMS_SUPPORT_Create", parameters, commandType: CommandType.StoredProcedure);
                   
                    restStatus = new ResponseModel();
                    return data;
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion

        #region Header
        public List<MenuMap> CMS_WebsiteCategory_Get(string key, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("Key", key, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<MenuMap>("CMS_WEBSITECATEGORY_GetList_Web", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<MenuMap> ?? datas.ToList(); ;
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region "Address"
        public List<ProvinceMap> MD_PROVINCE_GetList(string model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("ParentID", model, DbType.String, ParameterDirection.Input);
                    parameters.Add("TableName", "MD_PROVINCE", DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<ProvinceMap>("CBO_DungChung_GetAll", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ProvinceMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<DistictMap> MD_DISTRICT_GetList(string model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("ParentID", model, DbType.String, ParameterDirection.Input);
                    parameters.Add("TableName", "MD_DISTRICT", DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<DistictMap>("CBO_DungChung_GetAll", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DistictMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<WardMap> MD_WARD_GetList(string model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = CMSSolutionConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("ParentID", model, DbType.String, ParameterDirection.Input);
                    parameters.Add("TableName", "MD_WARD", DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<WardMap>("CBO_DungChung_GetAll", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<WardMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        
        #endregion
    }
}

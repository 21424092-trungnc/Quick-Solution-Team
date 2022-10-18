using Business.Entities;
using Business.Entities.Domain;
using Business.Services.Interfaces;
using Core.Common.Utilities;
using Data.Core.Repositories.Interfaces;
using log4net;
using System.Collections.Generic;
using System.Configuration;
using System.Linq; 

namespace Business.Services
{
    public class DungChungService : IDungChungService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(DungChungService));

        #region Private Repository & contractor
        //IDbConnection cons;
        //IDbTransaction trans;
        private static readonly string CMSSolutionConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
        private static IDungChungRepository _dungChungRepository;
        private static IBannerRepository _bannerRepository;
        public DungChungService(IDungChungRepository dungChungRepository ,
                                IBannerRepository bannerRepository)
        {
            _dungChungRepository = dungChungRepository;
            _bannerRepository = bannerRepository;
        }
        #endregion
        public ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAll(CBO_DungChungParam model)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey(model.TableName,"DungChung" + (string.IsNullOrEmpty(model.ParentID)?"": model.ParentID));
            var data = CacheHelper.GetData<List<CBO_DungChungViewModel>>(cacheKey);
            if (data == null)
            {
                data = _dungChungRepository.CBO_DungChung_GetAll(model, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<List<CBO_DungChungViewModel>>(response, data);
        }

        #region Urlrecord
        public ResultResponse<UrlRecord> GetUrlRecord(string slug)
        {
            var response = new ResponseModel();
            var list = _dungChungRepository.CMS_URLRECORD_GetList(out response);
            if (list != null && list.Count>0)
            {
               var result =  list.Where(x => x.Slug == slug).FirstOrDefault();
                return new ResultResponse<UrlRecord>(response, result); 
            }
           
            return new ResultResponse<UrlRecord>(response, null);
        }
        #endregion

        #region Banner
        public ResultResponse<List<BannerMap>> CMS_BANNER_GetList_Web()
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("CMS_BANNER", "");
            var data = CacheHelper.GetData<List<BannerMap>>(cacheKey);
            if (data == null)
            {
                data = _bannerRepository.CMS_BANNER_GetList_Web(out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<List<BannerMap>>(response, data);
        }
        #endregion

        #region Seo
        public ResultResponse<SeoSettings> CBO_GetSeoCommon_Web(SeoSettingParam model)
        {
            var response = new ResponseModel();
            var data = _dungChungRepository.CBO_GetSeoCommon_Web(model, out response);
            return new ResultResponse<SeoSettings>(response, data);
        }
        #endregion

        #region ContactUs
        public ResultResponse<int> CMS_ContactUs_Create(ContactUsMap model)
        {
            var response = new ResponseModel();
            var data = _dungChungRepository.CMS_ContactUs_Create(model, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        #endregion

        #region StaticContent
        public ResultResponse<StaticContentMap> CMS_StaticContent_Get(string systemName)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey(systemName, "CMS_StaticContent_" +systemName);
            var data = CacheHelper.GetData<StaticContentMap>(cacheKey);
            if (data == null)
            {
                data = _dungChungRepository.CMS_StaticContent_Get(systemName, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
           // var data = _dungChungRepository.CMS_StaticContent_Get(systemName, out response);
            return new ResultResponse<StaticContentMap> (response, data);
        }
        public ResultResponse<BannerMap> CMS_Banner_Get(string aliasname)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey(aliasname, "CMS_StaticContent_" + aliasname);
            var data = CacheHelper.GetData<BannerMap>(cacheKey);
            if (data == null)
            {
                data = _dungChungRepository.CMS_Banner_Get(aliasname, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            // var data = _dungChungRepository.CMS_StaticContent_Get(systemName, out response);
            return new ResultResponse<BannerMap>(response, data);
        }
        #endregion

        #region Service
        public ResultResponse<List<ServiceMap>> CMS_SetupService_GetList(string key)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("CMS_SetupService_"+ key, key);
            var data = CacheHelper.GetData<List<ServiceMap>>(cacheKey);
            if (data == null)
            {
                data = _dungChungRepository.CMS_SetupService_GetList(key, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            //var data = _dungChungRepository.CMS_SetupService_GetList(key, out response);
            return new ResultResponse<List<ServiceMap>>(response, data);
        }
        #endregion

        #region About
        public ResultResponse<List<BranchMap>> SYS_Branch_GetList(string key)
        {
            var response = new ResponseModel();
            var data = _dungChungRepository.SYS_Branch_GetList(key, out response);
            return new ResultResponse<List<BranchMap>>(response, data);
        }
        public ResultResponse<List<UserMap>> SYS_User_GetList(string key)
        {
            var response = new ResponseModel();
            var data = _dungChungRepository.SYS_User_GetList(key, out response);
            return new ResultResponse<List<UserMap>>(response, data);
        }
        #endregion

        #region Header
        public ResultResponse<List<MenuMap>> CMS_WebsiteCategory_Get(string key)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("CMS_WebsiteCategory_" + key, "CMS_WebsiteCategory_Get_" + key);
            var data = CacheHelper.GetData<List<MenuMap>>(cacheKey);
            if (data == null)
            {
                data = _dungChungRepository.CMS_WebsiteCategory_Get(key, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
           // var data = _dungChungRepository.CMS_WebsiteCategory_Get(key, out response);
            return new ResultResponse<List<MenuMap>> (response, data);
        }
        #endregion


        #region "Address"
        public ResultResponse<List<ProvinceMap>> MD_PROVINCE_GetList(string model)
        {
            var response = new ResponseModel();
            var data = _dungChungRepository.MD_PROVINCE_GetList(model, out response);
            var result = new ResultResponse<List<ProvinceMap>>(response, data);
            return result;
        }
        public ResultResponse<List<DistictMap>> MD_DISTRICT_GetList(string model)
        {
            var response = new ResponseModel();
            var data = _dungChungRepository.MD_DISTRICT_GetList(model, out response);
            var result = new ResultResponse<List<DistictMap>>(response, data);
            return result;
        }
        public ResultResponse<List<WardMap>> MD_WARD_GetList(string model)
        {
            var response = new ResponseModel();
            var data = _dungChungRepository.MD_WARD_GetList(model, out response);
            var result = new ResultResponse<List<WardMap>>(response, data);
            return result;
        }
        #endregion

        #region "DisposeCache" 
        public bool DisposeCacheService(string key)
        {
            try
            {
                string cacheKey = string.Empty;
                if (!string.IsNullOrEmpty(key))
                {
                    cacheKey = CacheHelper.BuildKey(key, "");
                    CacheHelper.Remove(cacheKey);
                }
                else {
                    CacheHelper.RemoveAll();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

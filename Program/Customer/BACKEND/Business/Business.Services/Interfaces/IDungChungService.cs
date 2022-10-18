using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IDungChungService
    {

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DC/CBO_DungChung_GetAll")]
        ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAll(CBO_DungChungParam model);

        #region url record
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DC/GetUrlRecord?slug={slug}")]
        ResultResponse<UrlRecord> GetUrlRecord(string slug);
        #endregion

        #region Banner
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DC/CMS_BANNER_GetList_Web")]
        ResultResponse<List<BannerMap>> CMS_BANNER_GetList_Web();
        #endregion

        #region seo

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DC/CBO_GetSeoCommon_Web")]
        ResultResponse<SeoSettings> CBO_GetSeoCommon_Web(SeoSettingParam model);
        #endregion

        #region ContactUs
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/CMS_ContactUs_Create")]
        ResultResponse<int> CMS_ContactUs_Create(ContactUsMap model);
        #endregion

        #region StaticContent
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/CMS_StaticContent_Get?systemname={systemname}")]
        ResultResponse<StaticContentMap> CMS_StaticContent_Get(string systemName);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/CMS_Banner_Get?aliasname={aliasname}")]
        ResultResponse<BannerMap> CMS_Banner_Get(string aliasname);
        #endregion

        #region About
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/SYS_Branch_GetList?key={key}")]
        ResultResponse<List<BranchMap>> SYS_Branch_GetList(string key);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/SYS_User_GetList?key={key}")]
        ResultResponse<List<UserMap>> SYS_User_GetList(string key);
        #endregion

        #region Service
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/CMS_SetupService_GetList?key={key}")]
        ResultResponse<List<ServiceMap>> CMS_SetupService_GetList(string key);
        #endregion

        #region Header
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/CMS_WebsiteCategory_Get?key={key}")]
        ResultResponse<List<MenuMap>> CMS_WebsiteCategory_Get(string key);
        #endregion


        #region "Address"
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DC/MD_PROVINCE_GetList")]
        ResultResponse<List<ProvinceMap>> MD_PROVINCE_GetList(string model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DC/MD_DISTRICT_GetList")]
        ResultResponse<List<DistictMap>> MD_DISTRICT_GetList(string model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DC/MD_WARD_GetList")]
        ResultResponse<List<WardMap>> MD_WARD_GetList(string model);
        #endregion

        #region "DisposeCache"

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DC/DisposeCacheService?key={key}")]
        bool DisposeCacheService(string key);
        #endregion
    }
}

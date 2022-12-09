using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDungChungRepository : IRepository<DM_DungChungViewModel>
    {
        List<CBO_DungChungViewModel> CBO_DungChung_GetAll(CBO_DungChungParam model, out ResponseModel restStatus);
        List<UrlRecord> CMS_URLRECORD_GetList(out ResponseModel restStatus);
        SeoSettings CBO_GetSeoCommon_Web(SeoSettingParam model, out ResponseModel restStatus);
        StaticContentMap CMS_StaticContent_Get(string systemName, out ResponseModel restStatus);
        BannerMap CMS_Banner_Get(string aliasname, out ResponseModel restStatus);
        List<ServiceMap> CMS_SetupService_GetList(string key, out ResponseModel restStatus);
        List<BranchMap> SYS_Branch_GetList(string key, out ResponseModel restStatus);
        List<UserMap> SYS_User_GetList(string key, out ResponseModel restStatus);
        #region ContactUs
        int CMS_ContactUs_Create(ContactUsMap model, out ResponseModel restStatus);
        #endregion

        #region Header
        List<MenuMap> CMS_WebsiteCategory_Get(string key, out ResponseModel restStatus);
        #endregion

        #region "Address"
        List<ProvinceMap> MD_PROVINCE_GetList(string model, out ResponseModel restStatus);
        List<DistictMap> MD_DISTRICT_GetList(string model, out ResponseModel restStatus);
        List<WardMap> MD_WARD_GetList(string model, out ResponseModel restStatus);
        #endregion
    }
}

using Business.Entities;
using Business.Entities.Domain;
using CMS_Solution.WebRuby.Common;
using CMS_Solution.WebRuby.ViewModel;
using Core.Common.UI.Paging;
using log4net;
using Module.Framework;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Core.Common.Utilities;

namespace CMS_Solution.WebRuby.Controllers
{
    public partial class CommonController : BasePublicController
    {
        #region dungchung
        private DungChungServiceClient _dungChungService;
        private RecruitmentServiceClient _recruitmentService;
        private NewsServiceClient _newService;
        private ProductServiceClient _proService;
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(BaseController));
        #endregion
        //page not found
        public virtual ActionResult PageNotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;
            this.Response.ContentType = "text/html";

            return View();
        }
        // GET: Common
        public virtual ActionResult Index()
        {
            return View();
        }

        //[ChildActionOnly]
        public virtual ActionResult AboutHome()
        {
            AboutHomeViewModel resultModel = new AboutHomeViewModel();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var AboutHome = new StaticContentMap();
                    string cacheKey = string.Empty;
                    cacheKey = CacheHelper.BuildKey(ConstBuildKeyCache.StaticContent, ConstBuildKeyCache.StaticContent_AboutHome);
                    AboutHome = CacheHelper.GetData<StaticContentMap>(cacheKey);
                    if (AboutHome == null)
                    {
                        var tempList = _dungChungService.CMS_StaticContent_Get("AboutHome");
                        if (tempList.Data != null && tempList.Data.resultObject != null)
                        {
                            AboutHome = tempList.Data.resultObject;
                        }
                        CacheHelper.SetData(cacheKey, AboutHome);
                    }
                    
                    resultModel.staticContentMap = AboutHome;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("About error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(resultModel);
        }
        //[ChildActionOnly]
        public virtual ActionResult HomeBanner()
        {
            var slider = new List<BannerMap>();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var CMS_BANNER = new List<BannerMap>();
                    string cacheKey = string.Empty;
                    cacheKey = CacheHelper.BuildKey("CMS_BANNER_GetList_Web", "");
                    CMS_BANNER = CacheHelper.GetData<List<BannerMap>>(cacheKey);
                    if (CMS_BANNER == null)
                    {
                        var tempList = _dungChungService.CMS_BANNER_GetList_Web();
                        if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                        {
                            CMS_BANNER = tempList.Data.resultObject;
                        }
                        CacheHelper.SetData(cacheKey, CMS_BANNER);
                    }
                    
                    slider = CMS_BANNER;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CommonController error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(slider);
        }

        //[ChildActionOnly]
        [HttpGet]
        public virtual ActionResult HomePageAdvertisement()
        {
            return PartialView();
        }
        //[ChildActionOnly]
        public virtual ActionResult Footer()
        {
            FooterViewModel resultModel = new FooterViewModel();
            try
            {
                
                using (_dungChungService = new DungChungServiceClient())
                {
                    var footerTop = new StaticContentMap();
                    string cacheKey = string.Empty;
                    cacheKey = CacheHelper.BuildKey("CMS_StaticContent_Get", "FooterTop");
                    footerTop = CacheHelper.GetData<StaticContentMap>(cacheKey);
                    if (footerTop == null)
                    {
                        var footerTopResult = _dungChungService.CMS_StaticContent_Get("FooterTop");
                        if (footerTopResult.Data != null && footerTopResult.Data.resultObject != null)
                        {
                            footerTop = footerTopResult.Data.resultObject;
                        }
                        CacheHelper.SetData(cacheKey, footerTop);
                    }

                    var menuMobile = new List<MenuMap>();
                    //Menu mobile
                    string cacheKeyMenu = string.Empty;
                    cacheKeyMenu = CacheHelper.BuildKey("CMS_WebsiteCategory_Get", "1");
                    menuMobile = CacheHelper.GetData<List<MenuMap>>(cacheKeyMenu);
                    if (menuMobile == null)
                    {
                        var tempList = _dungChungService.CMS_WebsiteCategory_Get("1");
                        if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                        {
                            menuMobile = tempList.Data.resultObject;
                        }
                        CacheHelper.SetData(cacheKeyMenu, menuMobile);
                    }
                    resultModel.footerTopMap = footerTop;
                    resultModel.menuMap = menuMobile;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CommonController error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(resultModel);
        }
        //[ChildActionOnly]
        public virtual ActionResult Logo()
        {
            return PartialView();
        }
        //[ChildActionOnly]
        public virtual ActionResult LogoMobi()
        {
            return PartialView();
        }
        //favicon
        //[ChildActionOnly]
        public virtual ActionResult Favicon()
        {
            return PartialView();
        }
        //[ChildActionOnly]
        public virtual ActionResult Recruitment()
        {
            var recruitments = new RecruitmentViewModel();
            try
            {
                var search = new RecruitmentParam();
                search.PageIndex = 1;
                search.PageSize = 10;
                using (_recruitmentService = new RecruitmentServiceClient())
                {
                    var tempList = _recruitmentService.HR_RECRUIT_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        recruitments.Items = tempList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = recruitments.Items[0].TotalCount;
                        pagedList.PageIndex = recruitments.Items[0].PageIndex - 1;
                        pagedList.PageSize = recruitments.Items[0].PageSize;
                        recruitments.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Recruitment error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(recruitments);
        }
        [HttpPost]
        public ActionResult Search(RecruitmentParam model)
        {
            var recruitments = new RecruitmentViewModel();
            try
            {
                model.PageIndex = 1;
                model.PageSize = 10;
                using (_recruitmentService = new RecruitmentServiceClient())
                {
                    var tempList = _recruitmentService.HR_RECRUIT_GetList_Web(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        recruitments.Items = tempList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = recruitments.Items[0].TotalCount;
                        pagedList.PageIndex = recruitments.Items[0].PageIndex - 1;
                        pagedList.PageSize = recruitments.Items[0].PageSize;
                        recruitments.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Recruitment error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView("Partial/RecruitmentListPartial", recruitments);
        }
        public virtual ActionResult RecruitmentItem(long recruitmentId = 0)
        {
            var recruitments = new RecruitmentDetailViewModel();
            try
            {
                using (_recruitmentService = new RecruitmentServiceClient())
                {
                    var tempList = _recruitmentService.HR_RECRUIT_GetRecruit_ByID(recruitmentId);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        recruitments.Detail = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Recruitment error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            recruitments.Candidate = new CandidateMapAdd()
            {
                RecruitId = recruitmentId
            };
            return View(recruitments);
        }
        [HttpPost]
        public ActionResult Apply(CandidateMapAdd mapAdd)
        {
            bool status = false;
            try
            {
                using (_recruitmentService = new RecruitmentServiceClient())
                {
                    mapAdd.UserID = AppSetting.UserWebID;
                    var tempList = _recruitmentService.HR_CANDIDATE_InsUpd_Web(mapAdd);
                    if (tempList.Data != null && tempList.Data.resultObject)
                    {
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Recruitment error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                status = false;
            }
            return Json(new { status = status });
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            try
            {
                HttpPostedFile myFile = System.Web.HttpContext.Current.Request.Files["UploadedFiles"];

                bool status = false;
                string filePath = string.Empty;
                string fileName = string.Empty;
                if (Request.Files.Count > 0)
                {
                    var fileUpload = Request.Files[0];
                    var fileInfo = CommonWebsite.UploadFile(fileUpload);

                    filePath = fileInfo.Url;
                    fileName = fileInfo.TenGoc;
                    if (!string.IsNullOrEmpty(fileInfo.Url))
                        status = true;
                }
                return Json(new { status = status, filePath = filePath, fileName = fileName });
            }
            catch (Exception ex)
            {
                _logger.Error("Recruitment error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                return Json(new { status = false });
            }
        }

        public virtual ActionResult GenericUrl()
        {
            //seems that no entity was found
            return InvokeHttp404();
        }

        #region About
        public virtual ActionResult About()
        {
            AboutViewModel viewModel = new AboutViewModel();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var staticContent = _dungChungService.CMS_StaticContent_Get("About");
                    if (staticContent.Data != null && staticContent.Data.resultObject != null)
                    {
                        viewModel.staticContentMap = staticContent.Data.resultObject;
                    }
                    var partnerContent = _dungChungService.CMS_StaticContent_Get("Partner");
                    if (partnerContent.Data != null && partnerContent.Data.resultObject != null)
                    {
                        viewModel.partnerContentMap = partnerContent.Data.resultObject;
                    }
                    var lstUser = _dungChungService.SYS_User_GetList("Ban giám đốc");
                    if (lstUser.Data != null && lstUser.Data.resultObject != null && lstUser.Data.resultObject.Count > 0)
                    {
                        viewModel.lstUserMap = lstUser.Data.resultObject;
                    }
                    var branch = _dungChungService.CMS_StaticContent_Get("Branch");
                    if (branch.Data != null && branch.Data.resultObject != null)
                    {
                        viewModel.branchMap = branch.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("About error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(viewModel);
        }
        #endregion

        #region ContactUs
        public virtual ActionResult ContactUs()
        {
            ContactUsViewModel resultModel = new ContactUsViewModel();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var tempList = _dungChungService.CMS_StaticContent_Get("ContactUs");
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        resultModel.contactContentMap = tempList.Data.resultObject;
                    }
                    var mapList = _dungChungService.CMS_StaticContent_Get("ContactUsMap");
                    if (mapList.Data != null && mapList.Data.resultObject != null)
                    {
                        resultModel.mapContentMap = mapList.Data.resultObject;
                    }
                    CBO_DungChungParam param = new CBO_DungChungParam();
                    param.TableName = "CMS_TOPIC";
                    var lstTopic = _dungChungService.CBO_DungChung_GetAll(param);
                    if (lstTopic != null && lstTopic.resultObject != null && lstTopic.resultObject.Any())
                    {
                        resultModel.contactContentMap.lstTopic = lstTopic.resultObject.Select(m => new CBO_DungChungViewModel()
                        {
                            Ma1 = m.Ma1.ToString(),
                            Ten1 = m.Ten1
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ContactUs error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(resultModel);
        }
        public virtual ActionResult ContactUs_Create()
        {
            bool status = false;
            try
            {
                var model = ColectionItem<ContactUsMap>("modelThongTin");
                if (model != null)
                {
                    using (_dungChungService = new DungChungServiceClient())
                    {
                        var result = _dungChungService.CMS_ContactUs_Create(model);
                        if (result.Data != null && result.Data.resultObject > 0)
                            SendMail.CMS_ContactUs_Send(model);
                        status = true;
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                _logger.Error("CommonController error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                return Json(new { status = false });
            }
        }
        #endregion
        protected T ColectionItem<T>(string nameRequest)
        {
            try
            {
                var model = Request.Form[nameRequest];
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                var item = JsonConvert.DeserializeObject<T>(model, settings);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region Header
        // [ChildActionOnly]
        public virtual ActionResult Header()
        {

            var key = "1";
            var result = new List<MenuMap>();
            var rsProductServiceClient = new List<ProductCategoryMap>();
            try
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("NEWS_GetList", "NEWS_home");
                result = CacheHelper.GetData<List<MenuMap>>(cacheKey);
                if (result == null)
                {
                    using (_dungChungService = new DungChungServiceClient())
                    {
                        var tempList = _dungChungService.CMS_WebsiteCategory_Get(key);
                        if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                        {
                            result = tempList.Data.resultObject;
                        }
                    }
                    CacheHelper.SetData(cacheKey, result);
                }


                string cacheKeyNew = string.Empty;
                cacheKeyNew = CacheHelper.BuildKey("NewsCategoryMap", "NEWS_Cate");
                // lay danh muc tin tuc
                List<NewsCategoryMap> modeView = new List<NewsCategoryMap>();
                modeView = CacheHelper.GetData<List<NewsCategoryMap>>(cacheKeyNew);
                if (modeView == null)
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
                    CacheHelper.SetData(cacheKeyNew, modeView);
                }
                ViewBag.NewsCategoryMap = modeView;
                //DateTime datetime2 = DateTime.Now;
                //Double rs = (datetime2 - dateTime).TotalMilliseconds;
                // lay danh catalo
                // lay danh muc tin tuc
                string CacheKeyProductService = string.Empty;
                CacheKeyProductService = CacheHelper.BuildKey("Product_Service", "PRODUCT");
                rsProductServiceClient = CacheHelper.GetData<List<ProductCategoryMap>>(CacheKeyProductService);
                if (rsProductServiceClient == null)
                {
                    var search = new ProductCategoryParam();
                    search.PageIndex = 1;
                    search.PageSize = 10000;
                    search.KeyCache = "MD_PRODUCTCATEGORY_TopMenu";
                    using (_proService = new ProductServiceClient())
                    {
                        var tempList = _proService.MD_PRODUCTCATEGORY_GetList_Web(search);
                        if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                        {
                            rsProductServiceClient = tempList.Data.resultObject;
                        }
                    }
                }
                CacheHelper.SetData(CacheKeyProductService, rsProductServiceClient);
                ViewBag.ProductServiceClient = rsProductServiceClient;
            }
            catch (Exception ex)
            {
                _logger.Error("CommonController error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(result);
        }
        #endregion

        #region "Get District"
        [HttpPost]
        public ActionResult GetDistrictByProvinceID(string ProvinceID = "0")
        {
            List<DistictMap> data = new List<DistictMap>();
            using (_dungChungService = new DungChungServiceClient())
            {
                if (ProvinceID != "0")
                {
                    var listWard = _dungChungService.MD_DISTRICT_GetList(ProvinceID.ToString());
                    if (listWard.Data != null && listWard.Data.resultObject != null && listWard.Data.resultObject.Count > 0)
                    {
                        data = listWard.Data.resultObject;
                    }
                }
            }
            return Json(new { data = data, status = true });
        }
        #endregion

        #region static pages
        public virtual ActionResult Polyci() {
            return View();
        }

        public virtual ActionResult Polyciwarranty()
        {
            return View();
        }

        public virtual ActionResult PolyciReturn()
        {
            return View();
        }
        public virtual ActionResult PolyciPayment()
        {
            return View();
        }
        public virtual ActionResult PolyciSecurity()
        {
            return View();
        }
        #endregion

    }
}
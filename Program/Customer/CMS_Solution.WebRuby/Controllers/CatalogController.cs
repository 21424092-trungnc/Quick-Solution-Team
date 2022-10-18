using Business.Entities;
using Business.Entities.Domain;
using CMS_Solution.WebRuby.ViewModel;
using Core.Common.UI.Paging;
using log4net;
using Module.Framework;
using Module.Framework.UltimateClient;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using CMS_Solution.WebRuby.Common;

namespace CMS_Solution.WebRuby.Controllers
{
    public partial class CatalogController : BasePublicController
    {
        #region dungchung
        private ProductServiceClient _proService;
        private DungChungServiceClient dungChungService;
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(CatalogController));
        #endregion
        // GET: Catalog
        public virtual ActionResult Index(ProductCategoryPagingFilteringModel command)
        {
            var result = new ProductCategoryMapViewModel();
            try
            {
                if(command == null)
                    return InvokeHttp404();
                var search = new ProductCategoryParam();
                if (command.PageIndex <= 0)
                    search.PageIndex = 1;
                else
                    search.PageIndex = command.PageNumber;
                search.PageSize = 8;
                search.KeyCache = "MD_PRODUCTCATEGORY_Index_PageIndex_" + search.PageIndex.ToString();
                using (_proService = new ProductServiceClient())
                {
                    var tempList = _proService.MD_PRODUCTCATEGORY_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        result.ProductCates = tempList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = result.ProductCates[0].TotalCount;
                        pagedList.PageIndex = result.ProductCates[0].PageIndex - 1;
                        pagedList.PageSize = result.ProductCates[0].PageSize;
                        result.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                    else
                    {
                        return InvokeHttp404();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CatalogController Index error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(result);
        }
       // [ChildActionOnly]
        public virtual ActionResult TopMenuCategory()
        {
            var result = new List<ProductCategoryMap>();
            try
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
                        result = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CatalogController TopMenuCategory error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(result);
        }
        [ChildActionOnly]
        public virtual ActionResult LeftMenuCategory(long categoryid)
        {
            var result = new List<ProductCategoryMap>();
              ViewData["categoryid"] = categoryid;
            try
            {
                var search = new ProductCategoryParam();
                search.PageIndex = 1;
                search.PageSize = 10000;
                search.KeyCache = "MD_PRODUCTCATEGORY_LeftMenu";
                using (_proService = new ProductServiceClient())
                {
                    var tempList = _proService.MD_PRODUCTCATEGORY_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        result = tempList.Data.resultObject.Where(x => x.ParentId == 0).ToList();
                       
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CatalogController LeftMenuCategory error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(result);
        }
        public virtual ActionResult Category(long categoryid, string[] p, string[] s, ProductPagingFilteringModel command)
        {
            var result = new ProductMapViewModel();
            ViewData["categoryid"] = categoryid;
            ViewData["p"] = p;
            ViewData["s"] = s;
            try
            {
                if (command == null)
                    return InvokeHttp404();
                var search = new ProductParam();
                var model = new SearchAttributesViewModel();
                model.CateoryId = categoryid;
                model.searchPrice = new List<SearchPriceModel>();
                if (p != null)
                {
                    foreach (string i in p)
                    {
                        SearchPriceModel searPrice = new SearchPriceModel();
                        string[] temp = i.Split(new char[] { '-' });
                        if (temp.Count() >= 4)
                        {
                            searPrice.PriceFrom = float.Parse(temp[1]);
                            searPrice.PriceTo = float.Parse(temp[2]);
                        }
                        else
                        {
                            if (temp[0].ToString().ToUpper() == "duoi".ToUpper())
                            {
                                searPrice.PriceFrom = 0;
                                searPrice.PriceTo = float.Parse(temp[1]);
                            }
                            else
                            {
                                searPrice.PriceFrom = float.Parse(temp[1]);
                                searPrice.PriceTo = 0;
                            }
                        }
                        model.searchPrice.Add(searPrice);
                    }
                }
                if (s != null)
                {
                    foreach (string j in s)
                    {
                        SearchParModel searPar = new SearchParModel();
                        string[] temp = j.Split(new char[] { '-' });
                        searPar.Id = int.Parse(temp[1]);
                        searPar.Name = temp[0];
                        model.searchPar.Add(searPar);
                    }
                }
                using (_proService = new ProductServiceClient())
                {
                    //seosetting
                    var param = new SeoSettingParam();
                    param.TableName = "MD_PRODUCTCATEGORY";
                    param.ParentID = categoryid.ToString();
                    dungChungService = new DungChungServiceClient();
                    var seoSetting = dungChungService.CBO_GetSeoCommon_Web(param);
                    if (seoSetting != null && seoSetting.Data.resultObject != null)
                    {
                        result.SeoSetting = seoSetting.Data.resultObject;
                    }
                    search = CommonWebsite.PrepareSearchProductAttributeModel(model, command);
                    var tempList = _proService.MD_PRODUCT_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null )
                    {
                        result.Product = tempList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = result.Product[0].TotalCount;
                        pagedList.PageIndex = result.Product[0].PageIndex - 1;
                        pagedList.PageSize = result.Product[0].PageSize;
                        result.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                    else
                    {
                        return InvokeHttp404();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CatalogController Category error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(result);
        }
        public virtual ActionResult FilterAttributeProductSearch(string id, string[] p, string[] s, ProductPagingFilteringModel command, string groupid = "0")
        {
            var result = new ProductMapViewModel();
            try
            {
                if (command == null)
                    return InvokeHttp404();
                var model = new SearchAttributesViewModel();
                model.CateoryId = long.Parse(id);
                model.ManufacturerId = long.Parse(groupid);
                model.searchPrice = new List<SearchPriceModel>();
                if (p != null)
                {
                    foreach (string i in p)
                    {
                        SearchPriceModel searPrice = new SearchPriceModel();
                        string[] temp = i.Split(new char[] { '-' });
                        if (temp.Count() >= 4)
                        {
                            searPrice.PriceFrom = float.Parse(temp[1]);
                            searPrice.PriceTo = float.Parse(temp[2]);
                        }
                        else
                        {
                            if (temp[0].ToString().ToUpper() == "duoi".ToUpper())
                            {
                                searPrice.PriceFrom = 0;
                                searPrice.PriceTo = float.Parse(temp[1]);
                            }
                            else
                            {
                                searPrice.PriceFrom = float.Parse(temp[1]);
                                searPrice.PriceTo = 0;
                            }
                        }
                        model.searchPrice.Add(searPrice);
                    }
                }
                if (s != null)
                {
                    foreach (string j in s)
                    {
                        SearchParModel searPar = new SearchParModel();
                        string[] temp = j.Split(new char[] { '-' });
                        searPar.Id = int.Parse(temp[1]);
                        searPar.Name = temp[0];
                        model.searchPar.Add(searPar);
                    }
                }
                using (_proService = new ProductServiceClient())
                {
                    var search = new ProductParam();
                    //seosetting
                    var param = new SeoSettingParam();
                    if(model.ManufacturerId > 0)
                    {
                        param.TableName = "MD_MANUFACTURER";
                        param.ParentID = model.ManufacturerId.ToString();
                    }
                    else {
                        param.TableName = "MD_PRODUCTCATEGORY";
                        param.ParentID = id;
                    }
                    
                    dungChungService = new DungChungServiceClient();
                    var seoSetting = dungChungService.CBO_GetSeoCommon_Web(param);
                    if (seoSetting != null && seoSetting.Data.resultObject != null)
                    {
                        result.SeoSetting = seoSetting.Data.resultObject;
                        result.SeoSetting.GroupID = model.ManufacturerId;
                    }
                    search = CommonWebsite.PrepareSearchProductAttributeModel(model, command);
                    var tempList = _proService.MD_PRODUCT_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        result.Product = tempList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = result.Product[0].TotalCount;
                        pagedList.PageIndex = result.Product[0].PageIndex - 1;
                        pagedList.PageSize = result.Product[0].PageSize;
                        result.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CatalogController FilterAttributeProductSearch error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView("SearchProductAttribute", result);
        }

        #region "Hãng sản xuất (Manufacturer)"
        public virtual ActionResult Manufacturer(long ManufacturerId, string[] p, string[] s, ProductPagingFilteringModel command)
        {
            var result = new ProductMapViewModel();
            ViewData["ManufacturerId"] = ManufacturerId;
            ViewData["categoryid"] = 0;
            ViewData["p"] = p;
            ViewData["s"] = s;
            try
            {
                if (command == null)
                    return InvokeHttp404();
                var search = new ProductParam();
                var model = new SearchAttributesViewModel();
               // model.CateoryId
                model.ManufacturerId = ManufacturerId;
                model.searchPrice = new List<SearchPriceModel>();
                if (p != null)
                {
                    foreach (string i in p)
                    {
                        SearchPriceModel searPrice = new SearchPriceModel();
                        string[] temp = i.Split(new char[] { '-' });
                        if (temp.Count() >= 4)
                        {
                            searPrice.PriceFrom = float.Parse(temp[1]);
                            searPrice.PriceTo = float.Parse(temp[2]);
                        }
                        else
                        {
                            if (temp[0].ToString().ToUpper() == "duoi".ToUpper())
                            {
                                searPrice.PriceFrom = 0;
                                searPrice.PriceTo = float.Parse(temp[1]);
                            }
                            else
                            {
                                searPrice.PriceFrom = float.Parse(temp[1]);
                                searPrice.PriceTo = 0;
                            }
                        }
                        model.searchPrice.Add(searPrice);
                    }
                }
                if (s != null)
                {
                    foreach (string j in s)
                    {
                        SearchParModel searPar = new SearchParModel();
                        string[] temp = j.Split(new char[] { '-' });
                        searPar.Id = int.Parse(temp[1]);
                        searPar.Name = temp[0];
                        model.searchPar.Add(searPar);
                    }
                }
                using (_proService = new ProductServiceClient())
                {
                    //seosetting
                    var param = new SeoSettingParam();
                    param.TableName = "MD_MANUFACTURER";
                    param.ParentID = ManufacturerId.ToString();
                    dungChungService = new DungChungServiceClient();
                    var seoSetting = dungChungService.CBO_GetSeoCommon_Web(param);
                    if (seoSetting != null && seoSetting.Data.resultObject != null)
                    {
                        result.SeoSetting = seoSetting.Data.resultObject;
                        result.SeoSetting.GroupID = ManufacturerId;
                    }
                    search = CommonWebsite.PrepareSearchProductAttributeModel(model, command);
                    var tempList = _proService.MD_PRODUCT_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        result.Product = tempList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = result.Product[0].TotalCount;
                        pagedList.PageIndex = result.Product[0].PageIndex - 1;
                        pagedList.PageSize = result.Product[0].PageSize;
                        result.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                    else
                    {
                        return InvokeHttp404();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CatalogController Category error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(result);
        }
        #endregion
    }
}
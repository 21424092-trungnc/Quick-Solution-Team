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

namespace CMS_Solution.WebRuby.Controllers
{
    public partial class ProductController : BasePublicController
    {

        #region dungchung
        private ProductServiceClient _proService;
        private DungChungServiceClient dungChungService;
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(BaseController));
        #endregion
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        //[ChildActionOnly]
        [HttpGet]
        public virtual ActionResult HomePageProductHot()
        {
            var result = new List<ProductMap>();
            try
            {
                var search = new ProductParam();
                search.IsHighLigh = 1;
                search.PageIndex = 1;
                search.PageSize = 8;
                using (_proService = new ProductServiceClient())
                {
                    var tempList = _proService.MD_PRODUCT_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        result = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ProductController HomePageProductHot error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(result);
        }
        //[ChildActionOnly]
        [HttpGet]
        public virtual ActionResult HomePageProductSelling()
        {
            var result = new List<ProductMap>();
            try
            {
                var search = new ProductParam();
                //search.IsSellWell = 1;
                search.PageIndex = 1;
                search.PageSize = 8;
                search.IsPromotion = 1;
                using (_proService = new ProductServiceClient())
                {
                    var tempList = _proService.MD_PRODUCT_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        result = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ProductController HomePageProductSelling error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(result);
        }
        [ChildActionOnly]
        public virtual ActionResult LeftMenuProductAttribute(long categoryid,string[] s)
        {
            ViewData["s"] = s;
            var result = new List<ProductAttributeMap>();
            try
            {
                using (_proService = new ProductServiceClient())
                {
                    var tempList = _proService.PRO_PRODUCTATTRIBUTE_GetList_Web(categoryid);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        result = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ProductController LeftMenuProductAttribute error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(result);
        }
        public virtual ActionResult ProductDetails(long productId)
        {
            var result = new ProductDetailAllViewModel();
            try
            {
                using (_proService = new ProductServiceClient())
                {
                    //seosetting
                    var param = new SeoSettingParam();
                    param.TableName = "MD_SEOPRODUCT";
                    param.ParentID = productId.ToString();
                    dungChungService = new DungChungServiceClient();
                    var seoSetting = dungChungService.CBO_GetSeoCommon_Web(param);
                    if (seoSetting != null && seoSetting.Data.resultObject != null)
                    {
                        result.SeoSetting = seoSetting.Data.resultObject;
                    }
                    var tempList = _proService.MD_PRODUCT_GetById_Web(productId);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        result.ProductDetail = tempList.Data.resultObject;
                    }
                    else
                    {
                        return InvokeHttp404();
                    }
                    var tempListRelate = _proService.MD_PRODUCT_GetRelationship_Web(productId);
                    if (tempListRelate.Data != null && tempListRelate.Data.resultObject != null)
                    {
                        result.ProductRelate = tempListRelate.Data.resultObject;
                    }
                    else
                    {
                        return InvokeHttp404();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ProductController ProductDetails error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            result.ProductCommentsAdd.ProductId = Convert.ToInt32(productId);
            result.CardAdd.ProductId = productId;
            result.CardAdd.Quantity = 1;
            return View(result);
        }
       
        [HttpPost]
        public ActionResult SendComment(ProductCommentsAdd mapAdd)
        {
            bool status = false;
            var result = new ProductDetailAll();
            try
            {
                using (_proService = new ProductServiceClient())
                {
                    //mapAdd.CommentReplyId = (mapAdd.CommentReplyId != null || mapAdd.CommentReplyId != 0) ? mapAdd.CommentReplyId: 0;
                    var tempList = _proService.PRO_COMMENT_CreateOrUpdate_Web(mapAdd);
                    if (tempList.Data != null && tempList.Data.resultObject)
                    {
                        status = tempList.Data.resultObject;
                    }
                }
                if (status)
                {
                    using (_proService = new ProductServiceClient())
                    {
                        var tempList = _proService.MD_PRODUCT_GetById_Web(mapAdd.ProductId);
                        if (tempList.Data != null && tempList.Data.resultObject != null)
                        {
                            result = tempList.Data.resultObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SendComment error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView("ProductCommentListPartial",result);
        }
        [ChildActionOnly]
        public virtual ActionResult RelatedProducts(long productId)
        {
            var result = new List<ProductMap>();
            try
            {
                using (_proService = new ProductServiceClient())
                {

                    var tempList = _proService.MD_PRODUCT_GetRelationship_Web(productId);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        result = tempList.Data.resultObject;
                    }
                    else
                    {
                        return InvokeHttp404();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ProductController RelatedProducts error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(result);
        }
        public virtual ActionResult ProductSearch(string tukhoa)
        {
            var productSearch = new ProductSearchViewModel();
            try
            {
                var search = new ProductParam();
                search.PageIndex = Request.QueryString["pagenumber"] !=null ? Convert.ToInt32(Request.QueryString["pagenumber"].ToString()) :  1;
                search.PageSize = 8;
                search.Keyword = tukhoa;

                productSearch.Search.Keyword = tukhoa;
                using (_proService = new ProductServiceClient())
                {
                    var tempList = _proService.MD_PRODUCT_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        productSearch.Items = tempList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = productSearch.Items[0].TotalCount;
                        pagedList.PageIndex = productSearch.Items[0].PageIndex - 1;
                        pagedList.PageSize = productSearch.Items[0].PageSize;
                        productSearch.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ProductController ProductSearch error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(productSearch);
        }

    }
}
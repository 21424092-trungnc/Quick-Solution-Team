using Business.Entities.Domain;
using CMS_Solution.WebRuby.ViewModel;
using Core.Common.UI.Paging;
using Core.Common.Utilities;
using log4net;
using Module.Framework;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CMS_Solution.WebRuby.Controllers
{
    public class CartController : Controller
    {
        #region dungchung
        private CartServiceClient _cartService;
        private ProductServiceClient _proService;
        private DungChungServiceClient _dungChungService;
        private AccountServiceClient _accountService;
        protected UserManager _userInfo;
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(CartController));
        #endregion
        // GET: GymSetup
        public ActionResult Index()
        {
            var Carts = new OrderCartViewModel();
            try
            {
                var search = new CartParam();
                search.PageIndex = 1;
                search.PageSize = 10;
                search.CookieId = (Request.Cookies["orderCart"] != null && Request.Cookies["orderCart"].Value != null && !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value)) ? Request.Cookies["orderCart"].Value : string.Empty;
                search.MemberId = 0;
                using (_cartService = new CartServiceClient())
                {
                    var tempList = _cartService.SL_CART_GetList(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        Carts.Items = tempList.Data.resultObject;
                        var pagedList = new PagedList();
                        pagedList.TotalCount = Carts.Items[0].TotalCount;
                        pagedList.PageIndex = Carts.Items[0].PageIndex - 1;
                        pagedList.PageSize = Carts.Items[0].PageSize;
                        Carts.PagingFilteringContext.LoadPagedList(pagedList);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Cart error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(Carts);
        }

        [HttpPost]
        public ActionResult AddItemToCart(CartAdd mapAdd)
        {
            List<CartMap> cardOrder = new List<CartMap>();
            bool status = false;
            try
            {
                _userInfo = Session[AppSetting.SessionUser] != null ? (UserManager)Session[AppSetting.SessionUser] : new UserManager();
                ProductDetailAll productItem = new ProductDetailAll();
                using (_proService = new ProductServiceClient())
                {
                    var tempList = _proService.MD_PRODUCT_GetById_Web(mapAdd.ProductId);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        productItem = tempList.Data.resultObject;
                    }
                }
                using (_cartService = new CartServiceClient())
                {
                    mapAdd.Price = productItem.Product.PriceNewMN;
                    mapAdd.TotalPriceItem = productItem.Product.PriceNewMN * mapAdd.Quantity;
                    mapAdd.CreatedUser = AppSetting.UserWebID ?? "";
                    mapAdd.MemberId = (_userInfo != null ? _userInfo.MemberID : 0);
                    mapAdd.CookieId = (Request.Cookies["orderCart"] != null && !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value)) ? Request.Cookies["orderCart"].Value : (Guid.NewGuid().ToString());//+ DateTime.Now.ToString("yyyyMMddhhmmsstt")
                    var tempList = _cartService.SL_CART_Create_Web(mapAdd);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        status = true;
                        var search = new CartParam();
                        search.PageIndex = 1;
                        search.PageSize = int.MaxValue;
                        search.CookieId = mapAdd.CookieId;
                        search.MemberId = mapAdd.MemberId;
                        var temp = _cartService.SL_CART_GetList(search);
                        cardOrder = temp.StatusCode == System.Net.HttpStatusCode.OK ? temp.Data.resultObject : new List<CartMap>();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("AddItemToCard error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                status = false;
            }

            return Json(new { data = cardOrder, status = status });
        }
        [HttpPost]
        public ActionResult AddItemToCartPromotion()
        {
            List<CartMap> cardOrder = new List<CartMap>();
            bool status = false;
            try
            {
                _userInfo = Session[AppSetting.SessionUser] != null ? (UserManager)Session[AppSetting.SessionUser] : new UserManager();
                ProductDetailAll productItem = new ProductDetailAll();
                var _mapAdd = Request.Form["dataModel"];
                var _CartPromotion = Request.Form["CartPromotion"];
                var mapAdd = new CartAdd();
                var CartPromotion = new List<CartPromotion>();
                mapAdd = JsonConvert.DeserializeObject<CartAdd>(_mapAdd);
                if (!string.IsNullOrEmpty(_CartPromotion))
                {
                    CartPromotion = JsonConvert.DeserializeObject<List<CartPromotion>>(_CartPromotion);
                    mapAdd.CartPromotion = CartPromotion;
                    if(CartPromotion.Count() > 0)
                    {
                        mapAdd.PromotionId = CartPromotion[0].PromotionId;
                        mapAdd.PromotionOfferId = CartPromotion[0].PromotionOfferId;
                    }
                }
                   
                using (_proService = new ProductServiceClient())
                { 
                    var tempList = _proService.MD_PRODUCT_GetById_Web(mapAdd.ProductId);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        productItem = tempList.Data.resultObject;
                    }
                }
                using (_cartService = new CartServiceClient())
                {
                    mapAdd.Price = productItem.Product.PriceNewMN;
                    mapAdd.TotalPriceItem = productItem.Product.PriceNewMN * mapAdd.Quantity;
                    mapAdd.CreatedUser = AppSetting.UserWebID ?? "";
                    mapAdd.MemberId = (_userInfo != null ? _userInfo.MemberID : 0);
                    mapAdd.CookieId = (Request.Cookies["orderCart"] != null && !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value)) ? Request.Cookies["orderCart"].Value : (Guid.NewGuid().ToString());//+ DateTime.Now.ToString("yyyyMMddhhmmsstt")
                    var tempList = _cartService.SL_CART_Create_Web(mapAdd);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        status = true;
                        var search = new CartParam();
                        search.PageIndex = 1;
                        search.PageSize = int.MaxValue;
                        search.CookieId = mapAdd.CookieId;
                        search.MemberId = mapAdd.MemberId;
                        var temp = _cartService.SL_CART_GetList(search);
                        cardOrder = temp.StatusCode == System.Net.HttpStatusCode.OK ? temp.Data.resultObject : new List<CartMap>();
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error("AddItemToCard error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                status = false;
            }

            return Json(new { data = cardOrder, status = status });
        }
        [HttpPost]
        public ActionResult AddItemToCartByProductID(long productId, string flag = "",int val = 1)
        {
            List<CartMap> cardOrder = new List<CartMap>();
            bool status = false;
            CartAdd mapAdd = new CartAdd();
            string cookieid = "";
            try
            {
                _userInfo = Session[AppSetting.SessionUser] != null ? (UserManager)Session[AppSetting.SessionUser] : new UserManager();
                ProductDetailAll productItem = new ProductDetailAll();
                using (_proService = new ProductServiceClient())
                {
                    var tempList = _proService.MD_PRODUCT_GetById_Web(productId);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        productItem = tempList.Data.resultObject;
                    }
                }
                mapAdd = new CartAdd()
                {
                    CookieId = (Request.Cookies["orderCart"] != null && !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value)) ? Request.Cookies["orderCart"].Value : (Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMddhhmmsstt")),
                    CreatedUser = AppSetting.UserWebID ?? "",
                    MemberId = (_userInfo != null ? _userInfo.MemberID : 0),
                    Price = productItem.Product.PriceNewMN,
                    ProductId = productId,
                    Quantity = (flag == "T" ? 1 : (flag == "G" ? -1 : (flag == "C" ? val : 1))),
                    TotalPriceItem = productItem.Product.PriceNewMN
                };
                using (_cartService = new CartServiceClient())
                {
                    var tempList = _cartService.SL_CART_Create_Web(mapAdd);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        status = true;
                        var search = new CartParam();
                        search.PageIndex = 1;
                        search.PageSize = int.MaxValue;
                        search.CookieId = mapAdd.CookieId;
                        search.MemberId = mapAdd.MemberId;
                        var temp = _cartService.SL_CART_GetList(search);
                        cardOrder = temp.StatusCode == System.Net.HttpStatusCode.OK ? temp.Data.resultObject : new List<CartMap>();
                        cookieid = cardOrder.Count > 0 ? cardOrder[0].CookieId : search.CookieId;
                    }
                    else
                    {
                        cookieid = mapAdd.CookieId;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("AddItemToCartByProductID error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                status = false;
            }
            return Json(new { data = cardOrder, status = status , cookieid = cookieid });
        }

        [HttpPost]
        public ActionResult DeleteItemToCartByProductID(long productId, long cartId)
        {
            List<CartMap> cardOrder = new List<CartMap>();
            bool status = false;
            CartAdd mapAdd = new CartAdd();
            string cookieid = "";
            try
            {
                _userInfo = Session[AppSetting.SessionUser] != null ? (UserManager)Session[AppSetting.SessionUser] : new UserManager();
                using (_cartService = new CartServiceClient())
                {
                    var tempList = _cartService.SL_CART_Delete_Web(cartId);
                    if (tempList.Data != null && tempList.Data.resultObject)
                    {
                        status = true; 
                        var search = new CartParam();
                        search.PageIndex = 1;
                        search.PageSize = int.MaxValue;
                        search.CookieId = (Request.Cookies["orderCart"] != null && !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value)) ? Request.Cookies["orderCart"].Value : (Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMddhhmmsstt"));
                        search.MemberId = _userInfo.MemberID;
                        var temp = _cartService.SL_CART_GetList(search);
                        cardOrder = temp.StatusCode == System.Net.HttpStatusCode.OK ? temp.Data.resultObject : new List<CartMap>();
                        cookieid = cardOrder.Count > 0 ? cardOrder[0].CookieId : search.CookieId;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DeleteItemToCartByProductID error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                status = false;
            }
            return Json(new { data = cardOrder, status = status, cookieid= cookieid });
        }
        #region "Thanh toán"
        public ActionResult CheckOutCart()
        {
            if (Session[AppSetting.SessionUser] != null)
            {
                return RedirectToAction("FormCheckOut");
            }
            return View();
        }

        public ActionResult FormCheckOut()
        {
            CheckOutViewModel viewModel = new CheckOutViewModel();
            try
            {
                UserManager user = new UserManager();
                if (Session[AppSetting.SessionUser] != null)
                {
                    user = (UserManager)Session[AppSetting.SessionUser];
                }
                //Get member detail
                using (_accountService = new AccountServiceClient())
                {
                    var MemberDetai = _accountService.Account_GetInfoByID(user.MemberID.ToString());
                    if (MemberDetai.Data != null && MemberDetai.Data.resultObject != null)
                    {
                        viewModel.AccountDetail = MemberDetai.Data.resultObject;
                    }
                    else
                    {
                        viewModel.AccountDetail = new UserManager();
                    }
                }
                //Get list product order              
                var search = new CartParam();
                search.PageIndex = 1;
                search.PageSize = 10;
                search.CookieId = !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value) ? Request.Cookies["orderCart"].Value : string.Empty;
                search.MemberId = user.MemberID;
                using (_cartService = new CartServiceClient())
                {
                    var tempList = _cartService.SL_CART_GetList(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        viewModel.ListProdctCart = tempList.Data.resultObject;
                        var offerList = _cartService.SL_BOOKING_GetOffers_Web(user.MemberID, search.CookieId);
                        if (offerList.Data != null && offerList.Data.resultObject != null && offerList.Data.resultObject.Count > 0)
                        {
                            viewModel.Offers = offerList.Data.resultObject;
                            var promoProduct = _cartService.SM_PROMOTIONAPPLY_PRODUCT_GetByPromtionId_Web(viewModel.Offers[0].PromotionId);
                            if (promoProduct.Data != null && promoProduct.Data.resultObject != null && promoProduct.Data.resultObject.Count > 0)
                            {
                                var listPromoProc = promoProduct.Data.resultObject;
                                if (viewModel.ListProdctCart.Count == listPromoProc.Count)
                                {
                                    long _countMoney = 0;
                                    long _countQuantity = 0;
                                    //Kiểm tra danh sách proc cart có đủ trong promotion proc ko
                                    foreach(var proc in viewModel.ListProdctCart)
                                    {
                                        _countMoney += proc.TotalCount;
                                        _countQuantity += proc.Quantity;
                                        var chkproc = listPromoProc.Where(x => x.ProductId == proc.ProductId).ToList();
                                        if(chkproc == null || chkproc.Count == 0)
                                        {
                                            viewModel.IsOffers = false;
                                           // break;
                                        }
                                    }
                                    if(viewModel.IsOffers != false)
                                    {
                                        if(viewModel.Offers[0].IsPromotionByToTalQuantity)
                                        {
                                            viewModel.IsOffersQuantity = (viewModel.Offers[0].MinPromotionTotalQuantity <= _countQuantity) ? true : false;
                                        }
                                        if (viewModel.Offers[0].IsPromotionByTotalMoney)
                                        {
                                            viewModel.IsOffersMoney = (viewModel.Offers[0].MinPromotionTotalMoney <= _countMoney) ? true : false;
                                        }

                                        if(viewModel.IsOffersMoney || viewModel.IsOffersQuantity)
                                        {
                                            viewModel.IsOffers = true;
                                        }
                                    }
                                }
                                else
                                    viewModel.IsOffers = false;
                            }
                        }
                    }
                }

                //Get thông tin address
                using (_dungChungService = new DungChungServiceClient())
                {
                    string CountryID = (AppSetting.CountryID != "") ? AppSetting.CountryID : "0";
                    var district = _dungChungService.MD_PROVINCE_GetList(AppSetting.CountryID);
                    if (district.Data != null && district.Data.resultObject != null && district.Data.resultObject.Count > 0)
                    {
                        viewModel.ListProvince = district.Data.resultObject;
                    }

                    string ProvinceID = (viewModel.AccountDetail != null && viewModel.AccountDetail.ProvinceID.ToString() != "") ? viewModel.AccountDetail.ProvinceID.ToString() : "0";
                    if (ProvinceID != "0")
                    {
                        var listWard = _dungChungService.MD_DISTRICT_GetList(ProvinceID);
                        if (listWard.Data != null && listWard.Data.resultObject != null && listWard.Data.resultObject.Count > 0)
                        {
                            viewModel.ListDistrict = listWard.Data.resultObject;
                        }
                    }
                    else
                    {
                        viewModel.ListWard = new List<WardMap>();
                    }

                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.Error("AddItemToCard error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult SaveInfoCheckOut(BookingMap model)
        {
            bool status = true;
            string type = "";
            string cookieid = "";
            try
            {
                UserManager user = new UserManager();
                if (Session[AppSetting.SessionUser] != null)//member exist
                {
                    user = (UserManager)Session[AppSetting.SessionUser];

                }
                using (_accountService = new AccountServiceClient())
                {
                    var _checkMember = _accountService.CRM_ACCOUNT_CheckUserName_Web(model.PhoneNumber);
                    if (_checkMember.Data != null && _checkMember.Data.resultObject != null && _checkMember.Data.resultObject.MEMBERID == 0) // member accept register
                    {
                        RegisterViewModel modelUser = new RegisterViewModel();
                        modelUser.FullName = model.FullName;
                        modelUser.PhoneNumber = model.PhoneNumber;
                        modelUser.Email = model.Email;
                        modelUser.Password = "P@ssword";
                        modelUser.RePassword = "P@ssword";
                        modelUser.CountryID = model.CountryID;
                        modelUser.ProvinceID = model.ProvinceID;
                        modelUser.DistrictID = model.DistrictID;
                        modelUser.WardID = model.WardID;
                        modelUser.Address = model.Address;
                        var _resultUser = CreateRegister(modelUser);
                        if (_resultUser <= 0)
                        {
                            status = false;
                            if (_resultUser == -1)
                                type = "email";
                        }
                        else
                        {
                            model.MemberID = _resultUser;
                        }
                    }
                    else
                    {
                        model.MemberID = _checkMember.Data.resultObject.MEMBERID;
                    }
                    if (model.MemberID > 0)
                    {
                        using (_cartService = new CartServiceClient())
                        {
                            var _resultBooking = _cartService.SL_BOOKING_CreateOrUpdate_Web(model);
                            if (_resultBooking.Data != null && _resultBooking.Data.resultObject > 0) // member accept register
                            {
                                model.BookingID = _resultBooking.Data.resultObject;

                                BookingDetailMap modelDetail = new BookingDetailMap();
                                modelDetail.CookieID = !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value) ? Request.Cookies["orderCart"].Value : string.Empty; ;
                                modelDetail.BookingID = model.BookingID;
                                modelDetail.UserID = (user != null) ? user.MemberID.ToString() : AppSetting.UserWebID;
                                modelDetail.MemberID = (user != null) ? user.MemberID : 0;
                                var _resultBookingDetail = _cartService.SL_BOOKINGDETAIL_Insert_Web(modelDetail);
                                if (_resultBookingDetail.Data != null && _resultBookingDetail.Data.resultObject == true)
                                {
                                    status = true;
                                    cookieid = modelDetail.CookieID;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            else
                            {
                                status = false;
                            }
                        }
                    }
                }
                return Json(new { data = model.BookingID, status = status, type = type , cookieid = cookieid });
            }
            catch (Exception ex)
            {
                _logger.Error("SaveInfoCheckOut error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                return null;
            }
        }

        public long CreateRegister(RegisterViewModel model)
        {
            try
            {
                if (model != null)
                {
                    string _DataLeadID = "";
                    using (_accountService = new AccountServiceClient())
                    {
                        var data = _accountService.CustomerDataLeads_GetByPhone(model.PhoneNumber);
                        if (data != null)
                        {
                            if (data.Data.resultObject != null && !string.IsNullOrEmpty(data.Data.resultObject.DATALEADSID))
                            {
                                _DataLeadID = data.Data.resultObject.DATALEADSID;
                            }
                            else
                            {
                                //Insert CustomerDataLead
                                CustomerDataLeadsParam dataLeadModel = new CustomerDataLeadsParam();
                                dataLeadModel.FullName = model.FullName;
                                dataLeadModel.PhoneNumber = model.PhoneNumber;
                                dataLeadModel.Email = model.Email;
                                dataLeadModel.BusinessID = 0;
                                dataLeadModel.CreatedUser = AppSetting.UserWebID;
                                var _resultDataLead = _accountService.CustomerDataLeads_InsOrUpd(dataLeadModel);
                                if (_resultDataLead != null && _resultDataLead.Data.resultObject != "")
                                {
                                    _DataLeadID = _resultDataLead.Data.resultObject;
                                }
                                else return -2;
                            }
                            if (_DataLeadID.Length > 0)
                            {
                                //Insert Account
                                AccountParam dataAccountModel = new AccountParam();
                                dataAccountModel.FullName = model.FullName;
                                dataAccountModel.PhoneNumber = model.PhoneNumber;
                                dataAccountModel.Email = model.Email;
                                dataAccountModel.Passwrord = Encrypt_Decrypt.hashPassword(model.Password);

                                dataAccountModel.DataLeadsID = _DataLeadID;
                                dataAccountModel.CreateUser = AppSetting.UserWebID;
                                dataAccountModel.IsActive = 1;
                                dataAccountModel.IsConfirm = 1;
                                dataAccountModel.CountryID = model.CountryID;
                                dataAccountModel.ProvinceID = model.ProvinceID;
                                dataAccountModel.DistrictID = model.DistrictID;
                                dataAccountModel.WardID = model.WardID;
                                dataAccountModel.Address = model.Address;
                                var _resultAccount = _accountService.Account_InsOrUpd(dataAccountModel);
                                if (_resultAccount != null && _resultAccount.Data.resultObject > 0)
                                {

                                    return _resultAccount.Data.resultObject;
                                }
                                else
                                {
                                    return -2;
                                }
                                //End: Insert Account
                            }
                        }
                    }

                }
                return -3;
            }
            catch (Exception ex)
            {
                _logger.Error("CreateRegister error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                return -4;
            }
        }


        [HttpPost]
        public ActionResult SaveInfoCheckOutPromotion()//BookingMap model
        {
            bool status = true;
            string type = "";
            string cookieid = "";
            try
            {
                var _model = Request.Form["dataModel"];
                var _CartPromotion = Request.Form["CartPromotion"];
                var model = new BookingMap();
                var CartPromotion = new List<CartPromotion>();
                model = JsonConvert.DeserializeObject<BookingMap>(_model);
                if (!string.IsNullOrEmpty(_CartPromotion))
                {
                    CartPromotion = JsonConvert.DeserializeObject<List<CartPromotion>>(_CartPromotion);
                    model.CartPromotion = CartPromotion;
                }

                UserManager user = new UserManager();
                if (Session[AppSetting.SessionUser] != null)//member exist
                {
                    user = (UserManager)Session[AppSetting.SessionUser];

                }
                using (_accountService = new AccountServiceClient())
                {
                    var _checkMember = _accountService.CRM_ACCOUNT_CheckUserName_Web(model.PhoneNumber);
                    if (_checkMember.Data != null && _checkMember.Data.resultObject != null && _checkMember.Data.resultObject.MEMBERID == 0) // member accept register
                    {
                        RegisterViewModel modelUser = new RegisterViewModel();
                        modelUser.FullName = model.FullName;
                        modelUser.PhoneNumber = model.PhoneNumber;
                        modelUser.Email = model.Email;
                        modelUser.Password = "P@ssword";
                        modelUser.RePassword = "P@ssword";
                        modelUser.CountryID = model.CountryID;
                        modelUser.ProvinceID = model.ProvinceID;
                        modelUser.DistrictID = model.DistrictID;
                        modelUser.WardID = model.WardID;
                        modelUser.Address = model.Address;
                        var _resultUser = CreateRegister(modelUser);
                        if (_resultUser <= 0)
                        {
                            status = false;
                            if (_resultUser == -1)
                                type = "email";
                        }
                        else
                        {
                            model.MemberID = _resultUser;
                        }
                    }
                    else
                    {
                        model.MemberID = _checkMember.Data.resultObject.MEMBERID;
                    }
                    if (model.MemberID > 0)
                    {
                        using (_cartService = new CartServiceClient())
                        {
                            var _resultBooking = _cartService.SL_BOOKING_CreateOrUpdate_Web(model);
                            if (_resultBooking.Data != null && _resultBooking.Data.resultObject > 0) // member accept register
                            {
                                model.BookingID = _resultBooking.Data.resultObject;

                                BookingDetailMap modelDetail = new BookingDetailMap();
                                modelDetail.CookieID = !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value) ? Request.Cookies["orderCart"].Value : string.Empty; ;
                                modelDetail.BookingID = model.BookingID;
                                modelDetail.UserID = (user != null) ? user.MemberID.ToString() : AppSetting.UserWebID;
                                modelDetail.MemberID = (user != null) ? user.MemberID : 0;
                                var _resultBookingDetail = _cartService.SL_BOOKINGDETAIL_Insert_Web(modelDetail);
                                if (_resultBookingDetail.Data != null && _resultBookingDetail.Data.resultObject == true)
                                {
                                    status = true;
                                    cookieid = modelDetail.CookieID;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            else
                            {
                                status = false;
                            }
                        }
                    }
                }
                return Json(new { data = model.BookingID, status = status, type = type, cookieid = cookieid });
            }
            catch (Exception ex)
            {
                _logger.Error("SaveInfoCheckOut error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                return null;
            }
        }

        #endregion
    }
}
using Business.Entities.Domain;
using CMS_Solution.WebRuby.Common;
using CMS_Solution.WebRuby.ViewModel;
using Core.Common.Utilities;
using log4net;
using Module.Framework;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace CMS_Solution.WebRuby.Controllers
{
    public partial class CustomerController : BasePublicController
    {
        #region dungchung
        private AccountServiceClient _accountService;
        private CartServiceClient _cartService;
        private DungChungServiceClient dungChungService;
        private int _pageSize;
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(BaseController));
        private UserManager userInfo;
        #endregion
        // GET: Customer
        public virtual ActionResult Index()
        {
            return View();
        }

        #region "Tạo tài khoản"
        public virtual ActionResult Register()
        {
            var model = new RegisterViewModel
            {
                TypeRegister = "Website"
            };
            if (TempData["sso"]  != null && !string.IsNullOrEmpty(TempData["sso"].ToString()))
            {
                var str = CommonWebsite.Base64Decode(TempData["sso"].ToString());
                model= JsonConvert.DeserializeObject<RegisterViewModel>(str);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRegister(RegisterViewModel model)
        {
            bool status = false;
            string type = "";
            try
            {
                if (model != null)
                {
                    string _DataLeadID = "";
                    using (_accountService = new AccountServiceClient())
                    {

                        var _checkMember = _accountService.CRM_ACCOUNT_CheckUserName_Web(model.PhoneNumber);
                        if (_checkMember.Data != null && _checkMember.Data.resultObject != null) // member accept register
                        {
                            if (_checkMember.Data.resultObject.MEMBERID > 0)
                            {
                                status = false;
                                type = "phone";
                            }
                            else
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
                                        CustomerDataLeadsParam dataLeadModel = CustomerDataLeadsParamMaps(model);
                                        dataLeadModel.CreatedUser = AppSetting.UserWebID;
                                        var _resultDataLead = _accountService.CustomerDataLeads_InsOrUpd(dataLeadModel);
                                        if (_resultDataLead != null && _resultDataLead.Data.resultObject != "")
                                        {
                                            _DataLeadID = _resultDataLead.Data.resultObject;
                                        }
                                    }
                                    if (_DataLeadID.Length > 0)
                                    {
                                        //Insert Account
                                        AccountParam dataAccountModel = AccountParamMaps(model);
                                        dataAccountModel.DataLeadsID = _DataLeadID;
                                        dataAccountModel.CreateUser = AppSetting.UserWebID;
                                        dataAccountModel.IsActive = 0;
                                        dataAccountModel.IsConfirm = 0;
                                        dataAccountModel.CountryID = 0;
                                        dataAccountModel.ProvinceID = 0;
                                        dataAccountModel.DistrictID = 0;
                                        dataAccountModel.WardID = 0;
                                        dataAccountModel.Address = "";
                                        var _resultAccount = _accountService.Account_InsOrUpd(dataAccountModel);
                                        if (_resultAccount != null && _resultAccount.Data.resultObject > 0)
                                        {
                                            status = true;
                                            SendMailActive(model.Email, _DataLeadID,model.FullName,model.Gender);
                                            //return Redirect("/dang-nhap");
                                        }
                                        else if (_resultAccount != null && _resultAccount.Data.resultObject == -2)
                                        {
                                            status = false;
                                        }
                                        //End: Insert Account
                                    }
                                }
                            }
                        }
                    }
                    
                }
                return Json(new { status = status, checkTen = false, type=type });
            }
            catch (Exception ex)
            {
                return Json(new { status = false });
            }
        }

        public CustomerDataLeadsParam CustomerDataLeadsParamMaps(RegisterViewModel model)
        {
            CustomerDataLeadsParam result = new CustomerDataLeadsParam();
            result.FullName = model.FullName;
            result.PhoneNumber = model.PhoneNumber;
            result.Email = model.Email;
            result.BusinessID = 0;
            result.GenDer = model.Gender;
            return result;
        }

        public AccountParam AccountParamMaps(RegisterViewModel model)
        {
            AccountParam result = new AccountParam();
            result.FullName = model.FullName;
            result.PhoneNumber = model.PhoneNumber;
            result.Email = model.Email;
            result.Passwrord = Encrypt_Decrypt.hashPassword(model.Password);
            result.Gender = model.Gender;
            result.TypeRegister = model.TypeRegister;
            result.GoogleID = model.GoogleID;
            result.FacebookID = model.FacebookID;
            return result;
        }
        #endregion

        #region "Login"
        public ActionResult Login()
        {
            if(Session[AppSetting.SessionUser] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]

        public ActionResult LoginAccount()
        {
            var model = new LoginDataModel();
            string _key = (Request.Form["key"] != null && !string.IsNullOrEmpty(Request.Form["key"].ToString())) ? Request.Form["key"].ToString() : "";
            try
            {
                string phoneLogin = (Request.Form["UserLogin"] != null && !string.IsNullOrEmpty(Request.Form["UserLogin"].ToString())) ? Request.Form["UserLogin"].ToString() : "";
                string passwordLogin = (Request.Form["PassWord"] != null && !string.IsNullOrEmpty(Request.Form["PassWord"].ToString())) ? Request.Form["PassWord"].ToString() : "";
                string _RequestVerificationToken = (Request.Form["__RequestVerificationToken"] != null && !string.IsNullOrEmpty(Request.Form["__RequestVerificationToken"].ToString())) ? Request.Form["__RequestVerificationToken"].ToString() : "";
               
                model = new LoginDataModel()
                {
                    PhoneNumber = phoneLogin,
                    Password = passwordLogin,
                    RequestVerificationToken = _RequestVerificationToken
                };
                if (!string.IsNullOrEmpty(model.RequestVerificationToken))
                {
                    var result = LoginFunction(model);
                    TempData["infoLogin"] = result.message;
                    TempData["statusLogin"] = result.status;
                    if (result.result)
                    {
                        using (_accountService = new AccountServiceClient())
                        {
                            var resultGetUser = _accountService.Account_GetInfoByPhone(model.PhoneNumber);
                            if (resultGetUser.Data != null && resultGetUser.Data.resultObject != null)
                            {
                                var _object = resultGetUser.Data.resultObject;
                                userInfo = new UserManager()
                                {
                                    MemberID = _object.MemberID, 
                                    FullName = _object.FullName,
                                    Email = _object.Email,
                                    ImageAvatar = _object.ImageAvatar
                                };
                                Session[AppSetting.SessionUser] = userInfo;
                                Session.Timeout = 60;
                                using (_cartService = new CartServiceClient())
                                {
                                    var search = new CartParam();
                                    search.PageIndex = 1;
                                    search.PageSize = 10;
                                    search.CookieId = !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value) ? Request.Cookies["orderCart"].Value : string.Empty;
                                    search.MemberId = userInfo.MemberID;
                                    var tempList = _cartService.SL_CART_GetList(search);
                                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                                    {
                                        var count = 0;
                                        foreach(var item in tempList.Data.resultObject)
                                        {
                                            count += item.Quantity;
                                        }
                                        TempData["updateCart"] = count;
                                        TempData["cookieCart"] = search.CookieId;
                                    }
                                }
                                if (Session[AppSetting.SessionBaseUrl] != null && !string.IsNullOrEmpty(Session[AppSetting.SessionBaseUrl].ToString()))
                                {
                                    string UrlRedirect = Session[AppSetting.SessionBaseUrl].ToString();
                                    Session[AppSetting.SessionBaseUrl] = null;
                                    return Redirect(UrlRedirect);
                                }
                                else
                                {
                                   
                                    return (_key == "CHECKOUT") ? RedirectToAction("FormCheckOut", "Cart") : RedirectToAction("Index", "Home");
                                }
                                //return Redirect("AccountCongDan/Login");
                            }
                        }
                    }
                }
                else
                {
                    TempData["infoLogin"] = "Đăng nhập thất bại";
                    TempData["statusLogin"] = 0;
                }
                Session[AppSetting.SessionUser] = null;

                //TempData["resultNonLogin"] = resultNonLogin;
                return (_key == "CHECKOUT") ? Redirect("/thanh-toan") : Redirect("/dang-nhap");
            }
            catch(Exception ex)
            {
                return (_key == "CHECKOUT") ? Redirect("/thanh-toan") : Redirect("/dang-nhap");
            }
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]

        public ActionResult LoginAccountSSO(UserSSO user)
        {
           
            try
            {
                using (_accountService = new AccountServiceClient())
                {
                    var param = new UserSSOParam
                    {
                        FacebookID = user.FacebookID,
                        GoogleID = user.GoogleID
                    };
                    var resultGetUser = _accountService.Account_GetInfoBySSO(param);
                    if (resultGetUser.Data != null && resultGetUser.Data.resultObject != null)
                    {
                        TempData["infoLogin"] = "Đăng nhập thành công";
                        TempData["statusLogin"] = 1;
                        var _object = resultGetUser.Data.resultObject;
                        userInfo = new UserManager()
                        {
                            MemberID = _object.MemberID,
                            FullName = _object.FullName,
                            Email = _object.Email
                        };
                        Session[AppSetting.SessionUser] = userInfo;
                        Session.Timeout = 60;
                        using (_cartService = new CartServiceClient())
                        {
                            var search = new CartParam();
                            search.PageIndex = 1;
                            search.PageSize = 10;
                            search.CookieId = !string.IsNullOrEmpty(Request.Cookies["orderCart"].Value) ? Request.Cookies["orderCart"].Value : string.Empty;
                            search.MemberId = userInfo.MemberID;
                            var tempList = _cartService.SL_CART_GetList(search);
                            if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                            {
                                var count = 0;
                                foreach (var item in tempList.Data.resultObject)
                                {
                                    count += item.Quantity;
                                }
                                TempData["updateCart"] = count;
                                TempData["cookieCart"] = search.CookieId;
                            }
                        }
                        if (Session[AppSetting.SessionBaseUrl] != null && !string.IsNullOrEmpty(Session[AppSetting.SessionBaseUrl].ToString()))
                        {
                            string UrlRedirect = Session[AppSetting.SessionBaseUrl].ToString();
                            Session[AppSetting.SessionBaseUrl] = null;
                            return Redirect(UrlRedirect);
                        }
                        else
                        {
                            
                            return Json(new { url = Url.Action("Index", "Home") });
                        }
                        //return Redirect("AccountCongDan/Login");
                    }
                }
                var modelReg = new RegisterViewModel
                {
                    FacebookID = user.FacebookID,
                    GoogleID = user.GoogleID,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Email = user.Email,
                    TypeRegister = !string.IsNullOrEmpty(user.FacebookID)?"Facebook":"Google",
                };
                var base64 = Common.CommonWebsite.Base64Encode(JsonConvert.SerializeObject(modelReg));
                TempData["sso"] = base64;
                //TempData["resultNonLogin"] = resultNonLogin;
                return Json(new { url = Url.Action("Register", "Customer")});
            }
            catch (Exception ex)
            {
                return Json(new { url = Url.Action("Login", "Customer") });
            }
        }

        private ResultLogin LoginFunction(LoginDataModel model)
        {
            try
            {
                using (_accountService = new AccountServiceClient())
                {
                    //check tồn tại tài khoản
                    var checkAcc = _accountService.CRM_ACCOUNT_CheckUserName_Web(model.PhoneNumber);
                    if (checkAcc.Data != null && checkAcc.Data.resultObject != null)
                    {
                        var dataResult = checkAcc.Data.resultObject;
                        //Tài khoản có tồn tại thõa điều kiện lấy thông tin đăng nhập
                        if (dataResult.status)
                        {
                            //Kiểm tra mật khẩu
                            model.Password = Encrypt_Decrypt.hashPassword(model.Password);
                            model.Salt = dataResult.Salt;
                            var checkLogin = _accountService.Account_CheckLogin(model);
                            if (checkLogin.Data != null)
                            {
                                if (checkLogin.Data.resultObject > 0)
                                    return new ResultLogin() { message = "Đăng nhập thành công", result = true, status = 1 };
                                else
                                    return new ResultLogin() { message = "Số điện thoại hoặc mật khẩu không đúng", result = false, status = 0 };
                            }
                            else
                            {
                                return new ResultLogin() { message = "Có lỗi trong quá trình đăng nhập. Vui lòng thử lại sau", result = false, status = 0 };
                            }
                        }
                        //Tài khoản không thõa thông tin đăng nhập
                        else
                        {
                            return new ResultLogin() { message = dataResult.Message, result = false, status = 0 };
                        }
                    }
                    return new ResultLogin() { message = "Số điện thoại hoặc mật khẩu không đúng", result = false, status = 0 };
                }
            }
            catch (Exception ex)
            {
                _logger.Error("LoginFunction error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
                return new ResultLogin() { message = "Lỗi đăng nhập", result = false, status = 0 };
            }
        }

        public ActionResult LogoutAccount()
        {
            TempData["infoLogin"] = "Đăng xuất thành công";
            Session[AppSetting.SessionUser] = null;
            Session[AppSetting.SessionBaseUrl] = null;
            return Redirect("/");
        }
        #endregion

        #region "Active account"
        public ActionResult ActiveUserRegister(string uID = "")
        {
            AccountIsActive modelView = new AccountIsActive();
            if (uID != "")
            {
                try
                {
                    using (_accountService = new AccountServiceClient())
                    {
                        var _isActive = _accountService.Account_Active(Encrypt_Decrypt.base64Decode(uID));
                        if (_isActive != null && _isActive.Data.resultObject != null)
                        {
                            modelView.status = _isActive.Data.resultObject;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("ActiveUserRegister error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);

                }
            }
            
            return View(modelView);
        }
        #endregion

        #region Send mail
        [HttpPost]
        public ActionResult SendMailActive(string _mailTo = "", string _keyActive="",string _fullName="", int _gender = 0)
        {

            string bodyMail = "<p>Kính gửi #DanhXung #FullName:<br />";
            bodyMail += "Bạn đã đăng ký tài khoản thành công<br />";
            bodyMail += "Vui lòng nhấn vào <a href=\"#XacThucToken\">đây</a> để kích hoạt tài khoản.</p>";
            string subjectMail = "Email kích hoạt tài khoản";
            string mailTo = string.Empty;
            bool status = false;
            try
            {
                var result = new UserManager();
                subjectMail = "Yêu cầu kích hoạt tài khoản";
                mailTo = _mailTo;
                result.XacThucToken = this.Url.Action("ActiveUserRegister", "Customer", new { uID = Encrypt_Decrypt.base64Encode(_keyActive) }, this.Request.Url.Scheme).ToString();
                result.FullName = _fullName;
                result.DanhXung = _gender ==0 ? "anh" : "chị";
                bodyMail = CommonHelper.GetBodyMail(bodyMail, result);
                       
                dynamic resultSend = SendMail.SendMailSync(mailTo, subjectMail, bodyMail);
                return Json(new { status = resultSend.status, _object = resultSend });
            }
            catch (Exception ex)
            {
                return Json(new { status = status });
            }
        }

        //[HttpPost]
        //public ActionResult SendMailReactive(ResultDKDoanhNghiep model)
        //{
        //    string bodyMail = string.Empty;
        //    string subjectMail = string.Empty;
        //    string mailTo = string.Empty;
        //    bool status = false;
        //    try
        //    {
        //        using (pageClient = new PageServiceClient())
        //        {
        //            var tempResult = pageClient.EmailManager_GetByKeyCode(KeyCodeMail.ReactiveAccount);
        //            if (tempResult.Data != null && tempResult.Data.resultObject.IsSendMail)
        //            {
        //                bodyMail = tempResult.Data.resultObject.Body;
        //            }
        //            else
        //            {
        //                return Json(new { status = false, mailObject = tempResult.Data.resultObject });
        //            }
        //        }
        //        using (_userSrv = new TaiKhoanCongDanServiceClient())
        //        {
        //            var tempList = _userSrv.QL_TaiKhoanCongDan_GetById(model.UserID, model.DoanhNghiepID);
        //            if (tempList.Data != null && tempList.Data.resultObject != null)
        //            {
        //                var result = tempList.Data.resultObject;
        //                subjectMail = "Yêu cầu kích hoạt tài khoản";
        //                mailTo = result.Email;
        //                result.XacThucToken = this.Url.Action("ActiveAccount", "AccountCongDan", new { uID = Encrypt_Decrypt.base64Encode(result.UserID.ToString()) }, this.Request.Url.Scheme).ToString();
        //                bodyMail = CommonHelper.GetBodyMail(bodyMail, result);
        //                //bodyMail = RenderRazorViewToString(ControllerContext, "_YeuCauKichHoatLaiPartial", result);
        //            }
        //        }
        //        //if (!string.IsNullOrEmpty(bodyMail))
        //        //{
        //        var reultSend = SendMail.SendMailSync(mailTo, subjectMail, bodyMail);
        //        return Json(new { status = true, _object = reultSend });
        //        //}
        //        //return Json(new { status = false });
        //    }
        //    catch (Exception ex)
        //    {
        //        modellog = new LogAddView()
        //        {
        //            ShortMessages = "AccountCongDanController/SendMailReactive error: " + ex.Message,
        //            ex = ex,
        //            UserID = new Guid(),
        //            UrlPath = Request.Url.AbsoluteUri,
        //            ReferrerUrl = Request.UrlReferrer != null ? Request.UrlReferrer.PathAndQuery : ""
        //        };
        //        _Log.LogError(modellog);
        //        return Json(new { status = status });
        //    }
        //}
        //[HttpPost]
        //public ActionResult SendMailForgotPassword(ResultDKDoanhNghiep model)
        //{
        //    string bodyMail = string.Empty;
        //    string subjectMail = string.Empty;
        //    string mailTo = string.Empty;
        //    bool status = false;
        //    try
        //    {
        //        var salt = Encrypt_Decrypt.GenerateOTP();
        //        var result = new QL_TaiKhoanCongDanAdd();
        //        using (pageClient = new PageServiceClient())
        //        {
        //            var tempResult = pageClient.EmailManager_GetByKeyCode(KeyCodeMail.ForgotPassword);
        //            if (tempResult.Data != null && tempResult.Data.resultObject.IsSendMail)
        //            {
        //                bodyMail = tempResult.Data.resultObject.Body;
        //            }
        //            else
        //            {
        //                return Json(new { status = false, mailObject = tempResult.Data.resultObject });
        //            }
        //        }
        //        using (_userSrv = new TaiKhoanCongDanServiceClient())
        //        {
        //            var tempList = _userSrv.QL_TaiKhoanCongDan_GetById(model.UserID, model.DoanhNghiepID);
        //            if (tempList.Data != null && tempList.Data.resultObject != null)
        //            {
        //                result = tempList.Data.resultObject;
        //                subjectMail = "Xác nhận quên mật khẩu";
        //                mailTo = result.Email;
        //                result.XacThucToken = salt;
        //                bodyMail = CommonHelper.GetBodyMail(bodyMail, result);
        //                // bodyMail = RenderRazorViewToString(ControllerContext, "_MaXacNhanPartial", result);
        //            }
        //        }
        //        //if (!string.IsNullOrEmpty(bodyMail))
        //        //{
        //        var reultSend = SendMail.SendMailSync(mailTo, subjectMail, bodyMail);
        //        //Save mã xác nhận
        //        LoginDataModel token = new LoginDataModel()
        //        {
        //            Email = result.Email,
        //            Salt = result.XacThucToken
        //        };
        //        using (_userSrv = new TaiKhoanCongDanServiceClient())
        //        {
        //            if (_userSrv.QL_TaiKhoanCongDan_UpdateToken(token).Data.resultObject)
        //            {
        //                return Json(new { status = true, _object = reultSend });
        //            }
        //        }
        //        return Json(new { status = false });
        //        //}
        //        //return Json(new { status = false });
        //    }
        //    catch (Exception ex)
        //    {
        //        modellog = new LogAddView()
        //        {
        //            ShortMessages = "AccountCongDanController/SendMailForgotPassword error: " + ex.Message,
        //            ex = ex,
        //            UserID = new Guid(),
        //            UrlPath = Request.Url.AbsoluteUri,
        //            ReferrerUrl = Request.UrlReferrer != null ? Request.UrlReferrer.PathAndQuery : ""
        //        };
        //        _Log.LogError(modellog);
        //        return Json(new { status = status });
        //    }
        //}
        #endregion Send mail
    }
}
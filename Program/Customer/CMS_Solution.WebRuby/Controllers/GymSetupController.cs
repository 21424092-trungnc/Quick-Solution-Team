using Business.Entities;
using Business.Entities.Domain;
using CMS_Solution.WebRuby.ViewModel;
using Core.Common.Utilities;
using log4net;
using Module.Framework;
using Module.Framework.UltimateClient;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMS_Solution.WebRuby.Controllers
{
    public class GymSetupController : BasePublicController
    {
        #region dungchung
        private AccountServiceClient _accountService;
        private DungChungServiceClient _dungChungService;
        private GymSetupServiceClient _gymSetupService;
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(BaseController));
        #endregion

        // GET: GymSetup
        public ActionResult Index()
        {
            return View();
        }
        //[ChildActionOnly]
        [HttpGet]
        public virtual ActionResult HomePageGymSetup()
        {
            var GymSetups = new List<GymSetupMap>();
            try
            {
                var search = new GymSetupParam();
                search.PageIndex = 1;
                search.PageSize = 3;
                using (_gymSetupService = new GymSetupServiceClient())
                {
                    var tempList = _gymSetupService.CMS_SETUPSERVICE_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        GymSetups = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GymSetup error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(GymSetups);
        }
        [ChildActionOnly]
        public virtual ActionResult GymSetupDiff()
        {
            var GymSetups = new List<GymSetupMap>();
            try
            {
                var search = new GymSetupParam();
                search.PageIndex = 1;
                search.PageSize = 3;
                using (_gymSetupService = new GymSetupServiceClient())
                {
                    var tempList = _gymSetupService.CMS_SETUPSERVICE_GetList_Web(search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                    {
                        GymSetups = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GymSetup error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return PartialView(GymSetups);
        }
        public virtual ActionResult GymSetupItem(int gymsetupId = 0)
        {
            var gymsetups = new GymSetupDetailViewModel();
            try
            {
                using (_gymSetupService = new GymSetupServiceClient())
                {
                    var tempList = _gymSetupService.CMS_SETUPSERVICE_GetService_ByID(gymsetupId);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        gymsetups.Detail = tempList.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Recruitment error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            gymsetups.GymTrial = new GymTrialAdd()
            {
                 SETUPSERVICEID = gymsetupId
            };
            var search = new GymSetupParam();
            search.PageIndex = 1;
            search.PageSize = 3;
            using (_gymSetupService = new GymSetupServiceClient())
            {
                var tempList = _gymSetupService.CMS_SETUPSERVICE_GetList_Web(search);
                if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Count > 0)
                {
                    gymsetups.ListSameCategory = tempList.Data.resultObject;
                }
            }
            return View(gymsetups);
        }
        [HttpPost]
        public ActionResult Register(GymTrialAdd mapAdd)
        {
            bool status = false;
            try
            {
                string _DataLeadID = "";
                using (_accountService = new AccountServiceClient())
                {
                    var data = _accountService.CustomerDataLeads_GetByPhone(mapAdd.PHONENUMBER);
                    if (data != null)
                    {
                        if (data.Data.resultObject != null && !string.IsNullOrEmpty(data.Data.resultObject.DATALEADSID))
                        {
                            _DataLeadID = data.Data.resultObject.DATALEADSID;
                        }
                        else
                        {
                            //Insert CustomerDataLead
                            CustomerDataLeadsParam dataLeadModel = CustomerDataLeadsParamMaps(mapAdd);
                            dataLeadModel.CreatedUser = AppSetting.UserWebID;
                            var _resultDataLead = _accountService.CustomerDataLeads_InsOrUpd(dataLeadModel);
                            if (_resultDataLead != null && _resultDataLead.Data.resultObject != "")
                            {
                                _DataLeadID = _resultDataLead.Data.resultObject;
                            }
                        }
                    }
                }
                using (_gymSetupService = new GymSetupServiceClient())
                {
                    mapAdd.DATALEADSID = _DataLeadID;
                    var tempList = _gymSetupService.CMS_SETUPSERVICE_REGISTER_InsUpd_Web(mapAdd);
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
            _dungChungService = new DungChungServiceClient();
            _dungChungService.DisposeCacheService("");
            CacheHelper.RemoveAll();
            return Json(new { status = status });
        }
        public CustomerDataLeadsParam CustomerDataLeadsParamMaps(GymTrialAdd model)
        {
            CustomerDataLeadsParam result = new CustomerDataLeadsParam();
            result.FullName = model.FULLNAME;
            result.PhoneNumber = model.PHONENUMBER;
            result.Email = model.EMAIL;
            result.BusinessID = 0;
            return result;
        }
    }
}
using Business.Entities.Domain;
using log4net;
using Module.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMS_Solution.WebRuby.Controllers
{
    public class ServiceController : Controller
    {
        #region dungchung
        private DungChungServiceClient _dungChungService;
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(BaseController));
        #endregion
        public ActionResult Index()
        {
            List<ServiceMap> lstService = new List<ServiceMap>();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var service = _dungChungService.CMS_SetupService_GetList("");
                    if (service.Data != null && service.Data.resultObject != null && service.Data.resultObject.Count > 0)
                    {
                        lstService = service.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("About error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(lstService);
        }
        public ActionResult HumanResource()
        {
            ServiceViewModel resultModel = new ServiceViewModel();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var tempList = _dungChungService.CMS_StaticContent_Get("HumanResource");
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        resultModel.staticContentMap = tempList.Data.resultObject;
                    }
                    var service = _dungChungService.CMS_SetupService_GetList("");
                    if (service.Data != null && service.Data.resultObject != null && service.Data.resultObject.Count > 0)
                    {
                        resultModel.lstService = service.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HumanResource error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(resultModel);
        }
        public ActionResult SetupGym()
        {
            ServiceViewModel resultModel = new ServiceViewModel();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var tempList = _dungChungService.CMS_StaticContent_Get("SetupGym");
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        resultModel.staticContentMap = tempList.Data.resultObject;
                    }
                    var service = _dungChungService.CMS_SetupService_GetList("");
                    if (service.Data != null && service.Data.resultObject != null && service.Data.resultObject.Count > 0)
                    {
                        resultModel.lstService = service.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SetupGym error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(resultModel);
        }
        public ActionResult GymDesign()
        {
            ServiceViewModel resultModel = new ServiceViewModel();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var tempList = _dungChungService.CMS_StaticContent_Get("GymDesign");
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        resultModel.staticContentMap = tempList.Data.resultObject;
                    }
                    var service = _dungChungService.CMS_SetupService_GetList("");
                    if (service.Data != null && service.Data.resultObject != null && service.Data.resultObject.Count > 0)
                    {
                        resultModel.lstService = service.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GymDesign error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(resultModel);
        }
        public ActionResult ProvidingTeachers()
        {
            ServiceViewModel resultModel = new ServiceViewModel();
            try
            {
                using (_dungChungService = new DungChungServiceClient())
                {
                    var tempList = _dungChungService.CMS_StaticContent_Get("ProvidingTeachers");
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        resultModel.staticContentMap = tempList.Data.resultObject;
                    }
                    var service = _dungChungService.CMS_SetupService_GetList("");
                    if (service.Data != null && service.Data.resultObject != null && service.Data.resultObject.Count > 0)
                    {
                        resultModel.lstService = service.Data.resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ProvidingTeachers error: " + ex.Message + "--" + ex.StackTrace + "--" + ex.InnerException);
            }
            return View(resultModel);
        }
    }
}
using Business.Entities;
using Business.Services.Interfaces;
using Core.Common.Utilities;
using Data.Core.Repositories.Interfaces;
using log4net;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Business.Services
{
    public class GymSetupService : IGymSetupService
    {
        #region Private Repository & contractor       
        private static readonly string CMSSolutionConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
        private static IGymSetupRepository _GymSetupRepository;
        public GymSetupService(IGymSetupRepository GymSetupRepository)
        {
            _GymSetupRepository = GymSetupRepository;
        }
        #endregion

        #region GymSetup
        public ResultResponse<List<GymSetupMap>> CMS_SETUPSERVICE_GetList_Web(GymSetupParam model)
        {
            var response = new ResponseModel();
            var data = _GymSetupRepository.CMS_SETUPSERVICE_GetList_Web(model, out response);
            var result = new ResultResponse<List<GymSetupMap>>(response, data);
            return result;
        }

        public ResultResponse<GymSetupDetail> CMS_SETUPSERVICE_GetService_ByID(long GymSetupID)
        {
            var response = new ResponseModel();
            var data = _GymSetupRepository.CMS_SETUPSERVICE_GetService_ByID(GymSetupID, out response);
            var result = new ResultResponse<GymSetupDetail>(response, data);
            return result;
        }
        #endregion

        #region Candidate
        public ResultResponse<bool> CMS_SETUPSERVICE_REGISTER_InsUpd_Web(GymTrialAdd model)
        {
            IDbConnection conns = new SqlConnection(CMSSolutionConn);
            conns.Open();
            IDbTransaction transCC = conns.BeginTransaction();
            var response = new ResponseModel();
            try
            {
                var data = _GymSetupRepository.CMS_SETUPSERVICE_REGISTER_InsUpd_Web(model, conns, transCC, out response);
                if (!data)
                {
                    if (transCC != null) transCC.Rollback();
                    return new ResultResponse<bool>(response, false);
                }
                transCC.Commit();
                return new ResultResponse<bool>(response, data);
            }
            catch
            {
                if (transCC != null) transCC.Rollback();
                return new ResultResponse<bool>(response, false);
            }
            finally
            {
                if (transCC != null) transCC.Dispose();
                if (conns != null) conns.Dispose();
            }
        }
        #endregion
    }
}

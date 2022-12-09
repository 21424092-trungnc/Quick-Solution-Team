using Business.Entities;
using Core.Common.Utilities;
using Dapper;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Core.Repositories
{
    public class GymSetupRepository : IGymSetupRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ProductRepository));
        private const string TableName = "";
        public GymSetupRepository(ILog logger)
        {
            _logger = logger;
        }

        #region GymSetup
        public List<GymSetupMap> CMS_SETUPSERVICE_GetList_Web(GymSetupParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<GymSetupMap>("CMS_SETUPSERVICE_GetList_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<GymSetupMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CMS_SETUPSERVICE_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public GymSetupDetail CMS_SETUPSERVICE_GetService_ByID(long GymSetupID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("GymSetupID", GymSetupID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QuerySingle<GymSetupDetail>("CMS_SETUPSERVICE_GetService_ByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CMS_SETUPSERVICE_GetService_ByID Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region Candidate
        public bool CMS_SETUPSERVICE_REGISTER_InsUpd_Web(GymTrialAdd model, IDbConnection conns, IDbTransaction tranCC, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("REGISTERSETUPID", model.REGISTERSETUPID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("SETUPSERVICEID", model.SETUPSERVICEID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("FULLNAME", model.FULLNAME, DbType.String, ParameterDirection.Input);
                paramters.Add("EMAIL", model.EMAIL, DbType.String, ParameterDirection.Input);
                paramters.Add("PHONENUMBER", model.PHONENUMBER, DbType.String, ParameterDirection.Input);
                paramters.Add("ADDRESS", model.ADDRESS, DbType.String, ParameterDirection.Input);
                paramters.Add("CONTENTREGISTRATION", model.CONTENTREGISTRATION, DbType.String, ParameterDirection.Input);
                paramters.Add("DATALEADSID", model.DATALEADSID, DbType.String, ParameterDirection.Input);
                paramters.Add("UserName", model.UserName, DbType.String, ParameterDirection.Input);
                var datas = conns.ExecuteScalar<bool>("CMS_SETUPSERVICE_REGISTER_InsUpd_Web", paramters, tranCC, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("CMS_SETUPSERVICE_REGISTER_InsUpd_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
        #endregion
    }
}

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
    public class RecruitmentRepository : IRecruitmentRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ProductRepository));
        private const string TableName = "";
        public RecruitmentRepository(ILog logger)
        {
            _logger = logger;
        }

        #region Recruitment
        public List<RecruitmentMap> HR_RECRUIT_GetList_Web(RecruitmentParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Keyword", model.Keyword, DbType.String, ParameterDirection.Input);
                    paramters.Add("BusinessId", model.BusinessId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PositionId", model.PositionId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<RecruitmentMap>("HR_RECRUIT_GetList_Web", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<RecruitmentMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HR_RECRUIT_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public RecruitmentDetail HR_RECRUIT_GetRecruit_ByID(long RecruitID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("RecruitID", RecruitID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QuerySingle<RecruitmentDetail>("HR_RECRUIT_GetRecruit_ByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HR_RECRUIT_GetRecruit_ByID Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region Candidate
        public long HR_CANDIDATE_InsUpd_Web(CandidateMapAdd model, IDbConnection conns, IDbTransaction tranCC, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("CandidateId", model.CandidateId, DbType.Int64, ParameterDirection.Input);
                paramters.Add("CandidateFullName", model.CandidateFullName, DbType.String, ParameterDirection.Input);
                paramters.Add("Gender", model.Gender, DbType.String, ParameterDirection.Input);
                paramters.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                paramters.Add("PhoneNumber", model.PhoneNumber, DbType.Int32, ParameterDirection.Input);
                paramters.Add("Introduction", model.Introduction, DbType.String, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.String, ParameterDirection.Input);
                paramters.Add("RecruitId", model.RecruitId, DbType.Int64, ParameterDirection.Input);
                var datas = conns.ExecuteScalar<long>("HR_CANDIDATE_InsUpd_Web", paramters, tranCC, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("HR_CANDIDATE_InsUpd_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long HR_CANDIDATE_ATTACHMENT_InsUpd_Web(CandidateAttachmentMapAdd model, IDbConnection conns, IDbTransaction tranCC, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("CandidateAttachmentId", model.CandidateAttachmentId, DbType.Int64, ParameterDirection.Input);
                paramters.Add("CandidateId", model.CandidateId, DbType.Int64, ParameterDirection.Input);
                paramters.Add("AttachmentName", model.AttachmentName, DbType.String, ParameterDirection.Input);
                paramters.Add("AttachmentPath", model.AttachmentPath, DbType.String, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.String, ParameterDirection.Input);
                var datas = conns.ExecuteScalar<long>("HR_CANDIDATE_ATTACHMENT_InsUpd_Web", paramters, tranCC, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("HR_CANDIDATE_ATTACHMENT_InsUpd_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion
    }
}

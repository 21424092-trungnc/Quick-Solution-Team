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
    public class RecruitmentService : IRecruitmentService
    {
        #region Private Repository & contractor       
        private static readonly string CMSSolutionConn = ConfigurationManager.ConnectionStrings["CMS_Solution.ConnString"].ConnectionString;
        private static IRecruitmentRepository _recruitmentRepository;
        public RecruitmentService(IRecruitmentRepository recruitmentRepository)
        {
            _recruitmentRepository = recruitmentRepository;
        }
        #endregion

        #region Recruitment
        public ResultResponse<List<RecruitmentMap>> HR_RECRUIT_GetList_Web(RecruitmentParam model)
        {
            var response = new ResponseModel();
            var data = _recruitmentRepository.HR_RECRUIT_GetList_Web(model, out response);
            var result = new ResultResponse<List<RecruitmentMap>>(response, data);
            return result;
        }

        public ResultResponse<RecruitmentDetail> HR_RECRUIT_GetRecruit_ByID(long RecruitID)
        {
            var response = new ResponseModel();
            var data = _recruitmentRepository.HR_RECRUIT_GetRecruit_ByID(RecruitID, out response);
            var result = new ResultResponse<RecruitmentDetail>(response, data);
            return result;
        }
        #endregion

        #region Candidate
        public ResultResponse<bool> HR_CANDIDATE_InsUpd_Web(CandidateMapAdd model)
        {
            IDbConnection conns = new SqlConnection(CMSSolutionConn);
            conns.Open();
            IDbTransaction transCC = conns.BeginTransaction();
            var response = new ResponseModel();
            try
            {
                var data = _recruitmentRepository.HR_CANDIDATE_InsUpd_Web(model, conns, transCC, out response);
                if (data <= 0)
                {
                    if (transCC != null) transCC.Rollback();
                    return new ResultResponse<bool>(response, false);
                }
                CandidateAttachmentMapAdd modelFile = new CandidateAttachmentMapAdd()
                {
                    AttachmentName = model.AttachmentName,
                    AttachmentPath = model.AttachmentPath,
                    CandidateId = data,
                    UserID = model.UserID
                };
                model.CandidateAttachmentMapAdd = modelFile;
                var dataFile = _recruitmentRepository.HR_CANDIDATE_ATTACHMENT_InsUpd_Web(model.CandidateAttachmentMapAdd, conns, transCC, out response);
                if (dataFile <= 0)
                {
                    if (transCC != null) transCC.Rollback();
                    return new ResultResponse<bool>(response, false);
                }
                transCC.Commit();
                return new ResultResponse<bool>(response, true);
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

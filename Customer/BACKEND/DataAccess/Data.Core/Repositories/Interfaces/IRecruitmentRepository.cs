using Business.Entities;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IRecruitmentRepository
    {
        List<RecruitmentMap> HR_RECRUIT_GetList_Web(RecruitmentParam model, out ResponseModel restStatus);
        RecruitmentDetail HR_RECRUIT_GetRecruit_ByID(long RecruitID, out ResponseModel restStatus);
        long HR_CANDIDATE_InsUpd_Web(CandidateMapAdd model, IDbConnection conns, IDbTransaction tranCC, out ResponseModel restStatus);
        long HR_CANDIDATE_ATTACHMENT_InsUpd_Web(CandidateAttachmentMapAdd model, IDbConnection conns, IDbTransaction tranCC, out ResponseModel restStatus);
    }
}

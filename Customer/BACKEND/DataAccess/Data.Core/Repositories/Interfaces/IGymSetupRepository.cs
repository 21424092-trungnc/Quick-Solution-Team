using Business.Entities;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IGymSetupRepository
    {
        List<GymSetupMap> CMS_SETUPSERVICE_GetList_Web(GymSetupParam model, out ResponseModel restStatus);
        GymSetupDetail CMS_SETUPSERVICE_GetService_ByID(long GymSetupID, out ResponseModel restStatus);
        bool CMS_SETUPSERVICE_REGISTER_InsUpd_Web(GymTrialAdd model, IDbConnection conns, IDbTransaction tranCC, out ResponseModel restStatus);
    }
}

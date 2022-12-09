using Business.Entities.Domain;
using Core.Common.Utilities;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IBannerRepository
    {
        List<BannerMap> CMS_BANNER_GetList_Web(out ResponseModel restStatus);
    }
}

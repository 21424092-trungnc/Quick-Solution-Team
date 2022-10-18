using Business.Entities.Domain;
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
    public class BannerRepository :  IBannerRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(BannerRepository));
        private const string TableName = "";
        public BannerRepository(ILog logger) 
        {
            _logger = logger;
        }

        public List<BannerMap> CMS_BANNER_GetList_Web(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = AppSetting.CMSSolutionConnection)
                {
                    conns.Open();                    
                    var datas = conns.Query<BannerMap>("CMS_BANNER_HomeGetList_Web", null, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BannerMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CMS_BANNER_GetList_Web Error: " + ex.Message);
                restStatus = new ResponseModel(ex);
                return null;
            }

        }
    }
   
}

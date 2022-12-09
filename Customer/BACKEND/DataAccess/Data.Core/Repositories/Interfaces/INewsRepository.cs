using Business.Entities;
using Core.Common.Utilities;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface INewsRepository 
    {
        List<NewsMap> NEWS_GetList(NewsParam model, out ResponseModel restStatus);
        List<NewsMap> GetNewsSameCategoryByID(NewsSameParam model, out ResponseModel restStatus);
        NewsDetail GetNews_ByID(long NewsID, out ResponseModel restStatus);
        List<NewsCategoryMap> GetNewsCategory_GetList(NewsCategoryParam model, out ResponseModel restStatus);
        NewsCategoryDetail GetNewsCategory_ByID(long NewsCategoryID, out ResponseModel restStatus);
    }
}

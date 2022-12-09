using Business.Entities;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface INewsService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "NEWS/NEWS_GetList")]
        ResultResponse<List<NewsMap>> NEWS_GetList(NewsParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "NEWS/GetNewsSameCategoryByID")]
        ResultResponse<List<NewsMap>> GetNewsSameCategoryByID(NewsSameParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "NEWS/GetNews_ByID")]
        ResultResponse<NewsDetail> GetNews_ByID(long NewsID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "NEWS/GetNewsCategory_GetList")]
        ResultResponse<List<NewsCategoryMap>> GetNewsCategory_GetList(NewsCategoryParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "NEWS/GetNewsCategory_ByID")]
        ResultResponse<NewsCategoryDetail> GetNewsCategory_ByID(long NewsCategoryID);

    }
}

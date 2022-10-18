using Business.Entities.Domain;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "MD/MD_PRODUCTCATEGORY_GetList_Web")]
        ResultResponse<List<ProductCategoryMap>> MD_PRODUCTCATEGORY_GetList_Web(ProductCategoryParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "MD/PRO_PRODUCTATTRIBUTE_GetList_Web?categoryid={categoryid}")]
        ResultResponse<List<ProductAttributeMap>> PRO_PRODUCTATTRIBUTE_GetList_Web(long categoryid);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "MD/MD_PRODUCT_GetList_Web")]
        ResultResponse<List<ProductMap>> MD_PRODUCT_GetList_Web(ProductParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "MD/MD_PRODUCT_GetById_Web?productId={productId}")]
        ResultResponse<ProductDetailAll> MD_PRODUCT_GetById_Web(long productId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "MD/PRO_COMMENT_CreateOrUpdate_Web")]
        ResultResponse<bool> PRO_COMMENT_CreateOrUpdate_Web(ProductCommentsAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "MD/MD_PRODUCT_GetRelationship_Web?productId={productId}")]
        ResultResponse<List<ProductMap>> MD_PRODUCT_GetRelationship_Web(long productId);



    }
}

using Core.Common.Localization;
using System;
using System.Web;
using System.Web.Routing;

namespace CMS_Solution.WebRuby.Seo
{
    /// <summary>
    /// Provides properties and methods for defining a SEO friendly route, and for getting information about the route.
    /// </summary>
    public partial class GenericPathRoute : LocalizedRoute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class and default parameter values.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class, default parameter values and constraints.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class, default parameter values, 
        /// constraints,and custom values.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. The route handler might need these values to process the request.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>
        /// An object that contains the values from the route definition.
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData data = base.GetRouteData(httpContext);
            if (data != null)
            {
                var slug = data.Values["generic_se_name"] as string;
                if (string.IsNullOrWhiteSpace(slug))
                {
                    data.Values["controller"] = "Common";
                    data.Values["action"] = "PageNotFound";
                    return data;
                }
                //performance optimization.
                //we load a cached verion here. it reduces number of SQL requests for each page load
                var urlRecord = Common.CommonWebsite.GetBySlugCached(slug);
                if (urlRecord == null)
                {
                    data.Values["controller"] = "Common";
                    data.Values["action"] = "PageNotFound";
                    return data;
                }
                //process URL
                switch (urlRecord.EntityName.ToLowerInvariant())
                {
                    case "product":
                        {
                            data.Values["controller"] = "Product";
                            data.Values["action"] = "ProductDetails";
                            data.Values["productid"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "category":
                        {
                            data.Values["controller"] = "Catalog";
                            data.Values["action"] = "Category";
                            data.Values["categoryid"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "vendor":
                        {
                            data.Values["controller"] = "Catalog";
                            data.Values["action"] = "Vendor";
                            data.Values["vendorid"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "newscategory":
                        {
                            data.Values["controller"] = "News";
                            data.Values["action"] = "NewsCategory";
                            data.Values["cateNewsItemId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "newsitem":
                        {
                            data.Values["controller"] = "News";
                            data.Values["action"] = "NewsItem";
                            data.Values["newsItemId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "blogpost":
                        {
                            data.Values["controller"] = "Blog";
                            data.Values["action"] = "BlogPost";
                            data.Values["blogPostId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "topic":
                        {
                            data.Values["controller"] = "Topic";
                            data.Values["action"] = "TopicDetails";
                            data.Values["topicId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "recruitmentitem":
                        {
                            data.Values["controller"] = "Common";
                            data.Values["action"] = "RecruitmentItem";
                            data.Values["recruitmentId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "gymsetupitem":
                        {
                            data.Values["controller"] = "GymSetup";
                            data.Values["action"] = "GymSetupItem";
                            data.Values["gymsetupId"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    case "md_manufacturer":
                        {
                            data.Values["controller"] = "Catalog";
                            data.Values["action"] = "Manufacturer";
                            data.Values["manufacturerid"] = urlRecord.EntityId;
                            data.Values["SeName"] = urlRecord.Slug;
                        }
                        break;
                    default:
                    {
                        //no record found

                        //generate an event this way developers could insert their own types
                        data.Values["controller"] = "Common";
                        data.Values["action"] = "PageNotFound";
                            
                    }
                        break;
                }
            }
            return data;
        }

        #endregion
    }
}
using Core.Common.Mvc.Routes;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMS_Solution.WebRuby
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            IRoutePublisher routePublisher = NinjectRouterFactory.Get<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "CMS_Solution.WebRuby.Controllers" }
            );
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //Registering some regular mvc stuff
            RegisterRoutes(RouteTable.Routes);
           /// RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "PageNotFound";
                        break;
                    case 500:
                        // server error
                        action = "PageNotFound";
                        break;
                    default:
                        action = "PageNotFound";
                        break;
                }
                // clear error on server
                Server.ClearError();
                // Call target Controller and pass the routeData.
                IController errorController = new Controllers.CommonController();

                var routeData = new RouteData();
                routeData.Values.Add("controller", "Common");
                routeData.Values.Add("action", action);

                errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
               // Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, exception.Message));
            }
        }
    }
}

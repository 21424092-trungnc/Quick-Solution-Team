using Core.Common.Utilities;
using System.Web.Mvc;

namespace CMS_Solution.WebRuby.Controllers
{
    public partial class HomeController : BasePublicController
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
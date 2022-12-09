using Core.Common.UI.Paging;
using System.Web.Mvc;

namespace CMS_Solution.WebRuby.Extensions
{
    public static class HtmlExtensions
    {
        public static Pager Pager(this HtmlHelper helper, IPageableModel pagination)
        {
            return new Pager(pagination, helper.ViewContext);
        }
    }
}
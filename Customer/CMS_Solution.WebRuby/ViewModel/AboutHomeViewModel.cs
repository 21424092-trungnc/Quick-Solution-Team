using Business.Entities.Domain;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class AboutHomeViewModel
    {
        public StaticContentMap staticContentMap { get; set; }
        public List<ServiceMap> lstService { get; set; }
    }
}
using Business.Entities.Domain;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby
{
    public class ServiceViewModel
    {
        public StaticContentMap staticContentMap { get; set; }
        public List<ServiceMap> lstService { get; set; }
    }
}
using Business.Entities.Domain;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class AboutViewModel
    {
        public StaticContentMap staticContentMap { get; set; }
        public StaticContentMap partnerContentMap { get; set; }
        public StaticContentMap branchMap { get; set; }
        public List<UserMap> lstUserMap { get; set; }
    }
}
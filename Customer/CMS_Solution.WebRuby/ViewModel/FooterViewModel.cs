using Business.Entities.Domain;
using System.Collections.Generic;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class FooterViewModel
    {
        public StaticContentMap footerTopMap { get; set; }
        public StaticContentMap footerContentMap { get; set; }
        public StaticContentMap footerBottomMap { get; set; }
        public List<ServiceMap> lstService { get; set; }

        public List<MenuMap> menuMap { get; set; }
    }
}
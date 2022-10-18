using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class StaticContentMap
    {
        public string StaticTitle { get; set; }
        public string StaticContent { get; set; }
        public List<CBO_DungChungViewModel> lstTopic { get; set; }
    }
}

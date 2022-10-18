using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Solution.WebRuby.ViewModel
{
    public class Files
    {
        public string TenGoc { get; set; }
        public string TenUpload { get; set; }
        public string Url { get; set; }
    }

    public class FileDataModel
    {
        public string base64 { get; set; }
        public string folder { get; set; }
    }
}
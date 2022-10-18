using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class HomePageViewModel
    {
        public List<SliderViewModel> lstSlider { get; set; }
    }
    public class SliderViewModel
    {
        public long PictureID { get; set; }
        public string PictureUrl { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
    }
    public class ThongBaoViewModel
    {
        public long ID { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDung { get; set; }
        public string Img { get; set; }
        public string NgayThongBao { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
    public class TinTucSuKienViewModel
    {
        public long ID { get; set; }
        public string TieuDe { get; set; }
        public string TomTat { get; set; }
        public string NoiDung { get; set; }
        public string Img { get; set; }
        public string NgayThongBao { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
    public class CauHoiThuongGapViewModel
    {
        public long ID { get; set; }
        public string CauHoi { get; set; }
        public string CauTraLoi { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
    public class GioiThieuViewModel
    {
        public string PageName { get; set; }
        public string Title { get; set; }
        public string MoTaNgan { get; set; }
        public string Body { get; set; }
        public string Img { get; set; }
        public string Slug { get; set; }
    }

    public class CommonDataModel
    {
        public string KeySearch { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

}

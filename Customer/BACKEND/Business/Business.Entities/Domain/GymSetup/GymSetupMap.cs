namespace Business.Entities
{
    public class GymSetupParam : PagesParamModel
    {
    }

    public class GymSetupMap : PagesModel
    {
        public long SETUPSERVICEID { get; set; }
        public string SETUPSERVICETITLE { get; set; }
        public string IMAGEURL { get; set; } //Mã cơ sở tuyển dụng
        public string SEONAME { get; set; }
        public string METAKEYWORDS { get; set; }
        public string METADESCRIPTIONS { get; set; }
        public string METATITLE { get; set; }
    }

    public class GymSetupDetail
    {
        public long GymSetupId { get; set; }
        public string GymSetupTitle { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string AuthorFullName { get; set; }
        public string ImageUrl { get; set; }
        public string SmallThumnailImageUrl { get; set; }
        public string MediumThumnailImageUrl { get; set; }
        public string LargeThumnailImageUrl { get; set; }
        public string XLargeThumnailImageUrl { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeoName { get; set; }
    }

    public class GymTrialAdd
    {
        public int REGISTERSETUPID { get; set; }
        public int SETUPSERVICEID { get; set; }
        public string FULLNAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONENUMBER { get; set; }
        public string ADDRESS { get; set; }
        public string CONTENTREGISTRATION { get; set; }
        public string DATALEADSID { get; set; }
        public string UserName { get; set; }
    }
}

namespace Business.Entities
{
    public class RecruitmentParam : PagesParamModel
    {
        public string Keyword { get; set; }
        public int BusinessId { get; set; } //Mã cơ sở tuyển dụng
        public int PositionId { get; set; } //Mã vị trí tuyển dụng
    }

    public class RecruitmentMap : PagesModel
    {
        public long RecruitmentId { get; set; }
        public string RecruitmentTitle { get; set; }
        public int BusinessId { get; set; } //Mã cơ sở tuyển dụng
        public string BusinessName { get; set; }
        public int PositionId { get; set; } //Mã vị trí tuyển dụng
        public string PositionName { get; set; }
        public string SalaryFrom { get; set; }
        public string SalaryTo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SeoName { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public string MetaTitle { get; set; }
    }

    public class RecruitmentDetail
    {
        public long RecruitmentId { get; set; }
        public string RecruitmentTitle { get; set; }
        public int BusinessId { get; set; } //Mã cơ sở tuyển dụng
        public string BusinessName { get; set; }
        public int PositionId { get; set; } //Mã vị trí tuyển dụng
        public string PositionName { get; set; }
        public string SalaryFrom { get; set; }
        public string SalaryTo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RecruitContent{ get; set; }
        //public string SeoName { get; set; }
        //public string MetaKeywords { get; set; }
        //public string MetaDescriptions { get; set; }
        //public string MetaTitle { get; set; }
    }
}

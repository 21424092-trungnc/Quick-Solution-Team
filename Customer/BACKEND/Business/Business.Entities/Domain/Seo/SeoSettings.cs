namespace Business.Entities
{
    public class SeoSettings
    {
        public long ID { get; set; }
        public string SeoName { get; set; }
        public string NameTitle { get; set; }
        public string ImageUrl { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public string MetaTitle { get; set; } 
        public long GroupID { get; set; } //ManufacturerId
    }
}

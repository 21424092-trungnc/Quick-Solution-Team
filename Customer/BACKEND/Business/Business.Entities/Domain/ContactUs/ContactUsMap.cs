namespace Business.Entities.Domain
{
    public class ContactUsMap : PagesParamModel
    {
        public int SupportId { get; set; }
        public int TopicId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ContentSupport { get; set; }
        public bool IsActive { get; set; }
        public string User { get; set; }
        public bool IsDeleted { get; set; }
    }
}

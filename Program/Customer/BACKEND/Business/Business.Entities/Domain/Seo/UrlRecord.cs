
namespace Business.Entities
{
    public class UrlRecord
    {
        public long UrlRecoreId { get; set; }
        public long EntityId { get; set; }
        public string EntityName { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
    }
}

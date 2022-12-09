namespace Business.Entities
{
    public class NewsParam : PagesParamModel
    {
        public int? IsHotNews { get; set; }
        public int? IsHighLight { get; set; }
        public int? IsVideo { get; set; }
        public int? IsShowHome { get; set; }
        public int? IsShowNotify { get; set; }
        public int? IsActive { get; set; }
        public int? IsSystem { get; set; }
        public long? NewsCategoryID { get; set; }
    }
    public class NewsSameParam : PagesParamModel
    {
        public long? NewsID { get; set; }
    }
    public class NewsMap : PagesModel
    {
        public long NewsID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDate { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string AuthorFullName { get; set; }
        public string SeoName { get; set; }
        public string ImageUrl { get; set; }
        public string SmallThumnailImageUrl { get; set; }
        public string MediumThumnailImageUrl { get; set; }
        public string LargeThumnailImageUrl { get; set; }
        public string XLargeThumnailImageUrl { get; set; }
    }
    public class NewsDetail
    {
        public long NewsID { get; set; }
        public long NewStatusID { get; set; }
        public string NewsStatusName { get; set; }
        public long NewsCategoryID { get; set; }
        public string NewsCategoryName { get; set; }
        public string NewsDate { get; set; }
        public string NewsTitle { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeoName { get; set; }
        public string NewsTag { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public string AuthorFullName { get; set; }
        public string NewsSource { get; set; }
        public int IsHotNews { get; set; }
        public int IsHighlight { get; set; }
        public int IsVideo { get; set; }
        public int IsShowHome { get; set; }
        public int IsShowNotify { get; set; }
        public string VideoLink { get; set; }
        public string SmallThumbnailImageUrl { get; set; }
        public string MediumThumbnailImageUrl { get; set; }
        public string LargeThumbnailImageUrl { get; set; }
        public string XLargeThumbnailImageUrl { get; set; }
        public string SmallThumbnailImageFileID { get; set; }
        public string MediumThumbnailImageFileID { get; set; }
        public string LargeThumbnailImageFileID { get; set; }
        public string XLargeThumbnailImageFileID { get; set; }
    }
    public class NewsCategoryParam : PagesParamModel
    {
        public int? ParentId { get; set; }
        public int? CategoryLevel { get; set; }
        public int? IsCateVideo { get; set; }
        public int? IsActive { get; set; }
        public int? IsSystem { get; set; }
        public int? IsDelete { get; set; }
    }
    public class NewsCategoryMap : PagesModel
    {
        public int NewsCategoryID { get; set; }
        public int ParentID { get; set; }
        public string NewsCategoryName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string CategoryLevel { get; set; }
        public string ImageFileID { get; set; }
        public int IsCateVideo { get; set; }
        public string SeoName { get; set; }
    }

    public class NewsCategoryDetail
    {
       public long NewsCategoryID { get; set; }
       public long ParentID { get; set; }
        public string ParentName { get; set; }
        public string NewsCategoryName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public long CategoryLevel { get; set; }
        public long ImageFileID { get; set; }
        public bool IsCateVideo { get; set; }
        public string SeoName { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
    }
}

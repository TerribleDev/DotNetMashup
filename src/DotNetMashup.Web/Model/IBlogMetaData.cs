namespace DotNetMashup.Web.Model
{
    public interface IBlogMetaData
    {
        string FeedUrl { get; set; }
        string AuthorName { get; set; }
        string AuthorEmail { get; set; }
        string ImageUrl { get; set; }
        string Id { get; set; }
        string BlogHomepage { get; set; }
    }
}
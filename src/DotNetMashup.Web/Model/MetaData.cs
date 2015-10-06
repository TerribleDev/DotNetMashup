namespace DotNetMashup.Web.Model
{
    //stolen idea from: https://github.com/NancyFx/Nancy.Blog/blob/master/src/Nancy.Blog/Model/MetaData.cs
    public class MetaData : IMetaData
    {
        public string FeedUrl { get; set; }
        public string Author { get; set; }
        public string AuthorEmail { get; set; }
        public string GravatarUrl { get; set; }
        public string Id { get; set; }
    }
}
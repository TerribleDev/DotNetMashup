using System;

namespace DotNetMashup.Web.Model
{
    //stolen idea from: https://github.com/NancyFx/Nancy.Blog/blob/master/src/Nancy.Blog/Model/MetaData.cs
    public class BlogMetaData : IBlogMetaData
    {
        public string FeedUrl { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string ImageUrl { get; set; }
        public string Id { get; set; }
        public string BlogHomepage { get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMashup.Web.Model
{
    public class BlogPostExternalData : IExternalData
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset PublishedDate { get; set; }
        public string Summary { get; set; }
        public string Localink { get; set; }
        public string OriginalLink { get; set; }
        public Author Author { get; set; }
    }
}
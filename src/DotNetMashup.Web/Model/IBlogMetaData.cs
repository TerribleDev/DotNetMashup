using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
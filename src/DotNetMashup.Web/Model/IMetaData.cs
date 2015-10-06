using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMashup.Web.Model
{
    public interface IMetaData
    {
        string FeedUrl { get; set; }
        string Author { get; set; }
        string AuthorEmail { get; set; }
        string GravatarUrl { get; set; }
        string Id { get; set; }
    }
}
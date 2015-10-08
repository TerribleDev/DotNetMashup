using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMashup.Web.Model
{
    public interface IExternalData
    {
        string Title { get; set; }
        string Content { get; set; }
        string OriginalLink { get; set; }
        Author Author { get; set; }
        DateTimeOffset PublishedDate { get; set; }
    }
}
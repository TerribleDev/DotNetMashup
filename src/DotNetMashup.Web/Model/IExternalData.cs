using System;

namespace DotNetMashup.Web.Model
{
    public interface IExternalData
    {
        string Title { get; set; }
        string Content { get; set; }
        Uri OriginalLink { get; set; }
        Author Author { get; set; }
        DateTimeOffset PublishedDate { get; set; }
    }
}
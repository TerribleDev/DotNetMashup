using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMashup.Web.Model
{
    /// <summary>
    /// Simple class to implement the standard external interface
    /// </summary>
    public class BaseExternalData : IExternalData
    {
        public Author Author { get; set; }

        public string Content { get; set; }

        public string OriginalLink { get; set; }

        public string Title { get; set; }
        public DateTimeOffset PublishedDate { get; set; }
    }
}
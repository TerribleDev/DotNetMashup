using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMashup.Web.Model
{
    public struct Author
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorUrl { get; set; }
        public string Email { get; set; }
    }
}
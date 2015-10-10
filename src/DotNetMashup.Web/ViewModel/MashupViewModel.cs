using System.Collections.Generic;
using DotNetMashup.Web.Model;

namespace DotNetMashup.Web.ViewModel
{
    public class MashupViewModel
    {
        public string Header { get; set; }
        public int CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public IEnumerable<IExternalData> Posts { get; set; }
    }
}
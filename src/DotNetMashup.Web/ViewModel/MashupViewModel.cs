using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMashup.Web.Model;

namespace DotNetMashup.Web.ViewModel
{
    public class MashupViewModel
    {
        public string Header { get; set; }
        public IEnumerable<IExternalData> posts { get; set; }
    }
}

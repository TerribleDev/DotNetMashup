using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMashup.Web.Global
{
    public class SiteSettings : ISiteSetting
    {
        public List<string> Categories { get; } = new List<string> { "c#", "csharp", "cs", "asp.net", "NancyFx", "Nancy", "vNext", "asp.net 5" };
    }
}
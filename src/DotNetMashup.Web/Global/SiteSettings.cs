using System.Collections.Generic;

namespace DotNetMashup.Web.Global
{
    public class SiteSettings : ISiteSetting
    {
        public short AmountPerPage { get { return 12; } }

        public List<string> Categories { get; } = new List<string> { "c#", "csharp", "cs", "asp.net", "NancyFx", "vNext", "asp.net 5", "aspnet5" };
        public string Title { get { return "DotNet Mashups"; } }
        public string Descriptions { get { return "Mashup of DotNet News"; } }
        public int TwitterMaxSearch { get { return 10; } }
    }
}
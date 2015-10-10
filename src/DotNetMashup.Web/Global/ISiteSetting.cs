using System.Collections.Generic;

namespace DotNetMashup.Web.Global
{
    public interface ISiteSetting
    {
        List<string> Categories { get; }
        short AmountPerPage { get; }
        string Title { get; }
        string Descriptions { get; }
        int TwitterMaxSearch { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMashup.Web.Global
{
    public interface ISiteSetting
    {
        List<string> Categories { get; }
        short AmountPerPage { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMashup.Web.Model;

namespace DotNetMashup.Web.Factories
{
    public interface IFactory
    {
        string FactoryName { get; }

        IEnumerable<IExternalData> GetData();
    }
}
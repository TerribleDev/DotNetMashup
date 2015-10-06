using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMashup.Web.Model;

namespace DotNetMashup.Web.Repositories
{
    public interface IRepository
    {
        string FactoryName { get; }

        Task<IEnumerable<IExternalData>> GetData();
    }
}
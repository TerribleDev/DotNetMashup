using System.Collections.Generic;
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
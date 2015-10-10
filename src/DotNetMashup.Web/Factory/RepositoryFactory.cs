using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMashup.Web.Global;
using DotNetMashup.Web.Model;
using DotNetMashup.Web.Repositories;
using Microsoft.Framework.Caching.Memory;
using Microsoft.Framework.Configuration;

namespace DotNetMashup.Web.Factory
{
    public class RepositoryFactory
    {
        private readonly IMemoryCache cache;
        private List<IRepository> Repos;
        private const string cacheKey = "data";

        public RepositoryFactory(IEnumerable<IBlogMetaData> data, ISiteSetting setting, IMemoryCache cache, IConfiguration config)
        {
            if(data == null)
            {
                throw new ArgumentNullException("data");
            }
            if(setting == null)
            {
                throw new ArgumentNullException("setting");
            }
            if(cache == null)
            {
                throw new ArgumentNullException("cache");
            }
            Repos = new List<IRepository>()
            {
                new GitHubRepository(config),
                new BlogPostRepository(data, setting)
            };
            this.cache = cache;
        }

        public async Task<IEnumerable<IExternalData>> GetData()
        {
            var cachedData = this.cache.Get<IEnumerable<IExternalData>>(cacheKey);
            if(cachedData != null && cachedData.Any()) return cachedData;
            var tasks = Repos.Select(a => a.GetData());
            await Task.WhenAll(tasks);
            var result = tasks.SelectMany(a => a.Result).ToList();
            cache.Set(cacheKey, result, new MemoryCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddHours(4) });
            return result;
        }
    }
}
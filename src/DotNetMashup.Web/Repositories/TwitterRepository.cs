using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotNetMashup.Web.Extensions;
using DotNetMashup.Web.Global;
using DotNetMashup.Web.Model;
using Microsoft.Framework.Configuration;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Parameters;

namespace DotNetMashup.Web.Repositories
{
    public class TwitterRepository : IRepository
    {
        private readonly IConfiguration config;
        private readonly ISiteSetting SiteSetting;
        private Regex filter = new Regex("(job|looking for|help|apply|career|careers|hire|hiring)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public TwitterRepository(ISiteSetting siteSetting, IConfiguration config)
        {
            this.SiteSetting = siteSetting;
            this.config = config;
        }

        public string FactoryName
        {
            get
            {
                return "Twitter";
            }
        }

        public Task<IEnumerable<IExternalData>> GetData()
        {
            ExceptionHandler.SwallowWebExceptions = true;
            var creds = new TwitterCredentials(config["twitterkey"], config["twittersecret"], config["twittertokenKey"], config["twittertokenSecret"]);
            Auth.SetCredentials(creds);

            return Task.Run(() => SiteSetting
                 .Categories
                 .Select(a => a.TrimAll())
                 .Where(a => !a.Equals("c#"))
                 .AsParallel()
                 .Select(a => Search.SearchTweets(new TweetSearchParameters("#" + a) { Lang = Language.English, MaximumNumberOfResults = 100, TweetSearchType = Tweetinvi.Core.Interfaces.Parameters.TweetSearchType.OriginalTweetsOnly }))
                 .SelectMany(a => a)
                 .Where(a => !a.IsRetweet && !filter.IsMatch(a.Text) && !a.Hashtags.Any(b => filter.IsMatch(b.Text)))
                 .GroupBy(a => a.CreatedAt.DayOfYear + a.CreatedAt.Year)
                 .SelectMany(a => a.Distinct().Take(3))
                 .Select(a => a.ToTwitterData())
                 .OrderByDescending(a => a.PublishedDate)
                 .Cast<IExternalData>()
                 .ToList()
                 .AsEnumerable());
        }
    }
}
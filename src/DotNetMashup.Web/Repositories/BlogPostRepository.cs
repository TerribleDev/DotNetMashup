using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using DotNetMashup.Web.Extensions;
using DotNetMashup.Web.Global;
using DotNetMashup.Web.Model;
using Microsoft.Framework.Caching.Memory;

namespace DotNetMashup.Web.Repositories
{
    public class BlogPostRepository : IRepository
    {
        private readonly ISiteSetting setting;

        private readonly IMemoryCache cache;
        private readonly IEnumerable<IBlogMetaData> _data;

        public BlogPostRepository(IEnumerable<IBlogMetaData> data, ISiteSetting setting)
        {
            this._data = data;
            this.setting = setting;
        }

        public string FactoryName
        {
            get
            {
                return "BlogPost";
            }
        }

        public async Task<IEnumerable<IExternalData>> GetData()
        {
            var syndicationFeeds = await GetSyndicationFeeds(_data);

            var data = syndicationFeeds
               .SelectMany(pair => pair.Value.Items, (pair, item) => new { Id = pair.Key, Item = item })
               .Where(x => x.Item.Categories.Any(category => setting.Categories.Any(setting => string.Equals(setting, category.Name, StringComparison.OrdinalIgnoreCase))))
               .Select(x =>
               {
                   var metaauthor = _data.First(y => y.Id == x.Id);
                   var authorname = metaauthor.AuthorName;
                   var authoremail = metaauthor.AuthorEmail;

                   var link = x.Item.Links.FirstOrDefault(y => y.RelationshipType == "alternate");
                   var locallink = string.Empty;
                   if(link != null)
                   {
                       locallink = link.Uri.Segments.Last();
                       if(locallink.Contains("."))
                       {
                           locallink = locallink.Substring(0, locallink.IndexOf(".", System.StringComparison.Ordinal));
                       }
                   }

                   var originallink = link == null ? string.Empty : link.Uri.AbsoluteUri;

                   var summary = x.Item.Summary == null
                       ? ((TextSyndicationContent)x.Item.Content).Text
                       : x.Item.Summary.Text;

                   var truncatedSummary = summary.TruncateHtml(700, "");

                   var encodedcontent = x.Item.ElementExtensions.ReadElementExtensions<string>("encoded",
                       "http://purl.org/rss/1.0/modules/content/");

                   var content = string.Empty;

                   if(encodedcontent.Any())
                   {
                       content = encodedcontent.First();
                   }
                   else if(x.Item.Content != null)
                   {
                       content = ((TextSyndicationContent)x.Item.Content).Text;
                   }
                   else
                   {
                       content = summary;
                   }

                   return new BlogPostExternalData
                   {
                       Title = x.Item.Title.Text,
                       Summary = truncatedSummary,
                       Author = new Author { Email = authoremail, Name = authorname, ImageUrl = metaauthor.ImageUrl, AuthorUrl = metaauthor.FeedUrl },
                       Localink = locallink,
                       OriginalLink = originallink,
                       PublishedDate = x.Item.PublishDate.DateTime,
                       Content = content
                   };
               });
            return data;
        }

        private async static Task<IEnumerable<KeyValuePair<string, SyndicationFeed>>> GetSyndicationFeeds(IEnumerable<IBlogMetaData> metadataEntries)
        {
            var syndicationFeeds = new List<KeyValuePair<string, SyndicationFeed>>();
            foreach(var metadata in metadataEntries)
            {
                syndicationFeeds.AddRange(await GetFeed(metadata.FeedUrl, metadata.Id, syndicationFeeds));
            }

            return syndicationFeeds;
        }

        private async static Task<List<KeyValuePair<string, SyndicationFeed>>> GetFeed(string url, string id, List<KeyValuePair<string, SyndicationFeed>> syndicationFeeds)
        {
            var feeds = new List<KeyValuePair<string, SyndicationFeed>>();
            try
            {
                SyndicationFeed feed = null;
                await Task.Run(() =>
                {
                    using(var reader = XmlReader.Create(url))
                    {
                        feed = SyndicationFeed.Load(reader);
                    }
                });

                if(feed != null)
                {
                    feeds.Add(new KeyValuePair<string, SyndicationFeed>(id, feed));
                    if(feed.Links.Any(x => x.RelationshipType == "next"))
                    {
                        foreach(var pagingLink in feed.Links.Where(x => x.RelationshipType == "next"))
                        {
                            feeds.AddRange(await GetFeed(pagingLink.Uri.AbsoluteUri, id, syndicationFeeds));
                        }
                    }
                }
            }
            catch(WebException)
            {
                //Unable to load RSS feed
            }
            catch(XmlException)
            {
                //Unable to load RSS feed
            }
            return feeds;
        }
    }
}
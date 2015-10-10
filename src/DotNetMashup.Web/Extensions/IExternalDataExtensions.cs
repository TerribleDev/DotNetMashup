using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using DotNetMashup.Web.Global;
using DotNetMashup.Web.Model;

namespace DotNetMashup.Web.Extensions
{
    public static class IExternalDataExtensions
    {
        public static SyndicationFeed ToRss(this IEnumerable<IExternalData> data)
        {
            var settings = new SiteSettings();
            var feed = new SyndicationFeed
            {
                Title = new TextSyndicationContent(settings.Title),
                Id = "DotNet Mashup",
                LastUpdatedTime = data.OrderByDescending(a => a.PublishedDate).FirstOrDefault().PublishedDate,
                Description = new TextSyndicationContent(settings.Descriptions)
            };
            var authors = data.Select(a => a.Author)
                .Distinct()
                .Select(a => new SyndicationPerson() { Name = a.Name, Email = a.Email, Uri = a.AuthorUrl });
            authors.ForEach(a => feed.Authors.Add(a));
            authors.ForEach(a => feed.Contributors.Add(a));
            feed.Links.Add(new SyndicationLink(new Uri("http://dotnetmashup.azurewebsites.net")));
            feed.Items = data.Select(a => new SyndicationItem(a.Title, a.Content, a.OriginalLink)).ToList();

            return feed;
        }
    }
}
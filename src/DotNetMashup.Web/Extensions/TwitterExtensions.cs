using System;
using System.Threading.Tasks;
using DotNetMashup.Web.Model;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.Models;

namespace DotNetMashup.Web.Extensions
{
    public static class TwitterExtensions
    {
        public static TwitterData ToTwitterData(this ITweet tweet)
        {
            return new TwitterData
            {
                PublishedDate = tweet.CreatedAt,
                Content = tweet.Text,
                OriginalLink = new Uri($"https://twitter.com/statuses/{tweet.Id}"),
                tweet = new Lazy<Task<IOEmbedTweet>>(() => tweet.GenerateOEmbedTweetAsync()),
                Author = new Author
                {
                    Name = tweet.CreatedBy.Name,
                    AuthorUrl = tweet.CreatedBy.Url,
                    Email = string.Empty,
                    ImageUrl = tweet.CreatedBy.ProfileImageUrl400x400
                }
            };
        }
    }
}
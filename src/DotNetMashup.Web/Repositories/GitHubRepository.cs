using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMashup.Web.Model;
using Microsoft.Framework.Configuration;
using Octokit;

namespace DotNetMashup.Web.Repositories
{
    public class GitHubRepository : IRepository
    {
        private readonly IConfiguration config;

        public string FactoryName
        {
            get
            {
                return "Github";
            }
        }

        public GitHubRepository(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<IEnumerable<IExternalData>> GetData()
        {
            CommonMark.CommonMarkSettings.Default.AdditionalFeatures = CommonMark.CommonMarkAdditionalFeatures.All;

            var client = new GitHubClient(new ProductHeaderValue("dotnetmashup"))
            {
                Credentials = new Credentials(config["github"])
            };
            var issues = await client.Issue.GetAllForRepository("aspnet", "Announcements");
            return issues.Select(a => new GithubAnnouncement
            {
                Author = new Model.Author { Name = !string.IsNullOrWhiteSpace(a.User.Name) ? a.User.Name : a.User.Login, AuthorUrl = a.User.Url, ImageUrl = a.User.AvatarUrl, Email = a.User.Email },
                Content = CommonMark.CommonMarkConverter.Convert(a.Body),
                Title = a.Title,
                OriginalLink = a.HtmlUrl,
                IssueNumber = a.Number,
                PublishedDate = a.CreatedAt
            });
        }
    }
}
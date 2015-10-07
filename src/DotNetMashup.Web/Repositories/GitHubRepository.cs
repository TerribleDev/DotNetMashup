using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMashup.Web.Model;
using Octokit;

namespace DotNetMashup.Web.Repositories
{
    public class GitHubRepository : IRepository
    {
        public string FactoryName
        {
            get
            {
                return "Github";
            }
        }

        public async Task<IEnumerable<IExternalData>> GetData()
        {
            var client = new GitHubClient(new ProductHeaderValue("dotnetmashup"));
            var issues = await client.Issue.GetAllForRepository("aspnet", "Announcements");
            return issues.Select(a => new GithubAnnouncement
            {
                Author = new Model.Author { Name = string.IsNullOrWhiteSpace(a.User.Name) ? a.User.Name : a.User.Login, AuthorUrl = a.User.Url, ImageUrl = a.User.AvatarUrl, Email = a.User.Email },
                Content = a.Body,
                Title = a.Title,
                OriginalLink = a.Url.AbsoluteUri
            });
        }
    }
}
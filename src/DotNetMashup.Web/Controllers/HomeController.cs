using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DotNetMashup.Web.Extensions;
using DotNetMashup.Web.Global;
using DotNetMashup.Web.Model;
using DotNetMashup.Web.ViewModel;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Caching.Memory;

namespace DotNetMashup.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly ISiteSetting setting;
        private readonly Factory.RepositoryFactory factory;

        public HomeController(Factory.RepositoryFactory factory, ISiteSetting setting, IMemoryCache cache)
        {
            if(factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            if(setting == null)
            {
                throw new ArgumentNullException("setting");
            }
            this.factory = factory;
            this.setting = setting;
            this.cache = cache;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var factoryData = (await factory.GetData());
            var data = factoryData.OrderByDescending(a => a.PublishedDate).Skip((page - 1) * setting.AmountPerPage).Take(setting.AmountPerPage * 2).ToList();
            if(data.Count < 1)
            {
                return new HttpStatusCodeResult(404);
            }
            return View(new MashupViewModel { CurrentPage = page, NextPage = data.Count > setting.AmountPerPage ? (int?)page + 1 : null, Header = setting.Title, Posts = data.Take(setting.AmountPerPage) });
        }

        public async Task<IActionResult> Tiles(int page = 1)
        {
            var factoryData = (await factory.GetData());
            var data = factoryData.OrderByDescending(a => a.PublishedDate).Skip((page - 1) * setting.AmountPerPage).Take(setting.AmountPerPage * 2).ToList();
            if(data.Count < 1)
            {
                return new HttpStatusCodeResult(404);
            }
            return PartialView("Tiles", data);
        }

        [Route("api/tiles")]
        public async Task<IEnumerable<IExternalData>> GetTiles()
        {
            return (await factory.GetData());
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        [Route("sitemap")]
        public async Task sitemap()
        {
            Response.ContentType = "text/xml";
            var serializer = new XmlSerializer(typeof(Urlset));
            var set = new Urlset();
            var data = await GetTiles();
            var page = 0;
            set.Url = data.Split(this.setting.AmountPerPage).Select(a => new Url()
            {
                Changefreq = "daily",
                Lastmod = a.Select(b => b.PublishedDate).Max().ToString(),
                Loc = Url.Action("Index", "Home", new { page = page++ }, this.Request.Scheme),
                Priority = "1"
            }).ToList();
            serializer.Serialize(Response.Body, set);
        }

        [Route("rss")]
        public async Task rss()
        {
            Response.ContentType = "text/xml";
            var writer = XmlWriter.Create(this.Response.Body, new XmlWriterSettings() { Async = true });
            (await factory.GetData()).ToRss().SaveAsRss20(writer);
            await writer.FlushAsync();
        }
    }
}
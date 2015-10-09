using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMashup.Web.Global;
using DotNetMashup.Web.Repositories;
using DotNetMashup.Web.ViewModel;
using Microsoft.AspNet.Mvc;

namespace DotNetMashup.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISiteSetting setting;
        private readonly Factory.RepositoryFactory factory;

        public HomeController(Factory.RepositoryFactory factory, ISiteSetting setting)
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
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var factoryData = (await factory.GetData());
            var data = factoryData.OrderByDescending(a => a.PublishedDate).Skip((page - 1) * setting.AmountPerPage).Take(setting.AmountPerPage * 2).ToList();
            if(data.Count < 1)
            {
                return new HttpStatusCodeResult(404);
            }
            return View(new MashupViewModel { CurrentPage = page, NextPage = data.Count > setting.AmountPerPage ? (int?)page + 1 : null, Header = "DotNet Mashups", Posts = data.Take(setting.AmountPerPage) });
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

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetMashup.Web.Repositories;
using Microsoft.AspNet.Mvc;

namespace DotNetMashup.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Factory.RepositoryFactory factory;

        public HomeController(Factory.RepositoryFactory factory)
        {
            this.factory = factory;
        }

        public async Task<IActionResult> Index()
        {
            var data = await factory.GetData();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
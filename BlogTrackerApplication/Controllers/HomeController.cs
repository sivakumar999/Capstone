using BlogTrackerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogTrackerApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}
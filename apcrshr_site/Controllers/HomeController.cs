using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CurrentNode = "Home";
            return View();
        }

        public ActionResult ProgramMainConference()
        {
            ViewBag.CurrentNode = "Program";
            return View();
        }

        public ActionResult AboutBackground()
        {
            ViewBag.CurrentNode = "About";
            return View();
        }

        public ActionResult AboutThemeObjective()
        {
            ViewBag.CurrentNode = "About";
            return View();
        }

    }
}

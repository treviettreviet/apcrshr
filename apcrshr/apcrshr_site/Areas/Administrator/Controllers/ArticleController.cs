using Site.Core.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Administrator/Article/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateArticle(string menuTitle)
        {
            return View();
        }

        public ActionResult SaveArticle(ArticleModel article)
        {
            return View();
        }

    }
}

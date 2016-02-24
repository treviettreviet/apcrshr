using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Controllers
{
    public class HomeController : BaseController
    {
        private IArticleService _articleService;
        private INewsService _newsService;

        public HomeController()
        {
            this._articleService = new ArticleService();
            this._newsService = new NewsService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.CurrentNode = "Home";
            FindAllItemReponse<NewsModel> newsReponse = _newsService.GetTopNews(3, this.culture);
            ViewBag.TopNews = newsReponse.Items;
            return View();
        }

        [HttpGet]
        public ActionResult ArticleView(string ActionURL, int pageIndex = 1)
        {
            FindItemReponse<MenuModel> response = _menuCategoryService.FindByActionURL(ActionURL);
            if (response.Item != null)
            {
                ViewBag.Title = response.Item.Title;

                if (response.Item.Parent != null)
                {
                    ViewBag.CurrentNode = response.Item.Parent.ActionURL;
                }
                else
                {
                    ViewBag.CurrentNode = response.Item.ActionURL;
                }

                //Find article
                FindAllItemReponse<ArticleModel> articleResponse = _articleService.GetArticles(Constants.Constants.PAGE_SIZE, pageIndex, culture, response.Item.MenuID);
                return View(articleResponse);
            }
            return View(new FindAllItemReponse<ArticleModel>());
        }
    }
}

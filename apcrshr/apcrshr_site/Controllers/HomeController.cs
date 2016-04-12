using Site.Core.DataModel.Enum;
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
        private IVideoService _videoService;
        private IUserService _userService;
        private IHomeService _homeService;
        private ISliderService _sliderService;

        public HomeController()
        {
            this._articleService = new ArticleService();
            this._newsService = new NewsService();
            this._videoService = new VideoService();
            this._userService = new UserService();
            this._homeService = new HomeService();
            this._sliderService = new SliderService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.CurrentNode = "Home";
            FindAllItemReponse<NewsModel> newsReponse = _newsService.GetTopNews(3, this.culture);
            ViewBag.TopNews = newsReponse.Items;

            FindAllItemReponse<VideoModel> videoReponse = _videoService.GetTopVideo(1, this.culture);
            ViewBag.TopVideo = videoReponse.Items;

            FindAllItemReponse<SliderModel> SliderReponse = _sliderService.GetTopSlider(9, this.culture);
            ViewBag.TopSlider = SliderReponse.Items;
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

        [HttpPost]
        public ActionResult Search(string KeyWord, int pageIndex = 1)
        {
            SearchResultResponse response = _homeService.Search(string.Format("%{0}%", KeyWord), Constants.Constants.PAGE_SIZE, pageIndex);
            response.KeySearch = KeyWord;
            response.PageIndex = pageIndex;
            return View(response);
        }


    }
}

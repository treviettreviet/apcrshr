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
    public class NewsController : BaseController
    {
        private INewsService _newsService;

        public NewsController()
        {
            this._newsService = new NewsService();
        }

        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewNews(string ActionURL, int pageIndex = 1)
        {
            FindItemReponse<NewsModel> response = _newsService.FindNewsByActionURL(ActionURL);
            FindAllItemReponse<NewsModel> newsReponse = new FindAllItemReponse<NewsModel>();
            List<NewsModel> list = new List<NewsModel>();
            string subActionURL = string.Empty;
            if (response.Item != null)
            {
                list.Add(response.Item);
                FindAllItemReponse<NewsModel> temp = _newsService.GetRelatedNews(response.Item.CreatedDate, Constants.Constants.PAGE_SIZE, pageIndex, culture);
                if (temp.Items != null)
                {
                    list.AddRange(temp.Items);
                    newsReponse.Items = list;
                    newsReponse.Count = temp.Count;
                }
            }
            return View(newsReponse);
        }
    }
}

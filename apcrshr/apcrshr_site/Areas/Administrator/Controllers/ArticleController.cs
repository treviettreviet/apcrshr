using apcrshr_site.Filters;
using apcrshr_site.Helper;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class ArticleController : Controller
    {
        private IMenuCategoryService _menuCategoryService;
        private IArticleService _articleService;

        public ArticleController()
        {
            this._menuCategoryService = new MenuCategoryService();
            this._articleService = new ArticleService();
        }

        //
        // GET: /Administrator/Article/
        [SessionFilter]
        public ActionResult Index()
        {
            return View();
        }

        [SessionFilter]
        public ActionResult CreateArticle(string menuTitle)
        {
            FindItemReponse<MenuModel> findParentMenu = _menuCategoryService.FindByTitle(menuTitle);
            if (findParentMenu.Item == null)
            {
                ViewBag.ErrorMessage = string.Format(Resources.AdminResource.msg_menuCategoryNotFound, menuTitle);
            }
            return View();
        }

        [SessionFilter]
        [ValidateAntiForgeryToken]
        public JsonResult SaveArticle(ArticleModel article, string menuTitle)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();

            FindItemReponse<MenuModel> findParentMenu = _menuCategoryService.FindByTitle(menuTitle);
            if (findParentMenu.Item == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Error, message = string.Format(Resources.AdminResource.msg_menuCategoryNotFound, menuTitle) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                article.Title = article.Title.Length > 200 ? article.Title.Substring(0, 100) + "..." : article.Title;
                if (article.ShortContent != null)
                {
                    article.ShortContent = article.ShortContent.Length > 300 ? article.ShortContent.Substring(0, 296) + "..." : article.ShortContent;
                }
                article.ArticleID = Guid.NewGuid().ToString();
                article.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(article.Title), UrlSlugger.Get8Digits());
                article.CreatedDate = DateTime.Now;
                article.CreatedBy = userSession.UserID;
                article.MenuID = findParentMenu.Item.MenuID;

                response = _articleService.CreateArticle(article);
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

    }
}

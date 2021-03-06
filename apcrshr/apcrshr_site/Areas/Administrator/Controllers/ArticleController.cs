﻿using apcrshr_site.Filters;
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
using System.IO;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class ArticleController : Controller
    {
        private IMenuCategoryService _menuCategoryService;
        private IArticleService _articleService;

        public ArticleController()
        {
            ViewBag.CurrentNode = "Article";
            this._menuCategoryService = new MenuCategoryService();
            this._articleService = new ArticleService();
        }

        //
        // GET: /Administrator/Article/
        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<ArticleModel> response = _articleService.GetArticles();
            return View(response.Items);
        }

        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult CreateArticle(string menuTitle = "")
        {
            if (!string.IsNullOrEmpty(menuTitle))
            {
                FindItemReponse<MenuModel> findParentMenu = _menuCategoryService.FindByTitle(menuTitle);
                if (findParentMenu.Item == null)
                {
                    ViewBag.ErrorMessage = string.Format(Resources.AdminResource.msg_menuCategoryNotFound, menuTitle);
                }
            }
            return View();
        }

        [SessionFilter]
        
        public JsonResult SaveArticle(ArticleModel article, string menuTitle, HttpPostedFileBase file)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();

            string menuID = null;
            string url = string.Empty;

            if (!string.IsNullOrEmpty(menuTitle))
            {
                FindItemReponse<MenuModel> findParentMenu = _menuCategoryService.FindByTitle(menuTitle);
                if (findParentMenu.Item == null)
                {
                    return Json(new { errorCode = (int)ErrorCode.Error, message = string.Format(Resources.AdminResource.msg_menuCategoryNotFound, menuTitle) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    menuID = findParentMenu.Item.MenuID;
                }
            }

            //Create article
            article.Title = article.Title.Length > 200 ? article.Title.Substring(0, 100) + "..." : article.Title;
            if (article.ShortContent != null)
            {
                article.ShortContent = article.ShortContent.Length > 300 ? article.ShortContent.Substring(0, 296) + "..." : article.ShortContent;
            }
            article.ArticleID = Guid.NewGuid().ToString();
            article.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(article.Title), UrlSlugger.Get8Digits());
            article.CreatedDate = DateTime.Now;
            article.CreatedBy = userSession.UserID;
            article.MenuID = menuID;

            if (string.IsNullOrEmpty(menuID))
            {
                url = string.Format("{0}://{1}:{2}/Home/ArticleView/{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port,article.ActionURL );
            }

            response = _articleService.CreateArticle(article);

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (file != null)
                {
                    //Create folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/article/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/article/"));
                        }
                    }
                    catch (Exception) { }

                    string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string filename = file.FileName.Substring(0, file.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    file.SaveAs(Server.MapPath("~/Content/upload/images/article/" + filename + extension));

                    article.ImageURL = "/Content/upload/images/article/" + filename + extension;
                    _articleService.UpdateArticle(article);
                }
            }

            return Json(new { errorCode = response.ErrorCode, message = response.Message, url = url }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteArticle(string articleID)
        {
            BaseResponse response = _articleService.DeleteArticle(articleID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult UpdateArticle(string articleID)
        {
            FindItemReponse<ArticleModel> response = _articleService.FindArticleByID(articleID);
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]
        
        public JsonResult SaveUpdateArticle(ArticleModel article)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            //if (!string.IsNullOrEmpty(article.MenuID))
            //{
            //    article.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(article.Title), UrlSlugger.Get8Digits());
            //}
            article.UpdatedBy = userSession.UserID;
            BaseResponse response = _articleService.UpdateArticle(article);

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}

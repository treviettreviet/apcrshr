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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class AdminNewsController : Controller
    {
        private INewsService _newsService;

        public AdminNewsController()
        {
            ViewBag.CurrentNode = "AdminNews";
            this._newsService = new NewsService();
        }

        //
        // GET: /Administrator/AdminNews/
        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<NewsModel> response = _newsService.GetNews();
            return View(response.Items);
        }

        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult CreateNews()
        {
            return View();
        }

        [SessionFilter]
        
        public JsonResult SaveNews(NewsModel news, HttpPostedFileBase imageFile)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();

            news.Title = news.Title.Length > 200 ? news.Title.Substring(0, 100) + "..." : news.Title;
            news.ShortContent = news.ShortContent.Length > 300 ? news.ShortContent.Substring(0, 296) + "..." : news.ShortContent;
            news.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(news.Title), UrlSlugger.Get8Digits());
            news.CreatedDate = DateTime.Now;
            news.NewsID = Guid.NewGuid().ToString();
            news.CreatedBy = userSession != null ? userSession.UserID : string.Empty;

            response = _newsService.CreateNews(news);

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/news/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/news/"));
                        }
                    }
                    catch (Exception) { }

                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/news/" + filename + extension));

                    news.ThumbnailURL = "/Content/upload/images/news/" + filename + extension;
                    _newsService.UpdateNews(news);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteNews(string newsID)
        {
            FindItemReponse<NewsModel> newsResponse = _newsService.FindNewsByID(newsID);
            if (newsResponse.Item != null)
            {
                try
                {
                    if (System.IO.File.Exists(Server.MapPath(newsResponse.Item.ThumbnailURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(newsResponse.Item.ThumbnailURL));
                    }
                }
                catch (Exception) { }
            }
            BaseResponse response = _newsService.DeleteNews(newsID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult UpdateNews(string newsID)
        {
            FindItemReponse<NewsModel> response = _newsService.GetNewsByID(newsID);
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]
        
        public JsonResult SaveUpdateNews(NewsModel news, HttpPostedFileBase imageFile)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            //news.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(news.Title), UrlSlugger.Get8Digits());
            news.UpdatedBy = userSession.UserID;
            BaseResponse response = _newsService.UpdateNews(news);

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/news/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/news/"));
                        }
                    }
                    catch (Exception) { }

                    if (!string.IsNullOrEmpty(news.ThumbnailURL))
                    {
                        if (System.IO.File.Exists(Server.MapPath(news.ThumbnailURL)))
                        {
                            System.IO.File.Delete(Server.MapPath(news.ThumbnailURL));
                        }
                    }

                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/news/" + filename + extension));

                    news.ThumbnailURL = "/Content/upload/images/news/" + filename + extension;
                    _newsService.UpdateNews(news);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}

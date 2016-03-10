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
    public class AdminVideoController : Controller
    {
        private IVideoService _videoService;

        public AdminVideoController()
        {
            ViewBag.CurrentNode = "AdminVideo";
            this._videoService = new VideoService();
        }
        //
        // GET: /Administrator/AdminVideo/
        [SessionFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<VideoModel> response = _videoService.GetVideo();
            return View(response.Items);
        }

        [SessionFilter]
        public ActionResult CreateVideo()
        {
            return View();
        }

        [SessionFilter]
        [ValidateAntiForgeryToken]
        public JsonResult SaveVideo(VideoModel video)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();
            video.Title = video.Title.Length > 200 ? video.Title.Substring(0, 100) + "..." : video.Title;
            if (!string.IsNullOrEmpty(video.Shortcontent))
            {
                video.Shortcontent = video.Shortcontent.Length > 300 ? video.Shortcontent.Substring(0, 296) + "..." : video.Shortcontent;
            }
            else
            {
                video.Shortcontent = null;
            }
            video.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(video.Title), UrlSlugger.Get8Digits());
            video.CreatedDate = DateTime.Now;
            video.VideoID = Guid.NewGuid().ToString();
            video.CreatedBy = userSession != null ? userSession.UserID : string.Empty;
            response = _videoService.CreateVideo(video);

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [HttpGet]
        public ActionResult UpdateVideo(string videoID)
        {
            FindItemReponse<VideoModel> response = _videoService.FindVideoByID(videoID);
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveUpdateVideo(VideoModel video)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            video.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(video.Title), UrlSlugger.Get8Digits());
            video.UpdatedBy = userSession.UserID;
            video.UpdatedDate = DateTime.Now;
            BaseResponse response = _videoService.UpdateVideo(video);

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult deleteVideo(string VideoID)
        {
            FindItemReponse<VideoModel> ConResponse = _videoService.FindVideoByID(VideoID);

            BaseResponse response = _videoService.DeleteVideo(VideoID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}

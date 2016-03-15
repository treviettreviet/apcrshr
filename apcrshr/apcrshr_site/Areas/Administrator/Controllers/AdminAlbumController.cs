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
    public class AdminAlbumController : Controller
    {

        private IAlbumService _albumService;
        public AdminAlbumController()
        {
            ViewBag.CurrentNode = "AdminAlbum";
            this._albumService = new AlbumService();
        }
        //
        // GET: /Administrator/AdminAlbum/

        public ActionResult Index()
        {
            FindAllItemReponse<AlbumModel> response = _albumService.GetAlbum();
            return View(response.Items);
        }

        [SessionFilter]
        public ActionResult CreateAlbum()
        {
            return View();
        }

        [SessionFilter]
        [ValidateAntiForgeryToken]
        public JsonResult SaveAlbum(AlbumModel album)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();

            album.Title = album.Title.Length > 200 ? album.Title.Substring(0, 100) + "..." : album.Title;
            if (!string.IsNullOrEmpty(album.Description))
            {
                album.Description = album.Description.Length > 300 ? album.Description.Substring(0, 296) + "..." : album.Description;
            }
            else
            {
                album.Description = null;
            }
            album.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(album.Title), UrlSlugger.Get8Digits());
            album.CreatedDate = DateTime.Now;
            album.AlbumID = Guid.NewGuid().ToString();
            album.CreatedBy = userSession != null ? userSession.UserID : string.Empty;

            response = _albumService.CreateAlbum(album);
            
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [HttpGet]
        public ActionResult UpdateAlbum(string AlbumID)
        {
            FindItemReponse<AlbumModel> response = _albumService.FindAlbumByID(AlbumID);
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveUpdateAlbum(AlbumModel album)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            album.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(album.Title), UrlSlugger.Get8Digits());
            album.UpdatedBy = userSession.UserID;
            album.UpdatedDate = DateTime.Now;
            BaseResponse response = _albumService.UpdateAlbum(album);

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteAlbum(string albumID)
        {
            FindItemReponse<AlbumModel> albumResponse = _albumService.FindAlbumByID(albumID);
            BaseResponse response = _albumService.DeleteAlbum(albumID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}

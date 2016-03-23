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
    public class AdminPhotoController : Controller
    {
        private IPhotoService _photoService;
        private IAlbumService _albumService;
        public AdminPhotoController()
        {
            ViewBag.CurrentNode = "AdminPhoto";
            this._photoService = new PhotoService();
            this._albumService = new AlbumService();
        }
        //
        // GET: /Administrator/AdminPhoto/

        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<PhotoModel> response = _photoService.GetPhoto();
            return View(response.Items);
        }

        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult CreatePhoto()
        {
            var response = _albumService.GetAlbum();
            if (response.ErrorCode == (int)ErrorCode.None && response.Items.Count > 0)
            {
                var albums = new SelectList(response.Items, "AlbumID", "Title");
                ViewBag.Albums = albums;
                return View(new PhotoModel());
            }
            response.ErrorCode = (int)ErrorCode.Redirect;
            response.Message = "Chưa Có Album.";
            TempData["Message"] = response;
            return RedirectToAction("Index");
        }

        [SessionFilter]
        [ValidateAntiForgeryToken]
        public JsonResult SavePhoto(PhotoModel photo, HttpPostedFileBase imageFile)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();

            photo.Title = photo.Title.Length > 200 ? photo.Title.Substring(0, 100) + "..." : photo.Title;
            if (!string.IsNullOrEmpty(photo.Description))
            {
                photo.Description = photo.Description.Length > 300 ? photo.Description.Substring(0, 296) + "..." : photo.Description;
            }
            else
            {
                photo.Description = null;
            }
            photo.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(photo.Title), UrlSlugger.Get8Digits());
            photo.CreatedDate = DateTime.Now;
            photo.PhotoID = Guid.NewGuid().ToString();
            photo.ImageURL = "";
            photo.CreatedBy = userSession != null ? userSession.UserID : string.Empty;

            response = _photoService.CreatePhoto(photo);
            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/Photo/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/Photo/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/Photo/" + filename + extension));
                    photo.ImageURL = "/Content/upload/images/Photo/" + filename + extension;
                    _photoService.UpdatePhoto(photo);
                }
                
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [HttpGet]
        [AuthorizationFilter]
        public ActionResult UpdatePhoto(string photoID)
        {
            var albumResponse = _albumService.GetAlbum();
            FindItemReponse<PhotoModel> photoResponse = _photoService.FindPhotoByID(photoID);
            if (albumResponse.ErrorCode == (int)ErrorCode.None && albumResponse.Items.Count > 0)
            {
                var albums = new SelectList(albumResponse.Items, "AlbumID", "Title");
                ViewBag.Albums = albums;
                return View(photoResponse.Item);
            }
            albumResponse.ErrorCode = (int)ErrorCode.Redirect;
            albumResponse.Message = "Chưa Có Album.";
            TempData["Message"] = albumResponse;
            return RedirectToAction("Index"); 
        }

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveUpdatePhoto(PhotoModel photo, HttpPostedFileBase imageFile)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            photo.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(photo.Title), UrlSlugger.Get8Digits());
            photo.UpdatedBy = userSession.UserID;
            photo.UpdatedDate = DateTime.Now;
            BaseResponse response = _photoService.UpdatePhoto(photo);

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/Photo/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/Photo/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/Photo/" + filename + extension));
                    photo.ImageURL = "/Content/upload/images/Photo/" + filename + extension;
                    _photoService.UpdatePhoto(photo);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeletePhoto(string photoID)
        {
            FindItemReponse<PhotoModel> photoResponse = _photoService.FindPhotoByID(photoID);
            if (photoResponse.Item != null)
            {
                try
                {
                    if (System.IO.File.Exists(Server.MapPath(photoResponse.Item.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(photoResponse.Item.ImageURL));
                    }
                }
                catch (Exception) { }
            }
            BaseResponse response = _photoService.DeletePhoto(photoID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}

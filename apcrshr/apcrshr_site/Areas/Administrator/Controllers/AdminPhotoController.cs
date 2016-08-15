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
            FindAllItemReponse<AlbumModel> albumResponse = _albumService.GetAlbum();
            ViewBag.AlbumList = albumResponse.Items;
            return View(response.Items);
        }

        [AuthorizationFilter]
        [SessionFilter]
        [HttpGet]
        public ActionResult CreatePhoto(string AlbumID)
        {
            return View();
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
            //photo.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(photo.Title), UrlSlugger.Get8Digits());
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

        [SessionFilter]
        [HttpPost]
        public ActionResult Upload(int? chunk, string name, string AlbumID)
        {
            FindItemReponse<AlbumModel> albumResponse = _albumService.FindAlbumByID(AlbumID);
            if (albumResponse.Item == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            chunk = chunk ?? 0;

            InsertResponse response = new InsertResponse();

            PhotoModel photo = new PhotoModel();

            if (chunk == 0)
            {
                photo.Title = name.Length > 200 ? name.Substring(0, 100) + "..." : name;
                photo.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(photo.Title), UrlSlugger.Get8Digits());
                photo.CreatedDate = DateTime.Now;
                photo.PhotoID = Guid.NewGuid().ToString();
                photo.ImageURL = "";
                photo.CreatedBy = userSession != null ? userSession.UserID : string.Empty;
                photo.AlbumID = AlbumID;

                response = _photoService.CreatePhoto(photo);
            }

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                var fileUpload = Request.Files[0];

                //Image
                if (fileUpload != null)
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

                    var uploadPath = Server.MapPath("~/Content/upload/images/Photo/");
                    string extension = name.Substring(name.LastIndexOf("."));
                    string filename = name.Substring(0, name.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());

                    using (var fs = new FileStream(Path.Combine(uploadPath, string.Format("{0}{1}", filename, extension)), chunk == 0 ? FileMode.Create : FileMode.Append))
                    {
                        var buffer = new byte[fileUpload.InputStream.Length];
                        fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                        fs.Write(buffer, 0, buffer.Length);
                    }

                    if (chunk == 0)
                    {
                        photo.ImageURL = "/Content/upload/images/Photo/" + filename + extension;
                        _photoService.UpdatePhoto(photo);
                    }
                }

            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}

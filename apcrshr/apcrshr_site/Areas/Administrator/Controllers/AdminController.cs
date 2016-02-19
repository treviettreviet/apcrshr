using Site.Core.Common.Ultil;
using Site.Core.Common.Ultil.Enum;
using Site.Core.Common.Ultil.Security;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using apcrshr_site.Filters;
using apcrshr_site.Helper;
using Site.Core.Service.Implementation;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class AdminController : Controller
    {
        private IAdminService _adminService;
        private IMenuCategoryService _menuCategoryService;
        private IUploadService _uploadService;

        public AdminController()
        {
            this._adminService = new AdminService();
            this._menuCategoryService = new MenuCategoryService();
            this._uploadService = new UploadService();
        }

        [SessionFilter]
        public ActionResult Index()
        {
            if (this.Session["UserName"] != null)
            {
                ViewBag.UserName = this.Session["UserName"].ToString();
            }
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            FindAllItemReponse<MenuModel> menuReponse = _menuCategoryService.FindAllMenus();

            return View(menuReponse.Items);
        }

        public ActionResult Logout()
        {
            string sessionId = string.Empty;
            if (this.Session["SessionID"] != null)
            {
                sessionId = this.Session["SessionID"].ToString();
            }
            if (string.IsNullOrEmpty(sessionId))
            {
                RedirectToAction("Login", "AdminHome");
            }
            BaseResponse response = _adminService.Logout(sessionId);
            this.Session["SessionID"] = null;
            this.Session["UserName"] = null;
            return RedirectToAction("Login", "AdminHome");
        }

        [SessionFilter]
        public ActionResult AdminList()
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            FindAllItemReponse<AdminModel> response = _adminService.GetAdminsExceptMe(userSession.UserID);
            if (response.Items == null)
            {
                response.Items = new List<AdminModel>();
            }
            return View(response.Items);
        }

        [SessionFilter]
        public ActionResult AddNewAdmin()
        {
            return View();
        }

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(AdminModel admin)
        {
            if (ModelState.IsValid)
            {
                admin.AdminID = Guid.NewGuid().ToString();
                BaseResponse response = _adminService.CreateAdmin(admin);

                ViewBag.Message = response;
            }
            return View("AddNewAdmin");
        }

        [SessionFilter]
        [HttpGet]
        public ActionResult UpdateAdmin(string adminID)
        {
            FindItemReponse<AdminModel> response = _adminService.FindAdminByID(adminID);
            ViewBag.AdminId = response.Item.AdminID;
            return View(response.Item);
        }

        [SessionFilter]
        [HttpGet]
        public ActionResult UpdateProfile(string adminID)
        {
            FindItemReponse<AdminModel> response = _adminService.FindAdminByID(adminID);
            ViewBag.AdminId = response.Item.AdminID;
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUpdateProfileAdmin(AdminModel admin)
        {
            if (ModelState.IsValid)
            {
                BaseResponse response = _adminService.UpdateAdmin(admin);
                ViewBag.Message = response;
            }
            return View("UpdateProfile", admin);
        }

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUpdateAdmin(AdminModel admin)
        {
            if (ModelState.IsValid)
            {
                BaseResponse response = _adminService.UpdateAdmin(admin);
                ViewBag.Message = response;
            }
            return View("UpdateAdmin", admin);
        }

        [HttpGet]
        public JsonResult DeleteAdmin(string adminID)
        {
            BaseResponse response = _adminService.DeleteAdmin(adminID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateCategoryMenu(MenuModel menu, string parentTitle)
        {
            FindItemReponse<MenuModel> findParentMenu = _menuCategoryService.FindByTitle(parentTitle);
            if (findParentMenu.Item != null)
            {
                menu.ParentID = findParentMenu.Item.MenuID;
            }
            menu.MenuID = Guid.NewGuid().ToString();
            menu.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(menu.Title), UrlSlugger.Get8Digits());
            menu.CreatedDate = DateTime.Now;

            InsertResponse response = _menuCategoryService.CreateMenu(menu);

            return Json(new { errorcode = response.ErrorCode, message = response.Message, title = menu.Title }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UploadFiles(HttpPostedFileBase file)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();

            string url = string.Empty;
            string msg = string.Empty;
            int errorcode = 0;

            if (file != null)
            {
                try
                {
                    //Create folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/files/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/files/"));
                        }
                    }
                    catch (Exception) {}

                    string filename = file.FileName.Substring(0, file.FileName.LastIndexOf("."));
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits()).Replace(" ", "_");
                    string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    file.SaveAs(Server.MapPath("~/Content/upload/files/" + filename + extension));
                    url = string.Format("{0}://{1}:{2}/{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, "Content/upload/files/" + filename + extension);

                    UploadModel upload = new UploadModel();
                    upload.UploadID = Guid.NewGuid().ToString();
                    upload.CreatedBy = userSession.UserID;
                    upload.CreatedDate = DateTime.Now;
                    upload.Title = filename;
                    upload.UploadURL = url;
                    upload.FilePath = "/Content/upload/files/" + filename + extension;

                    response = _uploadService.CreateUpload(upload);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    errorcode = (int)ErrorCode.Error;
                }
            }

            return Json(new { errorcode = errorcode, message = msg, url = url }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowUploads()
        {
            FindAllItemReponse<UploadModel> response = _uploadService.GetUploads();
            return Json(new { errorcode = response.ErrorCode, message = response.Message, data = response.Items }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteUpload(string uploadID)
        {
            FindItemReponse<UploadModel> uploadResponse = _uploadService.GetUploadByID(uploadID);
            if (uploadResponse.Item != null)
            {
                try
                {
                    if (System.IO.File.Exists(Server.MapPath(uploadResponse.Item.FilePath)))
                    {
                        System.IO.File.Delete(Server.MapPath(uploadResponse.Item.FilePath));
                    }
                }
                catch (Exception) { }
            }
            BaseResponse response = _uploadService.DeleteUpload(uploadID);
            return Json(new { errorcode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}

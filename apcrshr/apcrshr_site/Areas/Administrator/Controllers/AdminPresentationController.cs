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
    public class AdminPresentationController : Controller
    {

        private IPresentationService _presentationService;
        public AdminPresentationController()
        {
            ViewBag.CurrentNode = "AdminPresentation";
            this._presentationService = new PresentationService();
        }

        //
        // GET: /Administrator/AdminPresentation/
        [SessionFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<PresentationModel> response = _presentationService.GetPresentation();
            return View(response.Items);
        }

        [SessionFilter]
        public ActionResult CreatePresentation()
        {
            return View();
        }

        [SessionFilter]
        [ValidateAntiForgeryToken]
        public JsonResult SavePresentation(PresentationModel presentation, HttpPostedFileBase imageFile, HttpPostedFileBase file)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();

            presentation.Title = presentation.Title.Length > 200 ? presentation.Title.Substring(0, 100) + "..." : presentation.Title;
            if (!string.IsNullOrEmpty(presentation.ShortContent))
            {
                presentation.ShortContent = presentation.ShortContent.Length > 300 ? presentation.ShortContent.Substring(0, 296) + "..." : presentation.ShortContent;
            }
            else
            {
                presentation.ShortContent = null;
            }
            presentation.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(presentation.Title), UrlSlugger.Get8Digits());
            presentation.CreatedDate = DateTime.Now;
            presentation.PresentationID = Guid.NewGuid().ToString();
            presentation.CreatedBy = userSession != null ? userSession.UserID : string.Empty;

            response = _presentationService.CreatePresentation(presentation);
            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/Presentation/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/Presentation/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/Presentation/" + filename + extension));
                    presentation.ImageURL = "/Content/upload/images/Presentation/" + filename + extension;
                    _presentationService.UpdatePresentation(presentation);
                }
                if (file != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/documents/Presentation/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/documents/Presentation/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string filename = file.FileName.Substring(0, file.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    file.SaveAs(Server.MapPath("~/Content/upload/documents/Presentation/" + filename + extension));
                    presentation.AttachmentURL = "/Content/upload/documents/Presentation/" + filename + extension;
                    _presentationService.UpdatePresentation(presentation);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [HttpGet]
        public ActionResult UpdatePresentation(string presentationID)
        {
            FindItemReponse<PresentationModel> response = _presentationService.FindPresentationByID(presentationID);
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveUpdatePresentation(PresentationModel presentation, HttpPostedFileBase imageFile, HttpPostedFileBase file)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            presentation.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(presentation.Title), UrlSlugger.Get8Digits());
            presentation.UpdatedBy = userSession.UserID;
            presentation.UpdatedDate = DateTime.Now;
            BaseResponse response = _presentationService.UpdatePresentation(presentation);

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/Presentation/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/Presentation/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/Presentation/" + filename + extension));
                    presentation.ImageURL = "/Content/upload/images/Presentation/" + filename + extension;
                    _presentationService.UpdatePresentation(presentation);
                }
                if (file != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/documents/Presentation/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/documents/Presentation/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string filename = file.FileName.Substring(0, file.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    file.SaveAs(Server.MapPath("~/Content/upload/documents/Presentation/" + filename + extension));
                    presentation.AttachmentURL = "/Content/upload/documents/Presentation/" + filename + extension;
                    _presentationService.UpdatePresentation(presentation);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult DeletePresentation(string presentationID)
        {
            FindItemReponse<PresentationModel> preResponse = _presentationService.FindPresentationByID(presentationID);
            if (preResponse.Item != null)
            {
                try
                {
                    if (System.IO.File.Exists(Server.MapPath(preResponse.Item.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(preResponse.Item.ImageURL));
                    }
                    if (System.IO.File.Exists(Server.MapPath(preResponse.Item.AttachmentURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(preResponse.Item.AttachmentURL));
                    }
                }
                catch (Exception) { }
            }
            BaseResponse response = _presentationService.DeletePresentation(presentationID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

    }
}

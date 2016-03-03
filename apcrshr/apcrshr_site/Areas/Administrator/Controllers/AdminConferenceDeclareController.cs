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
    public class AdminConferenceDeclareController : Controller
    {
        private IConferenceDeclarationService _conferenceService;
        public AdminConferenceDeclareController()
        {
            ViewBag.CurrentNode = "AdminConferenceDelaration";
            this._conferenceService = new ConferenceDeclarationService();
        }

        //
        // GET: /Administrator/AdminConferenceDeclare/
        [SessionFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<ConferenceDeclarationModel> response = _conferenceService.GetConference();
            return View(response.Items);
        }

        [SessionFilter]
        public ActionResult CreateConference()
        {
            return View();
        }
        [SessionFilter]
        [ValidateAntiForgeryToken]
        public JsonResult SaveConference(ConferenceDeclarationModel conference, HttpPostedFileBase imageFile, HttpPostedFileBase file)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            InsertResponse response = new InsertResponse();

            conference.Title = conference.Title.Length > 200 ? conference.Title.Substring(0, 100) + "..." : conference.Title;
            conference.ShortContent = conference.ShortContent.Length > 300 ? conference.ShortContent.Substring(0, 296) + "..." : conference.ShortContent;
            conference.CreatedDate = DateTime.Now;
            conference.ConferenceID = Guid.NewGuid().ToString();
            conference.CreatedBy = userSession != null ? userSession.UserID : string.Empty;

            response = _conferenceService.CreateConference(conference);
            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/Conference/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/Conference/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/Conference/" + filename + extension));
                    conference.ImageURL = "~/Content/upload/images/Conference/" + filename + extension;
                    _conferenceService.UpdateConference(conference);
                }
                if (file != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/documents/Conference/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/documents/Conference/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string filename = file.FileName.Substring(0, file.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    file.SaveAs(Server.MapPath("~/Content/upload/documents/Conference/" + filename + extension));
                    conference.AttachmentURL = "~/Content/upload/documents/Conference/" + filename + extension;
                    _conferenceService.UpdateConference(conference);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }
        [SessionFilter]
        [HttpGet]
        public ActionResult UpdateConference(string conferenceID)
        {
            FindItemReponse<ConferenceDeclarationModel> response = _conferenceService.FindConferenceByID(conferenceID);
            return View(response.Item);
        }
        
        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveUpdateConference(ConferenceDeclarationModel conference, HttpPostedFileBase imageFile, HttpPostedFileBase file)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            conference.UpdatedBy = userSession.UserID;
            conference.UpdatedDate = DateTime.Now;
            BaseResponse response = _conferenceService.UpdateConference(conference);

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/Conference/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/Conference/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/Conference/" + filename + extension));
                    conference.ImageURL = "~/Content/upload/images/Conference/" + filename + extension;
                    _conferenceService.UpdateConference(conference);
                }
                if (file != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/documents/Conference/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/documents/Conference/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string filename = file.FileName.Substring(0, file.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    file.SaveAs(Server.MapPath("~/Content/upload/documents/Conference/" + filename + extension));
                    conference.AttachmentURL = "~/Content/upload/documents/Conference/" + filename + extension;
                    _conferenceService.UpdateConference(conference);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult DeleteConference(string conferenceID)
        {
            FindItemReponse<ConferenceDeclarationModel> ConResponse = _conferenceService.FindConferenceByID(conferenceID);
            if (ConResponse.Item != null)
            {
                try
                {
                    if (System.IO.File.Exists(Server.MapPath(ConResponse.Item.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(ConResponse.Item.ImageURL));
                    }
                    if (System.IO.File.Exists(Server.MapPath(ConResponse.Item.AttachmentURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(ConResponse.Item.AttachmentURL));
                    }
                }
                catch (Exception) { }
            }
            BaseResponse response = _conferenceService.DeleteConference(conferenceID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

    }
}

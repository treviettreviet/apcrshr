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
    public class AdminImportantDeadlineController : Controller
    {
        private IImportantDeadlineService _importantDeadline;

        public AdminImportantDeadlineController()
        {
            ViewBag.CurrentNode = "AdminImportantDeadline";
            this._importantDeadline = new ImportantDeadlineService();
        }

        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<ImportantDeadlineModel> response = _importantDeadline.GetImportantDeadlines();
            if (response.Items == null)
            {
                response.Items = new List<ImportantDeadlineModel>();
            }
            return View(response.Items);
        }

        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult CreateImportantDeadline()
        {
            return View();
        }

        [SessionFilter]
        [ValidateAntiForgeryToken]
        public JsonResult SaveImportantDeadline(ImportantDeadlineModel importantDeadline)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);
            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            InsertResponse response = new InsertResponse();

            importantDeadline.Title = importantDeadline.Title.Length > 200 ? importantDeadline.Title.Substring(0, 100) + "..." : importantDeadline.Title;
            if (importantDeadline.ShortContent != null)
            {
                importantDeadline.ShortContent = importantDeadline.ShortContent.Length > 300 ? importantDeadline.ShortContent.Substring(0, 296) + "..." : importantDeadline.ShortContent;
            }
            importantDeadline.DeadlineID = Guid.NewGuid().ToString();
            importantDeadline.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(importantDeadline.Title), UrlSlugger.Get8Digits());
            importantDeadline.CreatedDate = DateTime.Now;
            importantDeadline.CreatedBy = userSession != null ? userSession.UserID : string.Empty;
            response = _importantDeadline.CreateImportantDeadline(importantDeadline);

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult DeleteImportantDealine(string deadlineID)
        {
            BaseResponse response = _importantDeadline.DeleteImportantDeadline(deadlineID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [HttpGet]
        [AuthorizationFilter]
        public ActionResult UpdateImportantDeadline(string deadlineID)
        {
            FindItemReponse<ImportantDeadlineModel> response = _importantDeadline.FindImportantByID(deadlineID);
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveUpdateImportantDeadline(ImportantDeadlineModel importantDeadline)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            importantDeadline.ActionURL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(importantDeadline.Title), UrlSlugger.Get8Digits());
            importantDeadline.UpdatedBy = userSession.UserID;
            importantDeadline.UpdateDate = DateTime.Now;
            BaseResponse response = _importantDeadline.UpdateImportantDealine(importantDeadline);
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }


    }
}

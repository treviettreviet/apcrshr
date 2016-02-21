using apcrshr_site.Filters;
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
            this._importantDeadline = new ImportantDeadlineService();
        }

        [SessionFilter]
        public ActionResult Index()
        {
            IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
            FindAllItemReponse<ImportantDeadlineModel> response = _importantDeadline.GetImportantDeadlines();
            if (response.Items == null)
            {
                response.Items = new List<ImportantDeadlineModel>();
            }
            return View(response.Items);
        }

        [SessionFilter]
        public ActionResult AddNewImportantDeadline()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DeleteImportantDealine(string deadlineID)
        {
            BaseResponse response = _importantDeadline.DeleteImportantDeadline(deadlineID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [HttpGet]
        public ActionResult UpdateImportantDeadline(string deadlineID)
        {
            FindItemReponse<ImportantDeadlineModel> response = _importantDeadline.FindImportantByID(deadlineID);
            return View(response);
        }

    }
}

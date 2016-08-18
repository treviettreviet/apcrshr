using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Controllers
{
    public class AbstractController : BaseController
    {
        //
        // GET: /Abstract/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SubmitAbstract()
        {
            //Allow registration when registration opened
            FindItemReponse<ImportantDeadlineModel> importantDeadlineResponse = _importantDeadlineService.FindImportantDeadlineByType(DeadlineType.Abstract);
            if (importantDeadlineResponse.Item == null)
            {
                return RedirectToAction("AbstractNotAvailable", new { status = 1 });
            }
            else if (importantDeadlineResponse.Item.Deadline < DateTime.Now)
            {
                return RedirectToAction("AbstractNotAvailable", new { status = 2 });
            }
            return View();
        }

        /// <summary>
        /// Display registration status
        /// </summary>
        /// <param name="status">
        /// 1: Abstract is not opened
        /// 2: Abstract is expired
        /// </param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AbstractNotAvailable(int status)
        {
            ViewBag.Status = status;
            return View();
        }

    }
}

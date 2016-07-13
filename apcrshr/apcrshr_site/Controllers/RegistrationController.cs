using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Controllers
{
    public class RegistrationController : BaseController
    {
        //
        // GET: /Registration/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrationForm()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RegistrationWizard(string Title)
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}

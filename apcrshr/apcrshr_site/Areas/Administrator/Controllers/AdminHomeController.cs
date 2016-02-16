using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Response;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class AdminHomeController : Controller
    {
        private IAdminService _adminService;

        public AdminHomeController()
        {
            this._adminService = new AdminService();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            AdminLoginResponse response = _adminService.Login(username, password);
            if (response.ErrorCode == (int)ErrorCode.None)
            {
                this.Session["SessionID"] = response.SessionId;
                this.Session["UserName"] = response.AdminName;
                this.Session["UserId"] = response.AdminId;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Response = response;
            return View("Login");
        }
    }
}

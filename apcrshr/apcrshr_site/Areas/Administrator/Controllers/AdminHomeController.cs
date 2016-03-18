using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class AdminHomeController : Controller
    {
        private IAdminService _adminService;

        public AdminHomeController()
        {
            this._adminService = new AdminService();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminModel admin)
        {
            AdminLoginResponse response = _adminService.Login(admin.UserName, admin.Password);
            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Set cookie
                setCookies(response.AdminId, admin.RememberMe);

                this.Session["SessionID"] = response.SessionId;
                this.Session["UserName"] = response.AdminName;
                this.Session["UserId"] = response.AdminId;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Response = response;
            return View("Login");
        }

        private void setCookies(string adminID, bool rememberMe)
        {
            try
            {
                FindItemReponse<AdminModel> admin = _adminService.FindAdminByID(adminID);

                var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(admin);

                var ticket = new FormsAuthenticationTicket(1, admin.Item.UserName, DateTime.Now, DateTime.Now.AddHours(3), rememberMe, serializedUser);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var isSsl = Request.IsSecureConnection; // if we are running in SSL mode then make the cookie secure only

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    HttpOnly = true, // always set this to true!
                    Secure = isSsl,
                };

                if (rememberMe) // if the user needs to persist the cookie. Otherwise it is a session cookie
                    cookie.Expires = DateTime.Today.AddMonths(3); // currently hard coded to 3 months in the future

                Response.Cookies.Set(cookie);
            }
            catch (Exception) { }
        }
    }
}

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

namespace apcrshr_site.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController()
        {
            this._userService = new UserService();
        }


        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(UserModel user)
        {
            if (ModelState.IsValid)
            {
                UserLoginResponse response = _userService.Login(user.UserName, user.Password);
                if (response.ErrorCode == (int)ErrorCode.None)
                {
                    this.Session["User-SessionID"] = response.SessionId;
                    this.Session["User-UserName"] = response.UserName;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Response = response;
            }
            return View("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                InsertResponse response = _userService.CreateUser(user);
                ViewBag.Response = response;
            }
            return View("Register");
        }
    }
}

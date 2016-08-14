﻿using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using apcrshr_site.Filters;
using Site.Core.Service.Implementation;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class AdminUserController : Controller
    {
        private IUserService _userService;
        private IMainScholarshipService _mainScholarshipService;
        private IYouthScholarshipService _youthScholarshipService;

        public static readonly string SCHOLARSHIP_NOT_AVAILABLE = "Scholarship not available";
        public static readonly string SCHOLARSHIP_MAIN_TITLE = "Main scholarship";
        public static readonly string SCHOLARSHIP_YOUTH_TITLE = "Youth scholarship";

        public AdminUserController()
        {
            ViewBag.CurrentNode = "AdminMember";
            this._userService = new UserService();
            this._mainScholarshipService = new MainScholarshipService();
            this._youthScholarshipService = new YouthScholarshipService();
        }

        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<UserModel> response = _userService.GetUsers();
            if (response.Items == null)
            {
                response.Items = new List<UserModel>();
            }
            return View(response.Items);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult UpdateUser(string userId)
        {
            FindItemReponse<UserModel> response = _userService.FindUserByID(userId);
            if (response.ErrorCode != 0)
            {
                ViewBag.Message = new BaseResponse { ErrorCode = response.ErrorCode, Message = response.Message };
            }
            return View(response.Item);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult ViewUser(string userId)
        {
            //Scholarship title
            string scholarshipTitle = SCHOLARSHIP_NOT_AVAILABLE;

            //Get user information
            FindItemReponse<UserModel> response = _userService.FindUserByID(userId);

            //Find main scholarship by userID
            FindItemReponse<MainScholarshipModel> mainScholarshipReponse = _mainScholarshipService.FindByUserID(userId);
            if (mainScholarshipReponse.Item != null)
            {
                ViewBag.MainScholarship = mainScholarshipReponse.Item;
                scholarshipTitle = SCHOLARSHIP_MAIN_TITLE;
            }
            else
            {
                //Find youth scholarship by userID
                FindItemReponse<YouthScholarshipModel> youthScholarshipResponse = _youthScholarshipService.FindByUserID(userId);
                if (youthScholarshipResponse.Item != null)
                {
                    ViewBag.YouthScholarship = youthScholarshipResponse.Item;
                    scholarshipTitle = SCHOLARSHIP_YOUTH_TITLE;
                }
            }

            ViewBag.ScholarshipTitle = scholarshipTitle;

            return View(response.Item);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUpdateUser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                BaseResponse response = _userService.UpdateUser(user);
                ViewBag.Message = response;
            }
            return View("UpdateUser", user);
        }

        [HttpGet]
        public JsonResult DeleteUser(string userID)
        {
            BaseResponse response = _userService.DeleteUser(userID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LockUser(string userID)
        {
            LockResponse response = _userService.LockUser(userID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message, Locked = response.Locked }, JsonRequestBehavior.AllowGet);
        }
    }
}

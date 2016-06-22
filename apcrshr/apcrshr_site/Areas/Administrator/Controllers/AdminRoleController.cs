using apcrshr_site.Filters;
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
    public class AdminRoleController : Controller
    {
        private IAdminService _adminService;

        public AdminRoleController()
        {
            this._adminService = new AdminService();
        }

        //
        // GET: /Administrator/AdminRole/
        [HttpGet]
        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<AdminModel> adminResponse = _adminService.GetStandardAdmins();
            FindAllItemReponse<RoleModel> roleResponse = _adminService.GetRoles();
            FindAllItemReponse<ResourceModel> resourceResponse = _adminService.GetResources();
            ViewBag.AdminList = adminResponse.Items;
            ViewBag.RoleList = roleResponse.Items;
            ViewBag.ResourceList = resourceResponse.Items;
            return View();
        }

        [HttpGet]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult AssignRole(string adminID)
        {
            //Find available roles
            FindAllItemReponse<RoleModel> availableRoles = _adminService.GetAvailableRoles(adminID);
            //Find assigned roles
            FindAllItemReponse<RoleModel> assignedRoles = _adminService.GetAssignedRoles(adminID);
            ViewBag.AvailableRoles = availableRoles.Items;
            ViewBag.AssignedRoles = assignedRoles.Items;
            return View();
        }

        [HttpPost]
        [SessionFilter]
        public JsonResult SaveRoles(IList<string> roles, bool assign, string adminId)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            BaseResponse response = new BaseResponse();

            //If assign
            if (assign)
            {
                response = _adminService.AssignRoles(roles, adminId);
            }
            else
            {
                response = _adminService.RemoveRoles(roles, adminId);
            }

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult AssignResource(string roleID)
        {
            //Find available resources
            FindAllItemReponse<ResourceModel> availableResources = _adminService.GetAvailableResources(roleID);
            //Find assigned resources
            FindAllItemReponse<ResourceModel> assignedResources = _adminService.GetAssignedResources(roleID);
            ViewBag.AvailableResources = availableResources.Items;
            ViewBag.AssignedResources = assignedResources.Items;
            return View();
        }

        [HttpPost]
        [SessionFilter]
        public JsonResult SaveResources(IList<string> resources, bool assign, string roleID)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            BaseResponse response = new BaseResponse();

            //If assign
            if (assign)
            {
                response = _adminService.AssignResources(resources, roleID);
            }
            else
            {
                response = _adminService.RemoveResources(resources, roleID);
            }

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionFilter]
        public JsonResult SaveRole(RoleModel role)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            role.RoleID = Guid.NewGuid().ToString();
            role.CreatedBy = userSession.UserID;
            role.CreatedDate = DateTime.Now;
            role.Type = (int) RoleType.Custom;

            InsertResponse response = _adminService.CreateRole(role);

            return Json(new { errorCode = response.ErrorCode, message = response.Message, roleid = response.InsertID, roleName = Enum.GetName(typeof(RoleType), role.Type) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteRole(string roleID)
        {
            BaseResponse response = _adminService.DeleteRole(roleID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}

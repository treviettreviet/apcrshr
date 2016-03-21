using apcrshr_site.Filters;
using Site.Core.DataModel.Model;
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
        [SessionFilter]
        public ActionResult AssignRole(string adminID)
        {
            return View();
        }

    }
}

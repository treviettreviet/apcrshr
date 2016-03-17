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
        public ActionResult Index()
        {
            FindAllItemReponse<AdminModel> response = _adminService.GetStandardAdmins();
            ViewBag.AdminList = response.Items;
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

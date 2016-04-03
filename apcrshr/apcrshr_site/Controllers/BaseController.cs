using apcrshr_site.Helper;
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
    public class BaseController : Controller
    {
        protected string culture = "EN";

        protected IMenuCategoryService _menuCategoryService;
        protected IImportantDeadlineService _importantDeadlineService;

        protected BaseController()
        {
            this._menuCategoryService = new MenuCategoryService();
            this._importantDeadlineService = new ImportantDeadlineService();
        }

        protected override void ExecuteCore()
        {
            culture = "EN";
            if (this.Session == null || this.Session["CurrentCulture"] == null)
            {

                if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Culture"]))
                {
                    this.Session["CurrentCulture"] = culture;
                }
            }
            else
            {
                culture = this.Session["CurrentCulture"].ToString();
            }
            // calling CultureHelper class properties for setting  
            CultureHelper.CurrentCulture = culture;

            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

        public IList<MenuModel> GetCategoryMenu()
        {
            FindAllItemReponse<MenuModel> menuReponse = _menuCategoryService.FindAllMenus();
            return menuReponse.Items;
        }

        public IList<ImportantDeadlineModel> GetImportantDeadlines(int top)
        {
            FindAllItemReponse<ImportantDeadlineModel> importantDeadlineResponse = _importantDeadlineService.GetImportantDeadlines(top);
            return importantDeadlineResponse.Items;
        }
    }
}

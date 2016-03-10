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
    public class ImportantDeadlineController : BaseController
    {
        private IImportantDeadlineService _DeadlineService;
        public ImportantDeadlineController()
        {
            this._DeadlineService = new ImportantDeadlineService();
        }
        //
        // GET: /ImportantDeadline/

        public ActionResult Index(int pageIndex = 1)
        {
            FindAllItemReponse<ImportantDeadlineModel> response = _DeadlineService.GetImportantDeadlines(Constants.Constants.PAGE_SIZE, pageIndex);
            if (response.Items == null)
            {
                response.Items = new List<ImportantDeadlineModel>();
            }
            ViewBag.CurrentNode = "Deadline";
            return View(response);
        }

        [HttpGet]
        public ActionResult ViewImportantDeadline(string ActionURL, int pageIndex = 1)
        {
            FindItemReponse<ImportantDeadlineModel> response = _DeadlineService.FindImportantByActionURL(ActionURL);
            FindAllItemReponse<ImportantDeadlineModel> DeadlineReponse = new FindAllItemReponse<ImportantDeadlineModel>();
            List<ImportantDeadlineModel> list = new List<ImportantDeadlineModel>();
            string subActionURL = string.Empty;
            if (response.Item != null)
            {
                list.Add(response.Item);
                FindAllItemReponse<ImportantDeadlineModel> temp = _DeadlineService.GetRelatedImportantDeadline(response.Item.CreatedDate, Constants.Constants.PAGE_SIZE, pageIndex);
                if (temp.Items != null)
                {
                    list.AddRange(temp.Items);
                    DeadlineReponse.Items = list;
                    DeadlineReponse.Count = temp.Count;
                }
            }
            return View(DeadlineReponse);
        }
    }
}

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
    public class PresentationController : BaseController
    {

        private IPresentationService _preService;
        public PresentationController()
        {
            this._preService = new PresentationService();
        }
        //
        // GET: /Presentation/

        public ActionResult Index(int pageIndex = 1)
        {

            FindAllItemReponse<PresentationModel> response = _preService.GetPresentation(Constants.Constants.PAGE_SIZE, pageIndex);
            if (response.Items == null)
            {
                response.Items = new List<PresentationModel>();
            }
            ViewBag.CurrentNode = "Presentation";
            return View(response);
        }

        [HttpGet]
        public ActionResult ViewPresentation(string ActionURL, int pageIndex = 1)
        {
            FindItemReponse<PresentationModel> response = _preService.FindPresentationByActionURL(ActionURL);
            FindAllItemReponse<PresentationModel> preReponse = new FindAllItemReponse<PresentationModel>();
            List<PresentationModel> list = new List<PresentationModel>();
            string subActionURL = string.Empty;
            if (response.Item != null)
            {
                list.Add(response.Item);
                FindAllItemReponse<PresentationModel> temp = _preService.GetRelatedPresentation(response.Item.CreatedDate, Constants.Constants.PAGE_SIZE, pageIndex);
                if (temp.Items != null)
                {
                    list.AddRange(temp.Items);
                    preReponse.Items = list;
                    preReponse.Count = temp.Count;
                }
            }
            return View(preReponse);
        }

    }
}

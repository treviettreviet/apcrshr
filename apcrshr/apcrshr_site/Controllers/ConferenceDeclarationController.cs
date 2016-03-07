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
    public class ConferenceDeclarationController : BaseController
    {
        private IConferenceDeclarationService _conService;
        public ConferenceDeclarationController()
        {
            this._conService = new ConferenceDeclarationService();
        }
        //
        // GET: /ConferenceDeclaration/

        public ActionResult Index(int pageIndex = 1)
        {

            FindAllItemReponse<ConferenceDeclarationModel> response = _conService.GetConference(Constants.Constants.PAGE_SIZE, pageIndex);
            if (response.Items == null)
            {
                response.Items = new List<ConferenceDeclarationModel>();
            }
            ViewBag.CurrentNode = "Conference";
            return View(response);
        }

        [HttpGet]
        public ActionResult ViewConference(string ActionURL, int pageIndex = 1)
        {
            FindItemReponse<ConferenceDeclarationModel> response = _conService.FindConferenceByActionURL(ActionURL);
            FindAllItemReponse<ConferenceDeclarationModel> conReponse = new FindAllItemReponse<ConferenceDeclarationModel>();
            List<ConferenceDeclarationModel> list = new List<ConferenceDeclarationModel>();
            string subActionURL = string.Empty;
            if (response.Item != null)
            {
                list.Add(response.Item);
                FindAllItemReponse<ConferenceDeclarationModel> temp = _conService.GetRelatedConference(response.Item.CreatedDate, Constants.Constants.PAGE_SIZE, pageIndex);
                if (temp.Items != null)
                {
                    list.AddRange(temp.Items);
                    conReponse.Items = list;
                    conReponse.Count = temp.Count;
                }
            }
            return View(conReponse);
        }

    }
}

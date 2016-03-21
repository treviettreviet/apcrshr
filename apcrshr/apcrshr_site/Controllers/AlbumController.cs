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
    public class AlbumController : BaseController
    {
        private IAlbumService _albumService;
        public AlbumController()
        {
            this._albumService = new AlbumService();
        }
        //
        // GET: /Album/

        public ActionResult Index(int pageIndex = 1)
        {

            FindAllItemReponse<AlbumModel> response = _albumService.GetAlbum(Constants.Constants.PAGE_SIZE, pageIndex);
            if (response.Items == null)
            {
                response.Items = new List<AlbumModel>();
            }
            ViewBag.CurrentNode = "Album";
            return View(response);
        }

    }
}

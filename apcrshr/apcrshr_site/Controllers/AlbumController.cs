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

        public ActionResult Index(int ActionURL = 1)
        {
            FindAllItemReponse<AlbumModel> response = _albumService.GetAlbum(Constants.Constants.ALBUM_PAGE_SIZE, ActionURL);
            ViewBag.CurrentNode = "Album";
            return View(response);
        }

    }
}

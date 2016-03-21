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
    public class PhotoController : BaseController
    {
        private IPhotoService _photoService;
        public PhotoController()
        {
            this._photoService = new PhotoService();
        }
        //
        // GET: /Photo/

        public ActionResult Index(string ActionURL, string AlbumTitle, int pageIndex = 1)
        {

            FindAllItemReponse<PhotoModel> response = _photoService.GetPhotoByAlbum(ActionURL,Constants.Constants.PAGE_SIZE, pageIndex);
            if (response.Items == null)
            {
                response.Items = new List<PhotoModel>();
            }
            ViewBag.CurrentNode = "Photo";
            ViewBag.AlbumTitle = AlbumTitle;
            return View(response);
        }

    }
}

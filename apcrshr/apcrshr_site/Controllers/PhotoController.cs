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
        private IAlbumService _albumService;

        public PhotoController()
        {
            this._photoService = new PhotoService();
            this._albumService = new AlbumService();
        }
        //
        // GET: /Photo/

        public ActionResult Index(string ActionURL, int pageIndex = 1)
        {
            FindItemReponse<AlbumModel> albumResponse = _albumService.FindAlbumByActionURL(ActionURL);
            if (albumResponse.Item != null)
            {
                ViewBag.Title = albumResponse.Item.Title;
            }
            FindAllItemReponse<PhotoModel> response = _photoService.GetPhotoByAlbum(ActionURL,Constants.Constants.PHOTO_PAGE_SIZE, pageIndex);
            if (response.Items == null)
            {
                response.Items = new List<PhotoModel>();
            }
            ViewBag.CurrentNode = "Photo";
            return View(response);
        }

    }
}

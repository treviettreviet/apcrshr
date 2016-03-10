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
    public class VideoController : BaseController
    {
        private IVideoService _videoService;
        public VideoController()
        {
            this._videoService = new VideoService();
        }
        //
        // GET: /Video/

        public ActionResult Index(int pageIndex = 1)
        {

            FindAllItemReponse<VideoModel> response = _videoService.GetVideo(Constants.Constants.PAGE_SIZE, pageIndex);
            if (response.Items == null)
            {
                response.Items = new List<VideoModel>();
            }
            ViewBag.CurrentNode = "video";
            return View(response);
        }

        [HttpGet]
        public ActionResult ViewVideo(string ActionURL, string Language, int pageIndex = 1)
        {
            FindItemReponse<VideoModel> response = _videoService.FindVideoByActionURL(ActionURL);
            FindAllItemReponse<VideoModel> videoReponse = new FindAllItemReponse<VideoModel>();
            List<VideoModel> list = new List<VideoModel>();
            string subActionURL = string.Empty;
            if (response.Item != null)
            {
                list.Add(response.Item);
                FindAllItemReponse<VideoModel> temp = _videoService.GetRelatedVideo(response.Item.CreatedDate, Constants.Constants.PAGE_SIZE, pageIndex, Language);
                if (temp.Items != null)
                {
                    list.AddRange(temp.Items);
                    videoReponse.Items = list;
                    videoReponse.Count = temp.Count;
                }
            }
            return View(videoReponse);
        }

    }
}

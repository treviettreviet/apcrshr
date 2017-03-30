using apcrshr_site.Filters;
using apcrshr_site.Helper;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class AdminSliderController : Controller
    {
        private ISliderService _slider;

        public AdminSliderController()
        {
            ViewBag.CurrentNode = "AdminSlider";
            this._slider = new SliderService();
        }
        //
        // GET: /Administrator/AdminSlider/
        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<SliderModel> response = _slider.GetSliders();
            if (response.Items == null)
            {
                response.Items = new List<SliderModel>();
            }
            return View(response.Items);
        }

        [AuthorizationFilter]
        [SessionFilter]
        public ActionResult CreateSlider()
        {
            return View();
        }

        [SessionFilter]
        
        public JsonResult SaveSlider(SliderModel slider, HttpPostedFileBase imageFile)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);
            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            InsertResponse response = new InsertResponse();

            slider.Title = slider.Title.Length > 200 ? slider.Title.Substring(0, 100) + "..." : slider.Title;

            slider.SliderID = Guid.NewGuid().ToString();
            //slider.URL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(slider.Title), UrlSlugger.Get8Digits());
            slider.CreatedDate = DateTime.Now;
            slider.CreatedBy = userSession != null ? userSession.UserID : string.Empty;
            slider.ImageURL = "";
            response = _slider.CreateSlider(slider);
            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/slider/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/slider/"));
                        }
                    }
                    catch (Exception) { }

                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/slider/" + filename + extension));

                    slider.ImageURL = "/Content/upload/images/slider/" + filename + extension;
                    _slider.UpdateSlider(slider);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult UpdateSlider(string sliderID)
        {
            FindItemReponse<SliderModel> response = _slider.FindSliderByID(sliderID);
            return View(response.Item);
        }
        [SessionFilter]
        [HttpPost]
        
        public JsonResult SaveUpdateSlider(SliderModel slider, HttpPostedFileBase imageFile)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }
            //slider.URL = string.Format("{0}-{1}", UrlSlugger.ToUrlSlug(slider.Title), UrlSlugger.Get8Digits());
            slider.UpdatedBy = userSession.UserID;
            slider.UpdatedDate = DateTime.Now;
            BaseResponse response = _slider.UpdateSlider(slider);

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Image
                if (imageFile != null)
                {
                    //Create Folder
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/slider/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/slider/"));
                        }
                    }
                    catch (Exception) { }
                    string extension = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf("."));
                    string filename = imageFile.FileName.Substring(0, imageFile.FileName.LastIndexOf(".")).Replace(" ", "-");
                    filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                    imageFile.SaveAs(Server.MapPath("~/Content/upload/images/slider/" + filename + extension));
                    slider.ImageURL = "/Content/upload/images/slider/" + filename + extension;
                    _slider.UpdateSlider(slider);
                }
            }
            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteSlider(string SliderID)
        {
            FindItemReponse<SliderModel> sliderResponse = _slider.FindSliderByID(SliderID);
            if (sliderResponse.Item != null)
            {
                try
                {
                    if (System.IO.File.Exists(Server.MapPath(sliderResponse.Item.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(sliderResponse.Item.ImageURL));
                    }
                }
                catch (Exception) { }
            }
            BaseResponse response = _slider.DeleteSlider(SliderID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

    }
}

using apcrshr_site.Filters;
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
    public class ScholarshipController : BaseController
    {
        private IMainScholarshipService _mainScholarshipService;
        private IYouthScholarshipService _youthScholarshipService;
        private IExperienceService _experienceService;
        private IEducationService _educationService;
        private ITrainingService _trainingService;
        private ILeaderShipService _leadershipService;
        private IPublicationService _publicationService;

        public ScholarshipController()
        {
            this._mainScholarshipService = new MainScholarshipService();
            this._youthScholarshipService = new YouthScholarshipService();
            this._experienceService = new ExperienceService();
            this._educationService = new EducationService();
            this._trainingService = new TrainingService();
            this._leadershipService = new LeaderShipService();
            this._publicationService = new PublicationService();
        }

        //
        // GET: /Scholarship/

        public ActionResult Index()
        {
            return View();
        }

        [UserSessionFilter]
        public ActionResult MainScholarships()
        {
            string sessionId = Session["User-SessionID"].ToString();
            Site.Core.Repository.User _user = SessionUtil.GetInstance.GetUserBySessionID(sessionId);

            FindAllItemReponse<MainScholarshipModel> response = new FindAllItemReponse<MainScholarshipModel>();

            if (_user != null)
            {
                response = _mainScholarshipService.FindByUserID(_user.UserID);
                if (response.Items == null)
                {
                    response.Items = new List<MainScholarshipModel>();
                }
            }
            return View(response.Items);
        }

        [UserSessionFilter]
        public ActionResult UpdateMainScholarship(string scholarshipID)
        {
            FindItemReponse<MainScholarshipModel> response = _mainScholarshipService.FindByID(scholarshipID);
            return View(response.Item);
        }

        [UserSessionFilter]
        
        public ActionResult SaveMainScholarship(MainScholarshipModel scholarship)
        {
            string sessionId = Session["User-SessionID"].ToString();
            Site.Core.Repository.User _user = SessionUtil.GetInstance.GetUserBySessionID(sessionId);

            scholarship.UpdatedBy = _user.UserID;
            scholarship.UpdatedDate = DateTime.Now;
            BaseResponse response = _mainScholarshipService.Update(scholarship);
            ViewBag.Message = response;
            return View("UpdateMainScholarship", scholarship);
        }

        [UserSessionFilter]
        public ActionResult YouthScholarships(string scholarshipID)
        {
            string sessionId = Session["User-SessionID"].ToString();
            Site.Core.Repository.User _user = SessionUtil.GetInstance.GetUserBySessionID(sessionId);

            FindItemReponse<YouthScholarshipModel> response = new FindItemReponse<YouthScholarshipModel>();

            if (_user != null)
            {
                response = _youthScholarshipService.FindByUserID(_user.UserID);
                if (response.Item == null)
                {
                    response.Item = new YouthScholarshipModel();
                }
            }

            //Find experiences
            FindAllItemReponse<ExperienceModel> experiencesResponse = _experienceService.FindByscholarshipID(response.Item.YouthScholarshipID);
            if (experiencesResponse.Items != null && experiencesResponse.Items.Count > 0)
            {
                response.Item.ExperienceTitles = string.Join(",", experiencesResponse.Items.Select(i => i.Organization));
            }
            //Find educations
            FindAllItemReponse<EducationModel> educationsResponse = _educationService.FindByscholarshipID(response.Item.YouthScholarshipID);
            if (educationsResponse.Items != null && educationsResponse.Items.Count > 0)
            {
                response.Item.EducationTitles = string.Join(",", educationsResponse.Items.Select(i => i.MainCourseStudy));
            }
            //Find trainings
            FindAllItemReponse<TrainingModel> trainingsResponse = _trainingService.FindByscholarshipID(response.Item.YouthScholarshipID);
            if (trainingsResponse.Items != null && trainingsResponse.Items.Count > 0)
            {
                response.Item.AdditionalTrainingTitles = string.Join(",", trainingsResponse.Items.Select(i => i.NameOfCourse));
            }
            //Find leadership
            FindAllItemReponse<LeaderShipModel> leadershipsResponse = _leadershipService.FindByscholarshipID(response.Item.YouthScholarshipID);
            if (leadershipsResponse.Items != null && leadershipsResponse.Items.Count > 0)
            {
                response.Item.LeaderShipTitles = string.Join(",", leadershipsResponse.Items.Select(i => i.Organization));
            }
            //Find publication
            FindAllItemReponse<PublicationModel> publicationsResponse = _publicationService.FindByscholarshipID(response.Item.YouthScholarshipID);
            if (publicationsResponse.Items != null && publicationsResponse.Items.Count > 0)
            {
                response.Item.PublicationTitles = string.Join(",", publicationsResponse.Items.Select(i => i.Title));
            }

            return View(response.Item);
        }

        [UserSessionFilter]
        
        public ActionResult SaveYouthScholarship(YouthScholarshipModel scholarship)
        {
            string sessionId = Session["User-SessionID"].ToString();
            Site.Core.Repository.User _user = SessionUtil.GetInstance.GetUserBySessionID(sessionId);

            scholarship.UpdatedBy = _user.UserID;
            scholarship.UpdatedDate = DateTime.Now;
            BaseResponse response = _youthScholarshipService.Update(scholarship);
            ViewBag.Message = response;
            return View("YouthScholarships", scholarship);
        }
    }
}

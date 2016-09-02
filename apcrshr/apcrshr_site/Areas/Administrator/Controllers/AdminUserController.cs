using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using apcrshr_site.Filters;
using Site.Core.Service.Implementation;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class AdminUserController : Controller
    {
        private IUserService _userService;
        private IMainScholarshipService _mainScholarshipService;
        private IYouthScholarshipService _youthScholarshipService;
        private IExperienceService _experienceService;
        private IEducationService _educationService;
        private ITrainingService _trainingService;
        private ILeaderShipService _leadershipService;
        private IPublicationService _publicationService;
        private IUserSubmissionService _userSubmissionService;

        public static readonly string SCHOLARSHIP_NOT_AVAILABLE = "Scholarship not available";
        public static readonly string SCHOLARSHIP_MAIN_TITLE = "Main scholarship";
        public static readonly string SCHOLARSHIP_YOUTH_TITLE = "Youth scholarship";

        public AdminUserController()
        {
            ViewBag.CurrentNode = "AdminMember";
            this._userService = new UserService();
            this._mainScholarshipService = new MainScholarshipService();
            this._youthScholarshipService = new YouthScholarshipService();
            this._experienceService = new ExperienceService();
            this._educationService = new EducationService();
            this._trainingService = new TrainingService();
            this._leadershipService = new LeaderShipService();
            this._publicationService = new PublicationService();
            this._userSubmissionService = new UserSubmissionService();
        }

        [SessionFilter]
        [AuthorizationFilter]
        public ActionResult Index()
        {
            FindAllItemReponse<UserModel> response = _userService.GetUsers();
            if (response.Items == null)
            {
                response.Items = new List<UserModel>();
            }
            return View(response.Items);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult UpdateUser(string userId)
        {
            FindItemReponse<UserModel> response = _userService.FindUserByID(userId);
            if (response.ErrorCode != 0)
            {
                ViewBag.Message = new BaseResponse { ErrorCode = response.ErrorCode, Message = response.Message };
            }
            return View(response.Item);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult ViewUser(string userId)
        {
            //Get user information
            FindItemReponse<UserModel> response = _userService.FindUserByID(userId);

            //Find abstract
            FindAllItemReponse<UserSubmissionModel> abstractResponse = _userSubmissionService.FindByUserID(userId);
            ViewBag.Abstracts = abstractResponse.Items;

            //Find main scholarship by userID
            FindAllItemReponse<MainScholarshipModel> mainScholarshipReponse = _mainScholarshipService.FindByUserID(userId);
            if (mainScholarshipReponse.Items != null)
            {
                ViewBag.MainScholarship = mainScholarshipReponse.Items;
                ViewBag.ScholarshipTitleMain = SCHOLARSHIP_MAIN_TITLE;
            }

            //Find youth scholarship by userID
            FindItemReponse<YouthScholarshipModel> youthScholarshipResponse = _youthScholarshipService.FindByUserID(userId);
            if (youthScholarshipResponse.Item != null)
            {
                ViewBag.YouthScholarship = youthScholarshipResponse.Item;
                ViewBag.ScholarshipTitleYouth = SCHOLARSHIP_YOUTH_TITLE;

                //Find working experience
                FindAllItemReponse<ExperienceModel> experienceResponse = _experienceService.FindByscholarshipID(youthScholarshipResponse.Item.YouthScholarshipID);
                ViewBag.Experiences = experienceResponse.Items;

                //Find education
                FindAllItemReponse<EducationModel> educationResponse = _educationService.FindByscholarshipID(youthScholarshipResponse.Item.YouthScholarshipID);
                ViewBag.Educations = educationResponse.Items;

                //Find training
                FindAllItemReponse<TrainingModel> trainingResponse = _trainingService.FindByscholarshipID(youthScholarshipResponse.Item.YouthScholarshipID);
                ViewBag.Trainings = trainingResponse.Items;

                //Find leadership
                FindAllItemReponse<LeaderShipModel> leadershipResponse = _leadershipService.FindByscholarshipID(youthScholarshipResponse.Item.YouthScholarshipID);
                ViewBag.Leaderships = leadershipResponse.Items;

                //Find publication
                FindAllItemReponse<PublicationModel> publicationResponse = _publicationService.FindByscholarshipID(youthScholarshipResponse.Item.YouthScholarshipID);
                ViewBag.Publications = publicationResponse.Items;
            }

            return View(response.Item);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpPost]
        
        public ActionResult SaveUpdateUser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                BaseResponse response = _userService.UpdateUser(user);
                ViewBag.Message = response;
            }
            return View("UpdateUser", user);
        }

        [HttpGet]
        public JsonResult DeleteUser(string userID)
        {
            BaseResponse response = _userService.DeleteUser(userID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LockUser(string userID)
        {
            LockResponse response = _userService.LockUser(userID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message, Locked = response.Locked }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult UpdateMainScholarship(string scholarshipID)
        {
            FindItemReponse<MainScholarshipModel> response = _mainScholarshipService.FindByID(scholarshipID);
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]
        
        public JsonResult SaveUpdateMainScholarship(MainScholarshipModel scholarship)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            scholarship.UpdatedBy = userSession.UserID;
            scholarship.UpdatedDate = DateTime.Now;
            BaseResponse response = _mainScholarshipService.Update(scholarship);

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorkingExperience(string workingID)
        {
            FindItemReponse<ExperienceModel> response = _experienceService.FindByID(workingID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEducation(string educationID)
        {
            FindItemReponse<EducationModel> response = _educationService.FindByID(educationID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTraining(string trainingID)
        {
            FindItemReponse<TrainingModel> response = _trainingService.FindByID(trainingID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLeadership(string leadershipID)
        {
            FindItemReponse<LeaderShipModel> response = _leadershipService.FindByID(leadershipID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPublication(string publicationID)
        {
            FindItemReponse<PublicationModel> response = _publicationService.FindByID(publicationID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AssignAbstractSubmission(string userID, string submissionNumber, int status)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            UserSubmissionModel model = new UserSubmissionModel();
            model.SubmitID = Guid.NewGuid().ToString();
            model.CreatedDate = DateTime.Now;
            model.SubmissionNumber = submissionNumber;
            model.UserID = userID;
            model.Status = status;

            InsertResponse response = _userSubmissionService.Create(model);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteAbstract(string submitID)
        {
            BaseResponse response = _userSubmissionService.Delete(submitID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SessionFilter]
        public ActionResult UpdateAbstract(string submitID)
        {
            FindItemReponse<UserSubmissionModel> response = _userSubmissionService.FindByID(submitID);
            return View(response.Item);
        }

        [HttpPost]
        public JsonResult SaveUpdateAbstract(UserSubmissionModel submission)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            BaseResponse response = _userSubmissionService.Update(submission);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

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
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using apcrshr_site.Helper;
using System.IO.Packaging;
using System.IO.Compression;


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
        private IMailingAddressService _mailingAddressService;
        private IPaymentService _paymentService;
        private ILogisticSheduleService _logisticService;

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
            this._mailingAddressService = new MailingAddressService();
            this._paymentService = new PaymentService();
            this._logisticService = new LogisticSheduleService();
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

            //Find logistic
            FindItemReponse<LogisticScheduleModel> logisticResponse = _logisticService.FindByUserID(userId);
            if (logisticResponse.Item != null)
            {
                ViewBag.Logistic = logisticResponse.Item;
            }

            //Find mailing
            FindAllItemReponse<MailingAddressModel> mailingResponse = _mailingAddressService.FindMailingAddressByUser(userId);
            if (mailingResponse.Items != null && mailingResponse.Items.Count > 0)
            {
                ViewBag.Mailing = mailingResponse.Items[0];
            }

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

            //Find payment
            FindAllItemReponse<PaymentModel> paymentResponse = _paymentService.FindByUserID(userId);
            if (paymentResponse.Items != null)
            {
                ViewBag.Payments = paymentResponse.Items;
            }

            return View(response.Item);
        }

        [HttpPost]
        public JsonResult SaveLogisticInfo(LogisticScheduleModel logistic)
        {
            if (!string.IsNullOrEmpty(logistic.LogisticID))
            {
                var response = _logisticService.Update(logistic);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                logistic.LogisticID = Guid.NewGuid().ToString();
                logistic.CreatedDate = DateTime.Now;
                var response = _logisticService.Create(logistic);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [SessionFilter]
        public ActionResult CreatePayment(string UserId)
        {
            return View();
        }

        [HttpPost]
        public JsonResult SavePayment(PaymentModel payment)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            payment.PaymentID = Guid.NewGuid().ToString();
            payment.CreatedBy = userSession.UserID;
            payment.CreatedDate = DateTime.Now;
            InsertResponse response = _paymentService.Create(payment);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeletePayment(string paymentID)
        {
            BaseResponse response = _paymentService.Delete(paymentID);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [SessionFilter]
        [AuthorizationFilter]
        [HttpGet]
        public ActionResult UpdatePayment(string paymentID)
        {
            FindItemReponse<PaymentModel> response = _paymentService.FindByID(paymentID);
            return View(response.Item);
        }

        [SessionFilter]
        [HttpPost]

        public JsonResult SaveUpdatePayment(PaymentModel payment)
        {
            var sessionId = this.Session["SessionID"].ToString();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return Json(new { errorCode = (int)ErrorCode.Redirect, message = Resources.AdminResource.msg_sessionInvalid }, JsonRequestBehavior.AllowGet);
            }

            payment.UpdatedBy = userSession.UserID;
            BaseResponse response = _paymentService.Update(payment);

            return Json(new { errorCode = response.ErrorCode, message = response.Message }, JsonRequestBehavior.AllowGet);
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
        public JsonResult ResetPassword(string userID)
        {
            string newPassword = System.Web.Security.Membership.GeneratePassword(10, 3);
            BaseResponse response = _userService.ChangePassword(userID, newPassword);
            return Json(new { ErrorCode = response.ErrorCode, Message = response.Message, Password = newPassword }, JsonRequestBehavior.AllowGet);
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

        private static void AddToZip(string zipFilePath, Stream contentStream, string nameInZip)
        {
            using (Package zip = Package.Open(zipFilePath, FileMode.OpenOrCreate))
            {
                string fileNameInZip = @".\" + nameInZip;
                Uri fileUri = PackUriHelper.CreatePartUri(new Uri(fileNameInZip, UriKind.Relative));
                if (zip.PartExists(fileUri))
                {
                    zip.DeletePart(fileUri);
                }
                PackagePart part = zip.CreatePart(fileUri, string.Empty, CompressionOption.Normal);
                using (Stream dest = part.GetStream())
                {
                    contentStream.CopyTo(dest);
                }
            }
        }
        private static void AddToZip(string zipFilePath, FileInfo fileInfo)
        {
            if (fileInfo != null)
            {
                var openMode = System.IO.File.Exists(zipFilePath) ? ZipArchiveMode.Update : ZipArchiveMode.Create;
                using (ZipArchive zipFile = ZipFile.Open(zipFilePath, openMode))
                {
                    zipFile.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
                }
            }
        }

        [HttpGet]
        [SessionFilter]
        public FileStreamResult ExportImages()
        {
            string zipFilePath = string.Empty;
            FindAllItemReponse<MailingAddressModel> response = _mailingAddressService.GetMailingAddresses();
            if (response.Items != null)
            {
                //Create zip file path
                zipFilePath = Path.Combine(Path.GetTempPath(), "images.zip");
                if (System.IO.File.Exists(zipFilePath))
                {
                    System.IO.File.Delete(zipFilePath);
                }

                //Initial zip
                using (Package zip = Package.Open(zipFilePath, FileMode.OpenOrCreate))
                {
                }

                //Create zip
                foreach (MailingAddressModel mailing in response.Items)
                {
                    var user = _userService.FindUserByID(mailing.UserID);
                    if (user.Item != null)
                    {
                        //photo 1
                        if (!string.IsNullOrEmpty(mailing.PassportPhoto1) && System.IO.File.Exists(Server.MapPath(mailing.PassportPhoto1)))
                        {
                            FileInfo fileInfo = new FileInfo(Server.MapPath(mailing.PassportPhoto1));
                            using (FileStream fileStream = new FileStream(Server.MapPath(mailing.PassportPhoto1), FileMode.Open, FileAccess.Read))
                            {
                                AddToZip(zipFilePath, fileStream, user.Item.FullName.Replace(' ','-') + @"\" + fileInfo.Name);
                            }
                        }
                    }
                }
            }
            Stream stream = null;
            if (!string.IsNullOrEmpty(zipFilePath))
            {
                stream = new FileStream(zipFilePath, FileMode.Open);
            }
            else
            {
                stream = new MemoryStream();
            }
            return new FileStreamResult(stream, "application/zip");
        }

        private IList<UserExportModel> BuildExportList(IList<UserModel> users)
        {
            IList<UserExportModel> exports = new List<UserExportModel>();

            UserExportModel ex;
            MailingAddressModel mailing = null;
            MainScholarshipModel scholarship = null;
            PaymentModel payment = null;
            LogisticScheduleModel logistic = null;
            foreach (UserModel user in users)
            {
                ex = new UserExportModel();
                var _mailing = _mailingAddressService.FindMailingAddressByUser(user.UserID);
                var _scholarship = _mainScholarshipService.FindByUserID(user.UserID);
                var _payment = _paymentService.FindByUserID(user.UserID);

                mailing = _mailing.Items != null ? _mailing.Items.FirstOrDefault() : null;
                scholarship = _scholarship.Items != null ? _scholarship.Items.FirstOrDefault() : null;
                payment = _payment.Items != null ? _payment.Items.FirstOrDefault() : null;
                logistic = _logisticService.FindByUserID(user.UserID).Item;

                //Convert from user to userexport
                ConvertFromUser(user, ex);

                //Convert from mailing to userexport
                ConvertFromMailing(mailing, ex);

                //Convert from main scholarship
                ConvertFromMainScholarship(scholarship, ex);

                //Convert from payment
                ConvertFromPayment(payment, ex);

                //Convert logistic
                ConvertFromLogistic(logistic, ex);

                exports.Add(ex);
            }

            return exports;
        }

        private void ConvertFromLogistic(LogisticScheduleModel logistic, UserExportModel ex)
        {
            if (logistic == null) return;

            ex.ArrivalDate = logistic.ArrivalDate.HasValue ? logistic.ArrivalDate.Value.ToString("yyyy/MM/dd") : string.Empty;
            ex.ArrivalTime = logistic.ArrivalDate.HasValue ? logistic.ArrivalDate.Value.ToString("HH:mm") : string.Empty;
            ex.ArrivalFlightNumber = logistic.ArrivalFlightNumber;
            ex.ArrivalGate = logistic.ArrivalGate;
            ex.DepartureDate = logistic.DepartureDate.HasValue ? logistic.DepartureDate.Value.ToString("yyyy/MM/dd") : string.Empty;
            ex.DepartureTime = logistic.DepartureDate.HasValue ? logistic.DepartureDate.Value.ToString("HH:mm") : string.Empty;
            ex.DepartureFlightNumber = logistic.DepartureFlightNumber;
            ex.DepartureGate = logistic.DepartureGate;
            ex.WhenNeedPick = logistic.WhenNeedPick;
            ex.SpecialRequirement = logistic.SpecialRequirement;
            ex.HotelName = logistic.HotelName;
            ex.CheckinDate = logistic.CheckinDate;
            ex.CheckoutDate = logistic.CheckoutDate;
        }

        private void ConvertFromPayment(PaymentModel payment, UserExportModel ex)
        {
            if (payment == null) return;

            ex.PaymentType = payment.PaymentType;
            ex.Amount = payment.Amount;
            ex.PaidDate = payment.CreatedDate.ToShortDateString();
            ex.Status = Enum.GetName(typeof(PaymentStatus), payment.Status);
        }

        private void ConvertFromMainScholarship(MainScholarshipModel scholarship, UserExportModel ex)
        {
            if (scholarship == null) return;

            ex.SubmissionNumber = scholarship.SubmissionNumber;
            ex.Responsibility = scholarship.Responsibility;
            ex.ReasonScholarship = scholarship.ReasonScholarship;
            ex.AtLeastOneAbstract = scholarship.HasSubmitted;
            ex.Position = scholarship.Position;
            ex.DurationOfWork = scholarship.DurationOfWork;
            ex.SubmissionTitles = scholarship.SubmissionTitles;
            ex.ScholarshipPackage = scholarship.ScholarshipPackage;
        }

        private void ConvertFromMailing(MailingAddressModel mailing, UserExportModel ex)
        {
            if (mailing == null) return;

            ex.ParticipateYouth = mailing.ParticipateYouth;
            ex.OriginalNationality = mailing.OriginalNationality;
            ex.CurrentNationality = mailing.CurrentNationality;
            ex.PassportNumber = mailing.PassportNumber;
            ex.TypeOfPassport = mailing.TypeOfPassport;
            ex.Occupation = mailing.Occupation;
            ex.DateOfPassportIssue = mailing.DateOfPassportIssue.HasValue ? mailing.DateOfPassportIssue.Value.ToShortDateString() : "";
            ex.DateOfPassportExpiry = mailing.DateOfPassportExpiry.HasValue ? mailing.DateOfPassportExpiry.Value.ToShortDateString() : "";
            ex.DetailOfEmbassy = mailing.DetailOfEmbassy;
            ex.NeedVisaSupport = mailing.NeedVisaSupport;
            ex.ActivationCode = mailing.ActivationCode;
            ex.RegistrationNumber = mailing.RegistrationNumber;
        }

        private void ConvertFromUser(UserModel user, UserExportModel ex)
        {
            if (user == null) return;

            ex.UserID = user.UserID;
            ex.Title = user.Title;
            ex.FullName = user.FullName;
            ex.Sex = user.Sex;
            ex.Email = user.Email;
            ex.OtherEmail = user.OtherEmail;
            ex.DateOfBirth = user.DateOfBirth.HasValue ? user.DateOfBirth.Value.ToShortDateString() : "";
            ex.PhoneNumber = user.PhoneNumber;
            ex.Locked = user.Locked;
            ex.MealPreference = user.MealPreference;
            ex.DisabilitySpecialTreatment = user.DisabilitySpecialTreatment;
            ex.Address = user.Address;
            ex.City = user.City;
            ex.Country = user.Country;
            ex.WorkAddress = user.WorkAddress;
            ex.Organization = user.Organization;
            ex.ParticipantType = user.ParticipantType;
        }

        [HttpGet]
        [SessionFilter]
        public FileStreamResult ExportUsers()
        {
            FindAllItemReponse<UserModel> response = _userService.GetUsers();
            using (ExcelPackage pck = new ExcelPackage())
            {
                IList<UserExportModel> export = BuildExportList(response.Items);
                //if (response.Items != null)
                //{
                //    response.Items.Select(m => { m.MainScholarship = UserHelper.HasMainScholarship(m.UserID); m.YouthScholarship = UserHelper.HasYouthScholarship(m.UserID); return m; }).ToList();
                //}
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Members");
                //ExcelWorksheet wsMailing = pck.Workbook.Worksheets.Add("Mailing Address");

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromCollection(export, true);
                //wsMailing.Cells["A1"].LoadFromCollection(mailingResponse.Items, true);

                //Format the header for column 1-3
                using (ExcelRange rng = ws.Cells["A1:AO1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                //Format the header for column 1-3
                //using (ExcelRange rng = wsMailing.Cells["A1:U1"])
                //{
                //    rng.Style.Font.Bold = true;
                //    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                //    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                //    rng.Style.Font.Color.SetColor(Color.White);
                //}
                //Example how to Format Column 1 as numeric 
                //using (ExcelRange col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 1])
                //{
                //    col.Style.Numberformat.Format = "#,##0.00";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //Write it back to the client
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment;  filename=MembersList.xlsx");
                //Response.BinaryWrite(pck.GetAsByteArray());

                Stream stream = new MemoryStream(pck.GetAsByteArray());

                return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}

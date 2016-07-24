using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using apcrshr_site.Helper;
using apcrshr_site.Models;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;

namespace apcrshr_site.Controllers
{
    public class RegistrationController : BaseController
    {
        private static readonly string REGISTRATION_SUBJECT = "You have successfully registered for the 9th Asia- Pacific Conference on Reproductive and Sexual Health and Rights.";
        private IUserService _userService;
        private IMailingAddressService _mailingService;
        private ISessionService _sessionService;

        public RegistrationController()
        {
            this._userService = new UserService();
            this._mailingService = new MailingAddressService();
            this._sessionService = new SessionService();
        }

        //
        // GET: /Registration/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrationForm()
        {
            return View();
        }

        public ActionResult ActiveAccount(string activationCode)
        {
            FindAllItemReponse<MailingAddressModel> mailingResponse = _mailingService.GetMailingAddresses(activationCode);
            if (mailingResponse.Items != null)
            {
                foreach (var mailing in mailingResponse.Items)
                {
                    FindItemReponse<UserModel> userReponse = _userService.FindUserByID(mailing.UserID);
                    if (userReponse.Item != null)
                    {
                        var user = userReponse.Item;
                        user.Locked = false;
                        //Prevent password will be updated
                        user.Password = null;
                        _userService.UpdateUser(user);
                    }
                }
            }
            return View();
        }

        public ActionResult RegistrationCompleted()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EmailValidation(string email)
        {
            FindItemReponse<UserModel> response = _userService.FindUserByEmail(email);
            if(response.Item != null){
                return Json(new { ErrorCode = (int)ErrorCode.Error, Message = string.Format(Resources.Resource.msg_item_exists, "Email", email) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrationWizard(RegistrationModel registration, string hidSession)
        {
            FindItemReponse<UserModel> userResponse = new FindItemReponse<UserModel>();
            RegistrationModel temp = null;
            FindItemReponse<SessionModel> sessionResponse = null;
            SessionModel session = null;
            string sessionId = string.Empty;
            string mailingId = string.Empty;
            if (!string.IsNullOrEmpty(hidSession))
            {
                sessionResponse = _sessionService.FindID(hidSession);
                if (sessionResponse.Item != null && sessionResponse.Item.Options != null)
                {
                    temp = XmlSerializerUltil.Deserialize<RegistrationModel>(sessionResponse.Item.Options);
                    if (sessionResponse.Item.Completed)
                    {
                        //Just skip
                        return Json(new { }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                temp = new RegistrationModel();
            }
            if (registration != null)
            {
                switch (registration.CurrentStep)
                {
                    case 1:
                        if (!string.IsNullOrEmpty(registration.Title))
                        {
                            temp.Title = registration.Title;
                        }
                        if (!string.IsNullOrEmpty(registration.FullName))
                        {
                            temp.FullName = registration.FullName;
                        }
                        if (!string.IsNullOrEmpty(registration.Sex))
                        {
                            temp.Sex = registration.Sex;
                        }
                        if (!string.IsNullOrEmpty(registration.MealPreference))
                        {
                            temp.MealPreference = registration.MealPreference;
                        }
                        if (!string.IsNullOrEmpty(registration.DisabilityOrTreatment))
                        {
                            temp.DisabilityOrTreatment = registration.DisabilityOrTreatment;
                        }
                        if (sessionResponse != null && sessionResponse.Item != null)
                        {
                            session = sessionResponse.Item;
                            session.Step = registration.CurrentStep;
                            session.UpdatedDate = DateTime.Now;
                            session.Options = XmlSerializerUltil.Serialize<RegistrationModel>(temp);
                            _sessionService.Update(session);
                            sessionId = hidSession;
                        }
                        else
                        {
                            session = new SessionModel();
                            session.SessionID = Guid.NewGuid().ToString();
                            session.CreatedDate = DateTime.Now;
                            session.Step = registration.CurrentStep;
                            session.Options = XmlSerializerUltil.Serialize<RegistrationModel>(temp);
                            InsertResponse insertSession = _sessionService.Create(session);
                            sessionId = insertSession.InsertID;
                        }
                        return Json(new { SessionID = sessionId, MailingID = mailingId }, JsonRequestBehavior.AllowGet);
                    case 2:
                        if (!string.IsNullOrEmpty(registration.Email))
                        {
                            temp.Email = registration.Email;
                        }
                        if (!string.IsNullOrEmpty(registration.OtherEmail))
                        {
                            temp.OtherEmail = registration.OtherEmail;
                        }
                        if (!string.IsNullOrEmpty(registration.Phone))
                        {
                            temp.Phone = registration.Phone;
                        }
                        if (!string.IsNullOrEmpty(registration.Address))
                        {
                            temp.Address = registration.Address;
                        }
                        if (!string.IsNullOrEmpty(registration.City))
                        {
                            temp.City = registration.City;
                        }
                        if (!string.IsNullOrEmpty(registration.Country))
                        {
                            temp.Country = registration.Country;
                        }
                        if (!string.IsNullOrEmpty(registration.WorkAddress))
                        {
                            temp.WorkAddress = registration.WorkAddress;
                        }
                        if (!string.IsNullOrEmpty(registration.Organization))
                        {
                            temp.Organization = registration.Organization;
                        }
                        temp.RegistrationStatus = (int)RegistrationStatus.Created;
                        
                        session = sessionResponse.Item;
                        session.Step = registration.CurrentStep;
                        session.UpdatedDate = DateTime.Now;
                        session.Options = XmlSerializerUltil.Serialize<RegistrationModel>(temp);
                        _sessionService.Update(session);

                        sessionId = hidSession;
                        return Json(new { SessionID = sessionId, MailingID = mailingId }, JsonRequestBehavior.AllowGet);
                    case 3:
                        if (!string.IsNullOrEmpty(registration.ParticipantType))
                        {
                            temp.ParticipantType = registration.ParticipantType;
                        }
                        if (!string.IsNullOrEmpty(registration.YouthConference))
                        {
                            temp.YouthConference = registration.YouthConference;
                        }
                        temp.NeedVisaSupport = registration.NeedVisaSupport;
                        temp.DateOfBirth = registration.DateOfBirth;
                        if (!string.IsNullOrEmpty(registration.OriginalNationality))
                        {
                            temp.OriginalNationality = registration.OriginalNationality;
                        }
                        if (!string.IsNullOrEmpty(registration.CurrentNationality))
                        {
                            temp.CurrentNationality = registration.CurrentNationality;
                        }
                        if (!string.IsNullOrEmpty(registration.PassportNumber))
                        {
                            temp.PassportNumber = registration.PassportNumber;
                        }
                        if (!string.IsNullOrEmpty(registration.TypeOfPassport))
                        {
                            temp.TypeOfPassport = registration.TypeOfPassport;
                        }
                        if (!string.IsNullOrEmpty(registration.Occupation))
                        {
                            temp.Occupation = registration.Occupation;
                        }
                        temp.DateOfPassportIssue = registration.DateOfPassportIssue;
                        temp.DateOfPassportExpiry = registration.DateOfPassportExpiry;
                        if (!string.IsNullOrEmpty(registration.DetailOfEmbassy))
                        {
                            temp.DetailOfEmbassy = registration.DetailOfEmbassy;
                        }

                        session = sessionResponse.Item;
                        session.Step = registration.CurrentStep;
                        session.UpdatedDate = DateTime.Now;
                        session.Completed = true;
                        session.Options = XmlSerializerUltil.Serialize<RegistrationModel>(temp);
                        _sessionService.Update(session);

                        //Create user
                        InsertResponse insertUserResponse = CreateUser(temp);
                        if (insertUserResponse.ErrorCode == (int)ErrorCode.None)
                        {
                            temp.UserID = insertUserResponse.InsertID;
                            InsertResponse insertMailingResponse = CreateMailing(temp);
                            if (insertMailingResponse.ErrorCode == (int)ErrorCode.None)
                            {
                                mailingId = insertMailingResponse.InsertID;
                            }
                        }
                        else
                        {
                            session = sessionResponse.Item;
                            session.Step = registration.CurrentStep;
                            session.UpdatedDate = DateTime.Now;
                            session.Completed = false;
                            session.Options = XmlSerializerUltil.Serialize<RegistrationModel>(temp);
                            _sessionService.Update(session);
                            return Json(new { SessionID = sessionId, MailingID = mailingId, ErrorCode = insertUserResponse.ErrorCode, Message = insertUserResponse.Message }, JsonRequestBehavior.AllowGet);
                        }

                        sessionId = hidSession;
                        //Delete session
                        _sessionService.Delete(sessionId);
                        return Json(new { SessionID = sessionId, MailingID = mailingId }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        private InsertResponse CreateMailing(RegistrationModel registration)
        {
            MailingAddressModel mailing = ConvertMailing(registration);
            string activationcode = Guid.NewGuid().ToString();
            string registrationNumber = DataHelper.GetInstance().GetUniqueNumbers();
            mailing.MailingAddressID = Guid.NewGuid().ToString();
            mailing.CreatedDate = DateTime.Now;
            mailing.ActivationCode = activationcode;
            mailing.RegistrationNumber = registrationNumber;
            InsertResponse response = _mailingService.CreateMailingAddress(mailing);

            if (response.ErrorCode == (int)ErrorCode.None)
            {
                FindItemReponse<UserModel> userresponse = _userService.FindUserByID(mailing.UserID);
                if (userresponse.Item != null)
                {
                    UserModel user = userresponse.Item;
                    user.Password = System.Web.Security.Membership.GeneratePassword(10, 3);
                    _userService.UpdateUser(user);
                    string url = string.Format("{0}://{1}:{2}/Registration/ActiveAccount?activationCode={3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, activationcode);
                    string body = DataHelper.GetInstance().BuildMessage(userresponse.Item.UserName, user.Password, url, registrationNumber);
                    DataHelper.GetInstance().SendEmail(userresponse.Item.Email, REGISTRATION_SUBJECT, body);
                }
            }

            return response;
        }

        private InsertResponse CreateUser(RegistrationModel registration)
        {
            UserModel user = Convert(registration);
            user.UserID = Guid.NewGuid().ToString();
            user.Locked = true;
            user.CreatedDate = DateTime.Now;
            user.Password = System.Web.Security.Membership.GeneratePassword(10, 3);
            user.UserName = user.Email;
            user.RegistrationStatus = (int)RegistrationStatus.Created;

            InsertResponse response = _userService.CreateUser(user);

            return response;
        }

        private UserModel Convert(RegistrationModel registration)
        {
            UserModel user = new UserModel();
            user.UserID = registration.UserID;
            user.Address = registration.Address;
            user.City = registration.City;
            user.Country = registration.Country;
            user.CreatedDate = registration.CreatedDate;
            user.UpdatedDate = registration.UpdatedDate;
            user.DateOfBirth = registration.DateOfBirth;
            user.DisabilitySpecialTreatment = registration.DisabilityOrTreatment;
            user.Email = registration.Email;
            user.FullName = registration.FullName;
            user.Locked = registration.Locked;
            user.RegistrationStatus = registration.RegistrationStatus;
            user.MealPreference = registration.MealPreference;
            user.Organization = registration.Organization;
            user.OtherEmail = registration.OtherEmail;
            user.Password = registration.Password;
            user.PhoneNumber = registration.Phone;
            user.Sex = registration.Sex;
            user.Title = registration.Title;
            user.UserName = registration.UserName;
            user.WorkAddress = registration.WorkAddress;

            return user;
        }

        private RegistrationModel Convert(UserModel user)
        {
            RegistrationModel registration = new RegistrationModel();
            registration.UserID = user.UserID;
            registration.Address = user.Address;
            registration.City = user.City;
            registration.Country = user.Country;
            registration.CreatedDate = user.CreatedDate;
            registration.UpdatedDate = user.UpdatedDate;
            registration.DateOfBirth = user.DateOfBirth;
            registration.DisabilityOrTreatment = user.DisabilitySpecialTreatment;
            registration.Email = user.Email;
            registration.FullName = user.FullName;
            registration.Locked = user.Locked;
            registration.RegistrationStatus = user.RegistrationStatus;
            registration.MealPreference = user.MealPreference;
            registration.Organization = user.Organization;
            registration.OtherEmail = user.OtherEmail;
            registration.Password = user.Password;
            registration.Phone = user.PhoneNumber;
            registration.Sex = user.Sex;
            registration.Title = user.Title;
            registration.UserName = user.UserName;
            registration.WorkAddress = user.WorkAddress;

            return registration;
        }

        public MailingAddressModel ConvertMailing(RegistrationModel registration)
        {
            MailingAddressModel mailing = new MailingAddressModel();
            mailing.UserID = registration.UserID;
            mailing.CurrentNationality = registration.CurrentNationality;
            mailing.DateOfBirth = registration.DateOfBirth;
            mailing.DateOfPassportExpiry = registration.DateOfPassportExpiry;
            mailing.DateOfPassportIssue = registration.DateOfPassportIssue;
            mailing.DetailOfEmbassy = registration.DetailOfEmbassy;
            mailing.Occupation = registration.Occupation;
            mailing.OriginalNationality = registration.OriginalNationality;
            mailing.ParticipantType = registration.ParticipantType;
            mailing.ParticipateYouth = registration.YouthConference;
            mailing.PassportNumber = registration.PassportNumber;
            mailing.PassportPhoto1 = registration.PassportPhoto1;
            mailing.PassportPhoto2 = registration.PassportPhoto2;
            mailing.PassportPhoto3 = registration.PassportPhoto3;
            mailing.TypeOfPassport = registration.TypeOfPassport;

            return mailing;
        }

        public RegistrationModel ConvertMailing(MailingAddressModel mailing)
        {
            RegistrationModel registration = new RegistrationModel();
            registration.UserID = mailing.UserID;
            registration.CurrentNationality = mailing.CurrentNationality;
            registration.DateOfBirth = mailing.DateOfBirth;
            registration.DateOfPassportExpiry = mailing.DateOfPassportExpiry;
            registration.DateOfPassportIssue = mailing.DateOfPassportIssue;
            registration.DetailOfEmbassy = mailing.DetailOfEmbassy;
            registration.Occupation = mailing.Occupation;
            registration.OriginalNationality = mailing.OriginalNationality;
            registration.ParticipantType = mailing.ParticipantType;
            registration.YouthConference = mailing.ParticipateYouth;
            registration.PassportNumber = mailing.PassportNumber;
            registration.PassportPhoto1 = mailing.PassportPhoto1;
            registration.PassportPhoto2 = mailing.PassportPhoto2;
            registration.PassportPhoto3 = mailing.PassportPhoto3;
            registration.TypeOfPassport = mailing.TypeOfPassport;

            return registration;
        }

        [HttpPost]
        public JsonResult Upload(string SessionID)
        {
            try
            {
                RegistrationModel temp = null;
                FindItemReponse<SessionModel> sessionResponse = null;
                SessionModel session = null;
                if (!string.IsNullOrEmpty(SessionID))
                {
                    sessionResponse = _sessionService.FindID(SessionID);
                    if (sessionResponse.Item != null && sessionResponse.Item.Options != null)
                    {
                        temp = XmlSerializerUltil.Deserialize<RegistrationModel>(sessionResponse.Item.Options);
                        if (sessionResponse.Item.Completed)
                        {
                            //Just skip
                            return Json(new { }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    temp = new RegistrationModel();
                }
                int i = 0;
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        //Create Folder
                        try
                        {
                            if (!System.IO.File.Exists(Server.MapPath("~/Content/upload/images/passport/")))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Content/upload/images/passport/"));
                            }
                        }
                        catch (Exception) { }

                        string extension = fileContent.FileName.Substring(fileContent.FileName.LastIndexOf("."));
                        string filename = fileContent.FileName.Substring(0, fileContent.FileName.LastIndexOf(".")).Replace(" ", "-");
                        filename = string.Format("{0}-{1}", filename, UrlSlugger.Get8Digits());
                        fileContent.SaveAs(Server.MapPath("~/Content/upload/images/passport/" + filename + extension));
                        switch (i)
                        {
                            case 0:
                                temp.PassportPhoto1 = "/Content/upload/images/passport/" + filename + extension;
                                break;
                            case 1:
                                temp.PassportPhoto2 = "/Content/upload/images/passport/" + filename + extension;
                                break;
                            case 2:
                                temp.PassportPhoto3 = "/Content/upload/images/passport/" + filename + extension;
                                break;
                            default:
                                return Json("File uploaded successfully");
                        }
                        i++;
                        session = sessionResponse.Item;
                        session.UpdatedDate = DateTime.Now;
                        session.Options = XmlSerializerUltil.Serialize<RegistrationModel>(temp);
                        _sessionService.Update(session);
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }
            return Json("File uploaded successfully");
        }
    }
}

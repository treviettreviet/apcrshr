using System;
using System.Collections.Generic;
using System.Linq;
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

        public RegistrationController()
        {
            this._userService = new UserService();
            this._mailingService = new MailingAddressService();
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
            return View();
        }

        [HttpPost]
        public JsonResult RegistrationWizard(RegistrationModel registration, HttpPostedFileBase file)
        {
            InsertResponse response = new InsertResponse();
            FindItemReponse<UserModel> userResponse = new FindItemReponse<UserModel>();
            RegistrationModel temp = null;
            if (Session["Registration"] == null)
            {
                Session["Registration"] = registration;
            }
            else
            {
                temp = (RegistrationModel)Session["Registration"];
            }
            if (registration != null)
            {
                if (temp == null)
                {
                    temp = new RegistrationModel();
                }
                if (!string.IsNullOrEmpty(temp.Email))
                {
                    userResponse = _userService.FindUserByEmail(temp.Email);
                }
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
                        temp.CurrentStep = 1;
                        Session["Registration"] = temp;
                        break;
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
                        temp.CurrentStep = 2;
                        temp.RegistrationStatus = (int)RegistrationStatus.Created;
                        response = CreateUser(temp);
                        if (response.ErrorCode == (int)ErrorCode.None)
                        {
                            temp.UserID = response.InsertID;
                        }
                        Session["Registration"] = temp;
                        break;
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
                        response = CreateMailing(temp);
                        if (response.ErrorCode == (int)ErrorCode.None)
                        {
                            temp.MailingAddressID = response.InsertID;
                        }
                        Session["Registration"] = temp;
                        break;
                    default:
                        break;
                }
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        private InsertResponse CreateMailing(RegistrationModel registration)
        {
            MailingAddressModel mailing = ConvertMailing(registration);
            string activationcode = Guid.NewGuid().ToString();
            mailing.MailingAddressID = Guid.NewGuid().ToString();
            mailing.CreatedDate = DateTime.Now;
            mailing.ActivationCode = activationcode;
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
                    string body = DataHelper.GetInstance().BuildMessage(userresponse.Item.UserName, user.Password, url);
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
    }
}

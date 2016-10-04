using apcrshr_site.Filters;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using apcrshr_site.Models;
using Site.Core.Repository;
using apcrshr_site.Helper;

namespace apcrshr_site.Controllers
{
    [OutputCache(NoStore = true, Duration = 60, VaryByParam = "*")]
    public class UserController : BaseController
    {
        //Third party payment information
        private static readonly string SECURE_SECRET = "6D0870CDE5F24F34F3915FB0045120DB";
        private static readonly string VIRTUAL_PAYMENT_CLIENT = "https://mtf.onepay.vn/vpcpay/vpcpay.op";
        private static readonly string vpc_Version = "2";
        private static readonly string vpc_Command = "pay";
        //Test account
        private static readonly string vpc_AccessCode = "6BEB2546";
        private static readonly string vpc_Merchant = "TESTONEPAY";
        private static readonly string vpc_Locale = "en";
        //Return url
        private static readonly string vpc_ReturnURL = "http://localhost:56742/User/PaymentReturn";
        //Currency
        private static readonly string FROM_CURRENCY = "USD";
        private static readonly string TO_CURRENCY = "VND";

        private IUserService _userService;
        private IMailingAddressService _mailingService;
        private IPaymentService _paymentService;

        public UserController()
        {
            this._userService = new UserService();
            this._mailingService = new MailingAddressService();
            this._paymentService = new PaymentService();
        }


        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult UserLogin(UserModel user)
        {
            if (ModelState.IsValid)
            {
                UserLoginResponse response = _userService.Login(user.UserName, user.Password);
                if (response.ErrorCode == (int)ErrorCode.None)
                {
                    this.Session["User-UserID"] = response.UserId;
                    this.Session["User-SessionID"] = response.SessionId;
                    this.Session["User-UserName"] = response.UserName;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Response = response;
            }
            return View("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult RegisterUser(UserModel user)
        {
            if (ModelState.IsValid)
            {
                InsertResponse response = new InsertResponse();
                FindItemReponse<UserModel> usernameResponse = _userService.CheckUsername(user.UserName);
                if (usernameResponse.Item != null)
                {
                    response = new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = Resources.Resource.msg_username_exist
                    };
                    ViewBag.Response = response;
                    return View("Register");
                }
                else
                {
                    FindItemReponse<UserModel> emailResponse = _userService.CheckEmail(user.Email);
                    if (emailResponse.Item != null)
                    {
                        response = new InsertResponse
                        {
                            ErrorCode = (int)ErrorCode.Error,
                            Message = Resources.Resource.msg_email_exist
                        };
                        ViewBag.Response = response;
                        return View("Register");
                    }
                    else
                    {
                        user.UserID = Guid.NewGuid().ToString();
                        user.CreatedDate = DateTime.Now;
                        response = _userService.CreateUser(user);
                        ViewBag.Response = response;
                    }
                }
            }
            return View("Register");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            string sessionId = string.Empty;
            if (this.Session["User-SessionID"] != null)
            {
                sessionId = this.Session["User-SessionID"].ToString();
            }
            if (string.IsNullOrEmpty(sessionId))
            {
                RedirectToAction("Index", "Home");
            }
            BaseResponse response = _userService.Logout(sessionId);
            this.Session["User-SessionID"] = null;
            this.Session["User-UserName"] = null;
            this.Session["User-UserID"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [UserSessionFilter]
        public ActionResult ViewProfile()
        {
            if (this.Session["User-UserID"] != null)
            {
                RegistrationModel registration = new RegistrationModel();
                FindItemReponse<UserModel> response = _userService.FindUserByID(Session["User-UserID"].ToString());
                if (response.Item != null)
                {
                    registration.UserID = response.Item.UserID;
                    registration.Title = response.Item.Title;
                    registration.FullName = response.Item.FullName;
                    registration.Sex = response.Item.Sex;
                    registration.Email = response.Item.Email;
                    registration.OtherEmail = response.Item.OtherEmail;
                    registration.DateOfBirth = response.Item.DateOfBirth;
                    registration.Phone = response.Item.PhoneNumber;
                    registration.UserName = response.Item.UserName;
                    registration.MealPreference = response.Item.MealPreference;
                    registration.DisabilityOrTreatment = response.Item.DisabilitySpecialTreatment;
                    registration.Address = response.Item.Address;
                    registration.City = response.Item.City;
                    registration.Country = response.Item.Country;
                    registration.WorkAddress = response.Item.WorkAddress;
                    registration.Organization = response.Item.Organization;
                }
                FindAllItemReponse<MailingAddressModel> mailingResponse = _mailingService.FindMailingAddressByUser(Session["User-UserID"].ToString());
                if (mailingResponse.Items != null)
                {
                    var mailing = mailingResponse.Items.SingleOrDefault();
                    if (mailing != null)
                    {
                        registration.MailingAddressID = mailing.MailingAddressID;
                        registration.ParticipantType = mailing.ParticipantType;
                        registration.YouthConference = mailing.ParticipateYouth;
                        registration.OriginalNationality = mailing.OriginalNationality;
                        registration.CurrentNationality = mailing.CurrentNationality;
                        registration.PassportNumber = mailing.PassportNumber;
                        registration.TypeOfPassport = mailing.TypeOfPassport;
                        registration.Occupation = mailing.Occupation;
                        registration.DateOfPassportIssue = mailing.DateOfPassportIssue;
                        registration.DateOfPassportExpiry = mailing.DateOfPassportExpiry;
                        registration.PassportPhoto1 = mailing.PassportPhoto1;
                        registration.PassportPhoto2 = mailing.PassportPhoto2;
                        registration.PassportPhoto3 = mailing.PassportPhoto3;
                        registration.DetailOfEmbassy = mailing.DetailOfEmbassy;
                        registration.NeedVisaSupport = mailing.NeedVisaSupport;

                        //Get fee
                        int fee = -1;

                        //Find payment
                        FindAllItemReponse<PaymentModel> paymentResponse = _paymentService.FindByUserID(Session["User-UserID"].ToString());
                        if (paymentResponse != null && paymentResponse.Items != null && paymentResponse.Items.Count > 0)
                        {
                            var paid = paymentResponse.Items.Where(p => p.PaymentType.Equals(registration.ParticipantType)).SingleOrDefault();
                            if (paid != null && paid.Status == (int) PaymentStatus.Completed)
                            {
                                fee = 0;
                            }
                            else
                            {
                                ViewBag.PaymentStatus = "Your transaction has an <strong>error</strong> occurred, please contact administrator!";
                            }
                        }
                        else
                        {
                            //Caculate payment
                            DateTime earlyBird = new DateTime(2017, 7, 30);
                            switch (registration.ParticipantType)
                            {
                                case "International delegates":
                                    if (DateTime.UtcNow <= earlyBird)
                                    {
                                        fee = 500;
                                    }
                                    else
                                    {
                                        fee = 550;
                                    }
                                    break;
                                case "Vietnamese delegate":
                                    if (DateTime.UtcNow <= earlyBird)
                                    {
                                        fee = 150;
                                    }
                                    else
                                    {
                                        fee = 200;
                                    }
                                    break;
                                case "International youth":
                                    fee = 500;
                                    break;
                                case "Vietnamese youth":
                                    fee = 100;
                                    break;
                                default:
                                    fee = -1;
                                    break;
                            }
                        }
                        ViewBag.PaymentFee = fee;
                    }
                }
                return View(registration);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult RegistrationIncompleted()
        {
            return View();
        }

        [HttpGet]
        [UserSessionFilter]
        public ActionResult MakePayment()
        {
            FindItemReponse<UserModel> response = _userService.FindUserByID(Session["User-UserID"].ToString());
            if (response.Item != null)
            {
                FindAllItemReponse<MailingAddressModel> mailingResponse = _mailingService.FindMailingAddressByUser(Session["User-UserID"].ToString());
                if (mailingResponse.Items != null)
                {
                    var mailing = mailingResponse.Items.SingleOrDefault();
                    if (mailing != null)
                    {
                        //Caculate payment
                        //Get fee
                        int fee = -1;

                        DateTime earlyBird = new DateTime(2017, 7, 30);
                        switch (mailing.ParticipantType)
                        {
                            case "International delegates":
                                if (DateTime.UtcNow <= earlyBird)
                                {
                                    fee = 500;
                                }
                                else
                                {
                                    fee = 550;
                                }
                                break;
                            case "Vietnamese delegate":
                                if (DateTime.UtcNow <= earlyBird)
                                {
                                    fee = 150;
                                }
                                else
                                {
                                    fee = 200;
                                }
                                break;
                            case "International youth":
                                fee = 500;
                                break;
                            case "Vietnamese youth":
                                fee = 100;
                                break;
                            default:
                                fee = -1;
                                break;
                        }
                        if (fee == -1)
                        {
                            return RedirectToAction("RegistrationIncompleted");
                        }

                        //Parse currency
                        decimal amount = 0;
                        try
                        {
                            decimal usdrate = DataHelper.GetInstance().GetCurrencyRate(FROM_CURRENCY, 22265);

                            amount = fee * usdrate;
                            if (amount == 0)
                            {
                                return RedirectToAction("Index", "RequestError");
                            }
                            //amount X 100 before parse to OnePay
                            amount = amount * 100;
                        }
                        catch (Exception)
                        {
                            return RedirectToAction("Index", "RequestError");
                        }
                        // Khoi tao lop thu vien va gan gia tri cac tham so gui sang cong thanh toan
                        VPCRequest conn = new VPCRequest(VIRTUAL_PAYMENT_CLIENT);
                        conn.SetSecureSecret(SECURE_SECRET);

                        // Add the Digital Order Fields for the functionality you wish to use
                        // Core Transaction Fields
                        conn.AddDigitalOrderField("AgainLink", "http://onepay.vn");
                        conn.AddDigitalOrderField("Title", "onepay paygate");

                        //Chon ngon ngu hien thi tren cong thanh toan (vn/en)
                        conn.AddDigitalOrderField("vpc_Locale", vpc_Locale);
                        conn.AddDigitalOrderField("vpc_Version", vpc_Version);
                        conn.AddDigitalOrderField("vpc_Command", vpc_Command);

                        //Test account
                        conn.AddDigitalOrderField("vpc_Merchant", vpc_Merchant);
                        conn.AddDigitalOrderField("vpc_AccessCode", vpc_AccessCode);
                        conn.AddDigitalOrderField("vpc_MerchTxnRef", response.Item.UserID);

                        //Package order
                        conn.AddDigitalOrderField("vpc_OrderInfo", mailing.ParticipantType);
                        conn.AddDigitalOrderField("vpc_Amount", amount.ToString());

                        //Return url
                        conn.AddDigitalOrderField("vpc_ReturnURL", vpc_ReturnURL);

                        // Thong tin them ve khach hang. De trong neu khong co thong tin
                        //conn.AddDigitalOrderField("vpc_SHIP_Street01", "194 Tran Quang Khai");
                        //conn.AddDigitalOrderField("vpc_SHIP_Provice", "Hanoi");
                        //conn.AddDigitalOrderField("vpc_SHIP_City", "Hanoi");
                        //conn.AddDigitalOrderField("vpc_SHIP_Country", "Vietnam");
                        //conn.AddDigitalOrderField("vpc_Customer_Phone", "043966668");
                        //conn.AddDigitalOrderField("vpc_Customer_Email", "support@onepay.vn");
                        //conn.AddDigitalOrderField("vpc_Customer_Id", "onepay_paygate");

                        // Dia chi IP cua khach hang
                        conn.AddDigitalOrderField("vpc_TicketNo", Request.UserHostAddress);

                        // Chuyen huong trinh duyet sang cong thanh toan
                        String url = conn.Create3PartyQueryString();
                        return Redirect(url);
                    }
                }
            }
            return RedirectToAction("RegistrationIncompleted");
        }

        [HttpGet]
        [UserSessionFilter]
        public ActionResult PaymentReturn()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult SendPassword(string email)
        {
            FindItemReponse<UserModel> user = null;
            BaseResponse response = new BaseResponse();
            if (email != null)
            {
                user = _userService.FindUserByEmail(email);
                if (user.Item != null)
                {
                    string password = Guid.NewGuid().ToString("D").Substring(1, 6);

                    
                    UserModel _user = user.Item;
                    _user.UpdatedDate = DateTime.Now;
                    _user.Password = System.Web.Security.Membership.GeneratePassword(10, 3);
                    response = _userService.UpdateUser(_user);
                    if (response.ErrorCode == (int)ErrorCode.None)
                    {
                        response.Message = Resources.Resource.msg_forgotPassword_emailSend;

                        string body = string.Format("Your password: <b>{0}</b>", _user.Password);
                        DataHelper.GetInstance().SendEmail(user.Item.Email, "Password recovery", body);
                    }
                }
                else
                {
                    response = new BaseResponse { ErrorCode = (int)ErrorCode.Error, Message = Resources.Resource.msg_invalidEmail };
                }
            }
            ViewBag.Message = response;
            return View("ForgetPassword");
        }

        [UserSessionFilter]
        public ActionResult UpdateUser()
        {
            if (this.Session["User-SessionID"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                FindItemReponse<UserModel> response = _userService.FindUserByID(this.Session["User-UserID"].ToString());
                return View(response.Item);
            }
        }

        [UserSessionFilter]
        [HttpPost]
        
        public ActionResult SaveUpdateUser(UserModel user)
        {
            user.UpdatedDate = DateTime.Now;
            user.UserID = Session["User-UserID"].ToString();
            BaseResponse response = _userService.UpdateUser(user);
            ViewBag.Message = response;
            return View("UpdateUser", user);
        }

        [HttpPost]
        public JsonResult SaveUserInfomation(RegistrationModel registration)
        {
            if (this.Session["User-SessionID"] == null)
            {
                return Json(new { ErrorCode = (int) ErrorCode.Redirect}, JsonRequestBehavior.AllowGet);
            }
            BaseResponse response = new BaseResponse();
            FindItemReponse<UserModel> userResponse = _userService.FindUserByID(registration.UserID);
            if (userResponse.Item != null)
            {
                var user = userResponse.Item;
                user.Title = registration.Title != "Empty" ? registration.Title : user.Title;
                user.Sex = registration.Sex != "Empty" ? registration.Sex : user.Sex;
                user.MealPreference = registration.MealPreference != "Empty" ? registration.MealPreference : user.MealPreference;
                user.DisabilitySpecialTreatment = registration.DisabilityOrTreatment != "Empty" ? registration.DisabilityOrTreatment : user.DisabilitySpecialTreatment;
                user.OtherEmail = registration.OtherEmail != "Empty" ? registration.OtherEmail : user.OtherEmail;
                user.PhoneNumber = registration.Phone != "Empty" ? registration.Phone : user.PhoneNumber;
                user.Address = registration.Address != "Empty" ? registration.Address : user.Address;
                user.City = registration.City != "Empty" ? registration.City : user.City;
                user.Country = registration.Country != "Empty" ? registration.Country : user.Country;
                user.WorkAddress = registration.WorkAddress != "Empty" ? registration.WorkAddress : user.WorkAddress;
                user.Organization = registration.Organization != "Empty" ? registration.Organization : user.Organization;
                //Don't update password
                user.Password = null;

                //Updare user
                response = _userService.UpdateUser(user);
            }
            else
            {
                response.ErrorCode = (int)ErrorCode.Error;
                response.Message = Resources.Resource.msg_commonError;
            }
            FindItemReponse<MailingAddressModel> mailingResponse = _mailingService.FindMailingAddressByID(registration.MailingAddressID);
            if (mailingResponse.Item != null)
            {
                var mailing = mailingResponse.Item;
                mailing.ParticipantType = registration.ParticipantType != "Empty" ? registration.ParticipantType : mailing.ParticipantType;
                mailing.ParticipateYouth = registration.YouthConference;
                mailing.NeedVisaSupport = registration.NeedVisaSupport;
                mailing.OriginalNationality = registration.OriginalNationality != "Empty" ? registration.OriginalNationality : mailing.OriginalNationality;
                mailing.CurrentNationality = registration.CurrentNationality != "Empty" ? registration.CurrentNationality : mailing.CurrentNationality;
                mailing.Occupation = registration.Occupation != "Empty" ? registration.Occupation : mailing.Occupation;
                mailing.DetailOfEmbassy = registration.DetailOfEmbassy != "Empty" ? registration.DetailOfEmbassy : mailing.DetailOfEmbassy;

                //Update mailing
                response = _mailingService.UpdateMailingAddress(mailing);
            }
            else
            {
                response.ErrorCode = (int)ErrorCode.Error;
                response.Message = Resources.Resource.msg_commonError;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        
        public ActionResult ChangePassword(string UserID, string CurrentPassword, string NewPassword)
        {
            string sessionId = Session["User-SessionID"].ToString();
            UserSession userSession = SessionUtil.GetInstance.VerifySession(sessionId);
            if (userSession == null)
            {
                return Json(new { ErrorCode = (int)ErrorCode.Redirect, Message = Resources.Resource.msg_sessionTimeOut });
            }

            //Check user
            FindItemReponse<UserModel> userReponse = _userService.FindUserByID(UserID);
            if (userReponse.Item == null)
            {
                return Json(new { ErrorCode = (int)ErrorCode.Error, Message = Resources.Resource.msg_invalidUser });
            }

            UserLoginResponse loginresponse = _userService.Login(userReponse.Item.UserName, CurrentPassword);
            if (loginresponse.ErrorCode != (int)ErrorCode.None)
            {
                return Json(new { ErrorCode = loginresponse.ErrorCode, Message = loginresponse.Message });
            }

            //Update password
            BaseResponse response = _userService.ChangePassword(UserID, NewPassword);

            return Json(response);
        }
    }
}

﻿using apcrshr_site.Filters;
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
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace apcrshr_site.Controllers
{
    [OutputCache(NoStore = true, Duration = 60, VaryByParam = "*")]
    public class UserController : BaseController
    {
        private static readonly string PAYMENT_TITLE = "Acknowledgement of Payment - Registration for APCRSHR9 - Transaction ID {0}";
        //Third party payment information
        private static readonly string SECURE_SECRET = "CE73CA8FAE51D3D33D56F5D44812B409";
        private static readonly string VIRTUAL_PAYMENT_CLIENT = "https://onepay.vn/vpcpay/vpcpay.op";
        private static readonly string vpc_Version = "2";
        private static readonly string vpc_Command = "pay";
        //Test account
        private static readonly string vpc_AccessCode = "BEB4FDDB";
        private static readonly string vpc_Merchant = "APCRSHR9VN";
        private static readonly string vpc_Locale = "en";
        //Return url
		private static readonly string vpc_ReturnURL = "http://apcrshr9vn.org/User/PaymentReturn";
        //Currency
        private static readonly string FROM_CURRENCY = "USD";
        private static readonly string TO_CURRENCY = "VND";

        private IUserService _userService;
        private IMailingAddressService _mailingService;
        private IPaymentService _paymentService;
        private IUserSubmissionService _userSubmissionService;
        private ILogisticSheduleService _logisticService;
        private ITransactionHistoryService _transaction;

        public UserController()
        {
            this._userService = new UserService();
            this._mailingService = new MailingAddressService();
            this._paymentService = new PaymentService();
            this._userSubmissionService = new UserSubmissionService();
            this._logisticService = new LogisticSheduleService();
            this._transaction = new TransactionHistoryService();
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

                    //Find abstract
                    FindAllItemReponse<UserSubmissionModel> abstractResponse = _userSubmissionService.FindByUserID(response.Item.UserID);
                    ViewBag.Abstracts = abstractResponse.Items;
                }
                FindItemReponse<LogisticScheduleModel> logisticResponse = _logisticService.FindByUserID(Session["User-UserID"].ToString());
                registration.Logistic = logisticResponse.Item != null ? logisticResponse.Item : new LogisticScheduleModel();
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
                        var paymentCompleted = paymentResponse.Items.Where(p => p.PaymentType.Equals(registration.ParticipantType)
                            && (p.Status == (int)PaymentStatus.Completed 
                            || p.Status == (int)PaymentStatus.Cash
                            || p.Status == (int)PaymentStatus.BankTransfer)).ToList();
                        if (paymentCompleted != null && paymentCompleted.Count > 0)
                        {
                            fee = 0;
                            //var paid = paymentResponse.Items.Where(p => p.PaymentType.Equals(registration.ParticipantType)).SingleOrDefault();
                            //if (paid != null && (paid.Status == (int) PaymentStatus.Completed || paid.Status == (int) PaymentStatus.Cash))
                            //{
                            //    fee = 0;
                            //}
                            //else
                            //{
                            //    ViewBag.PaymentStatus = "Your transaction has an <strong>error</strong> occurred, please contact administrator!";
                            //}
                        }
                        else
                        {
                            //Caculate payment
                            DateTime earlyBird = new DateTime(2017, 6, 30);
                            DateTime regular = new DateTime(2017, 11, 26);
                            int age = DataHelper.GetInstance().CalculateAge(response.Item.DateOfBirth.Value);
                            switch (mailing.ParticipantType)
                            {
                                case "International delegates":
                                case "International youth":
                                    if (age < 25)
                                    {
                                        fee = 150;
                                    }
                                    else
                                    {
                                        if (DateTime.UtcNow <= earlyBird)
                                        {
                                            fee = 550;
                                        }
                                        else
                                        {
                                            fee = 600;
                                        }
                                    }
                                    break;
                                case "Vietnamese delegate":
                                case "Vietnamese youth":
                                    if (age < 25)
                                    {
                                        fee = 100;
                                    }
                                    else
                                    {
                                        if (DateTime.UtcNow <= earlyBird)
                                        {
                                            fee = 200;
                                        }
                                        else
                                        {
                                            fee = 250;
                                        }
                                    }
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

                        DateTime earlyBird = new DateTime(2017, 6, 30);
                        DateTime regular = new DateTime(2017, 11, 26);
                        int age = DataHelper.GetInstance().CalculateAge(response.Item.DateOfBirth.Value);
                        switch (mailing.ParticipantType)
                        {
                            case "International delegates":
                            case "International youth":
                                if (age < 25)
                                {
                                    fee = 150;
                                }
                                else
                                {
                                    if (DateTime.UtcNow <= earlyBird)
                                    {
                                        fee = 550;
                                    }
                                    else
                                    {
                                        fee = 600;
                                    }
                                }
                                break;
                            case "Vietnamese delegate":
                            case "Vietnamese youth":
                                if (age < 25)
                                {
                                    fee = 100;
                                }
                                else
                                {
                                    if (DateTime.UtcNow <= earlyBird)
                                    {
                                        fee = 200;
                                    }
                                    else
                                    {
                                        fee = 250;
                                    }
                                }
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
                        decimal usdrate = 0;
                        try
                        {
                            usdrate = DataHelper.GetInstance().GetCurrencyRate(FROM_CURRENCY, 22265);
                        }
                        catch (Exception)
                        {
                            //Try convert using google
                            try
                            {
                                string _amount = DataHelper.GetInstance().CurrencyConvert(fee, FROM_CURRENCY, TO_CURRENCY);
                                _amount = _amount.Substring(0, _amount.IndexOf(" "));
                                amount = decimal.Parse(_amount);
                            }
                            catch (Exception)
                            {
                                return RedirectToAction("Index", "RequestError");
                            }
                        }

                        //Calculate amount
                        if (usdrate != 0)
                        {
                            amount = fee * usdrate;
                        }
                        if (amount == 0)
                        {
                            return RedirectToAction("Index", "RequestError");
                        }
                        //amount X 100 before parse to OnePay
                        amount = amount * 100;

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
                        string subId = string.Format("{0}", DateTime.Now.Ticks);
                        conn.AddDigitalOrderField("vpc_Merchant", vpc_Merchant);
                        conn.AddDigitalOrderField("vpc_AccessCode", vpc_AccessCode);
                        conn.AddDigitalOrderField("vpc_MerchTxnRef", subId);

                        //Package order
                        conn.AddDigitalOrderField("vpc_Amount", amount.ToString());

                        var transactionReference = DateTime.Now.Ticks;

                        StringBuilder builder = new StringBuilder();
                        builder.Append(string.Format("Transaction vpc_MerchTxnRef {0}, ", subId));
                        builder.Append(string.Format("Transaction vpc_Merchant {0}, ", vpc_Merchant));
                        builder.Append(string.Format("Transaction vpc_Amount {0}, ", amount));
                        builder.Append(string.Format("Transaction fullname {0}, ", response.Item.FullName));
                        builder.Append(string.Format("Transaction email {0}", response.Item.Email));
                        builder.Append(string.Format("Transaction reference {0}", transactionReference));

                        TransactionHistoryModel trans = new TransactionHistoryModel
                        {
                            Action = "Create payment",
                            CreatedDate = DateTime.Now,
                            Log = builder.ToString(),
                            Status = (int)TransactionStatus.Created,
                            UserId = response.Item.UserID,
                            Email = response.Item.Email,
                            TransactionReference = transactionReference
                        };

                        var insertResponse = _transaction.Create(trans);

                        //Order info
                        conn.AddDigitalOrderField("vpc_OrderInfo", transactionReference.ToString());

                        //Return url
                        conn.AddDigitalOrderField("vpc_ReturnURL", vpc_ReturnURL);

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
        public ActionResult PaymentReturn()
        {
            string hashvalidateResult = "";
            string vpc_TxnResponseCode = "Unknown";
            string amount = "Unknown";
            string localed = "Unknown";
            string command = "Unknown";
            string version = "Unknown";
            string cardType = "Unknown";
            string orderInfo = "Unknown";
            string merchantID = "Unknown";
            string authorizeID = "Unknown";
            string merchTxnRef = "Unknown";
            string transactionNo = "Unknown";
            string acqResponseCode = "Unknown";
            string txnResponseCode = "Unknown";
            string message = "Unknown";
            string msg = string.Empty;

            try
            {
                // Khoi tao lop thu vien
                VPCRequest conn = new VPCRequest("http://onepay.vn");
                conn.SetSecureSecret(SECURE_SECRET);
                // Xu ly tham so tra ve va kiem tra chuoi du lieu ma hoa
                hashvalidateResult = conn.Process3PartyResponse(Request.QueryString);
                // Lay gia tri tham so tra ve tu cong thanh toan
                vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
                amount = conn.GetResultField("vpc_Amount", "Unknown");
                localed = conn.GetResultField("vpc_Locale", "Unknown");
                command = conn.GetResultField("vpc_Command", "Unknown");
                version = conn.GetResultField("vpc_Version", "Unknown");
                cardType = conn.GetResultField("vpc_Card", "Unknown");
                orderInfo = conn.GetResultField("vpc_OrderInfo", "Unknown");
                merchantID = conn.GetResultField("vpc_Merchant", "Unknown");
                authorizeID = conn.GetResultField("vpc_AuthorizeId", "Unknown");
                merchTxnRef = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
                transactionNo = conn.GetResultField("vpc_TransactionNo", "Unknown");
                acqResponseCode = conn.GetResultField("vpc_AcqResponseCode", "Unknown");
                txnResponseCode = vpc_TxnResponseCode;
                message = conn.GetResultField("vpc_Message", "Unknown");
            }
            catch (Exception ex)
            {
                msg = "The payment is in-progress, please ask administrator for looking at this!";
                Log(merchTxnRef, merchantID, amount, orderInfo, hashvalidateResult, ex.StackTrace);

                return View(new InsertResponse { Message = "An error occurs at payment service, please contact administrator!", ErrorCode = (int)ErrorCode.Error });
            }

            //Find user
            var participantType = "";
            long reference = 0;
            long.TryParse(orderInfo, out reference);
            var transactionResponse = _transaction.FindByTransactionReference(reference);

            string userID = "";
            string email = "";
            if (transactionResponse.Items != null && transactionResponse.Items.Count > 0)
            {
                FindItemReponse<UserModel> userResponse = _userService.FindUserByID(transactionResponse.Items.First().UserId);
                if (userResponse.Item != null)
                {
                    userID = userResponse.Item.UserID;
                    email = userResponse.Item.Email;
                    FindAllItemReponse<MailingAddressModel> mailingResponse = _mailingService.FindMailingAddressByUser(userResponse.Item.UserID);
                    if (mailingResponse.Items != null)
                    {
                        var mailing = mailingResponse.Items.SingleOrDefault();
                        if (mailing != null)
                        {
                            participantType = mailing.ParticipantType;
                        }
                    }
                }
            }

            //Save payment
            PaymentModel payment = new PaymentModel();
            payment.PaymentID = Guid.NewGuid().ToString();
            payment.UserID = userID;
            payment.Amount = double.Parse(amount) / 100;
            payment.CreatedBy = userID;
            payment.CreatedDate = DateTime.Now;
            payment.MerchRef = merchTxnRef;
            payment.PaymentType = participantType;

            if (string.IsNullOrEmpty(payment.PaymentType))
            {
                payment.PaymentType = "Unknown";
            }

            try
            {
                //Validate transaction
                if (hashvalidateResult == "CORRECTED" && txnResponseCode.Trim() == "0")
                {
                    //vpc_Result.Text = "Transaction was paid successful";
                    payment.Status = (int)PaymentStatus.Completed;
                    msg = "Your payment was paid successful!";

                    //Sending email
                    //USD
                    decimal usd = 0;
                    decimal usdrate = 0;
                    try
                    {
                        usdrate = DataHelper.GetInstance().GetCurrencyRate(FROM_CURRENCY, 22265);
                        usd = decimal.Parse(amount) / usdrate;
                    }
                    catch (Exception) { }

                    string messageBody = DataHelper.GetInstance().BuildInvoicePdfTemplate(payment.PaymentType, merchTxnRef, transactionNo, usd.ToString(), amount, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture));
                    string title = string.Format(PAYMENT_TITLE, transactionNo);
                    Task.Run(() => DataHelper.GetInstance().SendEmail(email, title, messageBody));
                }
                else if (hashvalidateResult == "INVALIDATED" && txnResponseCode.Trim() == "0")
                {
                    //vpc_Result.Text = "Transaction is pending";
                    payment.Status = (int)PaymentStatus.Pending;
                    msg = "Your payment was in pending status, please contact our administrator!";
                }
                else
                {
                    //vpc_Result.Text = "Transaction was not paid successful";
                    payment.Status = (int)PaymentStatus.Error;
                    msg = "The payment was not paid successful, please try again!";
                }
            }
            catch (Exception ex)
            {
                msg = "The payment is in-progress, please ask administrator for looking at this!";
                Log(merchTxnRef, merchantID, amount, orderInfo, hashvalidateResult, ex.StackTrace);
            }

            InsertResponse _response = _paymentService.Create(payment);
            _response.Message = msg;

            // Log info
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(string.Format("Transaction vpc_MerchTxnRef {0}, ", merchTxnRef));
            strBuilder.Append(string.Format("Transaction vpc_Merchant {0}, ", merchantID));
            strBuilder.Append(string.Format("Transaction vpc_Amount {0}, ", amount));
            strBuilder.Append(string.Format("Transaction vpc_OrderInfo {0}, ", orderInfo));
            strBuilder.Append(string.Format("Transaction hashvalidateResult {0}, ", hashvalidateResult));
            strBuilder.Append(string.Format("Transaction userId {0}, ", userID));
            strBuilder.Append(string.Format("Transaction email {0}, ", email));

            TransactionHistoryModel transaction = new TransactionHistoryModel
            {
                Action = "Payment completed",
                CreatedDate = DateTime.Now,
                Log = strBuilder.ToString(),
                Status = (int)TransactionStatus.Completed,
                UserId = merchTxnRef,
                Email = email,
                TransactionReference = reference
            };
            _transaction.Create(transaction);

            return View(_response);
        }

        private void Log(string merchTxnRef, string merchantID, string amount, string orderInfo, string hashvalidateResult, string exception)
        {
            //Find user
            string email = "";
            string userId = "";
            long reference = 0;
            long.TryParse(orderInfo, out reference);
            var transactionResponse = _transaction.FindByTransactionReference(reference);
            if (transactionResponse.Items != null && transactionResponse.Items.Count > 0)
            {
                FindItemReponse<UserModel> userResponse = _userService.FindUserByID(transactionResponse.Items.First().UserId);
                if (userResponse.Item != null)
                {
                    email = userResponse.Item.Email;
                    userId = userResponse.Item.UserID;
                }
            }

            // Log info
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("Transaction email {0}, ", email));
            builder.Append(string.Format("Transaction vpc_MerchTxnRef {0}, ", merchTxnRef));
            builder.Append(string.Format("Transaction vpc_Merchant {0}, ", merchantID));
            builder.Append(string.Format("Transaction vpc_Amount {0}, ", amount));
            builder.Append(string.Format("Transaction vpc_OrderInfo {0}, ", orderInfo));
            builder.Append(string.Format("Transaction hashvalidateResult {0}, ", hashvalidateResult));
            builder.Append(string.Format("Transaction error messate {0}, ", exception));

            string body = string.Format("The transaction error info: <b>{0}</b></br> Please contact administrator.", builder.ToString());
            Task.Run(() => DataHelper.GetInstance().SendEmail(email, "The transaction error", body));

            TransactionHistoryModel trans = new TransactionHistoryModel
            {
                Action = "Payment return",
                CreatedDate = DateTime.Now,
                Log = builder.ToString(),
                Status = (int)TransactionStatus.Error,
                UserId = userId,
                Email = email,
                TransactionReference = reference
            };
            _transaction.Create(trans);
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
                        Task.Run(() => DataHelper.GetInstance().SendEmail(user.Item.Email, "Password recovery", body));
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
                return Json(new { ErrorCode = (int)ErrorCode.Redirect }, JsonRequestBehavior.AllowGet);
            }
            BaseResponse response = new BaseResponse();
            FindItemReponse<UserModel> userResponse = _userService.FindUserByID(registration.UserID);
            if (userResponse.Item != null)
            {
                var user = userResponse.Item;
                user.Title = registration.Title != "Empty" ? registration.Title : user.Title;
                user.Email = registration.Email != "Empty" ? registration.Email : user.Email;
                user.FullName = registration.FullName != "Empty" ? registration.FullName : user.FullName;
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
                user.DateOfBirth = registration.DateOfBirth.HasValue ? registration.DateOfBirth : user.DateOfBirth;
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
                mailing.PassportNumber = registration.PassportNumber != "Empty" ? registration.PassportNumber : mailing.PassportNumber;
                mailing.DateOfPassportIssue = registration.DateOfPassportIssue.HasValue ? registration.DateOfPassportIssue.Value : mailing.DateOfPassportIssue;
                mailing.DateOfPassportExpiry = registration.DateOfPassportExpiry.HasValue ? registration.DateOfPassportExpiry.Value : mailing.DateOfPassportExpiry;

                //Update mailing
                response = _mailingService.UpdateMailingAddress(mailing);
            }
            else
            {
                var mailing = new MailingAddressModel
                {
                    MailingAddressID = Guid.NewGuid().ToString(),
                    ActivationCode = UrlSlugger.Get8Digits(),
                    CreatedDate = DateTime.Now,
                    ParticipantType = registration.ParticipantType,
                    ParticipateYouth = registration.YouthConference,
                    NeedVisaSupport = registration.NeedVisaSupport,
                    OriginalNationality = registration.OriginalNationality,
                    CurrentNationality = registration.CurrentNationality,
                    Occupation = registration.Occupation,
                    DetailOfEmbassy = registration.DetailOfEmbassy,
                    PassportNumber = registration.PassportNumber,
                    DateOfPassportIssue = registration.DateOfPassportIssue,
                    DateOfPassportExpiry = registration.DateOfPassportExpiry,
                    PassportPhoto1 = registration.PassportPhoto1,
                    PassportPhoto2 = registration.PassportPhoto2,
                    PassportPhoto3 = registration.PassportPhoto3,
                    RegistrationNumber = UrlSlugger.Get8Digits(),
                    TypeOfPassport = registration.TypeOfPassport,
                    UserID = userResponse.Item.UserID
                };
                var insertResponse = _mailingService.CreateMailingAddress(mailing);
                response.ErrorCode = insertResponse.ErrorCode;
                response.Message = insertResponse.ErrorCode != (int)ErrorCode.None ? "Please input required fields" : "Update succeeded";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                var userId = this.Session["User-UserID"].ToString();
                FindItemReponse<UserModel> userReponse = _userService.FindUserByID(userId);
                if (userReponse.Item == null)
                {
                    return Json(new { ErrorCode = (int)ErrorCode.Error, Message = Resources.Resource.msg_invalidUser });
                }

                var mailingResponse = _mailingService.FindMailingAddressByUser(userId);

                if (mailingResponse.Items != null)
                {
                    var mailing = mailingResponse.Items.FirstOrDefault();
                    if (mailing != null)
                    {
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
                                //string filename = fileContent.FileName.Substring(0, fileContent.FileName.LastIndexOf(".")).Replace(" ", "-");
                                string filename = RemoveSpecialCharacters(userReponse.Item.Email);
                                switch (i)
                                {
                                    case 0:
                                        fileContent.SaveAs(Server.MapPath("~/Content/upload/images/passport/" + filename + "-1" + extension));
                                        mailing.PassportPhoto1 = "/Content/upload/images/passport/" + filename + "-1" + extension;
                                        break;
                                    case 1:
                                        fileContent.SaveAs(Server.MapPath("~/Content/upload/images/passport/" + filename + "-2" + extension));
                                        mailing.PassportPhoto2 = "/Content/upload/images/passport/" + filename + "-2" + extension;
                                        break;
                                    case 2:
                                        fileContent.SaveAs(Server.MapPath("~/Content/upload/images/passport/" + filename + "-3" + extension));
                                        mailing.PassportPhoto3 = "/Content/upload/images/passport/" + filename + "-3" + extension;
                                        break;
                                    default:
                                        return Json("File uploaded successfully");
                                }
                                i++;
                                mailing.UpdatedDate = DateTime.Now;
                                _mailingService.UpdateMailingAddress(mailing);
                            }
                        }
                        return Json(new { ErrorCode = ErrorCode.None, Mailing = mailing });

                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }
            return Json(new { ErrorCode = ErrorCode.None });
        }

        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, "-");
        }
    }
}

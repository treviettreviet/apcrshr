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

namespace apcrshr_site.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController()
        {
            this._userService = new UserService();
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        public ActionResult ViewProfile()
        {
            if (this.Session["User-UserID"] != null)
            {
                FindItemReponse<UserModel> response = _userService.FindUserByID(Session["User-UserID"].ToString());
                return View(response.Item);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                    if (ModelState.IsValid)
                    {
                        var mail = new System.Net.Mail.MailMessage();
                        mail.To.Add(new MailAddress(email));
                        mail.From = new MailAddress("thudientu2102@gmail.com");
                        mail.Subject = "Đổi mật khẩu APCRSHR";
                        string body = "Thay đổi mật khẩu. Mật khẩu mới của bạn là: " + password;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential
                        ("thudientu2102@gmail.com", "Lamatkhau");// Enter seders User name and password
                        smtp.EnableSsl = true;
                        try
                        {
                            smtp.Send(mail);
                        }
                        catch (SmtpFailedRecipientsException ex)
                        {
                            for (int i = 0; i < ex.InnerExceptions.Length; i++)
                            {
                                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                                if (status == SmtpStatusCode.MailboxBusy ||
                                    status == SmtpStatusCode.MailboxUnavailable)
                                {
                                    Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                                    System.Threading.Thread.Sleep(5000);
                                    smtp.Send(mail);
                                }
                                else
                                {
                                    Console.WriteLine("Failed to deliver message to {0}",
                                        ex.InnerExceptions[i].FailedRecipient);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            response = new BaseResponse { ErrorCode = (int)ErrorCode.Error, Message = ex.Message };
                            ViewBag.Message = response;
                            return View("ForgetPassword");
                        }
                    }
                    UserModel _user = user.Item;
                    _user.UpdatedDate = DateTime.Now;
                    _user.Password = password;
                    response = _userService.UpdateUser(_user);
                    if (response.ErrorCode == (int)ErrorCode.None)
                    {
                        response.Message = Resources.Resource.msg_forgotPassword_emailSend;
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

        [SessionFilter]
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

        [SessionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUpdateUser(UserModel user)
        {
            user.UpdatedDate = DateTime.Now;
            user.UserID = Session["User-UserID"].ToString();
            BaseResponse response = _userService.UpdateUser(user);
            ViewBag.Message = response;
            return View("UpdateUser", user);
        }

    }
}

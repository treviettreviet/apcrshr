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
using System.Web.Security;

namespace apcrshr_site.Areas.Administrator.Controllers
{
    public class AdminHomeController : Controller
    {
        private IAdminService _adminService;

        public AdminHomeController()
        {
            this._adminService = new AdminService();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        
        public ActionResult Login(AdminModel admin)
        {
            AdminLoginResponse response = _adminService.Login(admin.UserName, admin.Password);
            if (response.ErrorCode == (int)ErrorCode.None)
            {
                //Set cookie
                setCookies(response.AdminId, admin.RememberMe);

                this.Session["SessionID"] = response.SessionId;
                this.Session["UserName"] = response.AdminName;
                this.Session["UserId"] = response.AdminId;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Response = response;
            return View("Login");
        }

        private void setCookies(string adminID, bool rememberMe)
        {
            try
            {
                FindItemReponse<AdminModel> admin = _adminService.FindAdminByID(adminID);

                var serializedUser = Newtonsoft.Json.JsonConvert.SerializeObject(admin);

                var ticket = new FormsAuthenticationTicket(1, admin.Item.UserName, DateTime.Now, DateTime.Now.AddHours(3), rememberMe, serializedUser);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var isSsl = Request.IsSecureConnection; // if we are running in SSL mode then make the cookie secure only

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    HttpOnly = true, // always set this to true!
                    Secure = isSsl,
                };

                if (rememberMe) // if the user needs to persist the cookie. Otherwise it is a session cookie
                    cookie.Expires = DateTime.Today.AddMonths(3); // currently hard coded to 3 months in the future

                Response.Cookies.Set(cookie);
            }
            catch (Exception) { }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult SendPassword(string email)
        {
            FindItemReponse<AdminModel> user = null;
            BaseResponse baseResponse = new BaseResponse();
            if (email != null)
            {
                user = _adminService.FindAdminByEmail(email);
                if (user.Item != null)
                {
                    string password = Guid.NewGuid().ToString("D").Substring(1, 6);

                    if (ModelState.IsValid)
                    {
                        var mail = new System.Net.Mail.MailMessage();
                        mail.To.Add(new MailAddress(email));
                        mail.From = new MailAddress("thudientu2102@gmail.com");
                        mail.Subject = "Đổi mật khẩu APCRSHR";
                        string body = "Thay đổi mật khẩu Admin. Mật khẩu mới của bạn là: " + password;
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
                            baseResponse = new BaseResponse { ErrorCode = (int)ErrorCode.Error, Message = ex.Message };
                            ViewBag.Message = baseResponse;
                            return View("ForgetPassword");
                        }
                    }
                    AdminModel _user = user.Item;
                    _user.Password = password;
                    baseResponse = _adminService.UpdateAdmin(_user);
                    if (baseResponse.ErrorCode == (int)ErrorCode.None)
                    {
                        baseResponse.Message = Resources.Resource.msg_forgotPassword_emailSend;
                    }
                    ViewBag.Message = baseResponse;
                }
                else
                {
                    baseResponse = new BaseResponse { ErrorCode = (int)ErrorCode.Error, Message = Resources.Resource.msg_invalidEmail };
                }
            }
            ViewBag.Message = baseResponse;
            return View("ForgetPassword");
        }
    }
}

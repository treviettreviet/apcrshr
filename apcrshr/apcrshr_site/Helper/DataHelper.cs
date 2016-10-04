﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;
using Site.Core.Repository.Repository;
using Site.Core.Repository;
using System.Net.Mail;
using Site.Core.Common.Ultil.Security;
using System.Text;
using Site.Core.DataModel.Enum;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace apcrshr_site.Helper
{
    public class DataHelper
    {
        private static readonly string EMAIL = "abstract.apcrshr9vn@gmail.com";
        private static readonly string PASSWORD = "kjKJSDIFU8sf7*U*&FJDkfskdfjdjfhyT%$%^sgfdjsnflksflkM%%$#VBskmf;ls,fpl_-sfKKLFN)(F*s9f8s98fosnflkJFisf89sufdflkdmflkdmfkdfjUY*UFdjfnKJHyts76%&D*y8768y78FdkjfF98sufj==";
        private static readonly string CIPHER = "z3HMcf1v7/r+g4FqNPL0CqwoKQbdvwofhpcbDL6vyFK1ZPZ/ZM9n/rbiigd+r037d2VtcgRRh/HQ53Hx1dsuUUOf/nAgL8RX5YYaER/HbyQJc1+2LbsfcP8ygoWkvdM/";
        private static readonly string HOST = "smtp.gmail.com";
        private static readonly int PORT = 25;
        private static DataHelper _instance;
        private Object _lock = new Object();

        private DataHelper()
        {
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public string GetMenuTitle(string menuID)
        {
            IMenuRepository menuRepository = RepositoryClassFactory.GetInstance().GetMenuRepository();
            Menu menu = menuRepository.FindByID(menuID);
            return menu != null ? menu.Title : menuID;
        }

        /*public IList<IntroductionModel> GetIntroductionMenu(string language)
        {
            IIntroductionService introductionService = new IntroductionService();
            GetAllIntroductionResponse response = introductionService.GetIntroductionsByTypes(5, 11);
            if (response.Introductions == null)
            {
                response.Introductions = new List<IntroductionModel>();
            }
            return response.Introductions;
        }*/

        public IDictionary<string, string> InternalLinks(string language)
        {
            IDictionary<string, string> links = new Dictionary<string, string>();

            //Get static links
            GetStaticLinks(links, language);

            return links;
        }

        public void GetStaticLinks(IDictionary<string, string> links, string language)
        {
            if (language.Equals("VN"))
            {
                links.Add("Mẫu đăng ký", "/Registration/RegistrationForm");
                //links.Add("Tham gia hội nghị", "/ConferenceDeclaration/Index");
                //links.Add("Dữ liệu trình vày", "/Presentation/Index");
                links.Add("Ảnh", "/Album/Index");
            }
            else
            {
                links.Add("Registration Form", "/Registration/RegistrationForm");
                //links.Add("Conference Declaration", "/ConferenceDeclaration/Index");
                //links.Add("Presentation", "/Presentation/Index");
                links.Add("Photo", "/Album/Index");
            }
        }

        public string GetArticleURLByAction(HttpRequestBase request, string actionURL)
        {
            if (!string.IsNullOrEmpty(actionURL))
            {
                return string.Format("{0}://{1}:{2}/Home/ArticleView/{3}", request.Url.Scheme, request.Url.Host, request.Url.Port, actionURL);
            }
            return string.Empty;
        }

        public string BuildMessage(string username, string password, string activeUrl, string registrationNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<p>You have successfully registered for the 9th Asia- Pacific Conference on Reproductive and Sexual Health and Rights. Below is your account information:</p></br>");
            builder.Append(string.Format("<p>User name: <b>{0}</b></p></br>", username));
            builder.Append(string.Format("<p>Registration Number: <b>{0}</b></p></br>", registrationNumber));
            builder.Append(string.Format("<p>Password: <b>{0}</b></p></br>", password));
            builder.Append("<p>Please click on the link below to activate your account:</p></br>");
            builder.Append(string.Format("<a href='{0}'>Active Your Account</a></br>", activeUrl));
            builder.Append("<p>You can log in to your account on the website <a href='http://apcrshr9vn.org/User/Login'>Login</a> to submit abstracts and apply for a conference scholarship. You can edit your information, abstract and application before the closing date of each submission.</p></br>");
            builder.Append("<p>If you have any question relating to your account or logistic issues, please contact us via email at: <a href='mailto:Secretariate@apcrshr9vn.org'><span>Secretariate@apcrshr9vn.org</span></a></p></br></br>");
            builder.Append("<b>Thank you and welcome to the conference.</b>");
            return builder.ToString();
        }

        public void SendEmail(string destinationEmail, string subject, string body)
        {
            var mail = new System.Net.Mail.MailMessage();
            mail.To.Add(new MailAddress(destinationEmail));
            mail.From = new MailAddress(EMAIL);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = HOST;
            smtp.Port = PORT;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (EMAIL, StringCipher.Decrypt(CIPHER, PASSWORD));
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
                Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
                        ex.ToString());
            }
        }

        public string GetUniqueNumbers()
        {
            lock (_lock)
            {
                int A = 0, B = 0;
                string Output = string.Empty;
                for (int i = 0; i < 20; i++)
                {
                    while (A == B)
                    {
                        Random r = new Random();
                        A = r.Next(1, 6);
                    }
                    Output = Output + A;
                    B = A;
                }
                return Output;
            }
        }

        public IDictionary<int, string> GetDeadlineTypes()
        {
            return Enum.GetValues(typeof(DeadlineType))
               .Cast<DeadlineType>()
               .ToDictionary(e => (int)e, e => e.ToString());
        }

        public SelectList GetDeadlineTypesForDropdown()
        {
            var directions = from DeadlineType d in Enum.GetValues(typeof(DeadlineType))
                             select new { ID = (int)d, Name = d.ToString() };
            return new SelectList(directions, "ID", "Name");
        }

        public SelectList GetScholarshipStatusForDropdown()
        {
            var directions = from ScholarshipStatus d in Enum.GetValues(typeof(ScholarshipStatus))
                             select new { ID = (int)d, Name = d.ToString() };
            return new SelectList(directions, "ID", "Name");
        }

        public SelectList GetAbstractStatusForDropdown()
        {
            var directions = from SubmissionStatus d in Enum.GetValues(typeof(SubmissionStatus))
                             select new { ID = (int)d, Name = d.ToString() };
            return new SelectList(directions, "ID", "Name");
        }

        public string GetEnumName<T>(T enumtype)
        {
            return Enum.GetName(typeof(T), enumtype);
        }

        public IDictionary<int, string> GetEnumSubmissionDictionary()
        {
            return Enum.GetValues(typeof(SubmissionStatus))
               .Cast<SubmissionStatus>()
               .ToDictionary(t => (int)t, t => t.ToString() );
        }

        public string GetAbstractStatusName(int status)
        {
            switch (status)
            {
                case (int)SubmissionStatus.Submitted:
                    return "Abstract is not reviewed";
                case (int)SubmissionStatus.Reviewed:
                    return "Abstract was reviewed";
                case (int)SubmissionStatus.Rejected:
                    return "Abstract was rejected";
                default:
                    throw new InvalidCastException("Invalid status");
            }
        }

        public string GetScholarshipStatusName(int status)
        {
            switch (status)
            {
                case (int)ScholarshipStatus.Submitted:
                    return "Scholarship is not reviewed";
                case (int)ScholarshipStatus.Reviewed:
                    return "Scholarship was reviewed";
                case (int)ScholarshipStatus.Rejected:
                    return "Scholarship was rejected";
                default:
                    throw new InvalidCastException("Invalid status");
            }
        }

        public string CurrencyConvert(decimal amount, string fromCurrency, string toCurrency)
        {
            //Grab your values and build your Web Request to the API
            string apiURL = String.Format("https://www.google.com/finance/converter?a={0}&from={1}&to={2}&meta={3}", amount, fromCurrency, toCurrency, Guid.NewGuid().ToString());

            //Make your Web Request and grab the results
            var request = WebRequest.Create(apiURL);

            //Get the Response
            var streamReader = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.ASCII);

            //Grab your converted value (ie 2.45 USD)
            var result = Regex.Matches(streamReader.ReadToEnd(), "<span class=\"?bld\"?>([^<]+)</span>")[0].Groups[1].Value;

            //Get the Result
            return result;
        }

        public decimal GetCurrencyRate(string currency, decimal defaultvalue)
        {
            XDocument xdoc = XDocument.Load("http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx");
            XElement root = xdoc.Element("ExrateList");
            if (root != null)
            {
                XElement rate = root.Elements("Exrate").Where(e => e.Attribute("CurrencyCode").Value.Equals(currency.ToUpper())).SingleOrDefault();
                if (rate != null)
                {
                    return decimal.Parse(rate.Attribute("Transfer").Value);
                }
            }
            return defaultvalue;
        }
    }
}
using System;
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
using Mailjet.Client;
using Newtonsoft.Json.Linq;
using Mailjet.Client.Resources;

namespace apcrshr_site.Helper
{
    public class DataHelper
    {
        private static readonly string NAME = "[no-reply] apcrshr9vn.org";
        private static readonly string EMAIL = "no-reply@apcrshr9vn.org";
        private static readonly string USERNAME = "c48002f4d325b6e9c63da59cc6af4c77";
        private static readonly string SECRET = "85d75c51447ae61949fa9817f4f8f825";
        private static readonly string SMTP_SERVER = "in-v3.mailjet.com";
        private static readonly int SMTP_PORT = 587;

        private static readonly string EMAIL_FORM = "secretariat@apcrshr9vn.org";
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
                links.Add("Gửi Học Bổng", "/Abstract/SubmitAbstract");
            }
            else
            {
                links.Add("Registration Form", "/Registration/RegistrationForm");
                //links.Add("Conference Declaration", "/ConferenceDeclaration/Index");
                //links.Add("Presentation", "/Presentation/Index");
                links.Add("Photo", "/Album/Index");
                links.Add("Abstract Submission Form", "/Abstract/SubmitAbstract");
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

        public string BuildInvoiceMessage(string fullname, string invoiceUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<p>Dear: <b>{0}</b></p></br></br>", fullname));
            builder.Append("<p>We would like to confirm that your payment for registration to APCHRSHR9 is successful.</p></br>");
            builder.Append(string.Format("<p>Please visit this <a href='{0}'>Link</a> to download your receipt.</p></br>", invoiceUrl));
            builder.Append("<p>For further information or support, please contact us at: <a href='mailto:Secretariate@apcrshr9vn.org'><span>Secretariate@apcrshr9vn.org</span></a> or visit website: <a href='http://www.apcrshr9vn.org'>http://www.apcrshr9vn.org</a></p></br></br>");
            builder.Append("<p>Thank you</p></br>");
            builder.Append("<p>APCRSHR9 Secretariat</p>");
            return builder.ToString();
        }

        public string BuildInvoicePdfTemplate(string product, string transaction, string invoice, string usd, string vnd, string date)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<!DOCTYPE html>");
            builder.Append("<html lang='en'>");
            builder.Append("<head>");
            builder.Append("<title>Invoice</title>");
            builder.Append("</head>");
            builder.Append("<body style='padding: 20px;'>");
            builder.Append("<header>");
            builder.Append("<img src='http://apcrshr9vn.org:80/Content/upload/files/header-09169669.png' alt='' width='100%' />");
            builder.Append("</header>");
            builder.Append("<br/><br/>");
            builder.Append("<div style='text-align: center; font-weight: bold;font-size:30px;'>RECEIPT</div>");
            builder.Append("<br/><br/>");
            builder.Append("<hr>");
            builder.Append("<br/>");
            builder.Append("<div><strong>You are paying to:</strong> Vietnam Public Health Association: </div>");
            builder.Append(string.Format("<div><strong>Product detail:</strong> APCRSHR9VN - Registration fee - {0}</div>", product));
            builder.Append(string.Format("<div><strong>Transaction Reference:</strong>.. {0}</div>", transaction));
            builder.Append(string.Format("<div><strong>Invoice Number:</strong>......... {0}</div>", invoice));
            builder.Append(string.Format("<div><strong>Amount (in VND):</strong>........ {0}</div>", vnd));
            builder.Append(string.Format("<div><strong>Equal to (in USD):</strong>...... {0}</div>", usd));
            builder.Append(string.Format("<div><strong>Transaction Date:</strong>....... {0}</div>", date));
            builder.Append("<br/>");
            builder.Append("<hr>");
            builder.Append("<br/>");
            builder.Append("<div style='border: solid 1px #000000; padding: 5px 10px 10px 10px;'>");
            builder.Append("<div style='text-align: center; font-weight: bold;'>Your payment is successful</div>");
            builder.Append("<br/>");
            builder.Append("<div>For further information or support, please contact us at <span style='color: #7156fa; text-decoration: underline;'>secretariat@apcrshr9vn.org</span> or visit website: <span style='color: #7156fa; text-decoration: underline;'>http://www.apcrshr9vn.org</span></div>");
            builder.Append("</div>");
            builder.Append("<footer>");
            builder.Append("<img src='http://apcrshr9vn.org:80/Content/upload/files/footer-10132628.png' alt='' width='100%;' />");
            builder.Append("</footer>");
            builder.Append("</body>");
            builder.Append("</html>");

            return builder.ToString();
        }

        public async void SendEmail(string destinationEmail, string subject, string body)
        {
            MailjetClient client = new MailjetClient(USERNAME, SECRET);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, EMAIL)
            .Property(Send.FromName, NAME)
            .Property(Send.Subject, subject)
            .Property(Send.TextPart, "")
            .Property(Send.HtmlPart, body)
            .Property(Send.Recipients, new JArray 
            {
                new JObject 
                {
                    {"Email", destinationEmail}
                }
            });

            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
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

        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        public int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            DateTime target = new DateTime(2017, 12, 1, 11, 59, 00);
            age = target.Year - dateOfBirth.Year;
            if (target.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        /// <summary>  
        /// For calculating remaining date  
        /// </summary>  
        /// <param name="date">Enter date</param>  
        /// <returns> years, months,days, hours...</returns>  
        public string CalculateYourAge(DateTime date)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(date).Ticks).Year - 1;
            DateTime PastYearDate = date.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            Years, Months, Days, Hours, Seconds);
        }   
    }
}
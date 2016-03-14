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

namespace apcrshr_site.Helper
{
    public class DataHelper
    {

        private static DataHelper _instance;

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
                links.Add("Tham gia hội nghị", "/ConferenceDeclaration/Index");
            }
            else
            {
                links.Add("Registration Form", "/Registration/RegistrationForm");
                links.Add("Conference Declaration", "/ConferenceDeclaration/Index");
            }
        }
    }
}
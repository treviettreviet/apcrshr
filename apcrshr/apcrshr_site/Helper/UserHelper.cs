using Site.Core.Repository;
using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apcrshr_site.Helper
{
    public class UserHelper
    {
        public static string GetAdminName(string adminId)
        {
            IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
            Admin admin = adminRepository.FindByID(adminId);
            return admin != null ?  !string.IsNullOrEmpty(admin.LastName) ? admin.LastName : admin.FirstName : adminId;
        }

        public static string HasMainScholarship(string userID)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                return userRepository.HasMainScholarship(userID) ? "Yes" : "No";
            }
            catch (Exception)
            {
                return "No";
            }
        }

        public static string HasYouthScholarship(string userID)
        {
            try
            {
                IUserRepository userRepository = RepositoryClassFactory.GetInstance().GetUserRepository();
                return userRepository.HasYouthScholarship(userID) ? "Yes" : "No";
            }
            catch (Exception)
            {
                return "No";
            }
        }
    }
}
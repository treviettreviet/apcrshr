using Site.Core.DataModel.Model;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apcrshr_site.Helper
{
    public class SessionUtil
    {
        public static readonly int TIME_OUT_MINUTES = 30;

        private static SessionUtil Instance;

        private SessionUtil() { }

        public static SessionUtil GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new SessionUtil();
                }
                return Instance;
            }
        }

        public UserSession VerifySession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                return null;
            }
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionId);

            if (userSession == null)
            {
                return null;
            }

            if (!userSession.UpdatedDate.HasValue)
            {
                return null;
            }

            TimeSpan time = DateTime.Now - userSession.UpdatedDate.Value;

            if (time.Milliseconds > TIME_OUT_MINUTES * 60 * 1000)
            {
                userSessionRepository.Delete(sessionId);
            }
            else
            {
                userSessionRepository.Update(userSession);
                return userSession;
            }
            return null;
        }

        public Admin GetAdminBySessionID(string sessionID)
        {
            IAdminRepository adminRepository = RepositoryClassFactory.GetInstance().GetAdminRepository();
            IUserSessionRepository userSessionRepository = RepositoryClassFactory.GetInstance().GetUserSessionRepository();
            UserSession userSession = userSessionRepository.FindByID(sessionID);
            if (userSession != null)
            {
                return adminRepository.FindByID(userSession.UserID);
            }
            return null;
        }
    }
}